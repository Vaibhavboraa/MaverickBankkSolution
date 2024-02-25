using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    public interface IBankEmployeeLoginService
    {
        Task<LoginUserDTO> Login(LoginUserDTO employee);
        Task<LoginUserDTO> Register(RegisterBankEmployeeDTO employee);
    }
}
