using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    public interface IBankEmployeeService
    {
        public Task<List<BankEmployees>> GetAllBankEmployee();
        public Task<BankEmployees> DeleteBankEmployee(int key);

        public Task<BankEmployees> GetBankEmployee(int key);
        public Task<BankEmployees> UpdateBankEmployeeName(UpdateBankEmployeeNameDTO updateBankEmployeeNameDTO);
    }
}
