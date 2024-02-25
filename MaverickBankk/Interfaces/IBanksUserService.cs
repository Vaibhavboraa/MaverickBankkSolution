using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IBanksUserService
    {
        public Task<List<Banks>> GetAllBanks();
        public Task<Banks> GetBank(int key);
    }
}
