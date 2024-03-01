using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{
    public class Admin : IEquatable<Admin>
    {
        [Key]
        public int AdminID { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        [ForeignKey("Email")]
        public Validation? Validation { get; set; }

       

        public Admin(int adminID, string name, string email, string phone)
        {
            AdminID = adminID;
            Name = name;
            Email = email;
            Phone = phone;
        }
        public Admin()
        {

        }
        public bool Equals(Admin? other)
        {
            return AdminID == this.AdminID;
        }
    }
}
