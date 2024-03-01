using MaverickBankk.Interfaces;
using MaverickBankk.Models;

namespace MaverickBankk.Services
{
    //public class AdminAvailableLoansService : IAdminAvailableLoansService
    //{
    //    private readonly IRepository<int, AvailableLoans> _availableLoansRepository;

    //    public AdminAvailableLoansService(IRepository<int, AvailableLoans> availableLoansRepository)
    //    {
    //        _availableLoansRepository = availableLoansRepository;
    //    }

    //    public async Task<AvailableLoans> AddLoan(AvailableLoans loan)
    //    {
    //        return await _availableLoansRepository.Add(loan);
    //    }


    //}


    public class AdminAvailableLoansService : IAdminAvailableLoansService
    {
        private readonly IRepository<int, AvailableLoans> _availableLoansRepository;

        public AdminAvailableLoansService(IRepository<int, AvailableLoans> availableLoansRepository)
        {
            _availableLoansRepository = availableLoansRepository;
        }

        public async Task<AvailableLoans> AddLoan(AvailableLoans loan)
        {
            return await _availableLoansRepository.Add(loan);
        }

        public async Task<AvailableLoans?> DeleteLoan(int loanId)
        {
            return await _availableLoansRepository.Delete(loanId);
        }

        public async Task<AvailableLoans> UpdateLoan(AvailableLoans loan)
        {
            return await _availableLoansRepository.Update(loan);
        }
    }
}
