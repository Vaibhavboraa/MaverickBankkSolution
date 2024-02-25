using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
   
        public interface ILoanService
        {
            Task<List<Loans>> GetLoanApplications();
            Task<bool> ReviewAndApproveLoan(int loanId, int employeeId);
            Task<bool> DisburseApprovedLoans();
        }

    
}
