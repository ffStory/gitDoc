using System;
using Logic;

namespace Core.Cost
{
    public class CostHandler: EventTarget
    {
        private readonly Game _game;
        private readonly TargetContext _targetContext;
        private readonly Cost _cost;
        private readonly Action _callBack;
        
        public CostHandler(Game game, Cost cost, TargetContext targetContext, Action successCall)
        {
            _game = game;
            _cost = cost;
            _targetContext = targetContext;
            _callBack = successCall;
            
            Release();
            RegisterCost();
        }

        private void RegisterCost()
        {
            foreach (var costItem in _cost.Items)
            {
                RegisterCostItem(costItem);
            }   
        }

        private void RegisterCostItem(CostItem.CostItem costItem)
        {
            var eventStr = costItem.GetEvent(_game, _targetContext);
            Register(eventStr, OnEventCall, this);
        }

        private void OnEventCall(EventDispatcherArgs obj)
        {
            if (_cost.Check(_game, _targetContext).IsSuccess)
            {
                _callBack();
                Release();
            }
        }

        public void Release()
        {
            UnRegisterTarget();
        }
    }
}
