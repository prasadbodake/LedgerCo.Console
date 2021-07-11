using LedgerCo.Data;
using LedgerCo.Logic;
using LedgerCo.Models;
using System;

namespace LedgerCo
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                System.Console.WriteLine("Please provide the input file name");
            }
            
            var inputLines = new InputProviderFactory().
                GetInputProvider(InputMethod.File).
                GetInputLinesAsync(args[0]).GetAwaiter().GetResult();
            
            var ledgerProcessor = new LedgerProcessor(new ActionFactory(), new ActionProcessorFactory(new DatabaseStore()));
            var outputLines = ledgerProcessor.ProcessInputAsync(inputLines).GetAwaiter().GetResult();

            foreach (var line in outputLines)
            {
                System.Console.WriteLine(line);
            }
            Environment.Exit(0);
        }
    }
}
