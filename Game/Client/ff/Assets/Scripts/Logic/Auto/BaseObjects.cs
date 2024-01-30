using System.Collections.Generic;
using Google.Protobuf;
using Logic.Object;
using Logic;
using Core;

public abstract class BaseHero : BaseObject
{
    public BaseHero(Game game) : base(game, ObjectType.Hero){}
    private uint _exp;
    public virtual uint Exp
    {
        get => _exp;
        set
        {
            var old = _exp;
            _exp = value;
            PostAttrEvent("Exp", old, _exp);
        }
    }
    public abstract uint Level { get; }
    private string _name;
    public virtual string Name
    {
        get => _name;
        set
        {
            var old = _name;
            _name = value;
            PostAttrEvent("Name", old, _name);
        }
    }
    private HeroState _state;
    public virtual HeroState State
    {
        get => _state;
        set
        {
            var old = _state;
            _state = value;
            PostAttrEvent("State", old, _state);
        }
    }
    private ulong _ownTime;
    public virtual ulong OwnTime
    {
        get => _ownTime;
        set
        {
            var old = _ownTime;
            _ownTime = value;
            PostAttrEvent("OwnTime", old, _ownTime);
        }
    }
    public override void LoadMsg(IMessage iMessage)
    {
        var message = iMessage as HeroMsg;
        if (message is null) {return;}
        Id = message.Id;
        Exp = message.Exp;
        Name = message.Name;
        OwnTime = message.OwnTime;
        AfterLoadMsg();
    }
}

public class BaseItem : BaseObject
{
    public BaseItem(Game game) : base(game, ObjectType.Item){}
    private uint _num;
    public virtual uint Num
    {
        get => _num;
        set
        {
            var old = _num;
            _num = value;
            PostAttrEvent("Num", old, _num);
        }
    }
    private ItemType _type;
    public virtual ItemType Type
    {
        get => _type;
        set
        {
            var old = _type;
            _type = value;
            PostAttrEvent("Type", old, _type);
        }
    }
    private ulong _ownTime;
    public virtual ulong OwnTime
    {
        get => _ownTime;
        set
        {
            var old = _ownTime;
            _ownTime = value;
            PostAttrEvent("OwnTime", old, _ownTime);
        }
    }
    public override void LoadMsg(IMessage iMessage)
    {
        var message = iMessage as ItemMsg;
        if (message is null) {return;}
        Id = message.Id;
        Num = message.Num;
        Type = message.Type;
        OwnTime = message.OwnTime;
        AfterLoadMsg();
    }
}

public class BasePlayer : BaseObject
{
    public BasePlayer(Game game) : base(game, ObjectType.Player){}
    private uint _exp;
    public virtual uint Exp
    {
        get => _exp;
        set
        {
            var old = _exp;
            _exp = value;
            PostAttrEvent("Exp", old, _exp);
        }
    }
    public virtual uint Level { get; }
    public Dictionary<uint, Hero> Heroes;
    private uint _gold;
    public virtual uint Gold
    {
        get => _gold;
        set
        {
            var old = _gold;
            _gold = value;
            PostAttrEvent("Gold", old, _gold);
        }
    }
    public Dictionary<uint, Item> Items;
    public override void LoadMsg(IMessage iMessage)
    {
        var message = iMessage as PlayerMsg;
        if (message is null) {return;}
        Id = message.Id;
        Exp = message.Exp;
        Heroes = new Dictionary<uint, Hero>();
        foreach (var pair in message.Heroes)
        {
            var item = new Hero(this);
            item.LoadMsg(pair.Value);
            Heroes.Add(pair.Key, item);
        }
        Gold = message.Gold;
        Items = new Dictionary<uint, Item>();
        foreach (var pair in message.Items)
        {
            var item = new Item(this);
            item.LoadMsg(pair.Value);
            Items.Add(pair.Key, item);
        }
        AfterLoadMsg();
    }
}
