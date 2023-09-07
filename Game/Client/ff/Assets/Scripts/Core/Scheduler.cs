using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Core
{
    public enum SchedulerType
    {
        Timer = 1,
        Frame = 2
    }
    
    public class SchedulerItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval">间隔 毫秒/帧</param>
        /// <param name="repeat">重复次数。0为永久重复</param>
        /// <param name="delay">首次调用延迟 毫秒/帧</param>
        /// <param name="callback"></param>
        /// <param name="schedulerType"></param>
        public SchedulerItem(long interval, uint repeat, long delay, Action<SchedulerItem> callback, SchedulerType schedulerType)
        {
            _interval = interval;
            _repeat = repeat;
            _delay = delay;
            _callback = callback;
            _schedulerType = schedulerType;
            
            CalculateNextTriggerTiming(true);
        }
        


        /// <summary>
        /// 计算下次调用事件
        /// </summary>
        /// <param name="isFirst">是否首次调用</param>
        private void CalculateNextTriggerTiming(bool isFirst)
        {
            if (_repeat > 0 && _calledTimes >= _repeat)
            {
                _nextTriggerTiming = -1;
                return;
            }

            var delay = isFirst ? _delay : _interval;
            if (_schedulerType == SchedulerType.Frame)
            {
                _nextTriggerTiming = Time.frameCount + delay;
            }
            else
            {
                _nextTriggerTiming = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + delay;
            }
        }

        public uint GetCalledTimes()
        {
            return _calledTimes;
        }

        public SchedulerType GetSchedulerType()
        {
            return _schedulerType;
        }
        
        public void Call()
        {
            _calledTimes++;
            CalculateNextTriggerTiming(false);
            try
            {
                _callback(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Cancel()
        {
            _canceled = true;
        }


        public bool IsCanceled()
        {
            return _canceled;
        }
        
        public bool IsFinished()
        {
            return _nextTriggerTiming < 0;
        }

        public long GetNextTriggerTiming()
        {
            return _nextTriggerTiming;
        }

        private bool _canceled;
        private readonly long _interval;
        private readonly uint _repeat;
        private readonly long _delay;
        private readonly Action<SchedulerItem> _callback;
        private readonly SchedulerType _schedulerType;

        /// <summary>
        /// 已经调用次数
        /// </summary>
        private uint _calledTimes;

        private long _nextTriggerTiming = -1;
    }
    
    public class Scheduler
    {
        public void Update()
        {
            
            var nowFrame = Time.frameCount;
            var nowTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            using var enumerator= _queuesDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var type = enumerator.Current.Key;
                var queue = enumerator.Current.Value;
                while (queue.GetSize() > 0)
                {
                    var item = queue.Top();
                    if (item.IsFinished())
                    {
                        queue.Pop();
                        continue;
                    }

                    if (item.IsCanceled())
                    {
                        queue.Pop();
                        continue;
                    }

                    if (item.GetNextTriggerTiming() > (type == SchedulerType.Frame? nowFrame:nowTime))
                    {
                        break;
                    }

                    queue.Pop();
                    item.Call();
                    if (!item.IsFinished())
                    {
                        queue.Push(item);
                    }
                } 
            }
        }

        public void AddItem(SchedulerItem schedulerItem)
        {
            _queuesDic[schedulerItem.GetSchedulerType()].Push(schedulerItem);
        }

        private Scheduler()
        {
            _queuesDic = new Dictionary<SchedulerType, PriorityQueue<SchedulerItem>>
            {
                {SchedulerType.Frame, new PriorityQueue<SchedulerItem>(MinHeapCompare)},
                {SchedulerType.Timer, new PriorityQueue<SchedulerItem>(MinHeapCompare)}
            };
        }

        private long MinHeapCompare(SchedulerItem a, SchedulerItem b)
        {
            return a.GetNextTriggerTiming() - b.GetNextTriggerTiming();
        }


        public SchedulerItem CallLater(long delay, Action<SchedulerItem> callback)
        {
            var timer = new SchedulerItem(0, 1, delay, callback, SchedulerType.Timer);
            AddItem(timer);
            return timer;
        }
        
        public SchedulerItem CallLaterFrame(long delay, Action<SchedulerItem> callback)
        {
            var frame = new SchedulerItem(0, 1, delay, callback, SchedulerType.Frame);
            AddItem(frame);
            return frame;
        }

        private readonly Dictionary<SchedulerType, PriorityQueue<SchedulerItem>> _queuesDic;
        private static Scheduler _instance;
        public static Scheduler Instance
        {
            get
            {
                _instance ??= new Scheduler();
                return _instance;
            }
        }
    }
}
