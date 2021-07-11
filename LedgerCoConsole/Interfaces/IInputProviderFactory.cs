using LedgerCo.Models;

namespace LedgerCo.Interfaces
{
    internal interface IInputProviderFactory
    {
        IInputProvider GetInputProvider(InputMethod method);
    }
}
