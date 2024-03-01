//using NUnit.Framework;
//using Moq;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using MaverickBankk.Services;
//using MaverickBankk.Interfaces;
//using MaverickBankk.Models;

//namespace MaverickBankk.Tests
//{
//    [TestFixture]
//    public class AvailableLoansUserServiceTests
//    {
//        private Mock<IRepository<int, AvailableLoans>> _availableLoansRepositoryMock;
//        private AvailableLoansUserService _availableLoansUserService;

//        [SetUp]
//        public void Setup()
//        {
//            _availableLoansRepositoryMock = new Mock<IRepository<int, AvailableLoans>>();
//            _availableLoansUserService = new AvailableLoansUserService(_availableLoansRepositoryMock.Object);
//        }

//        [Test]
//        public async Task GetAllLoans()
//        {
//            // Arrange
//            var expectedLoans = new List<AvailableLoans>
//            {
//                new AvailableLoans { LoanID = 1, LoanType = "Loan 1", LoanAmount = 10000 },
//                new AvailableLoans { LoanID = 2, LoanType = "Loan 2", LoanAmount = 20000 }
//            };

//            _availableLoansRepositoryMock.Setup(repo => repo.GetAll())
//                .ReturnsAsync(expectedLoans);

//            // Act
//            var result = await _availableLoansUserService.GetAllLoans();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(expectedLoans.Count, result.Count);
            
//        }

//        [Test]
//        public async Task GetLoanById()
//        {
//            // Arrange
//            int loanId = 1;
//            var expectedLoan = new AvailableLoans { LoanID = loanId, LoanType = "Loan 1", LoanAmount = 10000 };

//            _availableLoansRepositoryMock.Setup(repo => repo.Get(loanId))
//                .ReturnsAsync(expectedLoan);

//            // Act
//            var result = await _availableLoansUserService.GetLoanById(loanId);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(expectedLoan.LoanID, result.LoanID);
//            Assert.AreEqual(expectedLoan.LoanType, result.LoanType);
           
//        }
//    }
//}
