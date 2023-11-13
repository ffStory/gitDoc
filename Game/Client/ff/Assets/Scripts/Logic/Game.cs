using System.Collections.Generic;
using Logic.Manager;
using Logic.Object;

public class Game
{
    public Player player;
    public Game()
    {
        player = new Player(this);
        var protocol = new PlayerMsg();
        protocol.Exp = 100;
        player.LoadMsg(protocol);
    }

    public void Init()
    {
        ResManager.Instance.Init();
    }
}