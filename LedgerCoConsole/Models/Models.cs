using System.Collections.Generic;

namespace LedgerCo.Models
{
    internal class LoanRecord
    {
        public string BankName { get; set; }
        
        public string BorrowerName { get; set; }

        public decimal Principal { get; set; }
        
        public int NumberOfYears { get; set; }
        
        public decimal RateOfInterest { get; set; }

        public decimal EMIAmount { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
