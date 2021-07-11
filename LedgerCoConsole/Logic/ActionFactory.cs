using LedgerCo.Interfaces;
using LedgerCo.Models.Actions;
using System.Collections.Generic;

namespace LedgerCo.Logic
{
    internal class ActionFactory : IActionFactory
    {
        private readonly List<BaseAction> _availableActions = new List<BaseAction>
        {
            new LoanAction(),
            new PaymentAction(),
            new BalanceAction()
        };


        public BaseAction GetAction(string line)
        {
            foreach (var action in _availableActions)
            {
                var resultAction = action.Parse(line);
                if (resultAction != null)
                {
                    return resultAction;
                }
            }
            return null;
        }
    }
}
