using Logic;

namespace Core.Cost.CostItem
{
    public class CostItemEnumEqual : CostItemWithUnifiedAttr
    {
        public CostItemEnumEqual(CostItemResMsg msg) : base(msg)
        {
        }

        public override void Check(Game game, out CostItemCheckResult checkResult, TargetContext targetContext)
        {
            base.Check(game, out checkResult, targetContext);
            checkResult.IsSuccess = (int) GetValue(game, targetContext) == AttrValue;
        }
        
        public override void Consume(Game game, TargetContext optContext)
        {
        }

        public override bool IsConsume()
        {
            return false;
        }
    }
}
