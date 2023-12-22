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
        public Cost(List<uint> ids)
        {
            _items = new List<CostItem.CostItem>();
            
            if (ids is null){return;}

            foreach (var id in ids)
            {
                if (ResManager.Instance.CostItemResMapMsg.Map.TryGetValue(id, out CostItemResMsg costItemResMsg))
                {
                    _items.Add(CostItem.CostItem.CreateCostItem(costItemResMsg));
                }
            }
        }

        public CostCheckResult Check(Game game, TargetContext tartContext, bool isCheckAll = false)
        {
            var result = new CostCheckResult();
            foreach (var costItem in _items)
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
            foreach (var costItem in _items)
            {
                if (costItem.IsConsume())
                {
                    costItem.Consume(game, tartContext);
                }
            }
        }
        
        public Cost Multiply(uint times)
        {
            var newCost = new Cost(null);
            foreach (var item in _items)
            {
                newCost._items.Add(item.IsMultiply() ? item.Multiply(times) : item.Copy());
            }

            return newCost;
        }

        private readonly List<CostItem.CostItem> _items;
    }
}
