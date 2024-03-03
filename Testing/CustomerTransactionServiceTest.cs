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

namespace Testing
{
    [TestFixture]
    public class CustomerTransactionServiceTest
    {
        private Mock<IRepository<int, Transactions>> _mockTransactionsRepository;
        private Mock<IRepository<long, Accounts>> _mockAccountsRepository;
        private Mock<ILogger<CustomerTransactionService>> _mockLogger;
        private CustomerTransactionService _customerTransactionService;

        [SetUp]
        public void Setup()
        {
            _mockTransactionsRepository = new Mock<IRepository<int, Transactions>>();
            _mockAccountsRepository = new Mock<IRepository<long, Accounts>>();
            _mockLogger = new Mock<ILogger<CustomerTransactionService>>();
            _customerTransactionService = new CustomerTransactionService(
                _mockLogger.Object,
                _mockTransactionsRepository.Object,
                _mockAccountsRepository.Object);
        }

        [Test]
        public async Task Deposit_ValidData_AccountBalanceUpdated()
        {
            // Arrange
            int customerId = 1;
            var depositDTO = new DepositDTO { AccountNumber = 123, Amount = 500 };
            var account = new Accounts { AccountNumber = 123, CustomerID = customerId, Status = "Active", Balance = 1000 };
            _mockAccountsRepository.Setup(repo => repo.Get(depositDTO.AccountNumber)).ReturnsAsync(account);

            // Act
            await _customerTransactionService.Deposit(customerId, depositDTO);

            // Assert
            Assert.That(account.Balance, Is.EqualTo(1500));
        }

        [Test]
        public async Task Deposit_InvalidAccount_NoChangesInBalance()
        {
            // Arrange
            int customerId = 1;
            var depositDTO = new DepositDTO { AccountNumber = 123, Amount = 500 };
            var unused = _mockAccountsRepository.Setup(repo => repo.Get(depositDTO.AccountNumber)).ReturnsAsync((Accounts?)null);

            // Act
            await _customerTransactionService.Deposit(customerId, depositDTO);

            // Assert
            _mockAccountsRepository.Verify(repo => repo.Update(It.IsAny<Accounts>()), Times.Never);
        }

      
        [Test]
        public async Task Withdraw_ValidData_AccountBalanceUpdated()
        {
            // Arrange
            int customerId = 1;
            var withdrawalDTO = new WithdrawalDTO { AccountNumber = 123, Amount = 500 };
            var account = new Accounts { AccountNumber = 123, CustomerID = customerId, Status = "Active", Balance = 1000 };
            _mockAccountsRepository.Setup(repo => repo.Get(withdrawalDTO.AccountNumber)).ReturnsAsync(account);

            // Act
            await _customerTransactionService.Withdraw(customerId, withdrawalDTO);

            // Assert
            Assert.That(account.Balance, Is.EqualTo(500));
        }

        [Test]
        public async Task Withdraw_InvalidAccount_NoChangesInBalance()
        {
            // Arrange
            int customerId = 1;
            var withdrawalDTO = new WithdrawalDTO { AccountNumber = 123, Amount = 500 };
            _mockAccountsRepository.Setup(repo => repo.Get(withdrawalDTO.AccountNumber)).ReturnsAsync((Accounts?)null);

            // Act
            await _customerTransactionService.Withdraw(customerId, withdrawalDTO);

            // Assert
            _mockAccountsRepository.Verify(repo => repo.Update(It.IsAny<Accounts>()), Times.Never);
        }

    

        [Test]
        public async Task Transfer_ValidData_AccountBalancesUpdated()
        {
            // Arrange
            int customerId = 1;
            var transferDTO = new TransferDTO { SourceAccountNumber = 123, DestinationAccountNumber = 456, Amount = 500 };
            var sourceAccount = new Accounts { AccountNumber = 123, CustomerID = customerId, Status = "Active", Balance = 1000 };
            var destinationAccount = new Accounts { AccountNumber = 456, Status = "Active", Balance = 500 };
            _mockAccountsRepository.Setup(repo => repo.Get(transferDTO.SourceAccountNumber)).ReturnsAsync(sourceAccount);
            _mockAccountsRepository.Setup(repo => repo.Get(transferDTO.DestinationAccountNumber)).ReturnsAsync(destinationAccount);

            // Act
            await _customerTransactionService.Transfer(customerId, transferDTO);

          
            Assert.That(destinationAccount.Balance, Is.EqualTo(1000));

        }

