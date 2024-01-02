using System;
using System.Collections.Generic;
using System.Diagnostics;
using Core;
using Core.Cost;
using Logic;
using Logic.Manager;
using Logic.Object;
using UnityEngine;
using Util;
using Debug = UnityEngine.Debug;

public class Test : MonoBehaviour
{
    void OnEnable()
    {
        Game.Instance.Init();

        //TODO
        Game.Instance.Player.Heroes.Add(1, new Hero(Game.Instance.Player));
        Game.Instance.Player.Heroes[1].Id = 1;
        Game.Instance.Player.Heroes[1].Exp = 5000;
        UIManager.Instance.PushView(new BaseUISnapShoot(UIType.HeroList));

        var hero = Game.Instance.Player.Heroes[1];
        var cost = new Cost(new List<uint> {1, 2});
        
        var costItemRes = ResManager.Instance.CostItemResMapMsg.Map[1];
        
        var targetContext = new TargetContext();
        targetContext.Context = new Dictionary<ObjectType, object>();
        targetContext.Context.Add(ObjectType.Hero, hero);
        
        hero.State = HeroState.Battle;
        var result = cost.Check(Game.Instance, targetContext);
        cost.Consume(Game.Instance, targetContext);
        Debug.Log("...." + result.IsSuccess + "  " + hero.Exp);

    }
}
