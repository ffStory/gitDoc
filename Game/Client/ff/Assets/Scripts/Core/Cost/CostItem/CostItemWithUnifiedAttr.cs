using System;
using Logic;

namespace Core.Cost.CostItem
{
    public abstract class CostItemWithUnifiedAttr : CostItem
    {
        protected CostItemWithUnifiedAttr(CostItemResMsg msg) : base(msg)
        {
            AttrValue = msg.AttrValue;
            _unifiedAttr = UnifiedAttr.UnifiedAttr.CreateUnifiedAttr(msg);
        }

        // public override object GetValue(Game game, TargetContext optContext)
        // {
        //     return _unifiedAttr.GetValue(game, optContext);
        // }
        
        public override (Type Type, object Value) GetTypeAndValue(Game game, TargetContext optContext)
        {
            return _unifiedAttr.GetTypeAndValue(game, optContext);
        }

        protected void AddValue (Game game, long value, TargetContext targetContext)
        {
            _unifiedAttr.Add(game, value, targetContext);
        }

        public override string GetEvent(Game game, TargetContext optContext)
        {
            return _unifiedAttr.GetEvent(game, optContext);
        }

        protected readonly string AttrValue;
        private readonly Core.UnifiedAttr.UnifiedAttr _unifiedAttr;
    }
}
