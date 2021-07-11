using System.Threading.Tasks;

namespace LedgerCo.Interfaces
{
    internal interface IInputProvider
    {
        Task<string[]> GetInputLinesAsync(string path);
    }
}
