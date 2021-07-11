using LedgerCo.Models;
using System.Threading.Tasks;

namespace LedgerCo.Models.Interfaces
{
    internal interface IDatabaseStore
    {
        Task StoreDataAsync(LoanInfo record);

        Task<LoanInfo> GetDataAsync(string bankName, string borrowerName);
    }
}
