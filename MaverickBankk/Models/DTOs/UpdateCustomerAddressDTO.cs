namespace MaverickBankk.Models.DTOs
{
    public class UpdateCustomerAddressDTO
    {
        public int CustomerID { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
