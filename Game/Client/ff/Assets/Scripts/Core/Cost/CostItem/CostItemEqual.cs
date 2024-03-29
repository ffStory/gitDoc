using System;
using Logic;

namespace Core.Cost.CostItem
{
    public class CostItemEqual : CostItemWithUnifiedAttr
    {
        public CostItemEqual(CostItemResMsg msg, string[] strParams) : base(msg, strParams)
        {
        }

        public override void Check(Game game, out CostItemCheckResult checkResult, TargetContext targetContext)
        {
            base.Check(game, out checkResult, targetContext);

            var tuple = GetTypeAndValue(game, targetContext);
            checkResult.IsSuccess = tuple.Type.IsEnum ? 
                Enum.Parse(tuple.Type, AttrValue).Equals(tuple.Value) : 
                tuple.Value.Equals(Convert.ChangeType(AttrValue, tuple.Type));
        }
    }
}
