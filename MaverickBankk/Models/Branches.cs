using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{
    public class Branches : IEquatable<Branches>
    {
        [Key]
        public string IFSCNumber { get; set; }
        public string BranchName { get; set; }
        public int BankID { get; set; }
        [ForeignKey("BankID")]
        public Banks? Banks { get; set; }


        public Branches(string iFSCNumber, string branchName, int bankID)
        {
            IFSCNumber = iFSCNumber;
            BranchName = branchName;
            BankID = bankID;
        }

        public bool Equals(Branches? other)
        {
            return IFSCNumber == other.IFSCNumber;
        }
    }
}
