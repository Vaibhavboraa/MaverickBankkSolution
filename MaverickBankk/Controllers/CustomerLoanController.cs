using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MaverickBankk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace MaverickBankk.Controllers
{

    //namespace Capstone_Project.Controllers
    //{
    //    [Route("api/[controller]")]
    //    [ApiController]
    //    public class CustomerLoanController : ControllerBase
    //    {
    //        private readonly ILoanCustomerService _loanCustomerService;
    //        private readonly ILogger<CustomerLoanController> _logger;

    //        public CustomerLoanController(ILoanCustomerService loanCustomerService, ILogger<CustomerLoanController> logger)
    //        {
    //            _loanCustomerService = loanCustomerService;
    //            _logger = logger;
    //        }

    //        [Authorize(Roles = "Customer")]
    //        [Route("ApplyForLoan")]
    //        [HttpPost]
    //        public async Task<IActionResult> ApplyForLoan(LoanApplicationDTO loanApplication)
    //        {
    //            try
    //            {
    //                await _loanCustomerService.ApplyForLoan(loanApplication);
    //                return Ok("Loan application submitted successfully.");
    //            }
    //            catch (NoCustomersFoundException ex)
    //            {
    //                _logger.LogError(ex, $"Error applying for loan: {ex.Message}");
    //                return BadRequest(ex.Message);
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "Error applying for loan.");
    //                return StatusCode(500, "An error occurred while processing your request.");
    //            }
    //        }
    //        [Authorize(Roles = "Customer")]
    //        [Route("AvailedLoans")]
    //        [HttpGet]
    //        public async Task<IActionResult> ViewAvailedLoans(int customerId)
    //        {
    //            try
    //            {
    //                var availedLoans = await _loanCustomerService.ViewAvailedLoans(customerId);
    //                return Ok(availedLoans);
    //            }
    //            catch (NoCustomersFoundException ex)
    //            {
    //                _logger.LogError(ex, $"Error viewing availed loans: {ex.Message}");
    //                return NotFound(ex.Message);
    //            }
    //            catch (NoLoansFoundException ex)
    //            {
    //                _logger.LogError(ex, $"Error viewing availed loans: {ex.Message}");
    //                return NotFound(ex.Message);
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "Error viewing availed loans.");
    //                return StatusCode(500, "An error occurred while processing your request.");
    //            }
    //        }
    //    }
    //}
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class CustomerLoanController : ControllerBase
    {
        private readonly ILoanCustomerService _loanCustomerService;
        private readonly ILogger<CustomerLoanController> _logger;

        public CustomerLoanController(ILoanCustomerService loanCustomerService, ILogger<CustomerLoanController> logger)
        {
            _loanCustomerService = loanCustomerService;
            _logger = logger;
        }

       // [Authorize(Roles = "Customer")]
        [Route("ApplyForLoan")]
        [HttpPost]
        public async Task<IActionResult> ApplyForLoan(LoanApplicationDTO loanApplication)
        {
            try
            {
                await _loanCustomerService.ApplyForLoan(loanApplication);
                return Ok("Loan application submitted successfully.");
            }
            catch (NoCustomersFoundException ex)
            {
                _logger.LogError(ex, $"Error applying for loan: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying for loan.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [Route("AvailedLoans")]
        [HttpGet]
        public async Task<IActionResult> ViewAvailedLoans(int customerId)
        {
            try
            {
                var availedLoans = await _loanCustomerService.ViewAvailedLoans(customerId);
                return Ok(availedLoans);
            }
            catch (NoCustomersFoundException ex)
            {
                _logger.LogError(ex, $"Error viewing availed loans: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (NoLoansFoundException ex)
            {
                _logger.LogError(ex, $"Error viewing availed loans: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing availed loans.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
