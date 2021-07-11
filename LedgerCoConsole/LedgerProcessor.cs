using LedgerCo.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LedgerCo
{
    internal class LedgerProcessor
    {
        private readonly IActionFactory _actionFactory;
        private readonly IActionProcessorFactory _actionProcessorFactory;

        public LedgerProcessor(IActionFactory actionFactory, IActionProcessorFactory actionProcessorFactory)
        {
            _actionFactory = actionFactory;
            _actionProcessorFactory = actionProcessorFactory;
        }


        public async Task<List<string>> ProcessInputAsync(string[] lines)
        {
            var outputLines = new List<string>();
            foreach (var line in lines)
            {
                var action = _actionFactory.GetAction(line);
                var actionProcessor = _actionProcessorFactory.GetProcessor(action);
                var output = await actionProcessor.ProcessAsync(action);
                if (output != null)
                {
                    outputLines.Add(output.ToString());
                }
            }
            return outputLines;
        }
    }
}
