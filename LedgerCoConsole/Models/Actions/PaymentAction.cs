using LedgerCo.Console.Models.Actions;
using System;

namespace LedgerCo.Models.Actions
{
    internal class PaymentAction : BaseAction
    {
        private const string PaymentActionArg = "PAYMENT";
        public override ActionType Type => ActionType.Payment;

        public string BankName { get; set; }
        public string BorrowerName { get; set; }
        public decimal LumpSumAmount { get; set; }
        public int EMINumber { get; set; }

        public override BaseAction Parse(string line)
        {
            if (line.StartsWith(PaymentActionArg) == false)
            {
                return null;
            }
      
            var arguments = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var bankName = arguments[1];
            var borrowerName = arguments[2];
            var lumpSumAmount = decimal.Parse(arguments[3]);

            if (bankName.IsEmpty())
            {
                throw new Exception("Bank name cannot be empty");
            }
            if (borrowerName.IsEmpty())
            {
                throw new Exception("Borrower name cannot be empty");
            }
            if (!lumpSumAmount.IsGreatedThanZero())
            {
                throw new Exception("Lump sum anount cannot be less than 1");
            }

            return new PaymentAction
            {
                BankName = bankName,
                BorrowerName = borrowerName,
                LumpSumAmount = lumpSumAmount,                
                EMINumber = int.Parse(arguments[4])
            };
        }
    }
}
