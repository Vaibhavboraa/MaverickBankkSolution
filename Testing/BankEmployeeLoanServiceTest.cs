﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaverickBankk.Interfaces;
using MaverickBankk.Models;
using MaverickBankk.Models.DTOs;
using MaverickBankk.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MaverickBankk.Exceptions;
using NUnit.Framework;

namespace MaverickBankk.Tests
{
    public class BankEmployeeLoanServiceTests
    {
        private Mock<IRepository<int, Loans>> _mockLoansRepository;
        private Mock<IRepository<long, Accounts>> _mockAccountsRepository;
        private Mock<IRepository<int, Transactions>> _mockTransactionsRepository;
        private Mock<ILogger<BankEmployeeLoanService>> _mockLogger;
        private BankEmployeeLoanService _bankEmployeeLoanService;

        [SetUp]
        public void Setup()
        {
            _mockLoansRepository = new Mock<IRepository<int, Loans>>();
            _mockAccountsRepository = new Mock<IRepository<long, Accounts>>();
            _mockTransactionsRepository = new Mock<IRepository<int, Transactions>>();
            _mockLogger = new Mock<ILogger<BankEmployeeLoanService>>();
            _bankEmployeeLoanService = new BankEmployeeLoanService(
                _mockLoansRepository.Object,
                _mockAccountsRepository.Object,
                _mockTransactionsRepository.Object,
                _mockLogger.Object);
        }

        [Test]
        public async Task ReviewLoanApplication_ValidLoanId_ReturnsLoan()
        {

            var loanId = 123;
            var loan = new Loans { LoanID = loanId };

            _mockLoansRepository.Setup(repo => repo.Get(loanId)).ReturnsAsync(loan);

            // Act
            var result = await _bankEmployeeLoanService.ReviewLoanApplication(loanId);

            // Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task CheckCredit_ValidAccountId_ReturnsCreditCheckResultDTO()
        {
            // Arrange
            var accountId = 123;
            var transactions = new List<Transactions>
    {
        new Transactions { SourceAccountNumber = accountId, TransactionType = "Credit", Amount = 100 },
        new Transactions { SourceAccountNumber = accountId, TransactionType = "Debit", Amount = 50 }
    };

            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _bankEmployeeLoanService.CheckCredit(accountId);

            // Assert
            Assert.IsNotNull(result);
        }




        [Test]
        public async Task DisburseLoan_AcceptedLoan_ReturnsAccount()
        {
            // Arrange
            var loanId = 123;
            var accountId = 456;
            var loan = new Loans { LoanID = loanId, Status = "Accepted", LoanAmount = 100 };
            var account = new Accounts { AccountNumber = accountId, Balance = 500 };

            _mockLoansRepository.Setup(repo => repo.Get(loanId)).ReturnsAsync(loan);
            _mockAccountsRepository.Setup(repo => repo.Get(accountId)).ReturnsAsync(account);
            _mockAccountsRepository.Setup(repo => repo.Update(account)).ReturnsAsync(account);

            // Act
            var result = await _bankEmployeeLoanService.DisburseLoan(loanId, accountId);

            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task MakeLoanDecision_ValidLoanId_ReturnsDecisionMessage()
        {
            // Arrange
            var loanId = 123;
            var approved = true;
            var loan = new Loans { LoanID = loanId, Status = "Pending" };

            _mockLoansRepository.Setup(repo => repo.Get(loanId)).ReturnsAsync(loan);
            _mockLoansRepository.Setup(repo => repo.Update(loan)).ReturnsAsync(loan);

            var expectedMessage = $"Loan application with ID {loanId} decision updated to {(approved ? "Accepted" : "Rejected")}.";

            // Act
            var result = await _bankEmployeeLoanService.MakeLoanDecision(loanId, approved);

            // Assert
            Assert.That(result, Is.EqualTo(expectedMessage));
        }
        [Test]
        public void ReviewLoanApplication_InvalidLoanId_ThrowsLoanNotFoundException()
        {
            // Arrange
            var invalidLoanId = 999;

            _mockLoansRepository.Setup(repo => repo.Get(invalidLoanId)).ReturnsAsync((Loans?)null);

            // Act & Assert
            Assert.ThrowsAsync<NoLoansFoundException>(async () => await _bankEmployeeLoanService.ReviewLoanApplication(invalidLoanId));
        }
        [Test]
        public void CheckCredit_InvalidAccountId_ReturnsCreditCheckResultDTOWithZeroCredit()
        {
            // Arrange
            var invalidAccountId = 999;

            _mockTransactionsRepository.Setup(repo => repo.GetAll()).ReturnsAsync((List<Transactions>?)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<NoTransactionsException>(async () => await _bankEmployeeLoanService.CheckCredit(invalidAccountId));
            Assert.That(exception.Message, Is.EqualTo($"No transactions found for account ID: {invalidAccountId}"));
        }


      

        [Test]
        public void DisburseLoan_InvalidAccountId_ThrowsAccountNotFoundException()
        {
            // Arrange
            var validLoanId = 123;
            var invalidAccountId = 999;
            var loan = new Loans { LoanID = validLoanId, Status = "Accepted", LoanAmount = 100 };

            _mockLoansRepository.Setup(repo => repo.Get(validLoanId)).ReturnsAsync(loan);
            _mockAccountsRepository.Setup(repo => repo.Get(invalidAccountId)).ReturnsAsync((Accounts?)null);

            // Act & Assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await _bankEmployeeLoanService.DisburseLoan(validLoanId, invalidAccountId));
        }


       


    }
}
