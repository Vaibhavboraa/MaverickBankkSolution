using MaverickBankk.Models.DTOs;

namespace MaverickBankk.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(LoginUserDTO user);
    }
}
