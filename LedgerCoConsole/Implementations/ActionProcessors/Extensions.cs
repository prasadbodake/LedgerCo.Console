using LedgerCo.Models;

namespace LedgerCo.Implementations.ActionProcessors
{
    internal static class Extensions
    {
        public static decimal GetTotalAmountToBePaid(this LoanRecord loanRecord)
        {
            return loanRecord.Principal + ((loanRecord.Principal * loanRecord.NumberOfYears * loanRecord.RateOfInterest) / 100);
        }
    }
}
