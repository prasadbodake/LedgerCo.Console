using LedgerCo.Models.Interfaces;
using LedgerCo.Models;
using LedgerCo.Models.Actions;
using System.Threading.Tasks;

namespace LedgerCo.Logic.ActionProcessors
{
    internal abstract class ActionProcessor : IActionProcessor
    {
        private readonly IDatabaseStore dbStore;        

        protected ActionProcessor(IDatabaseStore dbStore)
        {
            this.dbStore = dbStore;
        }

        
        public abstract Task<object> ProcessAsync(BaseAction action);


        public async Task<LoanInfo> GetDataAsync(string bankName, string borrowerName)
        {
            return await dbStore.GetDataAsync(bankName, borrowerName);
        }


        public async Task StoreDataAsync(LoanInfo record)
        {
            await dbStore.StoreDataAsync(record);
        }
    }

}
