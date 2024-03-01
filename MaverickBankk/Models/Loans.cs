using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaverickBankk.Models
{
    public class Loans : IEquatable<Loans>
    {
        [Key]
        public int LoanID { get; set; }
        public double LoanAmount { get; set; }
        public string LoanType { get; set; } = string.Empty;
        public double Interest { get; set; }
        public int Tenure { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string Status { get; set; }= string.Empty;
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers? Customers { get; set; }
        public Loans()
        {

        }

        public Loans(int loanID, double loanAmount, string loanType, double interest, int tenure, string purpose, string status, int customerID)
        {
            LoanID = loanID;
            LoanAmount = loanAmount;
            LoanType = loanType;
            Interest = interest;
            Tenure = tenure;
            Purpose = purpose;
            Status = status;
            CustomerID = customerID;
        }

        public bool Equals(Loans? other)
        {
            return LoanID == other?.LoanID;
        }
    }
}
