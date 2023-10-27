using System;
using System.IO;
using System.Linq;
using Core;
using UnityEngine;

public class Player : BasePlayer
{
    public Player(Game game) : base(game)
    {
        RegisterAttrEvent("Exp", ExpChagnedEvent);
    }



    private void ExpChagnedEvent(EventDispatcherArgs args)
    {
        Debug.Log(".......changed:" + args.OldValue + " " + args.NewValue);
    }

    public override int Level { get => Exp / 10; }

    public override int Power => throw new System.NotImplementedException();
}



public class Hero : BaseHero
{
    public Hero(BasePlayer player):base(player.Game)
    {

    }

    public override int Level => throw new System.NotImplementedException();

}

public class Cost :BaseObject
{

}

public class WFTest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(Type.GetType("String") == null);
        var codePath = Path.Combine(Directory.GetCurrentDirectory(), "../../Resource/Data/Stage.byte");
        using var stream2 = new FileStream(codePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        var dic2 = StageResMapMsg.Parser.ParseFrom(stream2);

        Debug.Log(dic2);
    }

    public Type GetType(string typeName)
    {
        switch (typeName)
        {
            case "String":
            {
                return typeof(string);
            }
            case "Int32":
            {
                return typeof(Int32);
            }
        }

        return null;
    }
    
    // Update is called once per frame
    void Update()
    {
        Scheduler.Instance.Update();
    }
}