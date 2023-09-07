using System;
using UnityEngine;
using Util;

namespace Core
{
    public class SchedulerItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval">间隔 毫秒/帧</param>
        /// <param name="repeat">重复次数。0为永久重复</param>
        /// <param name="delay">首次调用延迟 毫秒/帧</param>
        /// <param name="callback"></param>
        /// <param name="isFrame"></param>
        public SchedulerItem(long interval, uint repeat, long delay, Action<SchedulerItem> callback, bool isFrame = false)
        {
            _interval = interval;
            _repeat = repeat;
            _delay = delay;
            _callback = callback;
            _isFrame = isFrame;
            
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
            if (_isFrame)
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

        public bool IsFrame()
        {
            return _isFrame;
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
        private readonly bool _isFrame;

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
            while (_frameQueue.GetSize() > 0)
            {
                var item = _frameQueue.Top();
                if (item.IsFinished())
                {
                    _frameQueue.Pop();
                    continue;
                }

                if (item.IsCanceled())
                {
                    _frameQueue.Pop();
                    continue;
                }

                if (item.GetNextTriggerTiming() > nowFrame)
                {
                    break;
                }

                _frameQueue.Pop();
                item.Call();
                if (!item.IsFinished())
                {
                    _frameQueue.Push(item);
                }
            }
            
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            while (_timerQueue.GetSize() > 0)
            {
                var item = _timerQueue.Top();
                if (item.IsFinished())
                {
                    _timerQueue.Pop();
                    continue;
                }

                if (item.IsCanceled())
                {
                    _timerQueue.Pop();
                    continue;
                }

                if (item.GetNextTriggerTiming() > now)
                {
                    break;
                }

                _timerQueue.Pop();
                item.Call();
                if (!item.IsFinished())
                {
                    _timerQueue.Push(item);
                }
            }
        }

        public void AddItem(SchedulerItem schedulerItem)
        {
            if (schedulerItem.IsFrame())
            {
                _frameQueue.Push(schedulerItem);
            }
            else
            {
                _timerQueue.Push(schedulerItem);
            }
        }

        private Scheduler()
        {
            _timerQueue = new PriorityQueue<SchedulerItem>(MinHeapCompare);
            _frameQueue = new PriorityQueue<SchedulerItem>(MinHeapCompare);
        }

        private long MinHeapCompare(SchedulerItem a, SchedulerItem b)
        {
            return a.GetNextTriggerTiming() - b.GetNextTriggerTiming();
        }


        public SchedulerItem CallLater(long delay, Action<SchedulerItem> callback)
        {
            var timer = new SchedulerItem(0, 1, delay, callback);
            AddItem(timer);
            return timer;
        }
        
        public SchedulerItem CallLaterFrame(long delay, Action<SchedulerItem> callback)
        {
            var frame = new SchedulerItem(0, 1, delay, callback, true);
            AddItem(frame);
            return frame;
        }

        private readonly PriorityQueue<SchedulerItem> _timerQueue;
        private readonly PriorityQueue<SchedulerItem> _frameQueue;

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
