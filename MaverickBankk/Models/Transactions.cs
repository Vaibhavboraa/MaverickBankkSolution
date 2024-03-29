﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{


    //public class Transactions : IEquatable<Transactions>
    //{
    //    [Key]
    //    public int TransactionID { get; set; }
    //    public double Amount { get; set; }
    //    public DateTime TransactionDate { get; set; } = DateTime.Now;
    //    public string Description { get; set; }
    //    public string TransactionType { get; set; }
    //    public string Status { get; set; }


    //    public long SourceAccountNumber { get; set; }
    //    [ForeignKey("SourceAccountNumber")]
    //    public Accounts Accounts { get; set; }


    //    public long? DestinationAccountNumber { get; set; }
    //    [ForeignKey("DestinationAccountNumber")]
    //    public Accounts? DestinationAccount { get; set; }


    //    public int? BeneficiaryID { get; set; } // Foreign key
    //    [ForeignKey("BeneficiaryID")] // Specify the foreign key constraint
    //    public Beneficiaries? Beneficiary { get; set; }

    //    public Transactions()
    //    {

    //    }

    //    public Transactions(int transactionID, double amount, DateTime transactionDate, string description, string transactionType, string status, long sourceAccountNumber, long? destinationAccountNumber, int? beneficiaryId)
    //    {
    //        TransactionID = transactionID;
    //        Amount = amount;
    //        TransactionDate = transactionDate;
    //        Description = description;
    //        TransactionType = transactionType;
    //        Status = status;
    //        SourceAccountNumber = sourceAccountNumber;
    //        DestinationAccountNumber = destinationAccountNumber;
    //        BeneficiaryID = beneficiaryId;
    //    }

    //    public bool Equals(Transactions? other)
    //    {
    //        return TransactionID == other.TransactionID;
    //    }
    //}
    public class Transactions : IEquatable<Transactions>
    {
        [Key]
        public int TransactionID { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string Description { get; set; }=string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public long SourceAccountNumber { get; set; }
        [ForeignKey("SourceAccountNumber")]
        public Accounts? Accounts { get; set; }
        public long? DestinationAccountNumber { get; set; }
        [ForeignKey("DestinationAccountNumber")]
        public Accounts? DestinationAccount { get; set; }

        public Transactions()
        {

        }
        public Transactions(int transactionID, double amount, DateTime transactionDate, string description, string transactionType, string status, long sourceAccountNumber, long destinationAccountNumber)
        {
            TransactionID = transactionID;
            Amount = amount;
            TransactionDate = transactionDate;
            Description = description;
            TransactionType = transactionType;
            Status = status;
            SourceAccountNumber = sourceAccountNumber;
            DestinationAccountNumber = destinationAccountNumber;
        }

        public bool Equals(Transactions? other)
        {
            return TransactionID == other?.TransactionID;
        }
    }
}
