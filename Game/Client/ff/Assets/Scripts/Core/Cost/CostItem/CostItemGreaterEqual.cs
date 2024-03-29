using System;
using Logic;

namespace Core.Cost.CostItem
{
    public class CostItemGreaterEqual : CostItemWithUnifiedAttr
    {
        public CostItemGreaterEqual(CostItemResMsg msg, string[] strParams) : base(msg, strParams)
        {
        }

        public override void Check(Game game, out CostItemCheckResult checkResult, TargetContext targetContext)
        {
            base.Check(game, out checkResult, targetContext);
            var tuple = GetTypeAndValue(game, targetContext);
            var curV = Convert.ChangeType(tuple.Value, tuple.Type) as IComparable;
            var targetV = Convert.ChangeType(AttrValue, tuple.Type);
            if (curV != null) checkResult.IsSuccess = curV.CompareTo(targetV) >= 0;
        }
    }
}
