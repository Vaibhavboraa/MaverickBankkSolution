using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    public interface IBranchesAdminService : IBranchesUserService
    {
        public Task<Branches> AddBranch(Branches item);
        public Task<Branches> UpdateBranchName(UpdateBranchNameDTO updateBranchNameDTO);
        public Task<Branches> DeleteBranch(string key);
    }
}
