﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class CustomerAccountServiceTest
    {
        private Mock<IRepository<long, Accounts>> _mockAccountsRepository;
        private Mock<IRepository<int, Transactions>> _mockTransactionsRepository;
        private Mock<ILogger<CustomerAccountService>> _mockLogger;
        private CustomerAccountService _customerAccountService;

        [SetUp]
        public void Setup()
        {
            _mockAccountsRepository = new Mock<IRepository<long, Accounts>>();
            _mockTransactionsRepository = new Mock<IRepository<int, Transactions>>();
            _mockLogger = new Mock<ILogger<CustomerAccountService>>();
            _customerAccountService = new CustomerAccountService(
                _mockAccountsRepository.Object,
                _mockTransactionsRepository.Object,
                _mockLogger.Object);
        }

        [Test]
        public async Task CloseAccount_ValidAccountNumber_ReturnsSuccessMessage()
        {
            // Arrange
            long accountNumber = 123456789;
            var account = new Accounts { AccountNumber = accountNumber, Status = "Active" };
            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(account);

            // Act
            var result = await _customerAccountService.CloseAccount(accountNumber);

            // Assert
            Assert.That(result, Is.EqualTo($"Account with number {accountNumber} is scheduled for deletion."));
        }


        

        [Test]
        public async Task GetAccountDetails_ValidAccountNumber_ReturnsAccountDetails()
        {
            // Arrange
            long accountNumber = 123456789;
            var account = new Accounts { AccountNumber = accountNumber, Balance = 1000, Status = "Active" };
            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(account);

            // Act
            var result = await _customerAccountService.GetAccountDetails(accountNumber);

            // Assert
            Assert.IsNotNull(result);
         
        }

      

     

        [Test]
        public async Task GetAllAccountsByCustomerId_ValidCustomerId_ReturnsCustomerAccounts()
        {
            // Arrange
            int customerId = 1;
            var accounts = new List<Accounts>
            {
                new Accounts { AccountNumber = 123, CustomerID = customerId },
                new Accounts { AccountNumber = 456, CustomerID = customerId },
            };
            _mockAccountsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(accounts);

            // Act
            var result = await _customerAccountService.GetAllAccountsByCustomerId(customerId);

            // Assert
            Assert.IsNotNull(result);
          
        }

     



        [Test]
        public async Task OpenNewAccount_ValidAccountOpeningDTO_ReturnsAddedAccount()
        {
            // Arrange
            var accountOpeningDTO = new AccountOpeningDTO { AccountType = "Savings", IFSC = "ABC123", CustomerID = 1 };
            var newAccount = new Accounts { AccountNumber = 123, AccountType = "Savings", Status = "Pending", CustomerID = 1 };
            _mockAccountsRepository.Setup(repo => repo.Add(It.IsAny<Accounts>())).ReturnsAsync(newAccount);

            // Act
            var result = await _customerAccountService.OpenNewAccount(accountOpeningDTO);

            // Assert
            Assert.IsNotNull(result);
         
        }
        [Test]
        public void CloseAccount_AccountNotFound_ThrowsNoAccountsFoundException()
        {
            // Arrange
            long accountNumber = 123456789;
            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync((Accounts?)null);

            // Act & Assert
            Assert.ThrowsAsync<NoAccountsFoundException>(() => _customerAccountService.CloseAccount(accountNumber));
        }
        [Test]
        public void GetAccountDetails_AccountNotFound_ThrowsNoAccountsFoundException()
        {
            // Arrange
            long accountNumber = 123456789;
            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync((Accounts?)null);

            // Act & Assert
            Assert.ThrowsAsync<NoAccountsFoundException>(() => _customerAccountService.GetAccountDetails(accountNumber));
        }
        [Test]
        public void GetAllAccountsByCustomerId_NoAccountsFound_ThrowsNoAccountsFoundException()
        {
            // Arrange
            int customerId = 1;
            _mockAccountsRepository.Setup(repo => repo.GetAll()).ReturnsAsync((List<Accounts>?)null);

            // Act & Assert
            Assert.ThrowsAsync<NoAccountsFoundException>(() => _customerAccountService.GetAllAccountsByCustomerId(customerId));
        }

     
      





    }
}





