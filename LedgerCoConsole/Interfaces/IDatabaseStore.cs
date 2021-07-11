using LedgerCo.Models;
using System.Threading.Tasks;

namespace LedgerCo.Interfaces
{
    internal interface IDatabaseStore
    {
        Task StoreDataAsync(LoanRecord record);

        Task<LoanRecord> GetDataAsync(string bankName, string borrowerName);
    }
}
