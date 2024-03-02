using System;
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
#nullable disable
namespace Testing
{
    [TestFixture]
    public class CustomerLoanServiceTests
    {
        private Mock<IRepository<int, Loans>> _mockLoansRepository;
        private Mock<IRepository<int, Customers>> _mockCustomerRepository;
        private Mock<ILogger<CustomerLoanService>> _mockLogger;
        private CustomerLoanService _customerLoanService;

        [SetUp]
        public void Setup()
        {
            _mockLoansRepository = new Mock<IRepository<int, Loans>>();
            _mockCustomerRepository = new Mock<IRepository<int, Customers>>();
            _mockLogger = new Mock<ILogger<CustomerLoanService>>();
            _customerLoanService = new CustomerLoanService(
                _mockLoansRepository.Object,
                _mockCustomerRepository.Object,
                _mockLogger.Object);
        }

        [Test]
        public async Task ApplyForLoan_ValidLoanApplication_Success()
        {
            // Arrange
            var loanApplication = new LoanApplicationDTO
            {
                LoanAmount = 10000,
                LoanType = "Personal",
                Interest = 5,
                Tenure = 12,
                Purpose = "Home Renovation",
                CustomerID = 1
            };

            _mockLoansRepository.Setup(repo => repo.Add(It.IsAny<Loans>())).ReturnsAsync(new Loans());

            // Act
            await _customerLoanService.ApplyForLoan(loanApplication);

            // Assert
            _mockLoansRepository.Verify(repo => repo.Add(It.IsAny<Loans>()), Times.Once);
        }

        [Test]
        public async Task ViewAvailedLoans_ValidCustomerId_ReturnsListOfLoans()
        {
            // Arrange
            int customerId = 1;
            var loansList = new List<Loans>
            {
                new Loans { LoanID = 1, CustomerID = customerId },
                new Loans { LoanID = 2, CustomerID = customerId }
            };

            _mockLoansRepository.Setup(repo => repo.GetAll()).ReturnsAsync(loansList);
            _mockCustomerRepository.Setup(repo => repo.Get(customerId)).ReturnsAsync(new Customers { CustomerID = customerId });

            // Act
            var availedLoans = await _customerLoanService.ViewAvailedLoans(customerId);

            // Assert
            Assert.That(availedLoans.Count, Is.EqualTo(2));
          //Assert.IsTrue(availedLoans.All(loan => loan.CustomerID == customerId));
        }



        [Test]
        public void ViewAvailedLoans_InvalidCustomerId_ThrowsNoCustomersFoundException()
        {
            // Arrange
            int invalidCustomerId = -1; // Assuming -1 is an invalid ID for demonstration purposes
            _mockCustomerRepository.Setup(repo => repo.Get(invalidCustomerId))
                .ReturnsAsync((Customers)null); // Simulating no customer found

            // Act & Assert
            var ex = Assert.ThrowsAsync<NoCustomersFoundException>(async () => await _customerLoanService.ViewAvailedLoans(invalidCustomerId));
            Assert.That(ex.Message, Is.EqualTo($"No customer found with ID {invalidCustomerId}"));
        }
    }
}
