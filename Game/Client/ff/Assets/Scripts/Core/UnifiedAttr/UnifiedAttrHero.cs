using Logic;
using Logic.Object;
using Util;

namespace Core.UnifiedAttr
{
    public class UnifiedAttrHero : UnifiedAttr
    {
        public UnifiedAttrHero(ObjectType type, string attrName, uint id) : base(type)
        {
            AttrName = attrName;
            Id = id;
        }

        public override object GetValue(Game game, TargetContext optContext)
        {
            if (Id != 0)
            {
                if (!game.Player.Heroes.TryGetValue(Id, out Hero hero)) { return 0; }

                return Utility.GetPropertyValue(hero, AttrName);
            }
            
            var target = GetTarget(game, optContext);
            if (target == null)
            {
                return 0;
            }

            return Utility.GetPropertyValue(target as Hero, AttrName);
        }

        public override object GetTarget(Game game, TargetContext optContext)
        {
            if (optContext?.Context == null)
            {
                return null;
            }

            optContext.Context.TryGetValue(ObjType, out object obj);
            return obj;
        }

        public override void Add(Game game, long value, TargetContext optContext)
        {
            var target = GetTarget(game, optContext);
            if (target == null){return;}

            var hero = target as Hero;
            var curV = (uint)Utility.GetPropertyValue(hero, AttrName);
            var result = 0u;
            if (value >= 0 || -value <= curV)
            {
                result = (uint)(curV + value);
            }
            
            Utility.SetProperty(hero, AttrName, result);
        }
    }
}
