

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Repositories;
using MaverickBankk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MaverickBankk.Controllers
{

    //[ApiController]
    //[Route("api/[controller]")]
    //public class AdminAvailableLoansController : ControllerBase
    //{
    //    private readonly IAdminAvailableLoansService _adminAvailableLoansService;

    //    public AdminAvailableLoansController(IAdminAvailableLoansService adminAvailableLoansService)
    //    {
    //        _adminAvailableLoansService = adminAvailableLoansService;
    //    }

    //    [HttpPost("add")]
    //    public async Task<IActionResult> AddLoan([FromBody] AvailableLoans loan)
    //    {
    //        try
    //        {
    //            var addedLoan = await _adminAvailableLoansService.AddLoan(loan);
    //            return Ok(addedLoan);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"Internal server error: {ex.Message}");
    //        }
    //    }
    //}
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class AdminAvailableLoansController : ControllerBase
    {
        private readonly IAdminAvailableLoansService _adminAvailableLoansService;
        private readonly ILogger<AdminAvailableLoansController> _logger;

        public AdminAvailableLoansController(IAdminAvailableLoansService adminAvailableLoansService, ILogger<AdminAvailableLoansController> logger)
        {
            _adminAvailableLoansService = adminAvailableLoansService;
            _logger = logger;
        }



        [HttpDelete("DeleteLoan")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            try
            {
                var deletedLoan = await _adminAvailableLoansService.DeleteLoan(loanId);
                if (deletedLoan != null)
                {
                    return Ok(deletedLoan);
                }
                else
                {
                    return NotFound($"Loan with ID {loanId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting loan.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [Route("AddLoan")]
        [HttpPost]
        public async Task<ActionResult<AvailableLoans>> AddLoan(AvailableLoans loan)
        {
            try
            {
                return await _adminAvailableLoansService.AddLoan(loan);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding loan.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Route("UpdateAvailableLoan")]
        [HttpPut]
        public async Task<ActionResult<AvailableLoans>> UpdateLoan(AvailableLoans loan)
        {
            try
            {
                var updatedLoan = await _adminAvailableLoansService.UpdateLoan(loan);
                return Ok(updatedLoan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating available loan.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



    }
}
