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
            checkResult.IsSuccess = (uint) GetValue(game, targetContext) >= AttrValue;
        }
        
        public override void Consume(Game game, TargetContext optContext)
        {
            AddValue(game, -AttrValue, optContext);
        }

        public override bool IsConsume()
        {
            //根据属性判断，一些状态类型的返回false
            return true;
        }
    }
}
