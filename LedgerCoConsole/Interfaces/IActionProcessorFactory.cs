using LedgerCo.Models.Actions;

namespace LedgerCo.Interfaces
{
    internal interface IActionProcessorFactory
    {
        IActionProcessor GetProcessor(BaseAction action);
    }
}
