using System.Collections.Generic;

public class Game
{
    public Player player;
    public Game()
    {
        player = new Player(this);

        var protocol = new PlayerProtocol();
        protocol.Exp = 100;
        player.LoadProtocol(protocol);
    }
}