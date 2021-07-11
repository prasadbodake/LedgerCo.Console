using LedgerCo.Models.Actions;
using System.Threading.Tasks;

namespace LedgerCo.Models.Interfaces
{
    internal interface IActionProcessor
    {
        Task<object> ProcessAsync(BaseAction action);
    }
}
