using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IAdminService
    {
        public Task<List<Admin>> GetAllAdmin();
        public Task<Admin> DeleteAdmin(int key);

        public Task<Admin> GetAdmin(int key);
        public Task<Admin> UpdateAdminName(UpdateBankAdminNameDTO updateAdminNameDTO);
    }
}
