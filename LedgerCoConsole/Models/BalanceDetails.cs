namespace LedgerCo.Models
{
    internal class BalanceDetails
    {
        public string BankName { get; set; }

        public string BorrowerName { get; set; }

        public decimal TotalAmountPaid { get; set; }

        public int RemainingEmiCount { get; set; }

        public override string ToString()
        {
            return $"{BankName} {BorrowerName} {TotalAmountPaid} {RemainingEmiCount}";
        }
    }
}
