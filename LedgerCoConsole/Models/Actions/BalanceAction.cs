using LedgerCo.Console.Models.Actions;
using System;

namespace LedgerCo.Models.Actions
{
    internal class BalanceAction : BaseAction
    {
        private const string BalanceActionArg = "BALANCE";
        public override ActionType Type => ActionType.Balance;

        public string BankName { get; set; }
        public string BorrowerName { get; set; }

        public int EMINumber { get; set; }

        public override BaseAction Parse(string line)
        {
            if (line.StartsWith(BalanceActionArg) == false)
            {
                return null;
            }
            var arguments = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var bankName = arguments[1];
            var borrowerName = arguments[2];
            var emiNumber = int.Parse(arguments[3]);

            if (bankName.IsEmpty())
            {
                throw new Exception("Bank name cannot be empty");
            }
            if (borrowerName.IsEmpty())
            {
                throw new Exception("Borrower name cannot be empty");
            }
            if (emiNumber.IsNegative())
            {
                throw new Exception("EMI Number cannot be negative");
            }

            return new BalanceAction
            {
                BankName = bankName,
                BorrowerName = borrowerName,
                EMINumber = emiNumber,
            };
        }
    }
}
