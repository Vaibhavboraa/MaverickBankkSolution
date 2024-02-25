namespace MaverickBankk.Models.DTOs
{
    public class UpdateCustomerNameDTO
    {
        public int CustomerID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