        [Test]
        public async Task Transfer_InvalidSourceAccount_NoChangesInBalances()
        {
            // Arrange
            int customerId = 1;
            var transferDTO = new TransferDTO { SourceAccountNumber = 123, DestinationAccountNumber = 456, Amount = 500 };
            _mockAccountsRepository.Setup(repo => repo.Get(transferDTO.SourceAccountNumber)).ReturnsAsync((Accounts?)null);

            // Act
            await _customerTransactionService.Transfer(customerId, transferDTO);

            // Assert
            _mockAccountsRepository.Verify(repo => repo.Update(It.IsAny<Accounts>()), Times.Never);
        }

     

    

    

       

        [Test]
        public async Task GetLastMonthTransactions_ValidAccountNumber_ReturnsTransactions()
        {
            // Arrange
            long accountNumber = 123;
            var transactions = new List<Transactions>
            {
                new Transactions { TransactionID = 1, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddMonths(-1).Date },
                new Transactions { TransactionID = 2, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddMonths(-1).Date },
               
            };
            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _customerTransactionService.GetLastMonthTransactions(accountNumber);

            // Assert
            CollectionAssert.AreEqual(transactions, result);
        }

      

      
        [Test]
        public async Task GetTransactionsBetweenDates_ValidData_ReturnsTransactions()
        {
            // Arrange
            long accountNumber = 123;
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;
            var transactions = new List<Transactions>
            {
                new Transactions { TransactionID = 1, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddDays(-5).Date },
                new Transactions { TransactionID = 2, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddDays(-10).Date },
                
            };
            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _customerTransactionService.GetTransactionsBetweenDates(accountNumber, startDate, endDate);

            // Assert
            CollectionAssert.AreEqual(transactions, result);
        }

        [Test]
        public void GetLastMonthTransactions_NoTransactionsFound_ThrowsNoTransactionsException()
        {
            // Arrange
            long accountNumber = 123;
            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Transactions>());

            // Act and Assert
            Assert.ThrowsAsync<NoTransactionsException>(async () => await _customerTransactionService.GetLastMonthTransactions(accountNumber));
        }
        [Test]
        public void GetTransactionsBetweenDates_NoTransactionsFound_ThrowsNoTransactionsException()
        {
            // Arrange
            long accountNumber = 123;
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;
            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Transactions>());

            // Act and Assert
            Assert.ThrowsAsync<NoTransactionsException>(async () => await _customerTransactionService.GetTransactionsBetweenDates(accountNumber, startDate, endDate));
        }

        [Test]
        public async Task GetTransactionsBetweenDates_ValidData_ReturnsFilteredTransactions()
        {
            // Arrange
            long accountNumber = 123;
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;
            var transactions = new List<Transactions>
    {
        new Transactions { TransactionID = 1, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddDays(-5).Date },
        new Transactions { TransactionID = 2, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddDays(-10).Date },
        new Transactions { TransactionID = 3, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddDays(-15).Date }
    };
            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _customerTransactionService.GetTransactionsBetweenDates(accountNumber, startDate, endDate);

            // Assert
            CollectionAssert.AreEqual(transactions, result);
        }

        [Test]
        public async Task GetLast10Transactions_ValidAccountNumber_ReturnsLast10Transactions()
        {
            // Arrange
            long accountNumber = 123;
            var transactions = new List<Transactions>
        {
            new Transactions { TransactionID = 1, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddDays(-1).Date },
            new Transactions { TransactionID = 2, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddDays(-2).Date },
           
        };
            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _customerTransactionService.GetLast10Transactions(accountNumber);

            // Assert
            CollectionAssert.AreEqual(transactions.Take(10).OrderByDescending(t => t.TransactionDate), result);
        }

        [Test]
        public void GetLast10Transactions_NoTransactionsFound_ThrowsNoTransactionsException()
        {
            // Arrange
            long accountNumber = 123;
            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Transactions>());

            // Act and Assert
            Assert.ThrowsAsync<NoTransactionsException>(async () => await _customerTransactionService.GetLast10Transactions(accountNumber));
        }

        [Test]
        public async Task GetLast10Transactions_LessThan10Transactions_ReturnsAllTransactions()
        {
            // Arrange
            long accountNumber = 123;
            var transactions = new List<Transactions>
        {
            new Transactions { TransactionID = 1, SourceAccountNumber = accountNumber, TransactionDate = DateTime.Now.AddDays(-1).Date },
            // Add fewer than 10 transactions
        };
            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _customerTransactionService.GetLast10Transactions(accountNumber);

            // Assert
            CollectionAssert.AreEqual(transactions.OrderByDescending(t => t.TransactionDate), result);
        }
    }
}




