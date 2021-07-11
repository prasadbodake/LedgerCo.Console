using LedgerCo.Console.Models.Actions;
using System;

namespace LedgerCo.Models.Actions
{
    internal class LoanAction : BaseAction
    {
        private const string LoanActionArg = "LOAN";
        public override ActionType Type => ActionType.Loan;

        public string BankName { get; set; }
        public string BorrowerName { get; set; }
        public decimal Principal { get; set; }
        public int NumberOfYears { get; set; }
        public decimal RateOfInterest { get; set; }

        public override BaseAction Parse(string line)
        {
            if (line.StartsWith(LoanActionArg))
            {
                var arguments = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var bankName = arguments[1];
                var borrowerName = arguments[2];
                var principal = decimal.Parse(arguments[3]);
                var numberOfYears = int.Parse(arguments[4]);
                var rateOfInterest = decimal.Parse(arguments[5]);

                if(bankName.IsEmpty())
                {
                    throw new Exception("Bank name cannot be empty");
                }
                if (borrowerName.IsEmpty())
                {
                    throw new Exception("Borrower name cannot be empty");
                }
                if (!principal.IsGreatedThanZero())
                {
                    throw new Exception("Principal anount cannot be less than 1");
                }

                return new LoanAction
                {
                    BankName = bankName,
                    BorrowerName = borrowerName,
                    Principal = principal,
                    NumberOfYears = numberOfYears,
                    RateOfInterest = rateOfInterest
                };
            }
            return null;
        }
    }
}
