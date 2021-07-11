namespace geektrust
{
    internal class LedgerDetails
    {
        public string BankName { get; set; }
        
        public string BorrowerName { get; set; }
        
        public int PrincipleAmount { get; set; }
        
        public int Years { get; set; }
        
        public int InterestRate { get; set; }

        public LedgerDetails(string bankName, string borrowerName, int principleAmount, int numberOfYears, int interestRate)
        {
            BankName = bankName;
            BorrowerName = borrowerName;
            PrincipleAmount = principleAmount;
            Years = numberOfYears;
            InterestRate = interestRate;
        }
    }
}
