using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Core;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UI;
using UI.Views;
using UnityEditor;
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

public class WFTest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Type.GetType("String") == null);
        var codePath = Path.Combine(Directory.GetCurrentDirectory(), "../../Resource/Data/Hero.byte");
        using var stream2 = new FileStream(codePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        var dic2 = HeroResMapMsg.Parser.ParseFrom(stream2);
        int a = 0;
        foreach (var item in dic2.Map)
        {
            Debug.Log("....." + item.Value.Id + " " + item.Value.Name + " " + item.Value.ItemType);
            for (int i = 0; i < item.Value.Rewards.Count; i++)
            {
                Debug.Log(".......reward:" + item.Value.Rewards[i]);
            }
            for (int i = 0; i < item.Value.Rewards2.Count; i++)
            {
                a += item.Value.Rewards2[i];
                Debug.Log(".......reward2:" + item.Value.Rewards2[i] + " " + a);
            }

            foreach (var pair in item.Value.DicTest)
            {
                Debug.Log("...dic:" + pair.Key + " " + pair.Value);
            }
        }
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