using System;
using System.Collections.Generic;
using Core;
using UI;
using UI.Views;
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
        // var game = new Game();
        // game.player.Exp = 200;
        
        // UIManager.Instance.JumpView(new BaseUISnapShoot("HeroListView", ViewType.HeroList));

        Debug.Log(".....init:" + Time.frameCount);
        var item1 = new SchedulerItem(0, 1, 5, item =>
        {
            Debug.Log(".....5s:" + item.GetCalledTimes() + " " + Time.frameCount);
        }, true);
        Scheduler.Instance.AddItem(item1);

        var item = new SchedulerItem(2, 4, 0, item =>
        {
            Debug.Log(".....2s:" + item.GetCalledTimes()  + " " + Time.frameCount);
        }, true);
        Scheduler.Instance.AddItem(item);
        //
        // Scheduler.Instance.CallLater(1000, timer =>
        // {
        //     Debug.Log("....1");
        // });
        //
        // Scheduler.Instance.CallLater(3000, timer =>
        // {
        //     Debug.Log("....3");
        // });
        //
        // Scheduler.Instance.CallLater(6000, timer =>
        // {
        //     Debug.Log("....6");
        // });
    }

    // Update is called once per frame
    void Update()
    {
        Scheduler.Instance.Update();
    }
}
