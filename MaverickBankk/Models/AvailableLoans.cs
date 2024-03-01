﻿using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{





    //public class AvailableLoans
    //{

    //    [Key]
    //    public int LoanID { get; set; }
    //    public double LoanAmount { get; set; }
    //    public string? LoanType { get; set; }
    //    public double Interest { get; set; }
    //    public int Tenure { get; set; }
    //    public string? Purpose { get; set; }
    //    public string? Status { get; set; }
    //    public AvailableLoans()
    //    {
    //    }
    //}
    public class AvailableLoans
    {
        [Key]
        public int LoanID { get; set; }
        public double LoanAmount { get; set; }
        public string? LoanType { get; set; }
        public double Interest { get; set; }
        public int Tenure { get; set; }
        public string? Purpose { get; set; }
        public string? Status { get; set; }
        public AvailableLoans()
        {
        }

    }
}
