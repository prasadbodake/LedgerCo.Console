using LedgerCo.Models.Interfaces;
using LedgerCo.Models;
using LedgerCo.Models.Actions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LedgerCo.Logic.ActionProcessors
{
    internal class PaymentActionProcessor : ActionProcessor
    {
        public PaymentActionProcessor(IDatabaseStore dbStore) : base(dbStore) { }

        public override async Task<object> ProcessAsync(BaseAction action)
        {
            var paymentAction = (PaymentAction)action;

            var existingRecord = await GetDataAsync(paymentAction.BankName, paymentAction.BorrowerName);

            if (existingRecord == null)
            {
                throw new Exception($"Loan record not found for bank {paymentAction.BankName} and borrower {paymentAction.BorrowerName}.");
            }
            if (paymentAction.EMINumber >= (existingRecord.NumberOfYears * 12))
            {
                return null;
            }

            if (existingRecord.Payments == null) existingRecord.Payments = new List<Payment>();

            existingRecord.Payments.Add(
                new Payment
                {
                    Amount = paymentAction.LumpSumAmount,
                    EmiNumber = paymentAction.EMINumber
                }
            );
            
            await StoreDataAsync(existingRecord);
            return null;
        }
    }
}
