using System;
using System.Threading.Tasks;
using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MaverickBankk.Tests
{
    public class AdminLoginServiceTests
    {
        private Mock<IRepository<int, Admin>> _mockAdminRepository;
        private Mock<ILogger<CustomerService>> _mockLogger;
        private Mock<IRepository<string, Validation>> _mockValidationRepository;
        private Mock<ITokenService> _mockTokenService;
        private AdminLoginService _adminLoginService;

        [SetUp]
        public void Setup()
        {
            _mockAdminRepository = new Mock<IRepository<int, Admin>>();
            _mockLogger = new Mock<ILogger<CustomerService>>();
            _mockValidationRepository = new Mock<IRepository<string, Validation>>();
            _mockTokenService = new Mock<ITokenService>();
            _adminLoginService = new AdminLoginService(
                _mockAdminRepository.Object,
                _mockLogger.Object,
                _mockValidationRepository.Object,
                _mockTokenService.Object);
        }


        [Test]
        public void Login_InvalidUserException()
        {
            // Arrange
            var userEmail = "nonexistent@example.com";
            var loginUserDTO = new LoginUserDTO
            {
                Email = userEmail,
                Password = "password"
            };

            _mockValidationRepository.Setup(repo => repo.Get(userEmail)).ReturnsAsync((Validation)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidUserException>(() => _adminLoginService.Login(loginUserDTO));
        }

        [Test]
        public void Login_InactiveUser_ThrowsInvalidUserException()
        {
            // Arrange
            var userEmail = "inactive@example.com";
            var userKey = new byte[64]; // Mocking key
            var validation = new Validation
            {
                Email = userEmail,
                Password = userKey, // Mocking password hash
                Key = userKey,
                Status = "Inactive",
                UserType = "Admin"
            };
            var loginUserDTO = new LoginUserDTO
            {
                Email = userEmail,
                Password = "password"
            };

            _mockValidationRepository.Setup(repo => repo.Get(userEmail)).ReturnsAsync(validation);

            // Act & Assert
            Assert.ThrowsAsync<InvalidUserException>(() => _adminLoginService.Login(loginUserDTO));
        }

        [Test]
        public async Task Register_ValidUser_ReturnsLoginUserDTO()
        {
            // Arrange
            var registerAdminDTO = new RegisterAdminDTO
            {
                Email = "newadmin@example.com",
                Password = "password"
            };
            var validation = new Validation
            {
                Email = registerAdminDTO.Email,
                Key = new byte[64],
                Status = "Active",
                UserType = "Admin"
            };
            var admin = new Admin
            {
                Email = registerAdminDTO.Email
            };

            _mockValidationRepository.Setup(repo => repo.Add(It.IsAny<Validation>())).ReturnsAsync(validation);
            _mockAdminRepository.Setup(repo => repo.Add(It.IsAny<Admin>())).ReturnsAsync(admin);

            // Act
            var result = await _adminLoginService.Register(registerAdminDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(registerAdminDTO.Email, result.Email);
            Assert.AreEqual("Admin", result.UserType);
            Assert.IsEmpty(result.Password);
        }
    }
}
