using System;
using System.Collections.Generic;
using Core;
using Core.Cost;
using Logic;
using Logic.Manager;
using Logic.Object;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        Game.Instance.Init();
        var player = Game.Instance.Player;
        var hero = new Hero(player)
        {
            Id = 1,
        };
        hero.AfterLoadMsg();
        player.Heroes = new Dictionary<uint, Hero>();
        player.Heroes.Add(1, hero);
        
        var item = new Item(player)
        {
            Id = 1,
        };
        player.Items = new Dictionary<uint, Item>();
        player.Items.Add(1, item);
    }

    void OnEnable()
    {
        var hero = Game.Instance.Player.Heroes[1];
        hero.State = HeroState.Battle;
        hero.Exp = 500;
        // var cost = new Cost(new List<uint> {1, 2, 3, 4});
        //
        // var costItemRes = ResManager.Instance.CostItemResMapMsg.Map[1];
        //
        // var targetContext = new TargetContext();
        // targetContext.Context = new Dictionary<ObjectType, object>();
        // targetContext.Context.Add(ObjectType.Hero, hero);
        //
        // hero.State = HeroState.Battle;
        // var result = cost.Check(Game.Instance, targetContext, true);
        // cost.Consume(Game.Instance, targetContext);
        // Debug.Log("...." + result.IsSuccess + "  " + hero.Exp);
    }

    private void Tesdt(uint x)
    {
        
    }
}
