using System.Collections.Generic;

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
}