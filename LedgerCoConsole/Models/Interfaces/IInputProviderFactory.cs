using LedgerCo.Models;

namespace LedgerCo.Models.Interfaces
{
    internal interface IInputProviderFactory
    {
        IInputProvider GetInputProvider(InputMethod method);
    }
}
