//using MaverickBankk.Models;

//namespace MaverickBankk.Interfaces
//{

//    public interface IAvailableLoansUserService
//    {
//        Task<List<AvailableLoans>?> GetAllLoans();
//    }
//    //public interface IAvailableLoansUserService
//    //{
//    //    Task<List<AvailableLoans>?> GetAllLoans();
//    //    Task<AvailableLoans?> GetLoanById(int loanId);
//    //}
//}


using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IAvailableLoansUserService
    {
        Task<List<AvailableLoans>?> GetAllLoans();
        Task<AvailableLoans?> GetLoanById(int loanId);
    }
}