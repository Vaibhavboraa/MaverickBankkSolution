//using MaverickBankk.Models;
//using MaverickBankk.Models.DTOs;

//namespace MaverickBankk.Interfaces
//{

//    public interface ICustomerBeneficiaryService
//    {
//        //Task<bool> AddBeneficiary(BeneficiaryDTO beneficiaryDTO);
//        Task<Beneficiaries> AddBeneficiary(BeneficiaryDTO beneficiaryDTO);
//        //Task<string> TransferToBeneficiary(long sourceAccountNumber, long beneficiaryAccountNumber, double amount);
//        Task<string> TransferToBeneficiary(BeneficiaryTransferDTO transferDTO);
//        Task<List<BranchDTO>> GetBranchesByBank(string bankName);
//        Task<string> GetIFSCByBranch(string branchName);
//        Task<List<Beneficiaries>> GetBeneficiariesByCustomerID(int customerID);
//    }
//    //public interface ICustomerBeneficiaryService
//    //{
//    //    //Task<bool> AddBeneficiary(BeneficiaryDTO beneficiaryDTO);
//    //    Task AddBeneficiary(BeneficiaryDTO beneficiaryDTO);
//    //    Task<List<BranchDTO>> GetBranchesByBank(string bankName);
//    //    Task<string> GetIFSCByBranch(string branchName);
//    //}
//}


using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    public interface ICustomerBeneficiaryService
    {
        //Task<bool> AddBeneficiary(BeneficiaryDTO beneficiaryDTO);
        Task AddBeneficiary(BeneficiaryDTO beneficiaryDTO);
        Task<List<BranchDTO>> GetBranchesByBank(string bankName);
        Task<string> GetIFSCByBranch(string branchName);
    }
}