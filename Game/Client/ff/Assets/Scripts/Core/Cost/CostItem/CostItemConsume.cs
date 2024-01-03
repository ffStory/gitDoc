using System;
using Logic;

namespace Core.Cost.CostItem
{
    public class CostItemConsume : CostItemWithUnifiedAttr
    {
        public CostItemConsume(CostItemResMsg msg) : base(msg)
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
        
        public override void Consume(Game game, TargetContext optContext)
        {
            var value = long.Parse(AttrValue);
            AddValue(game, -value, optContext);
        }
    }
}
