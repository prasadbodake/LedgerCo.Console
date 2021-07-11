namespace geektrust
{
    internal class LumpSumPaymentDetails
    {
        public string BankName { get; set; }

        public string BorrowerName { get; set; }

        public int AmountPaid { get; set; }

        public int EMINumber { get; set; }

        public LumpSumPaymentDetails(string bankName, string borrowerName, int amountPaid, int numberOfEMI)
        {
            BankName = bankName;
            BorrowerName = borrowerName;
            AmountPaid = amountPaid;
            EMINumber = numberOfEMI;
        }
    }
}
