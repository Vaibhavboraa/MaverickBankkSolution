﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{
    public class BankEmployees : IEquatable<BankEmployees>
    {
        [Key]
        public int EmployeeID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public string? Phone { get; set; }
        [ForeignKey("Email")]
        public Validation? Validation { get; set; }

        public BankEmployees()
        {

        }
        public BankEmployees(int employeeID, string name, string email, string position, string phone)
        {
            EmployeeID = employeeID;
            Name = name;
            Email = email;
            Phone = phone;
            Position = position;
        }

        public bool Equals(BankEmployees? other)
        {
            return EmployeeID == other?.EmployeeID;
        }
    }
}
