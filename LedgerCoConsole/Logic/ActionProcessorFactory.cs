using LedgerCo.Logic.ActionProcessors;
using LedgerCo.Interfaces;
using LedgerCo.Models;
using LedgerCo.Models.Actions;

namespace LedgerCo.Logic
{
    internal class ActionProcessorFactory : IActionProcessorFactory
    {
        private readonly IDatabaseStore _store;

        public ActionProcessorFactory(IDatabaseStore store)
        {
            _store = store;
        }

        public IActionProcessor GetProcessor(BaseAction action)
        {
            return action.Type switch
            {
                ActionType.Loan => new LoanActionProcessor(_store),

                ActionType.Payment => new PaymentActionProcessor(_store),

                ActionType.Balance => new BalanceActionProcessor(_store),

                _ => null,
            };
        }
    }

}
