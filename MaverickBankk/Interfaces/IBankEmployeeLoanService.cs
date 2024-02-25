using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaverickBankk.Interfaces
{
    public interface IBankEmployeeLoanService
    {
        Task<Loans> ReviewLoanApplication(int loanId);

        Task<string> MakeLoanDecision(int loanId, bool approved);
        Task<Accounts> DisburseLoan(int loanId, long AccId);
        Task<List<Loans>> GetAllLoans();
        Task<ActionResult<CreditCheckResultDTO>> CheckCredit(long accountId);
    }
}
