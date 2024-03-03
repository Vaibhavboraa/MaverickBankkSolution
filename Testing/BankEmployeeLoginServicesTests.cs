using System.Threading.Tasks;
using MaverickBankk.Exceptions;
using MaverickBankk.Interfaces;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;
using MaverickBankk.Services;
using Moq;
using NUnit.Framework;
using System.Security.Cryptography;
using System.Text;

namespace MaverickBankk.Tests.Services
{
    [TestFixture]
    public class BankEmployeeLoginServicesTests
    {
        private Mock<IRepository<int, BankEmployees>> _employeeRepositoryMock;
        private Mock<IRepository<string, Validation>> _validationRepositoryMock;
        private Mock<ITokenService> _tokenServiceMock;
        private BankEmployeeLoginServices _bankEmployeeLoginServices;

        [SetUp]
        public void Setup()
        {
            _employeeRepositoryMock = new Mock<IRepository<int, BankEmployees>>();
            _validationRepositoryMock = new Mock<IRepository<string, Validation>>();
            _tokenServiceMock = new Mock<ITokenService>();

            _bankEmployeeLoginServices = new BankEmployeeLoginServices(
                _employeeRepositoryMock.Object,
                _validationRepositoryMock.Object,
                _tokenServiceMock.Object
            );
        }

        [Test]
        public async Task Register()
        {
            // Arrange
            var employeeDTO = new RegisterBankEmployeeDTO
            {

                Email = "v@example.com",

            };

            var validation = new Validation
            {
                Email = "v@example.com",
                Status = "Active",
                Password = new byte[64],
                Key = new byte[64]

            };

            _validationRepositoryMock.Setup(repo => repo.Add(It.IsAny<Validation>()))
                .ReturnsAsync(validation);

            var bankEmployee = new BankEmployees
            {

                EmployeeID = 1,

            };

            _employeeRepositoryMock.Setup(repo => repo.Add(It.IsAny<BankEmployees>()))
                .ReturnsAsync(bankEmployee);

            // Act
            var result = await _bankEmployeeLoginServices.Register(employeeDTO);

            // Assert
            Assert.IsNotNull(result);

        }


        [Test]
        public void Login_ThrowsInvalidUserException()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO
            {
                Email = "V@gmail.com",
                Password = "wrongpassword"
            };

            var validation = new Validation
            {
                Email = "test@example.com",
                Status = "Active",
                Password = new byte[64],
                Key = new byte[64]

            };

            _validationRepositoryMock.Setup(repo => repo.Get(loginUserDTO.Email))
                .ReturnsAsync(validation);

            // Act & Assert
            Assert.ThrowsAsync<InvalidUserException>(() => _bankEmployeeLoginServices.Login(loginUserDTO));
        }


        [Test]
        public void Login_ThrowsDeactivatedUserException()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO
            {
                Email = "deactivated@example.com",
                Password = "password"
            };

            var deactivatedValidation = new Validation
            {
                Email = "deactivated@example.com",
                Status = "Deactivated", // Simulating a deactivated user
                Password = new byte[64],
                Key = new byte[64]
            };

            _validationRepositoryMock.Setup(repo => repo.Get(loginUserDTO.Email))
                .ReturnsAsync(deactivatedValidation);

            // Act & Assert
            Assert.ThrowsAsync<DeactivatedUserException>(() => _bankEmployeeLoginServices.Login(loginUserDTO));
        }

     


        [Test]
        public void Login_InvalidUserType_ThrowsInvalidUserException()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO
            {
                Email = "invaliduser@example.com",
                Password = "password"
            };

            var invalidUserTypeValidation = new Validation
            {
                Email = "invaliduser@example.com",
                Status = "Active",
                Password = new byte[64],
                Key = new byte[64],
                UserType = null 
            };

            _validationRepositoryMock.Setup(repo => repo.Get(loginUserDTO.Email))
                .ReturnsAsync(invalidUserTypeValidation);

            // Act & Assert
            Assert.ThrowsAsync<InvalidUserException>(() => _bankEmployeeLoginServices.Login(loginUserDTO));
        }
      




    }
}
