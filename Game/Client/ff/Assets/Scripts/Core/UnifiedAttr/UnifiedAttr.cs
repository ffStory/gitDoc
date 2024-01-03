using System;
using Logic;

namespace Core.UnifiedAttr
{
    public abstract class UnifiedAttr
    {
        private readonly UnifiedAttrType _type;

        protected UnifiedAttr(UnifiedAttrType type)
        {
            _type = type;
        }

        // public virtual object GetValue(Game game, TargetContext optContext);

        public abstract (Type Type, object Value) GetTypeAndValue(Game game, TargetContext optContext);

        public abstract object GetTarget(Game game, TargetContext optContext);

        public abstract void Add(Game game, long value, TargetContext optContext);

        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        public virtual string GetIcon()
        {
            return "";
        }

        /// <summary>
        /// 名字
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return "";
        }

        public static UnifiedAttr CreateUnifiedAttr(CostItemResMsg costItemResMsg)
        {
            var type = costItemResMsg.UaType;
            switch (type)
            {
                case UnifiedAttrType.UaHero:

                    return new UnifiedAttrHero(type, costItemResMsg.AttrName, costItemResMsg.TargetId);
            }
            return null;
        }

        public abstract string GetEvent(Game game, TargetContext optContext);

    }
}
