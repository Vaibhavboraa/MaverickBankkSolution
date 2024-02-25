using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    public interface IAdminLoginService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> Register(RegisterAdminDTO user);
    }
}
