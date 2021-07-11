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

            var loanInfo = ToLoanInfo(loanAction);

            await StoreDataAsync(loanInfo);
            return null;
        }

        private static LoanInfo ToLoanInfo(LoanAction loanAction)
        {
            var loanInfo = new LoanInfo
            {
                BankName = loanAction.BankName,
                BorrowerName = loanAction.BorrowerName,
                Principal = loanAction.Principal,
                NumberOfYears = loanAction.NumberOfYears,
                RateOfInterest = loanAction.RateOfInterest
            };
            loanInfo.EMIAmount = GetEMIAmount(loanInfo);
            return loanInfo;
        }

        private static decimal GetEMIAmount(LoanInfo loanRecord)
        {
            var totalAmountToRepay = loanRecord.GetTotalAmountToBePaid();

            var emiAmount = Math.Ceiling(totalAmountToRepay / (loanRecord.NumberOfYears * 12));
            return emiAmount;
        }
    }

}
