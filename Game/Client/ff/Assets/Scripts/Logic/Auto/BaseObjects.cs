using System.Collections.Generic;
using Google.Protobuf;
public abstract class BaseHero : BaseObject
{
    public BaseHero(Game game) : base(game, ObjectType.Hero){}
    protected int exp;
    public int Exp
    {
        get{return exp;}
        set
        {
            var old = exp;
            exp = value;
            PostAttrEvent("Exp", old, exp);
        }
    }
    public abstract int Level { get; }
    protected string name;
    public string Name
    {
        get{return name;}
        set
        {
            var old = name;
            name = value;
            PostAttrEvent("Name", old, name);
        }
    }
    public override void LoadMsg(IMessage iMessage)
    {
        var message = iMessage as HeroMsg;
        Id = message.Id;
        Exp = message.Exp;
        Name = message.Name;
        AfterLoadMsg();
    }
}
public abstract class BasePlayer : BaseObject
{
    public BasePlayer(Game game) : base(game, ObjectType.Player){}
    protected int exp;
    public int Exp
    {
        get{return exp;}
        set
        {
            var old = exp;
            exp = value;
            PostAttrEvent("Exp", old, exp);
        }
    }
    public abstract int Level { get; }
    public Dictionary<int, Hero> Heroes;
    protected int gold;
    public int Gold
    {
        get{return gold;}
        set
        {
            var old = gold;
            gold = value;
            PostAttrEvent("Gold", old, gold);
        }
    }
    public List<Hero> Items;
    public List<int> ItemsInt;
    public Dictionary<int, int> HeroesDicInt;
    public abstract int Power { get; }
    protected ItemType itemType;
    public ItemType ItemType
    {
        get{return itemType;}
        set
        {
            var old = itemType;
            itemType = value;
            PostAttrEvent("ItemType", old, itemType);
        }
    }
    protected int attackBoost;
    public int AttackBoost
    {
        get{return attackBoost;}
        set
        {
            var old = attackBoost;
            attackBoost = value;
            PostAttrEvent("AttackBoost", old, attackBoost);
        }
    }
    public override void LoadMsg(IMessage iMessage)
    {
        var message = iMessage as PlayerMsg;
        Id = message.Id;
        Exp = message.Exp;
        Heroes = new Dictionary<int, Hero>();
        foreach (var pair in message.Heroes)
        {
            var item = new Hero(this);
            item.LoadMsg(pair.Value);
            Heroes.Add(pair.Key, item);
        }
        Gold = message.Gold;
        Items = new List<Hero>();
        for (var i = 0; i < message.Items.Count; i++)
        {
            var item = new Hero(this);
            item.LoadMsg(message.Items[i]);
            Items.Add(item);
        }
        ItemsInt = new List<int>(message.ItemsInt);
        HeroesDicInt = new Dictionary<int, int>(message.HeroesDicInt);
        ItemType = message.ItemType;
        AfterLoadMsg();
    }
}
