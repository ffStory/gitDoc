using System;
using Logic;

namespace Core.Cost.CostItem
{
    public class CostItemCheckResult
    {
        public uint Id;
        public bool IsSuccess;
        public string Tips;
    } 
    
    public abstract class CostItem
    {
        private readonly uint _id;
        private CostItemType _type;
        private CostItemResMsg _config;

        protected CostItem(CostItemResMsg msg)
        {
            _id = msg.Id;
            _type = msg.CostType;
            _config = msg;
        }

        public abstract object GetValue(Game game, TargetContext optContext);

        public virtual void Check(Game game, out CostItemCheckResult checkResult, TargetContext targetContext)
        {
            checkResult = new CostItemCheckResult
            {
                Id = _id
            };
        }
        public abstract void Consume(Game game, TargetContext optContext);

        public abstract bool IsConsume();


        public virtual bool IsMultiply()
        {
            return IsConsume();
        }
        
        public virtual CostItem Multiply(uint times)
        {
            return Copy();
        }

        public virtual CostItem Copy()
        {
            var item = (CostItem) MemberwiseClone();
            return item;
        }

        public static CostItem CreateCostItem(CostItemResMsg msg)
        {
            switch (msg.CostType)
            {
                case CostItemType.Consume:
                    return new CostItemConsume(msg);
                case CostItemType.EnumEqual:
                    return new CostItemEnumEqual(msg);
                case CostItemType.GreaterEqual:
                    return new CostItemGreaterEqual(msg);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
