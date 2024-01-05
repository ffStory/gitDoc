using System.Linq;
using Core;
using Core.Cost;
using Logic.Manager;
using UnityEngine;

namespace Logic.Object
{
    public class Hero : BaseHero
    {
        public HeroResMsg Config;
        // public Cost UpgradeCost;
        public Cost LevelCost;
        private CostHandler costHandler;
        public Hero(BasePlayer player):base(player.Game)
        {
            
        }

        public override void AfterLoadMsg()
        {
            base.AfterLoadMsg();
            ResManager.Instance.HeroResMapMsg.Map.TryGetValue(Id, out Config);
            if (Config == null){return;}

            // var context = new TargetContext();
            // context.Context.Add(ObjectType.Hero, this);
            LevelCost = new Cost(Config.LevelCost.ToList());

            // costHandler = new CostHandler(this.Game, LevelCost, null, () =>
            // {
            //     costHandler.Release();
            //     Debug.Log(".........满足升级条件:" + LevelCost.Check(Game, context).IsSuccess + " " + this.Exp);
            //     LevelCost.Consume(Game, context);
            //     Debug.Log(".........满足升级条件2:" + LevelCost.Check(Game, context).IsSuccess + " " + this.Exp);
            // });
        }

        public override uint Level => throw new System.NotImplementedException();
    }
}