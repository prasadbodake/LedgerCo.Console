using System.Threading.Tasks;

namespace LedgerCo.Models.Interfaces
{
    internal interface IInputProvider
    {
        Task<string[]> GetInputLinesAsync(string path);
    }
}
