using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IAccountManagementService
    {
        Task<Accounts> OpenNewAccount(AccountOpeningDTO accountOpeningDTO);
        Task<string> CloseAccount(long accountNumber);
        Task<List<Accounts>> GetAllAccountsByCustomerId(int customerId);
        Task<Accounts> GetAccountDetails(long accountNumber);
        //    Task<List<Transactions>> GetLast10Transactions(long accountNumber);
        //    Task<List<Transactions>> GetLastMonthTransactions(long accountNumber);
        //    Task<List<Transactions>> GetTransactionsBetweenDates(long accountNumber, DateTime startDate, DateTime endDate);
        //}
    }
}
