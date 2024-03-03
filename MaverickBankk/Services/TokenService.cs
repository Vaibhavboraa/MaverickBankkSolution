using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MaverickBankk.Services
{
    public class TokenService : ITokenService
    {
      

       
            private readonly string _keyString;
            private readonly SymmetricSecurityKey _key;
            public TokenService(IConfiguration configuration)
            {
                _keyString = configuration["SecretKey"].ToString();
                _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keyString));
            }

            public async Task<string> GenerateToken(LoginUserDTO user)
            {
                await Task.CompletedTask;
                string token = string.Empty;
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserType))
            {
                throw new ArgumentNullException(nameof(user), "User information cannot be null or empty.");
            }

            var claims = new List<Claim>

            {

                new Claim(JwtRegisteredClaimNames.NameId,user.Email),
                new Claim(ClaimTypes.Role,user.UserType)
            };
                var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = cred
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var myToken = tokenHandler.CreateToken(tokenDescription);
                token = tokenHandler.WriteToken(myToken);
                return token;
            }
        }

     

    
}

//private readonly string _keyString;
//private readonly SymmetricSecurityKey _key;
//public TokenService(IConfiguration configuration)
//{
//    _keyString = configuration["SecretKey"].ToString();
//    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keyString));
//}

//public async Task<string> GenerateToken(LoginUserDTO user)
//{
//    string token = string.Empty;
//    if ( user.UserType == null)
//    {
//        throw new ValidationNotFoundException("No Email found");
//    }
//    var claims = new List<Claim>
//    {
//        new Claim(JwtRegisteredClaimNames.NameId,user.Email),
//        new Claim(ClaimTypes.Role,user.UserType)
//    };
//    var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
//    var tokenDescription = new SecurityTokenDescriptor
//    {
//        Subject = new ClaimsIdentity(claims),
//        Expires = DateTime.Now.AddDays(1),
//        SigningCredentials = cred
//    };
//    var tokenHandler = new JwtSecurityTokenHandler();
//    var myToken = tokenHandler.CreateToken(tokenDescription);
//    token = tokenHandler.WriteToken(myToken);
//    return token;