using Google.Protobuf;
using System;
public class BaseObject : EventTarget
{
    public BaseObject(Game game, ObjectType type)
    {
        this.game = game;
        this.type = type;
    }

    public string GetAttrEvent(string attrName)
    {
        return type.ToString() + '-' + attrName + "-" + id;
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

    public virtual void LoadProtocol(IMessage baseProtocol)
    {
    }

    public virtual void AfterLoadProtocol()
    {

    }

    public Game game { get; private set; }
    public int id { get; set; }
    public ObjectType type { get; set; }
}
