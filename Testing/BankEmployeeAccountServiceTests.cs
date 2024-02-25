using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;

namespace MaverickBankk.Services.Tests
{
    [TestFixture]
    public class BankEmployeeAccountServiceTests
    {
        [Test]
        public async Task GetCustomers()
        {
            // Arrange
            var customerId = 1;
            var expectedCustomer = new Customers { CustomerID = customerId, Name = "V" };

            var customerRepositoryMock = new Mock<IRepository<int, Customers>>();
            customerRepositoryMock.Setup(repo => repo.Get(customerId)).ReturnsAsync(expectedCustomer);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(null, customerRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.GetCustomers(customerId);

            // Assert
            Assert.AreEqual(expectedCustomer, result);
        }

        [Test]
        public async Task GetCustomersListasync()
        {
            // Arrange
            var expectedCustomers = new List<Customers>
            {
                new Customers { CustomerID = 1, Name = "V" },
                new Customers { CustomerID = 2, Name = "B" }
            };

            var customerRepositoryMock = new Mock<IRepository<int, Customers>>();
            customerRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedCustomers);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(null, customerRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.GetCustomersListasync();

            // Assert
            Assert.AreEqual(expectedCustomers, result);
        }

        [Test]
        public async Task ApproveAccountCreation()
        {
            // Arrange
            var accountNumber = 123456789;
            var pendingAccount = new Accounts { AccountNumber = accountNumber, Status = "Pending" };

            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(pendingAccount);
            accountsRepositoryMock.Setup(repo => repo.Update(pendingAccount)).ReturnsAsync(pendingAccount);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(accountsRepositoryMock.Object, null, loggerMock.Object);

            // Act
            var result = await service.ApproveAccountCreation(accountNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ApproveAccountDeletion()
        {
            // Arrange
            var accountNumber = 123456789;
            var pendingDeletionAccount = new Accounts { AccountNumber = accountNumber, Status = "PendingDeletion" };

            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(pendingDeletionAccount);
            accountsRepositoryMock.Setup(repo => repo.Delete(accountNumber)).ReturnsAsync(pendingDeletionAccount);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(accountsRepositoryMock.Object, null, loggerMock.Object);

            // Act
            var result = await service.ApproveAccountDeletion(accountNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetPendingAccounts()
        {
            // Arrange
            var pendingAccounts = new List<Accounts>
            {
                new Accounts { AccountNumber = 1, Status = "Pending" },
                new Accounts { AccountNumber = 2, Status = "Pending" }
            };

            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(pendingAccounts);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(accountsRepositoryMock.Object, null, loggerMock.Object);

            // Act
            var result = await service.GetPendingAccounts();

            // Assert
            Assert.AreEqual(pendingAccounts, result);
        }

        [Test]
        public async Task GetPendingDeletionAccounts()
        {
            // Arrange
            var pendingDeletionAccounts = new List<Accounts>
            {
                new Accounts { AccountNumber = 1, Status = "PendingDeletion" },
                new Accounts { AccountNumber = 2, Status = "PendingDeletion" }
            };

            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(pendingDeletionAccounts);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(accountsRepositoryMock.Object, null, loggerMock.Object);

            // Act
            var result = await service.GetPendingDeletionAccounts();

            // Assert
            Assert.AreEqual(pendingDeletionAccounts, result);
        }

       
    }
}
