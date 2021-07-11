using LedgerCo.Models.Interfaces;
using LedgerCo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LedgerCo.Data
{
    internal class DatabaseStore : IDatabaseStore
    {
        private static readonly Dictionary<int, LoanInfo> _dataDictionary = new Dictionary<int, LoanInfo>();

        public Task<LoanInfo> GetDataAsync(string bankName, string borrowerName)
        {
            var key = GetRecordKey(bankName, borrowerName);
            return Task.FromResult(_dataDictionary.ContainsKey(key) ? _dataDictionary[key] : null);
        }

        public Task StoreDataAsync(LoanInfo info)
        {
            var key = GetRecordKey(info.BankName, info.BorrowerName);
            if (_dataDictionary.ContainsKey(key))
            {
                _dataDictionary[key] = info;
            }
            else
            {
                _dataDictionary.Add(key, info);
            }
            return Task.CompletedTask;
        }

        private static int GetRecordKey(string bankName, string borrowerName)
        {
            return string.GetHashCode($"{bankName}{borrowerName}");
        }
    }
}
