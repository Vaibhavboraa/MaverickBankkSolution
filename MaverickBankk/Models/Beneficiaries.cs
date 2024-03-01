using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{




    //public class Beneficiaries : IEquatable<Beneficiaries>
    //{
    //    [Key]
    //    public int BeneficiaryID { get; set; }
    //    public long BeneficiaryAccountNumber { get; set; }
    //    public float Balance { get; set; }

    //    public string Name { get; set; } = string.Empty;
    //    public string IFSC { get; set; } = string.Empty;

    //    [ForeignKey("IFSC")]
    //    public Branches Branch { get; set; }

    //    public int CustomerID { get; set; }

    //    [ForeignKey("CustomerID")]
    //    public Customers Customer { get; set; }

    //    public bool Equals(Beneficiaries? other)
    //    {
    //        return BeneficiaryAccountNumber == other.BeneficiaryAccountNumber;
    //    }
    //}

    public class Beneficiaries : IEquatable<Beneficiaries>
    {
        [Key]
        public int BeneficiaryID { get; set; }

        public long AccountNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IFSC { get; set; } = string.Empty;

        [ForeignKey("IFSC")]
        public Branches? Branch { get; set; }

        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customers? Customer { get; set; }






        public bool Equals(Beneficiaries? other)
        {
            return BeneficiaryID == other?.BeneficiaryID;
        }
    }
}
