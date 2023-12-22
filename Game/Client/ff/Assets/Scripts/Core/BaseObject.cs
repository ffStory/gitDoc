using Google.Protobuf;
using System;
using Logic;

public class BaseObject : EventTarget
{
    public BaseObject(Game game, ObjectType type)
    {
        this.Game = game;
        this.Type = type;
    }
    
    public BaseObject()
    {

    }

    public string GetAttrEvent(string attrName)
    {
        return Type.ToString() + '-' + attrName + "-" + Id;
    }
    public void PostAttrEvent(string attrName, object oldV = null, object newV = null)
    {
        var eventArgs = new EventDispatcherArgs(GetAttrEvent(attrName), oldV, newV);
        EventPost(eventArgs);
    }

    public void RegisterAttrEvent(string attrName, Action<EventDispatcherArgs> callBack)
    {
        EventRegister(GetAttrEvent(attrName), callBack, this);
    }

    public virtual void LoadMsg(IMessage iMessage)
    {
    }

    public virtual void AfterLoadMsg()
    {
    }

    public Game Game { get; private set; }
    public uint Id { get; set; }
    public ObjectType Type { get; set; }
}
