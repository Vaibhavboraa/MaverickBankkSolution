using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaverickBankk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class BankEmployeeLoginController : ControllerBase
    {
        private readonly IBankEmployeeLoginService _bankEmployeeService;

        public BankEmployeeLoginController(IBankEmployeeLoginService bankEmployeeService)
        {
            _bankEmployeeService = bankEmployeeService;
        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<BankEmployees>> Login(LoginUserDTO loginUserDTO)
        {
            try
            {
                var result = await _bankEmployeeService.Login(loginUserDTO);
                return Ok(result);
            }
            catch (InvalidUserException)
            {
                return Unauthorized("Invalid username or password");
            }
            catch (DeactivatedUserException)
            {
                return Unauthorized("User deactivated");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<BankEmployees>> Register(RegisterBankEmployeeDTO registerBankEmployeeDTO)
        {
            try
            {
                var result = await _bankEmployeeService.Register(registerBankEmployeeDTO);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
