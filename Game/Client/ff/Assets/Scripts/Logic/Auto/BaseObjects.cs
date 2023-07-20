using System.Collections.Generic;
using Google.Protobuf;
public abstract class BaseHero : BaseObject
{
    public BaseHero(Game game) : base(game, ObjectType.Hero){}
    protected int _exp;
    public int exp
    {
        get{return _exp;}
        set
        {
            var old = _exp;
            _exp = value;
            PostAttrEvent("exp", old, exp);
        }
    }
    public abstract int level { get; }
    protected string _name;
    public string name
    {
        get{return _name;}
        set
        {
            var old = _name;
            _name = value;
            PostAttrEvent("name", old, name);
        }
    }
    public override void LoadMsg(IMessage iMessage)
    {
        var message = iMessage as HeroMsg;
        id = message.Id;
        exp = message.Exp;
        name = message.Name;
        AfterLoadMsg();
    }
}
public abstract class BasePlayer : BaseObject
{
    public BasePlayer(Game game) : base(game, ObjectType.Player){}
    protected int _exp;
    public int exp
    {
        get{return _exp;}
        set
        {
            var old = _exp;
            _exp = value;
            PostAttrEvent("exp", old, exp);
        }
    }
    public abstract int level { get; }
    public Dictionary<int, Hero> heroes;
    protected int _gold;
    public int gold
    {
        get{return _gold;}
        set
        {
            var old = _gold;
            _gold = value;
            PostAttrEvent("gold", old, gold);
        }
    }
    public List<Hero> items;
    public List<int> itemsInt;
    public Dictionary<int, int> heroesDicInt;
    public abstract int power { get; }
    public override void LoadMsg(IMessage iMessage)
    {
        var message = iMessage as PlayerMsg;
        id = message.Id;
        exp = message.Exp;
        heroes = new Dictionary<int, Hero>();
        foreach (var pair in message.Heroes)
        {
            var item = new Hero(this);
            item.LoadMsg(pair.Value);
            heroes.Add(pair.Key, item);
        }
        gold = message.Gold;
        items = new List<Hero>();
        for (int i = 0; i < message.Items.Count; i++)
        {
            var item = new Hero(this);
            item.LoadMsg(message.Items[i]);
            items.Add(item);
        }
        itemsInt = new List<int>(message.ItemsInt);
        heroesDicInt = new Dictionary<int, int>(message.HeroesDicInt);
        AfterLoadMsg();
    }
}
