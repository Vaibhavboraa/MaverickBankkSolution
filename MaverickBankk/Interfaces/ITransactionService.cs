//using MaverickBankk.Models;
//using MaverickBankk.Models.DTOs;

//namespace MaverickBankk.Interfaces
//{

//    public interface ITransactionService
//    {
//        Task<string> Deposit(int customerId, DepositDTO depositDTO);
//        Task<string> Withdraw(int customerId, WithdrawalDTO withdrawalDTO);
//        Task<string> Transfer(int customerId, TransferDTO transferDTO);
//        Task<List<Transactions>> GetLast10Transactions(long accountNumber);
//        Task<List<Transactions>> GetLastMonthTransactions(long accountNumber);
//        Task<List<Transactions>> GetTransactionsBetweenDates(long accountNumber, DateTime startDate, DateTime endDate);

//    }
//    //public interface ITransactionService
//    //{
//    //    Task<string> Deposit(DepositDTO depositDTO);
//    //    Task<string> Withdraw(WithdrawalDTO withdrawalDTO);
//    //    Task<string> Transfer(TransferDTO transferDTO);
//    //    Task<List<Transactions>> GetLast10Transactions(long accountNumber);
//    //    Task<List<Transactions>> GetLastMonthTransactions(long accountNumber);
//    //    Task<List<Transactions>> GetTransactionsBetweenDates(long accountNumber, DateTime startDate, DateTime endDate);

//    //}
//}


using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    //public interface ITransactionService
    //{
    //    Task<string> Deposit(DepositDTO depositDTO);
    //    Task<string> Withdraw(WithdrawalDTO withdrawalDTO);
    //    Task<string> Transfer(TransferDTO transferDTO);
    //    Task<List<Transactions>> GetLast10Transactions(long accountNumber);
    //    Task<List<Transactions>> GetLastMonthTransactions(long accountNumber);
    //    Task<List<Transactions>> GetTransactionsBetweenDates(long accountNumber, DateTime startDate, DateTime endDate);

    //}


    //vat

    public interface ITransactionService
    {
        Task<string> Deposit(int customerId, DepositDTO depositDTO);
        Task<string> Withdraw(int customerId, WithdrawalDTO withdrawalDTO);
        Task<string> Transfer(int customerId, TransferDTO transferDTO);
        Task<List<Transactions>> GetLast10Transactions(long accountNumber);
        Task<List<Transactions>> GetLastMonthTransactions(long accountNumber);
        Task<List<Transactions>> GetTransactionsBetweenDates(long accountNumber, DateTime startDate, DateTime endDate);

    }
}