using LedgerCo.Models.Actions;

namespace LedgerCo.Models.Interfaces
{
    internal interface IActionProcessorFactory
    {
        IActionProcessor GetProcessor(BaseAction action);
    }
}
