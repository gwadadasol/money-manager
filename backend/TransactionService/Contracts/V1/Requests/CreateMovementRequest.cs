using System;

namespace TransactionService.Contracts.V1
{
    public class UpdateMovementRequest    
    {
        public string AccountType {get; set;}
        public string AccountNumber {get; set;}
        public DateTime TransactionDate {get; set;}
        public string ChequeNumber {get; set;}
        public string Description {get; set;}
        public string Description2 {get; set;}
        public double AmountCAD {get; set;}
        public double AmountUSD {get; set;}
    }
}