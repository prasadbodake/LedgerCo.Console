using LedgerCo.Models;
using System.Threading.Tasks;

namespace LedgerCo.Models.Interfaces
{
    internal interface IDatabaseStore
    {
        Task StoreDataAsync(LoanRecord record);

        Task<LoanRecord> GetDataAsync(string bankName, string borrowerName);
    }
}
