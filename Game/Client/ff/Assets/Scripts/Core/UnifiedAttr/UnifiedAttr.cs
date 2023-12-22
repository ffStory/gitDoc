using Logic;

namespace Core.UnifiedAttr
{
    public abstract class UnifiedAttr
    {
        protected readonly ObjectType ObjType;
        protected string AttrName;
        protected uint Id;
        
        protected UnifiedAttr(ObjectType type)
        {
            ObjType = type;
        }

        public virtual object GetValue(Game game, TargetContext optContext)
        {
            return 0;
        }

        public abstract object GetTarget(Game game, TargetContext optContext);


        public abstract void Add(Game game, long value, TargetContext optContext);
        

        public virtual bool IsConsumable()
        {
            return true;
        }

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
            var type = costItemResMsg.ObjType;
            switch (type)
            {
                case ObjectType.Hero:

                    return new UnifiedAttrHero(type, costItemResMsg.AttrName, costItemResMsg.ObjId);
            }
            return null;
        }
    }
}
