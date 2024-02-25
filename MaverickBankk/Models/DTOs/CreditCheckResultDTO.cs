﻿namespace MaverickBankk.Models.DTOs
{
    public class CreditCheckResultDTO
    {

        public double InboundAmount { get; set; }
        public double OutboundAmount { get; set; }
        public string CreditScore { get; set; } = string.Empty;

    }
}