//using MaverickBankk.Context;
//using MaverickBankk.Exceptions;
//using MaverickBankk.Interfaces;
//using MaverickBankk.Mappers;
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
//    public class CustomerTransactionServiceTest
//    {
//        private CustomerTransactionService _transactionService;
//        private Mock<IRepository<long, Accounts>> _mockAccountsRepository;
//        private Mock<IRepository<int, Transactions>> _mockTransactionsRepository;
//        private Mock<ILogger<CustomerTransactionService>> _mockLogger;

//        [SetUp]
//        public void Setup()
//        {
//            _mockAccountsRepository = new Mock<IRepository<long, Accounts>>();
//            _mockTransactionsRepository = new Mock<IRepository<int, Transactions>>();
//            _mockLogger = new Mock<ILogger<CustomerTransactionService>>();

//            _transactionService = new CustomerTransactionService(
//                _mockLogger.Object,
//                _mockTransactionsRepository.Object,
//                _mockAccountsRepository.Object,
//                new TransactionMapper());
//        }

//        [Test]
//        public async Task Deposit_Successful()
//        {
//            // Arrange
//            var depositDTO = new DepositDTO
//            {
//                AccountNumber = 123456789,
//                Amount = 100
//            };

//            var account = new Accounts
//            {
//                AccountNumber = depositDTO.AccountNumber,
//                Balance = 0,
//                Status = "Active"
//            };

//            _mockAccountsRepository.Setup(repo => repo.Get(depositDTO.AccountNumber)).ReturnsAsync(account);

//            // Act
//            var result = await _transactionService.Deposit(depositDTO);

//            // Assert
//            Assert.AreEqual("Deposit successful.", result);
//            Assert.AreEqual(100, account.Balance);
//        }

//        [Test]
//        public async Task Withdraw_From_Active_Account_With_Sufficient_Balance()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            double initialBalance = 1000;
//            double withdrawalAmount = 500;

//            var account = new Accounts
//            {
//                AccountNumber = accountNumber,
//                Status = "Active",
//                Balance = initialBalance
//            };

//            _mockAccountsRepository.Setup(repo => repo.Get(accountNumber)).ReturnsAsync(account);

//            var withdrawalDTO = new WithdrawalDTO
//            {
//                AccountNumber = accountNumber,
//                Amount = withdrawalAmount
//            };

//            // Act
//            var result = await _transactionService.Withdraw(withdrawalDTO);

//            // Assert
//            Assert.AreEqual("Withdrawal successful.", result);
//            Assert.AreEqual(initialBalance - withdrawalAmount, account.Balance);
//        }

//        [Test]
//        public async Task Transfer()
//        {
//            // Arrange
//            long sourceAccountNumber = 123456789;
//            long destinationAccountNumber = 987654321;
//            double initialSourceBalance = 1000;
//            double transferAmount = 500;

//            var sourceAccount = new Accounts
//            {
//                AccountNumber = sourceAccountNumber,
//                Status = "Active",
//                Balance = initialSourceBalance
//            };

//            _mockAccountsRepository.Setup(repo => repo.Get(sourceAccountNumber)).ReturnsAsync(sourceAccount);

//            var transferDTO = new TransferDTO
//            {
//                SourceAccountNumber = sourceAccountNumber,
//                DestinationAccountNumber = destinationAccountNumber,
//                Amount = transferAmount
//            };

