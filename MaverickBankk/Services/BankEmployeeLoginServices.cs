﻿using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Mappers;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;
using System.Security.Cryptography;
using System.Text;

namespace MaverickBankk.Services
{
    public class BankEmployeeLoginServices : IBankEmployeeLoginService
    {
        private readonly IRepository<int, BankEmployees> _employeeRepository;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ITokenService _tokenService;

        public BankEmployeeLoginServices(IRepository<int, BankEmployees> employeeRepository,
            IRepository<string, Validation> validationRepository,
            ITokenService tokenService)
        {
            _employeeRepository = employeeRepository;
            _validationRepository = validationRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginUserDTO> Login(LoginUserDTO employee)
        {
            var myUser = await _validationRepository.Get(employee.Email);
            if (myUser == null || myUser.Status != "Active")
            {
                throw new DeactivatedUserException();
            }
            var userPassword = GetPasswordEncrypted(employee.Password, myUser.Key);
            var checkPasswordMatch = ComparePasswords(myUser.Password, userPassword);
            if (checkPasswordMatch)
            {
                employee.Password = "";
                //
                if (myUser.UserType == null)
                {
                    throw new InvalidUserException("UserType is null");
                }
                employee.UserType = myUser.UserType;
                employee.UserType = myUser.UserType;
                employee.Token = await _tokenService.GenerateToken(employee);
                return employee;
            }
            throw new InvalidUserException();
        }

        private bool ComparePasswords(byte[] password, byte[] userPassword)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] != userPassword[i])
                    return false;
            }
            return true;
        }



        private byte[] GetPasswordEncrypted(string password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            var userpassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userpassword;
        }

        public async Task<LoginUserDTO> Register(RegisterBankEmployeeDTO employee)
        {
            Validation myuser = new RegisterToBankEmployeeUser(employee).GetValidation();
            myuser.Status = "Active";
            myuser = await _validationRepository.Add(myuser);
            BankEmployees bankEmployees = new RegiterToBankEmployee(employee).GetBankEmployees();
            bankEmployees = await _employeeRepository.Add(bankEmployees);
            if(myuser.Email==null)
            {
                throw new InvalidUserException("Invalid User");
            }
            LoginUserDTO result = new LoginUserDTO
            {

                Email = myuser.Email,
                UserType = myuser.UserType,
            };
            return result;
        }
    }
}
