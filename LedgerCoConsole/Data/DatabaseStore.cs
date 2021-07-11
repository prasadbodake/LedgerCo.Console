using LedgerCo.Models.Interfaces;
using LedgerCo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LedgerCo.Data
{
    internal class DatabaseStore : IDatabaseStore
    {
        private static readonly Dictionary<int, LoanRecord> _dataDictionary = new Dictionary<int, LoanRecord>();

        public Task<LoanRecord> GetDataAsync(string bankName, string borrowerName)
        {
            var key = GetRecordKey(bankName, borrowerName);
            return Task.FromResult(_dataDictionary.ContainsKey(key) ? _dataDictionary[key] : null);
        }

        public Task StoreDataAsync(LoanRecord record)
        {
            var key = GetRecordKey(record.BankName, record.BorrowerName);
            if (_dataDictionary.ContainsKey(key))
            {
                _dataDictionary[key] = record;
            }
            else
            {
                _dataDictionary.Add(key, record);
            }
            return Task.CompletedTask;
        }

        private static int GetRecordKey(string bankName, string borrowerName)
        {
            return string.GetHashCode($"{bankName}{borrowerName}");
        }
    }
}
