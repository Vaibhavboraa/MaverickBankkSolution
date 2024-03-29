﻿using MaverickBankk.Interfaces;
using MaverickBankk.Mappers;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MaverickBankk.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics.CodeAnalysis;


namespace MaverickBankk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("ReactPolicy")]
    [ExcludeFromCodeCoverage]
    public class AdminBankEmployeesController : ControllerBase
    {
        private readonly IAdministratorBankEmployeeManagementService _bankEmployeeService;
        private readonly ILogger<AdminBankEmployeesController> _logger;

        public AdminBankEmployeesController(IAdministratorBankEmployeeManagementService bankEmployeeService,
            ILogger<AdminBankEmployeesController> logger)
        {
            _bankEmployeeService = bankEmployeeService;
            _logger = logger;
        }
        [Authorize(Roles = "Admin")]
        [Route("GetAllEmployees")]
        [HttpGet]
        public async Task<ActionResult<List<BankEmployees>>> GetAllEmployees()
        {
            try
            {
                var employees = await _bankEmployeeService.GetAllEmployees();

                return Ok(employees);
            }
            catch (EmployeeNotFoundException ex)
            {
                _logger.LogError($"No employees found: {ex.Message}");
                return NotFound("No employees found.");
            }

        }

        [Authorize(Roles = "Admin")]
        [Route("get employee by id")]
        [HttpGet]
        public async Task<ActionResult<BankEmployees>> GetEmployee(int employeeId)
        {
            try
            {
                var employee = await _bankEmployeeService.GetEmployee(employeeId);

                return Ok(employee);
            }
            catch (EmployeeNotFoundException ex)
            {
                _logger.LogError($"Employee not found: {ex.Message}");
                return NotFound($"Employee with ID {employeeId} not found.");
            }

        }


        [Authorize(Roles = "Admin")]
        [Route("Activate Employee")]
        [HttpPost]
        public async Task<ActionResult<BankEmployees>> ActivateEmployee(int employeeId)
        {
            try
            {
                var activatedEmployee = await _bankEmployeeService.ActivateEmployee(employeeId);

                return Ok($"User with ID {employeeId} activated successfully");
            }
            catch (EmployeeNotFoundException ex)
            {
                _logger.LogError($"Employee not found: {ex.Message}");
                return NotFound($"Employee with ID {employeeId} not found.");
            }
            catch (ValidationNotFoundException ex)
            {
                _logger.LogError($"Validation not found: {ex.Message}");
                return NotFound($"Validation for employee with ID {employeeId} not found.");
            }

        }


        [Authorize(Roles = "Admin")]
        [Route("Deactivate Employee")]
        [HttpPost]
        public async Task<ActionResult<BankEmployees>> DeactivateEmployee(int employeeId)
        {
            try
            {
                var deactivatedEmployee = await _bankEmployeeService.DeactivateEmployee(employeeId);

                return Ok($"User with ID {employeeId} deactivated successfully");
            }
            catch (EmployeeNotFoundException ex)
            {
                _logger.LogError($"Employee not found: {ex.Message}");
                return NotFound($"Employee with ID {employeeId} not found.");
            }
            catch (ValidationNotFoundException ex)
            {
                _logger.LogError($"Validation not found: {ex.Message}");
                return NotFound($"Validation for employee with ID {employeeId} not found.");
            }

        }

        [Authorize(Roles = "Admin")]
        [Route("Register Bank Employee")]
        [HttpPost]
        public async Task<ActionResult<BankEmployees>> CreateBankEmployee(RegisterBankEmployeeDTO employeeDTO)
        {
            try
            {
                var addedBankEmployee = await _bankEmployeeService.CreateBankEmployee(employeeDTO);

                return Ok("Bank Employee Created Successfully");
            }
            catch (BankEmployeeCreationException ex)
            {
                _logger.LogError($"Error creating bank employee: {ex.Message}");
                return StatusCode(500, "Error creating bank employee");
            }
        }




        [Authorize(Roles = "Admin")]
        [Route("Update Bank Employee")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(int employeeId, UpdateBankEmployeeByAdminDTO updateDTO)
        {
            try
            {
                var employee = await _bankEmployeeService.GetEmployee(employeeId);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {employeeId} not found.");
                }

                // Update the employee
                UpdateBankEmployeeByAdminMapper.MapToBankEmployee(updateDTO, employee);


                var updatedEmployee = await _bankEmployeeService.UpdateEmployee(employee);

                if (updatedEmployee != null)
                {
                    return Ok(updatedEmployee);
                }
                else
                {
                    return StatusCode(500, "Failed to update employee.");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                _logger.LogError($"Employee not found: {ex.Message}");
                return NotFound($"Employee with ID {employeeId} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating employee: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the employee.");
            }
        }
    }
}
