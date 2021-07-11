using LedgerCo.Models;

namespace LedgerCo.Logic.ActionProcessors
{
    internal static class Extensions
    {
        public static decimal GetTotalAmountToBePaid(this LoanInfo loanRecord)
        {
            return loanRecord.Principal + ((loanRecord.Principal * loanRecord.NumberOfYears * loanRecord.RateOfInterest) / 100);
        }
    }
}
