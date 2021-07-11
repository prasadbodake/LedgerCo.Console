using LedgerCo.Models.Interfaces;
using LedgerCo.Models;
using LedgerCo.Models.Actions;
using System;
using System.Threading.Tasks;

namespace LedgerCo.Logic.ActionProcessors
{
    internal class LoanActionProcessor : ActionProcessor
    {
        public LoanActionProcessor(IDatabaseStore dbStore) : base(dbStore) { }

        public override async Task<object> ProcessAsync(BaseAction action)
        {
            var loanAction = (LoanAction)action;

            var existingRecord = await GetDataAsync(loanAction.BankName, loanAction.BorrowerName);

            if (existingRecord != null)
            {
                throw new Exception($"The loan record with bank {loanAction.BankName} and borrower {loanAction.BorrowerName} already exists. Invalid action.");
            }

            var loanRecord = ToLoanRecord(loanAction);

            await StoreDataAsync(loanRecord);
            return null;
        }

        private static LoanRecord ToLoanRecord(LoanAction loanAction)
        {
            var loanRecord = new LoanRecord
            {
                BankName = loanAction.BankName,
                BorrowerName = loanAction.BorrowerName,
                Principal = loanAction.Principal,
                NumberOfYears = loanAction.NumberOfYears,
                RateOfInterest = loanAction.RateOfInterest
            };
            loanRecord.EMIAmount = GetEMIAmount(loanRecord);
            return loanRecord;
        }

        private static decimal GetEMIAmount(LoanRecord loanRecord)
        {
            var totalAmountToRepay = loanRecord.GetTotalAmountToBePaid();

            var emiAmount = Math.Ceiling(totalAmountToRepay / (loanRecord.NumberOfYears * 12));
            return emiAmount;
        }
    }

}
