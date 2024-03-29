﻿using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace MaverickBankk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("ReactPolicy")]
    [ExcludeFromCodeCoverage]
    public class AdminLoginController : ControllerBase
    {
        private readonly IAdminLoginService _adminLoginService;
        private readonly ILogger<AdminLoginController> _logger;

        public AdminLoginController(IAdminLoginService adminLoginService, ILogger<AdminLoginController> logger)
        {
            _adminLoginService = adminLoginService;
            _logger = logger;
        }
        [Route("AdminLogin")]
        [HttpPost]
        public async Task<ActionResult<Admin>> Login(LoginUserDTO loginUserDTO)
        {
            try
            {
                var user = await _adminLoginService.Login(loginUserDTO);
                return Ok(user);
            }
            catch (InvalidUserException)
            {
                return Unauthorized("Invalid email or password.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error logging in: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the login request.");
            }
        }
        [Route("AdminRegister")]
        [HttpPost]
        public async Task<ActionResult<Admin>> Register(RegisterAdminDTO registerAdminDTO)
        {
            try
            {
                var user = await _adminLoginService.Register(registerAdminDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering user: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the registration request.");
            }
        }
    }
}
