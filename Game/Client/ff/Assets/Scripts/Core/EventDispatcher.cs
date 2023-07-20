using System;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcherArgs: EventArgs
{
    public object OldValue;
    public object NewValue;
    private List<string> _keys;
    private int _index = -1;

    public EventDispatcherArgs(string key, object oldV = null, object newV = null)
    {
        OldValue = oldV;
        NewValue = newV;
        _keys = new List<string>();
        var array = key.Split('-');
        ProcessKeys(array);
    }

    public EventDispatcherArgs(string[] keys, object oldV = null, object newV = null)
    {
        OldValue = oldV;
        NewValue = newV;
        _keys = new List<string>();
        ProcessKeys(keys);
    }

    private void ProcessKeys(string[] keys)
    {
        var keyLen = keys.Length;
        for (int i = keyLen - 1; i >= 0; --i)
        {
            _keys.Add(string.Join("-", keys, 0, i + 1));
        }
    }

    public string MoveNextKey()
    {
        var index = ++_index;
        if (index < _keys.Count)
            return _keys[index];

        return "";
    }
}

public interface IEventTarget
{
    public void EventPost(EventDispatcherArgs args);
    public void EventRegister(string eventKey, Action<EventDispatcherArgs> callBack, IEventTarget eventTarget);
    public void EventRegisterListener(EventListener listener);
    public void EventUnRegisterListener(EventListener listener);
    public void EventUnRegisterEventTarget();
}

public class EventMonoTarget : MonoBehaviour, IEventTarget
{
    private EventTarget _eventTarget;
    public EventMonoTarget()
    {
        _eventTarget = new EventTarget();
    }
    public void EventPost(EventDispatcherArgs args)
    {
        _eventTarget.EventPost(args);
    }

    public void EventRegister(string eventKey, Action<EventDispatcherArgs> callBack, IEventTarget eventTarget)
    {
        _eventTarget.EventRegister(eventKey, callBack, eventTarget);
    }
    public void EventRegisterListener(EventListener listener)
    {
        _eventTarget.EventRegisterListener(listener);
    }

    public void EventUnRegisterListener(EventListener listener)
    {
        _eventTarget.EventUnRegisterListener(listener);
    }

    public void EventUnRegisterEventTarget()
    {
        _eventTarget.EventUnRegisterEventTarget();
    }
}

public class EventTarget : IEventTarget
{
    private List<EventListener> _listeners;

    public EventTarget()
    {
        _listeners = new List<EventListener>();
    }

    public void EventPost(EventDispatcherArgs args)
    {
        EventDispatcher.Instance.Post(args);
    }

    public void EventRegister(string eventKey, Action<EventDispatcherArgs> callBack, IEventTarget eventTarget)
    {
        var listener = new EventListener(eventKey, callBack, eventTarget);
        EventRegisterListener(listener);
    }

    public void EventRegisterListener(EventListener listener)
    {
        _listeners.Add(listener);
        EventDispatcher.Instance.RegisterListener(listener);
    }

    public void EventUnRegisterListener(EventListener listener)
    {
        _listeners.Remove(listener);
        EventDispatcher.Instance.UnRegisterListener(listener);
    }

    public void EventUnRegisterEventTarget()
    {
        for (int i = _listeners.Count - 1; i >= 0; --i)
        {
            EventUnRegisterListener(_listeners[i]);
        }
    }
}

public class EventListener
{
    public EventListener(string eventKey, Action<EventDispatcherArgs> callBack, IEventTarget eventTarget)
    {
        EventKey = eventKey;
        Callback = callBack;
        EventTarget = eventTarget;
    }

    public string EventKey;
    public Action<EventDispatcherArgs> Callback;
    public IEventTarget EventTarget;
}

public class EventDispatcher
{
    private EventDispatcher()
    {
        _keyToListeners = new Dictionary<string, List<EventListener>>();
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="eventKey"></param>
    /// <param name="callBack"></param>
    /// <param name="eventTarget">此字段是为了快速删除Listener</param>
    public void Register(string eventKey, Action<EventDispatcherArgs> callBack, IEventTarget eventTarget)
    {
        var listener = new EventListener(eventKey, callBack, eventTarget);
        RegisterListener(listener);
    }

    public void RegisterListener(EventListener listener)
    {
        if (!_keyToListeners.ContainsKey(listener.EventKey))
        {
            _keyToListeners[listener.EventKey] = new List<EventListener>();
        }
        _keyToListeners[listener.EventKey].Add(listener);
    }

    /// <summary>
    /// 反注册事件
    /// </summary>
    /// <param name="listener"></param>
    public void UnRegisterListener(EventListener listener)
    {
        _keyToListeners[listener.EventKey].Remove(listener);

        if (_keyToListeners[listener.EventKey].Count == 0)
        {
            _keyToListeners.Remove(listener.EventKey);
        }
    }

    /// <summary>
    /// 发事件
    /// </summary>
    /// <param name="args"></param>
    public void Post(EventDispatcherArgs args)
    {
        DoPost(args);
    }

    private void DoPost(EventDispatcherArgs args)
    {
        var key = args.MoveNextKey();
        if (!string.IsNullOrEmpty(key))
        {
            if (_keyToListeners.ContainsKey(key))
            {
                var listeners = _keyToListeners[key];
                for (int i = 0; i < listeners.Count; ++i)
                {
                    var listener = listeners[i];
                    listeners[i].Callback?.Invoke(args);
                }
            }
            DoPost(args);
        }
    }

    private Dictionary<string, List<EventListener>> _keyToListeners;
    private static EventDispatcher _instance;
    public static EventDispatcher Instance
    {
        get
        {
            _instance ??= new EventDispatcher();
            return _instance;
        }
    }
}
