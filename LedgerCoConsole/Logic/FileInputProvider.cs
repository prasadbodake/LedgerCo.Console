using LedgerCo.Models.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace LedgerCo.Logic
{
    internal class FileInputProvider : IInputProvider
    {
        public async Task<string[]> GetInputLinesAsync(string argument)
        {
            if (File.Exists(argument) == false)
            {
                throw new FileNotFoundException("File not found.", argument);
            }
            return await Task.FromResult(File.ReadAllLines(argument));
        }
    }
}
