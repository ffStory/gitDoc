using System.Collections.Generic;
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
    void OnEnable()
    {
        var game = new Game();
        game.player.Exp = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
