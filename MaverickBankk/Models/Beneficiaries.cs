using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{

    public class Beneficiaries : IEquatable<Beneficiaries>
    {
        [Key]
        public int BeneficiaryID { get; set; } 

        public long AccountNumber { get; set; }
        public string Name { get; set; }
        public string IFSC { get; set; }

        [ForeignKey("IFSC")]
        public Branches Branch { get; set; }

        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customers Customer { get; set; }






        public bool Equals(Beneficiaries? other)
        {
            return BeneficiaryID == other.BeneficiaryID;
        }
    }
}
