namespace Logic.Object
{
    public class Hero : BaseHero
    {
        public Hero(BasePlayer player):base(player.Game)
        {

        }

        public override int Level => throw new System.NotImplementedException();

    }
}