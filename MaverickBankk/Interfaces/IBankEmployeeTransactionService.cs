using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IBankEmployeeTransactionService
    {
        Task<List<Transactions>?> GetAllTransactions();
        Task<List<Transactions>?> GetTransactionsByAccountNumber(long accountNumber);
        Task<double> GetTotalInboundTransactions(long accountNumber);
        Task<double> GetTotalOutboundTransactions(long accountNumber);
    }
}
