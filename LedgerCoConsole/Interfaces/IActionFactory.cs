using LedgerCo.Models.Actions;

namespace LedgerCo.Interfaces
{
    internal interface IActionFactory
    {
        BaseAction GetAction(string line);
    }
}
