using System.Collections.Generic;
using Google.Protobuf;
using Logic.Object;
using Logic;
using Core;

public abstract class BaseHero : BaseObject
{
    public BaseHero(Game game) : base(game, ObjectType.Hero){}
    protected uint exp;
    public virtual uint Exp
    {
        get => exp;
        set
        {
            var old = exp;
            exp = value;
            PostAttrEvent("Exp", old, exp);
        }
    }
    public abstract uint Level { get; }
    protected string name;
    public virtual string Name
    {
        get => name;
        set
        {
            var old = name;
            name = value;
            PostAttrEvent("Name", old, name);
        }
    }
    protected HeroState state;
    public virtual HeroState State
    {
        get => state;
        set
        {
            var old = state;
            state = value;
            PostAttrEvent("State", old, state);
        }
    }
    protected ulong ownTime;
    public virtual ulong OwnTime
    {
        get => ownTime;
        set
        {
            var old = ownTime;
            ownTime = value;
            PostAttrEvent("OwnTime", old, ownTime);
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

public abstract class BaseItem : BaseObject
{
    public BaseItem(Game game) : base(game, ObjectType.Item){}
    protected uint num;
    public virtual uint Num
    {
        get => num;
        set
        {
            var old = num;
            num = value;
            PostAttrEvent("Num", old, num);
        }
    }
    protected ItemType type;
    public virtual ItemType Type
    {
        get => type;
        set
        {
            var old = type;
            type = value;
            PostAttrEvent("Type", old, type);
        }
    }
    protected ulong ownTime;
    public virtual ulong OwnTime
    {
        get => ownTime;
        set
        {
            var old = ownTime;
            ownTime = value;
            PostAttrEvent("OwnTime", old, ownTime);
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

public abstract class BasePlayer : BaseObject
{
    public BasePlayer(Game game) : base(game, ObjectType.Player){}
    protected uint exp;
    public virtual uint Exp
    {
        get => exp;
        set
        {
            var old = exp;
            exp = value;
            PostAttrEvent("Exp", old, exp);
        }
    }
    public abstract uint Level { get; }
    public Dictionary<uint, Hero> Heroes;
    protected uint gold;
    public virtual uint Gold
    {
        get => gold;
        set
        {
            var old = gold;
            gold = value;
            PostAttrEvent("Gold", old, gold);
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
