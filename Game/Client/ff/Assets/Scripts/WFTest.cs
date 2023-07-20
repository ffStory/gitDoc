using System.Collections.Generic;
using UnityEngine;

public class Player : BasePlayer
{
    public Player(Game game) : base(game)
    {
        RegisterAttrEvent("exp", ExpChagnedEvent);
    }


    public override void AfterLoadProtocol()
    {
    }

    private void ExpChagnedEvent(EventDispatcherArgs args)
    {
        Debug.Log(".......changed:" + args.OldValue + " " + args.NewValue);
    }

    public override int level { get => exp / 10; }
}



public class Hero : BaseHero
{
    public Hero(BasePlayer player):base(player.game)
    {

    }

    public override int level => throw new System.NotImplementedException();

    public override void AfterLoadProtocol()
    {
        base.AfterLoadProtocol();
    }
}


public class WFTest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void OnEnable()
    {
        var game = new Game();
        game.player.exp = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
