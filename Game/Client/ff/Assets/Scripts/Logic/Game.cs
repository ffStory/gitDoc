using System;
using System.Diagnostics;
using Logic.Manager;
using Logic.Object;

namespace Logic
{
    public class Game
    {
        private static Game _instance;
        public static Game Instance => _instance ??= new Game();
        public Player Player;
        private Game()
        {
            Player = new Player(this);
            var protocol = new PlayerMsg();
            protocol.Exp = 100;
            Player.LoadMsg(protocol);
        }

        public void Init()
        {
            ResManager.Instance.Init();
        }

        public object GetObjectByType(ObjectType type, uint id)
        {
            switch (type)
            {
                case ObjectType.Player:
                    return Player;
                case ObjectType.Hero:
                    return Player.Heroes[id];
                default:
                    throw new NotImplementedException("Game.GetObjectByType");
            }
        }
    }
}