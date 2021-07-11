using LedgerCo.Models.Interfaces;
using LedgerCo.Models;
using LedgerCo.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedgerCo.Logic.ActionProcessors
{
    internal class BalanceActionProcessor : ActionProcessor
    {
        public BalanceActionProcessor(IDatabaseStore dbStore) : base(dbStore) { }


        public override async Task<object> ProcessAsync(BaseAction action)
        {
            var balanceAction = (BalanceAction)action;

            var record = await GetDataAsync(balanceAction.BankName, balanceAction.BorrowerName);
            if (record == null)
            {
                throw new Exception($"Loan record not found for bank {balanceAction.BankName} and borrower {balanceAction.BorrowerName}.");
            }

            var totalPaidAmount = (record.EMIAmount * balanceAction.EMINumber) + GetLumpsumPaidAmount(record.Payments, balanceAction.EMINumber);

            var totalAmountToBePaid = record.GetTotalAmountToBePaid();
            var remainingAmount = totalAmountToBePaid - totalPaidAmount;

            var numberOfRemainingEMIs = Math.Ceiling(remainingAmount / (record.EMIAmount));
            if (numberOfRemainingEMIs < 0) numberOfRemainingEMIs = 0;

            return new BalanceInfo
            {
                BankName = balanceAction.BankName,
                BorrowerName = balanceAction.BorrowerName,
                TotalAmountPaid = totalPaidAmount,
                RemainingEmiCount = (int)numberOfRemainingEMIs
            };
        }

        private decimal GetLumpsumPaidAmount(List<Payment> payments, int emiNumber)
        {
            return payments == null
                ? 0
                : payments.Where(p => p.EmiNumber <= emiNumber).Sum(p => p.Amount);
        }
    }

}