//using MaverickBankk.Context;
//using MaverickBankk.Exceptions;
//using MaverickBankk.Interfaces;
//using MaverickBankk.Models;
//using MaverickBankk.Models.DTOs;
//using MaverickBankk.Repositories;
//using MaverickBankk.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Testing
//{
//    public class CustomerAccountServiceTest
//    {
//        private CustomerAccountService _accountService;
//        private Mock<IRepository<long, Accounts>> _mockAccountsRepository;
//        private Mock<IRepository<int, Transactions>> _mockTransactionsRepository;
//        private Mock<ILogger<CustomerAccountService>> _mockLogger;

//        [SetUp]
//        public void Setup()
//        {
//            _mockAccountsRepository = new Mock<IRepository<long, Accounts>>();
//            _mockTransactionsRepository = new Mock<IRepository<int, Transactions>>();
//            _mockLogger = new Mock<ILogger<CustomerAccountService>>();

//            _accountService = new CustomerAccountService(
//                _mockAccountsRepository.Object,
//                _mockTransactionsRepository.Object,
//                _mockLogger.Object);
//        }

//        [Test]
//        public async Task CloseAccount_AccountExists_Success()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            var account = new Accounts
//            {
//                AccountNumber = accountNumber,
//                Status = "Active"
//            };

//            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(account);

//            // Act
//            var result = await _accountService.CloseAccount(accountNumber);

//            // Assert
//            Assert.AreEqual($"Account with number {accountNumber} is scheduled for deletion.", result);
//            Assert.AreEqual("PendingDeletion", account.Status);
//        }

//        [Test]
//        public void CloseAccount_AccountDoesNotExist_ExceptionThrown()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync((Accounts)null);

//            // Act & Assert
//            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await _accountService.CloseAccount(accountNumber));
//        }

//        [Test]
//        public async Task GetAccountDetails_AccountExists_Success()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            var account = new Accounts
//            {
//                AccountNumber = accountNumber
//            };

//            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(account);

//            // Act
//            var result = await _accountService.GetAccountDetails(accountNumber);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(accountNumber, result.AccountNumber);
//        }

//        [Test]
//        public void GetAccountDetails_AccountDoesNotExist_ExceptionThrown()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync((Accounts)null);

//            // Act & Assert
//            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await _accountService.GetAccountDetails(accountNumber));
//        }

//        [Test]
//        public async Task OpenNewAccount()
//        {
//            // Arrange
//            var accountOpeningDTO = new AccountOpeningDTO
//            {
//                AccountType = "Savings",
//                IFSC = "ABCD123456",
//                CustomerID = 1
//            };

//            var newAccount = new Accounts
//            {
//                AccountNumber = 123456789,
//                Balance = 0,
//                AccountType = accountOpeningDTO.AccountType,
//                Status = "Pending",
//                IFSC = accountOpeningDTO.IFSC,
//                CustomerID = accountOpeningDTO.CustomerID
//            };

//            _mockAccountsRepository.Setup(repo => repo.Add(It.IsAny<Accounts>())).ReturnsAsync(newAccount);

//            // Act
//            var result = await _accountService.OpenNewAccount(accountOpeningDTO);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual("Pending", result.Status);
//            Assert.AreEqual(accountOpeningDTO.AccountType, result.AccountType);
//            Assert.AreEqual(accountOpeningDTO.IFSC, result.IFSC);
//            Assert.AreEqual(accountOpeningDTO.CustomerID, result.CustomerID);
//        }

//        [Test]
//        public async Task GetAllAccountsByCustomerId()
//        {
//            // Arrange
//            int customerId = 123;
//            var accounts = new List<Accounts>
//    {
//        new Accounts { AccountNumber = 1, CustomerID = customerId },
//        new Accounts { AccountNumber = 2, CustomerID = customerId },
//        new Accounts { AccountNumber = 3, CustomerID = customerId }
//    };

//            _mockAccountsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(accounts);

//            // Act
//            var result = await _accountService.GetAllAccountsByCustomerId(customerId);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(accounts.Count, result.Count);
//            foreach (var account in result)
//            {
//                Assert.AreEqual(customerId, account.CustomerID);
//            }
//        }





//    }
//}
