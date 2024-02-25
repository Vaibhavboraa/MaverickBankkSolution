﻿using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaverickBankk.Services;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;
using Microsoft.Extensions.Logging;
using MaverickBankk.Exceptions;

namespace MaverickBankk.Tests
{
    [TestFixture]
    public class AdministratorCustomerManagementServiceTests
    {
        private Mock<IRepository<int, Customers>> _customersRepositoryMock;
        private Mock<IRepository<string, Validation>> _validationRepositoryMock;
        private Mock<ILogger<AdministratorCustomerManagementService>> _loggerMock;
        private AdministratorCustomerManagementService _adminCustomerService;

        [SetUp]
        public void Setup()
        {
            _customersRepositoryMock = new Mock<IRepository<int, Customers>>();
            _validationRepositoryMock = new Mock<IRepository<string, Validation>>();
            _loggerMock = new Mock<ILogger<AdministratorCustomerManagementService>>();
            _adminCustomerService = new AdministratorCustomerManagementService(
                _customersRepositoryMock.Object,
                _validationRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Test]
        public async Task ActivateUser()
        {
            // Arrange
            int customerId = 1;
            var customer = new Customers { CustomerID = customerId, Name = "V" };
            var validation = new Validation { Email = "v@example.com", Status = "Inactive" };

            _customersRepositoryMock.Setup(repo => repo.Get(customerId))
                .ReturnsAsync(customer);
            _validationRepositoryMock.Setup(repo => repo.Get(customer.Email))
                .ReturnsAsync(validation);
            _validationRepositoryMock.Setup(repo => repo.Update(validation))
                .ReturnsAsync(validation);

            // Act
            var result = await _adminCustomerService.ActivateUser(customerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customerId, result.CustomerID);
            Assert.AreEqual("Active", validation.Status);
        }

        [Test]
        public async Task DeactivateUser()
        {
            // Arrange
            int customerId = 1;
            var customer = new Customers { CustomerID = customerId, Name = "v" };
            var validation = new Validation { Email = "v@example.com", Status = "Active" };

            _customersRepositoryMock.Setup(repo => repo.Get(customerId))
                .ReturnsAsync(customer);
            _validationRepositoryMock.Setup(repo => repo.Get(customer.Email))
                .ReturnsAsync(validation);
            _validationRepositoryMock.Setup(repo => repo.Update(validation))
                .ReturnsAsync(validation);

            // Act
            var result = await _adminCustomerService.DeactivateUser(customerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customerId, result.CustomerID);
            Assert.AreEqual("deactivated", validation.Status);
        }

        [Test]
        public void DeactivateUser_ThrowsNoCustomersFoundException()
        {
            // Arrange
            int customerId = 1;
            _customersRepositoryMock.Setup(repo => repo.Get(customerId))
                .ReturnsAsync((Customers)null);

            // Act & Assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await _adminCustomerService.DeactivateUser(customerId));
        }

        [Test]
        public void DeactivateUser_ValidationNotFound_ThrowsValidationNotFoundException()
        {
            // Arrange
            int customerId = 1;
            var customer = new Customers { CustomerID = customerId, Name = "John Doe" };

            _customersRepositoryMock.Setup(repo => repo.Get(customerId))
                .ReturnsAsync(customer);
            _validationRepositoryMock.Setup(repo => repo.Get(customer.Email))
                .ReturnsAsync((Validation)null);

            // Act & Assert
            Assert.ThrowsAsync<ValidationNotFoundException>(async () => await _adminCustomerService.DeactivateUser(customerId));
        }
        [Test]
        public async Task GetAllUsers()
        {
            // Arrange
            var validations = new List<Validation>
            {
                new Validation { Email = "v@gmail.com", UserType = "Customer" },
                new Validation { Email = "b@example.com", UserType = "Customer" }
            };
            var customers = new List<Customers>
            {
                new Customers { CustomerID = 1, Name = "Customer 1", Email = "v@gmail.com" },
                new Customers { CustomerID = 2, Name = "Customer 2", Email = "b@example.com" }
            };

            _validationRepositoryMock.Setup(repo => repo.GetAll())
                .ReturnsAsync(validations);
            _customersRepositoryMock.Setup(repo => repo.GetAll())
                .ReturnsAsync(customers);

            // Act
            var result = await _adminCustomerService.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
          
        }

        [Test]
        public async Task GetUser()
        {
            // Arrange
            var customerId = 1;
            var expectedCustomer = new Customers { CustomerID = customerId, Name = "Test Customer", Email = "v@e.com" };
            _customersRepositoryMock.Setup(repo => repo.Get(customerId))
                .ReturnsAsync(expectedCustomer);

            // Act
            var result = await _adminCustomerService.GetUser(customerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCustomer, result);
        }

        [Test]
        public async Task GetUser_ThrowsException()
        {
            // Arrange
            var invalidCustomerId = 999;
            _customersRepositoryMock.Setup(repo => repo.Get(invalidCustomerId))
                .ReturnsAsync((Customers)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<NoCustomersFoundException>(() => _adminCustomerService.GetUser(invalidCustomerId));
            Assert.AreEqual($"User with ID {invalidCustomerId} not found.", exception.Message);
        }

        [Test]
        public async Task UpdateCustomerContact()
        {
            // Arrange
            var customerId = 1;
            var contactDTO = new AdminUpdateCustomerContactDTO { PhoneNumber = 1234567890 };
            var originalCustomer = new Customers { CustomerID = customerId, Name = " Customer", Email = "v@g.com" };
            var updatedCustomer = new Customers { CustomerID = customerId, Name = " Customer", Email = "v@g.com", PhoneNumber = 1234567890 };

            _customersRepositoryMock.Setup(repo => repo.Get(customerId))
                .ReturnsAsync(originalCustomer);

            // Act
            var result = await _adminCustomerService.UpdateCustomerContact(customerId, contactDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedCustomer, result);
        }

        [Test]
        public async Task UpdateCustomerDetails()
        {
            // Arrange
            var customerId = 1;
            var mockCustomersRepository = new Mock<IRepository<int, Customers>>();
            var mockValidationRepository = new Mock<IRepository<string, Validation>>();
            var mockLogger = new Mock<ILogger<AdministratorCustomerManagementService>>();

            var service = new AdministratorCustomerManagementService(
                mockCustomersRepository.Object,
                mockValidationRepository.Object,
                mockLogger.Object
            );

            var detailsDTO = new AdminUpdateCustomerDetailsDTO
            {
                DOB = new DateTime(2002, 1, 1),
                Age = 30,
                PANNumber = "ABCDE1234F",
                Gender = "Male"
            };

            var existingCustomer = new Customers
            {
                CustomerID = customerId,
                Name = "v",
                Email = "v@e.com"
               
               
            };

            mockCustomersRepository.Setup(repo => repo.Get(customerId))
                                   .ReturnsAsync(existingCustomer);

            // Act
            var updatedCustomer = await service.UpdateCustomerDetails(customerId, detailsDTO);

            // Assert
            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual(detailsDTO.DOB, updatedCustomer.DOB);
            Assert.AreEqual(detailsDTO.Age, updatedCustomer.Age);
            Assert.AreEqual(detailsDTO.PANNumber, updatedCustomer.PANNumber);
            Assert.AreEqual(detailsDTO.Gender, updatedCustomer.Gender);
        }
        [Test]
        public async Task UpdateCustomerName()
        {
            // Arrange
            var customerId = 1;
            var mockCustomersRepository = new Mock<IRepository<int, Customers>>();
            var mockValidationRepository = new Mock<IRepository<string, Validation>>();
            var mockLogger = new Mock<ILogger<AdministratorCustomerManagementService>>();

            var service = new AdministratorCustomerManagementService(
                mockCustomersRepository.Object,
                mockValidationRepository.Object,
                mockLogger.Object
            );

            var nameDTO = new AdminUpdateCustomerNameDTO
            {
                Name = "T"
            };

            var existingCustomer = new Customers
            {
                CustomerID = customerId,
                Name = "V",
                Email = "v@e.com"
                
            };

            mockCustomersRepository.Setup(repo => repo.Get(customerId))
                                   .ReturnsAsync(existingCustomer);

            // Act
            var updatedCustomer = await service.UpdateCustomerName(customerId, nameDTO);

            // Assert
            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual(nameDTO.Name, updatedCustomer.Name);
           
            Assert.AreEqual(existingCustomer.Email, updatedCustomer.Email);
        }

        [Test]
        public async Task CreateCustomer()
        {
            // Arrange
            var mockCustomersRepository = new Mock<IRepository<int, Customers>>();
            var mockValidationRepository = new Mock<IRepository<string, Validation>>();
            var mockLogger = new Mock<ILogger<AdministratorCustomerManagementService>>();

            var service = new AdministratorCustomerManagementService(
                mockCustomersRepository.Object,
                mockValidationRepository.Object,
                mockLogger.Object
            );

            var customerDTO = new RegisterCustomerDTO
            {
              
                Name = "V",
                Email = "V@b.com",
               
            };

            // Mock repository behavior
            mockValidationRepository.Setup(repo => repo.Add(It.IsAny<Validation>()))
           .ReturnsAsync(new Validation { Email = customerDTO.Email });

            mockCustomersRepository.Setup(repo => repo.Add(It.IsAny<Customers>()))
            .ReturnsAsync(new Customers { CustomerID = 1, Name = customerDTO.Name, Email = customerDTO.Email });

            // Act
            var createdCustomer = await service.CreateCustomer(customerDTO);

            // Assert
            Assert.IsNotNull(createdCustomer);
            Assert.AreEqual(customerDTO.Name, createdCustomer.Name);
            Assert.AreEqual(customerDTO.Email, createdCustomer.Email);
           
        }
     

    }
}
