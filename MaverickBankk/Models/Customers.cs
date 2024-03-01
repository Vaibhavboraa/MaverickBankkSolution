using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{
    public class Customers : IEquatable<Customers>
    {
        [Key]
        public int CustomerID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public long? AadharNumber { get; set; }
        public string PANNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [ForeignKey("Email")]
        public Validation? Validation { get; set; }

        public ICollection<Accounts>? Accounts { get; set; }
        public ICollection<Beneficiaries>? Beneficiaries { get; set; }
        public ICollection<Loans>? Loans { get; set; }

        public Customers(int customerID, string name, DateTime dOB, int age, long phoneNumber, string address, long? aadharNumber, string pANNumber, string gender, string email)
        {
            CustomerID = customerID;
            Name = name;
            DOB = dOB;
            Age = age;
            PhoneNumber = phoneNumber;
            Address = address;
            AadharNumber = aadharNumber;
            PANNumber = pANNumber;
            Gender = gender;
            Email = email;
        }

        public Customers()
        {
        }

        public bool Equals(Customers? other)
        {
            return CustomerID == other?.CustomerID;
        }


    }
}
