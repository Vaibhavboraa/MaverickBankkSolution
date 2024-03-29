﻿using MaverickBankk.Interfaces;
using MaverickBankk.Mappers;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;
using MaverickBankk.Exceptions;

namespace MaverickBankk.Services
{
    public class AdministratorBankEmployeeManagementService : IAdministratorBankEmployeeManagementService
    {
        private readonly IRepository<int, BankEmployees> _bankEmployeesRepository;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ILogger<AdministratorBankEmployeeManagementService> _logger;

        public AdministratorBankEmployeeManagementService(IRepository<int, BankEmployees> bankEmployeesRepository,
            IRepository<string, Validation> validationRepository,
            ILogger<AdministratorBankEmployeeManagementService> logger)
        {
            _bankEmployeesRepository = bankEmployeesRepository;
            _validationRepository = validationRepository;
            _logger = logger;
        }

        public async Task<BankEmployees?> ActivateEmployee(int employeeId)
        {
            var employee = await _bankEmployeesRepository.Get(employeeId);
            if (employee == null)
            {
                throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
            }
            if(employee.Email==null)
            {
                throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
            }
            var validation = await _validationRepository.Get(employee.Email);
            if (validation == null)
            {
                throw new ValidationNotFoundException($"Validation for employee with ID {employeeId} not found.");
            }

            validation.Status = "Active";
            await _validationRepository.Update(validation);
            return employee;
        }

        //public async Task<BankEmployees?> DeactivateEmployee(int employeeId)
        //{
        //    var employee = await _bankEmployeesRepository.Get(employeeId);
        //    if (employee == null)
        //    {
        //        throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
        //    }

        //    var validation = await _validationRepository.Get(employee.Email);
        //    if (validation == null)
        //    {
        //        throw new ValidationNotFoundException($"Validation for employee with ID {employeeId} not found.");
        //    }

        //    validation.Status = "deactivated";
        //    await _validationRepository.Update(validation);

        //    _logger.LogInformation($"Employee with ID {employeeId} deactivated.");
        //    return employee;
        //}

        public async Task<BankEmployees?> DeactivateEmployee(int employeeId)
        {
            var employee = await _bankEmployeesRepository.Get(employeeId);
            if (employee == null)
            {
                throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
            }
            if (employee.Email == null)
            {
                throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
            }

            var validation = await _validationRepository.Get(employee.Email);
            if (validation == null)
            {
                throw new ValidationNotFoundException($"Validation for employee with ID {employeeId} not found.");
            }

            if (employee.Email != null)
            {
                validation.Status = "deactivated";
                await _validationRepository.Update(validation);

                _logger.LogInformation($"Employee with ID {employeeId} deactivated.");
                return employee;
            }

            throw new InvalidOperationException($"Email is null for employee with ID {employeeId}.");
        }






        public async Task<List<BankEmployees>?> GetAllEmployees()
        {
            var bankEmployees = await _bankEmployeesRepository.GetAll();

            if (bankEmployees == null || bankEmployees.Count == 0)
            {
                throw new EmployeeNotFoundException("No employees found.");
            }



            return bankEmployees;
        }


        public async Task<BankEmployees?> GetEmployee(int employeeId)
        {
            var employee = await _bankEmployeesRepository.Get(employeeId);
            if (employee == null)
            {
                throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
            }
            return employee;
        }

        public async Task<BankEmployees?> CreateBankEmployee(RegisterBankEmployeeDTO employeeDTO)
        {
            try
            {

                var newBankEmployee = new RegiterToBankEmployee(employeeDTO).GetBankEmployees();


                var newValidation = new RegisterToBankEmployeeUser(employeeDTO).GetValidation();


                var addedValidation = await _validationRepository.Add(newValidation);


                newBankEmployee.Email = addedValidation.Email;


                var addedBankEmployee = await _bankEmployeesRepository.Add(newBankEmployee);

                return addedBankEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating bank employee: {ex.Message}");
                throw new BankEmployeeCreationException($"Error creating bank employee: {ex.Message}");
            }
        }

        public async Task<BankEmployees?> UpdateEmployee(BankEmployees employee)
        {
            try
            {

                var updatedEmployee = await _bankEmployeesRepository.Update(employee);
                return updatedEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating employee: {ex.Message}");
                throw new EmployeeUpdateException($"Error updating employee: {ex.Message}");
            }
        }
    }
}
