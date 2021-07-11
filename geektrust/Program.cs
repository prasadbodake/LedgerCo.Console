using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace geektrust
{
    class Program
    {
        private static readonly List<LedgerDetails> ledgerDetails = new List<LedgerDetails>();
        private static readonly List<LumpSumPaymentDetails> lumpSumPaymentDetails = new List<LumpSumPaymentDetails>();

        static void Main(string[] args)
        {
            String filename = args[0];
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            string line;

            using (StreamReader reader = new StreamReader(fileStream))
            {
                while ((line = reader.ReadLine()) != null)
                {                    
                    string result = DoOperation(line);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        Console.WriteLine(result);
                    }
                }
            }

            //remove while submit
            Console.ReadKey();
        }
        private static string DoOperation(string line)
        {
            if (line.StartsWith("LOAN"))
            {
                StoreLoan(line);
                return string.Empty;
            }
            else if (line.StartsWith("PAYMENT"))
            {
                ProcessPayment(line);
                return string.Empty;
            }
            else if (line.StartsWith("BALANCE"))
            {
                return GetBalance(line);
            }
            else
            {
                return "Error in command";
            }
        }

        private static void ProcessPayment(string line)
        {
            var arguments = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arguments.Length == 5)
            {
                var bankName = arguments[1].Trim();
                var borrowerName = arguments[2].Trim();

                if (!Int32.TryParse(arguments[3].Trim(), out int lumsumAmount))
                {
                    Console.WriteLine($"Error while parsing number of EMI : {arguments[3].Trim()}");
                }

                if (!Int32.TryParse(arguments[4].Trim(), out int numberOfEMI))
                {
                    Console.WriteLine($"Error while parsing number of EMI : {arguments[4].Trim()}");
                }

                var detail = ledgerDetails.FirstOrDefault(d => d.BankName == bankName && d.BorrowerName == borrowerName);
                if (detail == null)
                {
                    Console.WriteLine($"Details not found for: {bankName} with borrower {borrowerName}");
                }


                lumpSumPaymentDetails.Add(new LumpSumPaymentDetails(bankName, borrowerName, lumsumAmount, numberOfEMI));
            }
        }

        private static void StoreLoan(string line)
        {
            var arguments = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if(arguments.Length == 6)
            {
                var bankName = arguments[1].Trim();
                var borrowerName = arguments[2].Trim();
                if (!Int32.TryParse(arguments[3].Trim(), out int principleAmount))
                {
                    Console.WriteLine($"Error while parsing principle amount: {arguments[3].Trim()}");
                }
                if (!Int32.TryParse(arguments[4].Trim(), out int numberOfYears))
                {
                    Console.WriteLine($"Error while parsing years: {arguments[4].Trim()}");
                }
                if (!Int32.TryParse(arguments[5].Trim(), out int interestRate))
                {
                    Console.WriteLine($"Error while parsing interest rate: {arguments[5].Trim()}");
                }

                ledgerDetails.Add(new LedgerDetails(bankName, borrowerName, principleAmount, numberOfYears, interestRate));                
            }
        }

        private static string GetBalance(string line)
        {
            var arguments = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arguments.Length == 4)
            {
                var bankName = arguments[1].Trim();
                var borrowerName = arguments[2].Trim();
                if (!Int32.TryParse(arguments[3].Trim(), out int numberOfEMI))
                {
                    return $"Error while parsing number of EMI : {arguments[3].Trim()}";
                }

                var detail = ledgerDetails.FirstOrDefault(d => d.BankName == bankName && d.BorrowerName == borrowerName);
                if (detail == null)
                {
                    return $"Details not found for: {bankName} with borrower {borrowerName}";
                }

                var paymentDetail = lumpSumPaymentDetails.FirstOrDefault(d => d.BankName == bankName && d.BorrowerName == borrowerName);

                if (paymentDetail != null && paymentDetail.EMINumber <= numberOfEMI)
                {
                    int totalAmountToPay = TotalAmountToPay(detail);
                    int amountPaidTillLumpSum = GetEMIAmountPaidSoFar(detail, paymentDetail.EMINumber) + paymentDetail.AmountPaid;
                    int remainingAmount = totalAmountToPay - amountPaidTillLumpSum;

                    int emiAmount = RoundNumber((double)totalAmountToPay / (detail.Years * 12));
                    var remainingEMIsFromLumpsum = GetRemainingEMIs(remainingAmount, emiAmount);

                    var emisFromLumpsumToInputDate = numberOfEMI - paymentDetail.EMINumber;

                    var totalAmountPaid = amountPaidTillLumpSum + (emiAmount * emisFromLumpsumToInputDate);
                    var remainingEMIs = remainingEMIsFromLumpsum - emisFromLumpsumToInputDate;

                    return $"{bankName} {borrowerName} {totalAmountPaid} {remainingEMIs}";
                }
                else
                {
                    int amountPaid = GetEMIAmountPaidSoFar(detail, numberOfEMI);
                    int emisLeft = (detail.Years * 12) - numberOfEMI;

                    return $"{bankName} {borrowerName} {amountPaid} {emisLeft}";
                }
            }
            return string.Empty;
        }

        private static int GetEMIAmountPaidSoFar(LedgerDetails detail, int numberOfEMI)
        {
            int totalAmount = TotalAmountToPay(detail);
            int emiAmount = RoundNumber((double)totalAmount / (detail.Years * 12));
            return emiAmount * numberOfEMI;
        }

        private static int TotalAmountToPay(LedgerDetails detail)
        {
            double interestRate = (double) detail.InterestRate / 100;
            int interest = RoundNumber(detail.PrincipleAmount * detail.Years * interestRate);
            int totalAmount = interest + detail.PrincipleAmount;
            return totalAmount;
        }

        private static int GetRemainingEMIs(double amountpending, int emiAmount)
        {
            return RoundNumber(amountpending / emiAmount);
        }

        private static int RoundNumber(double number)
        {
            return (int)Math.Ceiling(number);
        }
    }

    public class LedgerDetails
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

    public class LumpSumPaymentDetails
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
