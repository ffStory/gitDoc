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
        public Cost UpgradeCost;
        public Hero(BasePlayer player):base(player.Game)
        {
            
        }

        public override void AfterLoadMsg()
        {
            base.AfterLoadMsg();
            ResManager.Instance.HeroResMapMsg.Map.TryGetValue(Id, out Config);
            if (Config != null) UpgradeCost = new Cost(Config.UpgradeCost.ToList());

            var context = new TargetContext();
            context.Context.Add(ObjectType.Hero, this);
            var costHandler = new CostHandler(this.Game, UpgradeCost, context, () =>
            {
                Debug.Log(".........满足升级条件");
            });
        }

        public override uint Level => throw new System.NotImplementedException();
    }
}