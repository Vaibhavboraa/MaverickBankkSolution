namespace MaverickBankk.Models.DTOs
{

    //public class BeneficiaryDTO
    //{

    //    public long BeneficiaryAccountNumber { get; set; }
    //    public string Name { get; set; }
    //    public string IFSC { get; set; }
    //    public int CustomerID { get; set; }
    //}


    public class BeneficiaryDTO
    {
        public long AccountNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public int CustomerId { get; set; }
    }
}
