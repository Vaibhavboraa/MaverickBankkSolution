using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    public interface ICustomerLoginService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> Register(RegisterCustomerDTO user);
    }
}
