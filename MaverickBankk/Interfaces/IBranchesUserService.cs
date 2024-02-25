using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IBranchesUserService
    {
        public Task<List<Branches>> GetAllBranches();
        public Task<Branches> GetBranch(string key);
    }
}
