using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface ICustomerUserService
    {
        public Task<Customers> GetCustomers(int id);
    }
}
