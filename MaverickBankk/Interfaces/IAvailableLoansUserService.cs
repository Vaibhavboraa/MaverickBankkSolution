using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IAvailableLoansUserService
    {
        Task<List<AvailableLoans>?> GetAllLoans();
        Task<AvailableLoans?> GetLoanById(int loanId);
    }
}
