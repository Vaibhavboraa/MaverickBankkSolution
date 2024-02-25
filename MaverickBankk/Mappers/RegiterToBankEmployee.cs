using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;

namespace MaverickBankk.Mappers
{
    public class RegiterToBankEmployee
    {
        BankEmployees bankEmployees;
        public RegiterToBankEmployee(RegisterBankEmployeeDTO register)
        {
            bankEmployees = new BankEmployees();
            bankEmployees.Name = register.Name;
            bankEmployees.Email = register.Email;
            bankEmployees.Position = register.Position;
            bankEmployees.Phone = register.Phone;
        }
        public BankEmployees GetBankEmployees()
        {
            return bankEmployees;
        }
    }
}
