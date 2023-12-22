using System;
using Logic;

namespace Core.Cost.CostItem
{
    public class CostItemGreaterEqual : CostItemWithUnifiedAttr
    {
        public CostItemGreaterEqual(CostItemResMsg msg) : base(msg)
        {

        }

        public override void Check(Game game, out CostItemCheckResult checkResult, TargetContext targetContext)
        {
            base.Check(game, out checkResult, targetContext);
            checkResult.IsSuccess = (uint) GetValue(game, targetContext) >= AttrValue;
        }

        public override void Consume(Game game, TargetContext optContext)
        {
            throw new NotImplementedException();
        }

        public override bool IsConsume()
        {
            return false;
        }
    }
}
