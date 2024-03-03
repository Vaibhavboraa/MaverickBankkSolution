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
        public async Task GetCustomers_ReturnsSingleCustomer()
        {
            // Arrange
            var customerId = 1;
            var expectedCustomer = new Customers { CustomerID = customerId, Name = "V" };

            var customerRepositoryMock = new Mock<IRepository<int, Customers>>();
            customerRepositoryMock.Setup(repo => repo.Get(customerId)).ReturnsAsync(expectedCustomer);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                Mock.Of<IRepository<long, Accounts>>(),
                customerRepositoryMock.Object,
                loggerMock.Object);

            // Act
            var result = await service.GetCustomers(customerId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCustomer));
        }

       



        [Test]
        public async Task GetCustomersList_ReturnsListOfCustomers()
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

            var service = new BankEmployeeAccountService(
                Mock.Of<IRepository<long, Accounts>>(),
                customerRepositoryMock.Object,
                loggerMock.Object);

            // Act
            var result = await service.GetCustomersListasync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedCustomers));
        }

        [Test]
        public async Task ApproveAccountCreation_ReturnsTrue()
        {
            // Arrange
            var accountNumber = 123456789;
            var pendingAccount = new Accounts { AccountNumber = accountNumber, Status = "Pending" };

            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(pendingAccount);
            accountsRepositoryMock.Setup(repo => repo.Update(pendingAccount)).ReturnsAsync(pendingAccount);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                accountsRepositoryMock.Object,
                Mock.Of<IRepository<int, Customers>>(),
                loggerMock.Object);

            // Act
            var result = await service.ApproveAccountCreation(accountNumber);

            // Assert
            Assert.IsTrue(result);
        }
     






        [Test]
        public async Task ApproveAccountDeletion_ReturnsTrue()
        {
            // Arrange
            var accountNumber = 123456789;
            var pendingDeletionAccount = new Accounts { AccountNumber = accountNumber, Status = "PendingDeletion" };

            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(pendingDeletionAccount);
            accountsRepositoryMock.Setup(repo => repo.Delete(accountNumber)).ReturnsAsync(pendingDeletionAccount);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                accountsRepositoryMock.Object,
                Mock.Of<IRepository<int, Customers>>(),
                loggerMock.Object);

            // Act
            var result = await service.ApproveAccountDeletion(accountNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetPendingAccounts_ReturnsListOfAccounts()
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

            var service = new BankEmployeeAccountService(
                accountsRepositoryMock.Object,
                Mock.Of<IRepository<int, Customers>>(),
                loggerMock.Object);

            // Act
            var result = await service.GetPendingAccounts();

            // Assert
            Assert.That(result, Is.EqualTo(pendingAccounts));
        }





        [Test]
        public async Task GetPendingDeletionAccounts_ReturnsListOfAccounts()
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

            var service = new BankEmployeeAccountService(
                accountsRepositoryMock.Object,
                Mock.Of<IRepository<int, Customers>>(),
                loggerMock.Object);

            // Act
            var result = await service.GetPendingDeletionAccounts();

            // Assert
            Assert.That(result, Is.EqualTo(pendingDeletionAccounts));
        }
        [Test]
        public void GetCustomers_InvalidCustomerId_ThrowsNotFoundException()
        {
            // Arrange
            var invalidCustomerId = 999; // An invalid customer ID

            var customerRepositoryMock = new Mock<IRepository<int, Customers>>();
            customerRepositoryMock.Setup(repo => repo.Get(invalidCustomerId)).ReturnsAsync((Customers?)null);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                Mock.Of<IRepository<long, Accounts>>(),
                customerRepositoryMock.Object,
                loggerMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await service.GetCustomers(invalidCustomerId));
        }

        [Test]
        public async Task GetCustomersList_NoCustomers_ReturnsEmptyList()
        {
            // Arrange
            var emptyCustomersList = new List<Customers>();

            var customerRepositoryMock = new Mock<IRepository<int, Customers>>();
            customerRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(emptyCustomersList);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                Mock.Of<IRepository<long, Accounts>>(),
                customerRepositoryMock.Object,
                loggerMock.Object);

            // Act
            var result = await service.GetCustomersListasync();

            // Assert
            Assert.IsEmpty(result);
        }


     

        [Test]
        public async Task GetPendingAccounts_NoPendingAccounts_ReturnsEmptyList()
        {
            // Arrange
            var emptyPendingAccountsList = new List<Accounts>();

            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(emptyPendingAccountsList);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                accountsRepositoryMock.Object,
                Mock.Of<IRepository<int, Customers>>(),
                loggerMock.Object);

            // Act
            var result = await service.GetPendingAccounts();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetPendingDeletionAccounts_NoPendingDeletionAccounts_ReturnsEmptyList()
        {
            // Arrange
            var emptyPendingDeletionAccountsList = new List<Accounts>();

            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(emptyPendingDeletionAccountsList);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                accountsRepositoryMock.Object,
                Mock.Of<IRepository<int, Customers>>(),
                loggerMock.Object);

            // Act
            var result = await service.GetPendingDeletionAccounts();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetCustomers_InvalidCustomerId_ThrowsNoCustomersFoundException()
        {
            // Arrange
            var invalidCustomerId = 999; // An invalid customer ID

            var customerRepositoryMock = new Mock<IRepository<int, Customers>>();
            customerRepositoryMock.Setup(repo => repo.Get(invalidCustomerId)).ReturnsAsync((Customers?)null);

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                Mock.Of<IRepository<long, Accounts>>(),
                customerRepositoryMock.Object,
                loggerMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await service.GetCustomers(invalidCustomerId));
        }

        [Test]
        public void GetPendingDeletionAccounts_ErrorFetchingAccounts_ThrowsAccountFetchException()
        {
            // Arrange
            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Simulated error"));

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                accountsRepositoryMock.Object,
                Mock.Of<IRepository<int, Customers>>(),
                loggerMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<AccountFetchException>(async () => await service.GetPendingDeletionAccounts());
        }

        [Test]
        public void GetPendingAccounts_ErrorFetchingAccounts_ThrowsAccountFetchException()
        {
            // Arrange
            var accountsRepositoryMock = new Mock<IRepository<long, Accounts>>();
            accountsRepositoryMock.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Simulated error"));

            var loggerMock = new Mock<ILogger<BankEmployeeAccountService>>();

            var service = new BankEmployeeAccountService(
                accountsRepositoryMock.Object,
                Mock.Of<IRepository<int, Customers>>(),
                loggerMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<AccountFetchException>(async () => await service.GetPendingAccounts());
        }
      











    }
}
