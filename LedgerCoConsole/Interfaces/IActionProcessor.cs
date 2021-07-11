using LedgerCo.Models.Actions;
using System.Threading.Tasks;

namespace LedgerCo.Interfaces
{
    internal interface IActionProcessor
    {
        Task<object> ProcessAsync(BaseAction action);
    }
}
