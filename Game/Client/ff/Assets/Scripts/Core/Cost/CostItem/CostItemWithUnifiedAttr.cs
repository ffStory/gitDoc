using Logic;

namespace Core.Cost.CostItem
{
    public abstract class CostItemWithUnifiedAttr : CostItem
    {
        protected CostItemWithUnifiedAttr(CostItemResMsg msg) : base(msg)
        {
            AttrValue = msg.AttrValue;
            _unifiedAttr = Core.UnifiedAttr.UnifiedAttr.CreateUnifiedAttr(msg);
        }

        public override object GetValue(Game game, TargetContext optContext)
        {
            return _unifiedAttr.GetValue(game, optContext);
        }

        protected void AddValue (Game game, long value, TargetContext targetContext)
        {
            _unifiedAttr.Add(game, value, targetContext);
        }

        protected readonly uint AttrValue;
        private readonly Core.UnifiedAttr.UnifiedAttr _unifiedAttr;
    }
}
