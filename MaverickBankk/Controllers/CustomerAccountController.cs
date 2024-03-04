using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace MaverickBankk.Controllers
{

    //[Route("api/[controller]")]
    //[ApiController]
    //public class CustomerAccountController : ControllerBase
    //{
    //    private readonly IAccountManagementService _accountManagementService;
    //    private readonly ILogger<CustomerAccountController> _logger;

    //    public CustomerAccountController(IAccountManagementService accountManagementService, ILogger<CustomerAccountController> logger)
    //    {
    //        _accountManagementService = accountManagementService;
    //        _logger = logger;
    //    }
    //    [Authorize(Roles = "Customer")]
    //    [Route("Open Account")]
    //    [HttpPost]
    //    public async Task<ActionResult<Accounts>> OpenAccount(AccountOpeningDTO accountOpeningDTO)
    //    {
    //        try
    //        {
    //            var newAccount = await _accountManagementService.OpenNewAccount(accountOpeningDTO);
    //            return Ok(newAccount);
    //        }
    //        catch (NoAccountsFoundException nafe)
    //        {
    //            _logger.LogError(nafe, "Error occurred while opening account");
    //            return StatusCode(500, "Internal server error");
    //        }
    //    }
    //    [Authorize(Roles = "Customer")]
    //    [Route("Close Account")]
    //    [HttpPost]
    //    public async Task<ActionResult<bool>> CloseAccount(long accountNumber)
    //    {
    //        try
    //        {
    //            var result = await _accountManagementService.CloseAccount(accountNumber);
    //            return Ok(result);
    //        }
    //        catch (NoAccountsFoundException ex)
    //        {
    //            _logger.LogError(ex, $"No account found with number: {accountNumber}");
    //            return NotFound($"No account found with number: {accountNumber}");
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, $"Error closing account with number: {accountNumber}");
    //            return StatusCode(500, "Internal server error");
    //        }
    //    }
    //    //[Route("GetAccountDetailsByAccountNumber")]
    //    //[HttpGet]
    //    //public async Task<ActionResult<Accounts>> GetAccountDetails(long accountNumber)
    //    //{
    //    //    try
    //    //    {
    //    //        var account = await _accountManagementService.GetAccountDetails(accountNumber);

    //    //            return Ok(account);

    //    //    }
    //    //    catch (NoAccountsFoundException ex)
    //    //    {
    //    //        _logger.LogError(ex, $"No account found with number: {accountNumber}");
    //    //        return NotFound($"No account found with number: {accountNumber}");
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        _logger.LogError(ex, $"Error getting account details for number: {accountNumber}");
    //    //        return StatusCode(500, "Internal server error");
    //    //    }
    //    //}
    //    [Authorize(Roles = "Customer")]
    //    [HttpGet("{accountNumber}/{customerId}")]
    //    public async Task<ActionResult<Accounts>> GetAccountDetails(long accountNumber, int customerId)
    //    {
    //        try
    //        {
    //            var account = await _accountManagementService.GetAccountDetails(accountNumber, customerId);
    //            return Ok(account);
    //        }
    //        catch (NoAccountsFoundException ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"Internal server error: {ex.Message}");
    //        }
    //    }


    //    [Route("GetAccountDetailsByCustomerId")]
    //    [HttpGet]
    //    public async Task<ActionResult<List<Accounts>>> GetAllAccountsByCustomerId(int customerId)
    //    {
    //        try
    //        {
    //            var customerAccounts = await _accountManagementService.GetAllAccountsByCustomerId(customerId);
    //            return Ok(customerAccounts);


    //        }
    //        catch (NoCustomersFoundException ex)
    //        {
    //            _logger.LogError(ex, $"No customer found with ID: {customerId}");
    //            return NotFound($"No customer found with ID: {customerId}");
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, $"Error getting accounts for customer with ID: {customerId}");
    //            return StatusCode(500, "Internal server error");
    //        }
    //    }
    //}
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    [ExcludeFromCodeCoverage]
    public class CustomerAccountController : ControllerBase
    {
        private readonly IAccountManagementService _accountManagementService;
        private readonly ILogger<CustomerAccountController> _logger;

        public CustomerAccountController(IAccountManagementService accountManagementService, ILogger<CustomerAccountController> logger)
        {
            _accountManagementService = accountManagementService;
            _logger = logger;
        }
        [Authorize(Roles = "Customer")]
        [Route("Open Account")]
        [HttpPost]
        public async Task<ActionResult<Accounts>> OpenAccount(AccountOpeningDTO accountOpeningDTO)
        {
            try
            {
                var newAccount = await _accountManagementService.OpenNewAccount(accountOpeningDTO);
                return Ok(newAccount);
            }
            catch (NoAccountsFoundException nafe)
            {
                _logger.LogError(nafe, "Error occurred while opening account");
                return StatusCode(500, "Internal server error");
            }
        }
        [Authorize(Roles = "Customer")]
        [Route("Close Account")]
        [HttpPost]
        public async Task<ActionResult<bool>> CloseAccount(long accountNumber)
        {
            try
            {
                var result = await _accountManagementService.CloseAccount(accountNumber);
                return Ok(result);
            }
            catch (NoAccountsFoundException ex)
            {
                _logger.LogError(ex, $"No account found with number: {accountNumber}");
                return NotFound($"No account found with number: {accountNumber}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error closing account with number: {accountNumber}");
                return StatusCode(500, "Internal server error");
          
            }
        }
        [Authorize(Roles = "Customer")]
        [Route("GetAccountDetailsByAccountNumber")]
        [HttpGet]
        public async Task<ActionResult<Accounts>> GetAccountDetails(long accountNumber)
        {
            try
            {
                var account = await _accountManagementService.GetAccountDetails(accountNumber);

                return Ok(account);

            }
            catch (NoAccountsFoundException ex)
            {
                _logger.LogError(ex, $"No account found with number: {accountNumber}");
                return NotFound($"No account found with number: {accountNumber}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting account details for number: {accountNumber}");
                return StatusCode(500, "Internal server error");
            }
        }
      
        [Route("GetAccountDetailsByCustomerId")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> GetAllAccountsByCustomerId(int customerId)
        {
            try
            {
                var customerAccounts = await _accountManagementService.GetAllAccountsByCustomerId(customerId);
                return Ok(customerAccounts);


            }
            catch (NoCustomersFoundException ex)
            {
                _logger.LogError(ex, $"No customer found with ID: {customerId}");
                return NotFound($"No customer found with ID: {customerId}");
            }
            catch(NoAccountsFoundException ex)
            {
                _logger.LogError(ex, $"No account found");
                return NotFound($"No account found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting accounts for customer with ID: {customerId}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
