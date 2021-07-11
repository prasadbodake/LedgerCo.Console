using LedgerCo.Models.Interfaces;
using LedgerCo.Models;

namespace LedgerCo.Logic
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
