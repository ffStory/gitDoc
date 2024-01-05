using System.Collections.Generic;
using Core.Cost.CostItem;
using Logic;
using Logic.Manager;

namespace Core.Cost
{
    
    public class CostCheckResult
    {
        public CostCheckResult()
        {
            FailCheckResults = new List<CostItemCheckResult>();
        }
        
        public bool IsSuccess => FailCheckResults.Count == 0;
        public readonly List<CostItemCheckResult> FailCheckResults;
    }

    public class Cost
    {
        private Cost()
        {
            Items = new List<CostItem.CostItem>();
        }
        
        public Cost(List<string> items)
        {
            Items = new List<CostItem.CostItem>();
            
            if (items is null){return;}

            foreach (var item in items)
            {
                var array = item.Split('-');
                var id = uint.Parse(array[0]);
                if (ResManager.Instance.CostItemResMapMsg.Map.TryGetValue(id, out CostItemResMsg costItemResMsg))
                {
                    Items.Add(CostItem.CostItem.CreateCostItem(costItemResMsg, array));
                }
            }
        }

        public CostCheckResult Check(Game game, TargetContext tartContext, bool isCheckAll = false)
        {
            var result = new CostCheckResult();
            foreach (var costItem in Items)
            {
                costItem.Check(game, out CostItemCheckResult itemResult, tartContext);
                if (itemResult.IsSuccess){continue;}
                result.FailCheckResults.Add(itemResult);
                if (!isCheckAll){break;}
            }
            return result;
        }

        public void Consume(Game game, TargetContext tartContext)
        {
            foreach (var costItem in Items)
            {
                if (costItem.IsConsume())
                {
                    costItem.Consume(game, tartContext);
                }
            }
        }
        
        public Cost Multiply(uint times)
        {
            var newCost = new Cost();
            foreach (var item in Items)
            {
                newCost.Items.Add(item.IsMultiply() ? item.Multiply(times) : item.Copy());
            }

            return newCost;
        }

        public readonly List<CostItem.CostItem> Items;
    }
}
