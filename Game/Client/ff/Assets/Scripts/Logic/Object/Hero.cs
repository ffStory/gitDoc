namespace Logic.Object
{
    public class Hero : BaseHero
    {
        public Hero(BasePlayer player):base(player.Game)
        {

        }

        public override uint Level => throw new System.NotImplementedException();
    }
}