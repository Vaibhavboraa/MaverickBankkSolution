﻿using MaverickBankk.Interfaces;
using MaverickBankk.Models;

namespace MaverickBankk.Services
{

    //public class AvailableLoansUserService : IAvailableLoansUserService
    //{
    //    private readonly IRepository<int, AvailableLoans> _availableLoansRepository;

    //    public AvailableLoansUserService(IRepository<int, AvailableLoans> availableLoansRepository)
    //    {
    //        _availableLoansRepository = availableLoansRepository;
    //    }

    //    public async Task<List<AvailableLoans>?> GetAllLoans()
    //    {
    //        return await _availableLoansRepository.GetAll();
    //    }


    //}
    public class AvailableLoansUserService : IAvailableLoansUserService
    {
        private readonly IRepository<int, AvailableLoans> _availableLoansRepository;

        public AvailableLoansUserService(IRepository<int, AvailableLoans> availableLoansRepository)
        {
            _availableLoansRepository = availableLoansRepository;
        }

        public async Task<List<AvailableLoans>?> GetAllLoans()
        {
            return await _availableLoansRepository.GetAll();
        }

        public async Task<AvailableLoans?> GetLoanById(int loanId)
        {
            return await _availableLoansRepository.Get(loanId);

        }


    }
}
