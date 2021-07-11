using LedgerCo.Interfaces;
using LedgerCo.Models;

namespace LedgerCo.Implementations
{
    internal class InputProviderFactory : IInputProviderFactory
    {
        public IInputProvider GetInputProvider(InputMethod method)
        {
            return method switch
            {
                InputMethod.File => new FileInputProvider(),

                _ => null,
            };
        }
    }
}
