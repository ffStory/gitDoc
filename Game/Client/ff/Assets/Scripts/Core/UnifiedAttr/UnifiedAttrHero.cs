using System;
using Logic;
using Logic.Object;
using Util;

namespace Core.UnifiedAttr
{
    public class UnifiedAttrHero : UnifiedAttr
    {
        private readonly uint _id;
        private readonly string _attrName;
        public UnifiedAttrHero(UnifiedAttrType type, string attrName, uint id) : base(type)
        {
            _attrName = attrName;
            _id = id;
        }

        public override (Type Type, object Value) GetTypeAndValue(Game game, TargetContext optContext)
        {
            var target = GetTarget(game, optContext);
            if (target == null)
            {
                var type = Utility.GetPropertyType<Hero>(_attrName);
                if (type.IsEnum)
                {
                    return (type, Enum.Parse(type, "0"));
                }
                
                return  (type, Convert.ChangeType("0", type));
            }
            return Utility.GetPropertyTypeAndValue(target as Hero, _attrName);
        }

        public override object GetTarget(Game game, TargetContext optContext)
        {
            if (_id != 0)
            {
                game.Player.Heroes.TryGetValue(_id, out Hero hero);
                return hero;
            }
            
            if (optContext?.Context == null)
            {
                return null;
            }

            optContext.Context.TryGetValue(ObjectType.Hero, out object obj);
            return obj;
        }

        public override void Add(Game game, long value, TargetContext optContext)
        {
            var target = GetTarget(game, optContext);
            if (target == null){return;}

            var hero = target as Hero;
            var curV = (uint)Utility.GetPropertyValue(hero, _attrName);
            var result = 0u;
            if (value >= 0 || -value <= curV)
            {
                result = (uint)(curV + value);
            }
            
            Utility.SetProperty(hero, _attrName, result);
        }

        public override string GetEvent(Game game, TargetContext optContext)
        {
            var target = GetTarget(game, optContext);
            if (target is Hero hero)
            {
                return hero.GetAttrEvent(_attrName);
            }

            return string.Empty;
        }
    }
}
