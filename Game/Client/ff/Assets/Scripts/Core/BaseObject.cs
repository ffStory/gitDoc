using System;
using Google.Protobuf;
using Logic;

namespace Core
{
    public class BaseObject : EventTarget
    {
        protected BaseObject(Game game, ObjectType type)
        {
            Game = game;
            Type = type;
        }

        public string GetAttrEvent(string attrName)
        {
            return Type.ToString() + '-' + attrName + "-" + Id;
        }

        protected void PostAttrEvent(string attrName, object oldV = null, object newV = null)
        {
            var eventArgs = new EventDispatcherArgs(GetAttrEvent(attrName), oldV, newV);
            Post(eventArgs);
        }

        protected void RegisterAttrEvent(string attrName, Action<EventDispatcherArgs> callBack)
        {
            Register(GetAttrEvent(attrName), callBack, this);
        }

        public virtual void LoadMsg(IMessage iMessage)
        {
        }

        public virtual void AfterLoadMsg()
        {
        }

        public Game Game { get; private set; }
        public uint Id { get; set; }
        public ObjectType Type { get;}
    }
}
