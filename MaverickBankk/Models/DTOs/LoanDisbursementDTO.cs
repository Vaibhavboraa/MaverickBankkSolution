﻿namespace MaverickBankk.Models.DTOs
{
    public class LoanDisbursementDTO
    {
        public int LoanId { get; set; }
        public double LoanAmount { get; set; }
        public List<long>? AccountNumbers { get; set; }

    }
}
