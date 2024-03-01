//using MaverickBankk.Models;

//namespace MaverickBankk.Interfaces
//{

//    public interface IAdminAvailableLoansService
//    {
//        Task<AvailableLoans> AddLoan(AvailableLoans loan);
//    }
//    //public interface IAdminAvailableLoansService
//    //{
//    //    Task<AvailableLoans> AddLoan(AvailableLoans loan);
//    //    Task<AvailableLoans?> DeleteLoan(int loanId);
//    //    Task<AvailableLoans> UpdateLoan(AvailableLoans loan);
//    //}
//}

using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IAdminAvailableLoansService
    {
        Task<AvailableLoans> AddLoan(AvailableLoans loan);
        Task<AvailableLoans?> DeleteLoan(int loanId);
        Task<AvailableLoans> UpdateLoan(AvailableLoans loan);
    }
}