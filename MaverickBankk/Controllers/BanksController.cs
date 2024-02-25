using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace MaverickBankk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class BanksController : ControllerBase
    {
        private readonly IBanksAdminService _banksService;
        private readonly ILogger<BanksController> _loggerBanksController;

        public BanksController(IBanksAdminService banksService, ILogger<BanksController> loggerBanksController)
        {
            _banksService = banksService;
            _loggerBanksController = loggerBanksController;
        }
        [EnableCors("ReactPolicy")]
        [Route("GetAllBanks")]
        [HttpGet]
        public async Task<ActionResult<List<Banks>>> GetAllBanks()
        {
            try
            {
                return await _banksService.GetAllBanks();
            }
            catch (NoBanksFoundException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetBank")]
        [HttpGet]
        public async Task<ActionResult<Banks>> GetBank(int key)
        {
            try
            {
                return await _banksService.GetBank(key);
            }
            catch (NoBanksFoundException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("AddBank")]
        [HttpPost]
        public async Task<ActionResult<Banks>> AddBank(Banks item)
        {
            return await _banksService.AddBank(item);
        }

        [Route("UpdateBankName")]
        [HttpPut]
        public async Task<ActionResult<Banks>> UpdateBankName(UpdateBankNameDTO updateBankNameDTO)
        {
            try
            {
                return await _banksService.UpdateBankName(updateBankNameDTO);
            }
            catch (NoBanksFoundException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("DeleteBank")]
        [HttpDelete]
        public async Task<ActionResult<Banks>> DeleteBank(int key)
        {
            try
            {
                return await _banksService.DeleteBank(key);
            }
            catch (NoBanksFoundException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
