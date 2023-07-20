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
    public override void LoadProtocol(IMessage baseProtocol)
    {
        var protoco = baseProtocol as HeroProtocol;
        id = protoco.Id;
        exp = protoco.Exp;
        name = protoco.Name;
        AfterLoadProtocol();
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
    public override void LoadProtocol(IMessage baseProtocol)
    {
        var protoco = baseProtocol as PlayerProtocol;
        id = protoco.Id;
        exp = protoco.Exp;
        heroes = new Dictionary<int, Hero>();
        foreach (var pair in protoco.Heroes)
        {
            var item = new Hero(this);
            item.LoadProtocol(pair.Value);
            heroes.Add(pair.Key, item);
        }
        gold = protoco.Gold;
        items = new List<Hero>();
        for (int i = 0; i < protoco.Items.Count; i++)
        {
            var item = new Hero(this);
            item.LoadProtocol(protoco.Items[i]);
            items.Add(item);
        }
        itemsInt = new List<int>(protoco.ItemsInt);
        heroesDicInt = new Dictionary<int, int>(protoco.HeroesDicInt);
        AfterLoadProtocol();
    }
}
