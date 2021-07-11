using LedgerCo.Models.Actions;

namespace LedgerCo.Models.Interfaces
{
    internal interface IActionFactory
    {
        BaseAction GetAction(string line);
    }
}