//            // Act
//            var result = await _transactionService.Transfer(customerId,transferDTO);

//            // Assert
//            Assert.AreEqual("Transfer successful.", result);
//            Assert.AreEqual(initialSourceBalance - transferAmount, sourceAccount.Balance);
//        }

//        [Test]
//        public async Task GetTransactionsBetweenDates()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            DateTime startDate = new DateTime(2024, 1, 1);
//            DateTime endDate = new DateTime(2024, 1, 31);

//            // Mock transactions within the specified date range
//            var transactions = new List<Transactions>
//            {
//                new Transactions { TransactionID = 1, SourceAccountNumber = accountNumber, DestinationAccountNumber = 987654321, Amount = 100, TransactionDate = new DateTime(2024, 1, 15) },
//                new Transactions { TransactionID = 2, SourceAccountNumber = 987654321, DestinationAccountNumber = accountNumber, Amount = 50, TransactionDate = new DateTime(2024, 1, 20) }
//            };

//            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

//            // Act
//            var result = await _transactionService.GetTransactionsBetweenDates(accountNumber, startDate, endDate);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOf<List<Transactions>>(result);
//            Assert.AreEqual(2, result.Count);
//        }

//        [Test]
//        public async Task GetLastMonthTransactions_NoTransactionsFound()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            var lastMonth = DateTime.Now.AddMonths(-1);
//            var transactions = new List<Transactions>();

//            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

//            // Act + Assert
//            var ex = Assert.ThrowsAsync<NoTransactionsException>(async () => await _transactionService.GetLastMonthTransactions(accountNumber));
//            Assert.AreEqual("No transactions found for the account in the last month.", ex.Message);
//        }

//        [Test]
//        public async Task GetLast10Transactions()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            var transactions = new List<Transactions>
//            {
//                new Transactions { TransactionID = 1, SourceAccountNumber = accountNumber, Amount = 100, TransactionDate = DateTime.Now.AddDays(-1) },
//                new Transactions { TransactionID = 2, SourceAccountNumber = accountNumber, Amount = 200, TransactionDate = DateTime.Now.AddDays(-2) },
//                new Transactions { TransactionID = 3, SourceAccountNumber = accountNumber, Amount = 300, TransactionDate = DateTime.Now.AddDays(-3) },
//                new Transactions { TransactionID = 4, SourceAccountNumber = accountNumber, Amount = 400, TransactionDate = DateTime.Now.AddDays(-4) },
//                new Transactions { TransactionID = 5, SourceAccountNumber = accountNumber, Amount = 500, TransactionDate = DateTime.Now.AddDays(-5) },
//                new Transactions { TransactionID = 6, SourceAccountNumber = accountNumber, Amount = 600, TransactionDate = DateTime.Now.AddDays(-6) },
//                new Transactions { TransactionID = 7, SourceAccountNumber = accountNumber, Amount = 700, TransactionDate = DateTime.Now.AddDays(-7) },
//                new Transactions { TransactionID = 8, SourceAccountNumber = accountNumber, Amount = 800, TransactionDate = DateTime.Now.AddDays(-8) },
//                new Transactions { TransactionID = 9, SourceAccountNumber = accountNumber, Amount = 900, TransactionDate = DateTime.Now.AddDays(-9) },
//                new Transactions { TransactionID = 10, SourceAccountNumber = accountNumber, Amount = 1000, TransactionDate = DateTime.Now.AddDays(-10) },
//                new Transactions { TransactionID = 11, SourceAccountNumber = accountNumber, Amount = 1100, TransactionDate = DateTime.Now.AddDays(-11) }
//            };

//            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

//            // Act
//            var result = await _transactionService.GetLast10Transactions(accountNumber);

//            Assert.IsNotNull(result);
//            Assert.AreEqual(10, result.Count);
//        }

//        [Test]
//        public void GetLast10Transactions_ThrowsException()
//        {
//            // Arrange
//            long accountNumber = 123456789;
//            var transactions = new List<Transactions>();

//            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

//            // Act & Assert
//            Assert.ThrowsAsync<NoTransactionsException>(async () => await _transactionService.GetLast10Transactions(accountNumber));
//        }

//    }
//}
