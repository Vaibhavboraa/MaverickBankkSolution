using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaverickBankk.Controllers
{



    //    [ApiController]
    //    [Route("api/[controller]")]
    //    public class CustomerBeneficiariesController : ControllerBase
    //    {
    //        private readonly ICustomerBeneficiaryService _customerBeneficiaryService;
    //        private readonly ILogger<CustomerBeneficiariesController> _logger;

    //        public CustomerBeneficiariesController(
    //            ICustomerBeneficiaryService customerBeneficiaryService,
    //            ILogger<CustomerBeneficiariesController> logger)
    //        {
    //            _customerBeneficiaryService = customerBeneficiaryService;
    //            _logger = logger;
    //        }

    //        [HttpPost("AddBeneficiary")]
    //        public async Task<IActionResult> AddBeneficiary(BeneficiaryDTO beneficiaryDTO)
    //        {
    //            try
    //            {
    //                var beneficiary = await _customerBeneficiaryService.AddBeneficiary(beneficiaryDTO);
    //                return Ok("Beneficiary added successfully.");
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "Error adding beneficiary.");
    //                return StatusCode(500, "An error occurred while processing your request.");
    //            }
    //        }




    //        [Route("GetBeneficiaryByCustomerId")]
    //        [HttpGet]
    //        public async Task<IActionResult> GetBeneficiaries(int customerId)
    //        {
    //            try
    //            {
    //                var beneficiaries = await _customerBeneficiaryService.GetBeneficiariesByCustomerID(customerId);
    //                return Ok(beneficiaries);
    //            }
    //            catch (NoCustomersFoundException ex)
    //            {
    //                _logger.LogError(ex, "No Customer Found");
    //                return StatusCode(500, "No Customer Found");
    //            }
    //            catch (NoBeneficiariesFoundException ex)
    //            {
    //                _logger.LogError(ex, "No Beneficiary found");
    //                return StatusCode(500, "No Beneficiary Found");
    //            }
    //        }




    //        [Route("GetBankBranchesByBankName")]
    //        [HttpGet]
    //        public async Task<IActionResult> GetBranchesByBank(string bankName)
    //        {
    //            try
    //            {
    //                var branches = await _customerBeneficiaryService.GetBranchesByBank(bankName);
    //                return Ok(branches);
    //            }
    //            catch (NoBanksFoundException nbfe)
    //            {
    //                _logger.LogError(nbfe, "Bank Not found");
    //                return StatusCode(500, "No bank found");
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "Error fetching branches by bank.");
    //                return StatusCode(500, "An error occurred while processing your request.");
    //            }
    //        }
    //        [Route("GetIFSCByBranchName")]
    //        [HttpGet]
    //        public async Task<IActionResult> GetIFSCByBranch(string branchName)
    //        {
    //            try
    //            {
    //                var ifsc = await _customerBeneficiaryService.GetIFSCByBranch(branchName);
    //                return Ok(ifsc);
    //            }
    //            catch (NoBranchesFoundException ex)
    //            {
    //                _logger.LogError(ex, "Error fetching IFSC by branch: Branch not found.");
    //                return NotFound(ex.Message);
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "Error fetching IFSC by branch.");
    //                return StatusCode(500, "An error occurred while processing your request.");
    //            }
    //        }

    //        [HttpPost("TransferToBeneficiary")]
    //        public async Task<IActionResult> TransferToBeneficiary(BeneficiaryTransferDTO transferDTO)
    //        {
    //            try
    //            {
    //                var result = await _customerBeneficiaryService.TransferToBeneficiary(transferDTO);
    //                return Ok(result);
    //            }
    //            catch (NoAccountsFoundException ex)
    //            {
    //                _logger.LogError(ex, "Error transferring to beneficiary: Account not found.");
    //                return NotFound(ex.Message);
    //            }
    //            catch (NotSufficientBalanceException ex)
    //            {
    //                _logger.LogError(ex, "Error transferring to beneficiary: Not sufficient balance.");
    //                return BadRequest(ex.Message);
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "Error transferring to beneficiary.");
    //                return StatusCode(500, "An error occurred while processing your request.");
    //            }
    //        }

    //    }
    //}
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("ReactPolicy")]
    public class CustomerBeneficiariesController : ControllerBase
    {
        private readonly ICustomerBeneficiaryService _customerBeneficiaryService;
        private readonly ILogger<CustomerBeneficiariesController> _logger;

        public CustomerBeneficiariesController(
            ICustomerBeneficiaryService customerBeneficiaryService,
            ILogger<CustomerBeneficiariesController> logger)
        {
            _customerBeneficiaryService = customerBeneficiaryService;
            _logger = logger;
        }

        [Route("Add Beneficiary")]
        [HttpPost]
        public async Task<IActionResult> AddBeneficiary(BeneficiaryDTO beneficiaryDTO)
        {
            try
            {
                await _customerBeneficiaryService.AddBeneficiary(beneficiaryDTO);
                return Ok("Beneficiary added successfully.");
            }
            catch (NoCustomersFoundException ex)
            {
                _logger.LogError(ex, "Error adding beneficiary: No customer found.");
                return NotFound(ex.Message);
            }
            catch (NoBranchesFoundException ex)
            {
                _logger.LogError(ex, "Error adding beneficiary: Branch not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding beneficiary.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [Route("Get Bank Branches By Bank Name")]
        [HttpGet]
        public async Task<IActionResult> GetBranchesByBank(string bankName)
        {
            try
            {
                var branches = await _customerBeneficiaryService.GetBranchesByBank(bankName);
                return Ok(branches);
            }
            catch (NoBanksFoundException nbfe)
            {
                _logger.LogError(nbfe, "Bank Not found");
                return StatusCode(500, "No bank found");
            }
            catch (NoBranchesFoundException np)
            {
                _logger.LogError(np, "Branch Not found");
                return StatusCode(500, "No branch found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching branches by bank.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [Route("Get IFSC By Branc Name")]
        [HttpGet]
        public async Task<IActionResult> GetIFSCByBranch(string branchName)
        {
            try
            {
                var ifsc = await _customerBeneficiaryService.GetIFSCByBranch(branchName);
                return Ok(ifsc);
            }
            catch (NoBranchesFoundException ex)
            {
                _logger.LogError(ex, "Error fetching IFSC by branch: Branch not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching IFSC by branch.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}

