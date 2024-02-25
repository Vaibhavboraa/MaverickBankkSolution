using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    public interface ITransactionService
    {
        Task<string> Deposit(DepositDTO depositDTO);
        Task<string> Withdraw(WithdrawalDTO withdrawalDTO);
        Task<string> Transfer(TransferDTO transferDTO);
        Task<List<Transactions>> GetLast10Transactions(long accountNumber);
        Task<List<Transactions>> GetLastMonthTransactions(long accountNumber);
        Task<List<Transactions>> GetTransactionsBetweenDates(long accountNumber, DateTime startDate, DateTime endDate);

    }
}
