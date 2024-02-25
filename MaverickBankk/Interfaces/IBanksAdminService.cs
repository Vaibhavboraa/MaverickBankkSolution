using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IBanksAdminService : IBanksUserService
    {
        public Task<Banks> AddBank(Banks item);
        public Task<Banks> UpdateBankName(UpdateBankNameDTO updateBankNameDTO);
        public Task<Banks> DeleteBank(int key);
    }
}
