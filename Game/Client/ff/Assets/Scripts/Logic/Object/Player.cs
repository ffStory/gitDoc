using Core;
using UnityEngine;

namespace Logic.Object
{
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

        public override uint Level { get => Exp / 10; }
    }
}
