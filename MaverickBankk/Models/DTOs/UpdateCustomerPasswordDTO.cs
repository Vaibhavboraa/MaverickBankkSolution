﻿namespace MaverickBankk.Models.DTOs
{
    public class UpdateCustomerPasswordDTO
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
