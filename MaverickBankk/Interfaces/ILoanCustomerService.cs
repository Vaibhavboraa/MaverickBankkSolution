using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
   
    
        public interface ILoanCustomerService
        {
            Task ApplyForLoan(LoanApplicationDTO loanApplication);
            //Task<List<Loans>> ViewAvailedLoans();
            Task<List<Loans>> ViewAvailedLoans(int customerId);
        }
    
}
