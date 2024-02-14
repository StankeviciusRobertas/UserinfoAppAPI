using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserinfoApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.DAL.Repositories.Tests
{
    [TestClass()]
    public class AccountRepositoryTests
    {
        private UserinfoAppDbContext _context;
        private AccountRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize the DbContext and Repository
            var options = new DbContextOptionsBuilder<UserinfoAppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new UserinfoAppDbContext(options);
            _repository = new AccountRepository(_context);
        }

        [TestMethod()]
        public void Create_AccountCreatedSuccessfully()
        {
            // Arrange
            var account = new Account
            {
                UserName = "testuser",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };

            // Act
            var result = _repository.Create(account);

            // Assert
            Assert.IsTrue(result.Contains("testuser")); // Assuming Create method returns a boolean indicating success
        }

        [TestMethod()]
        public void Get_ReturnsAccountIfExists()
        {
            // Arrange
            var account = new Account
            {
                UserName = "existinguser",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            // Act
            var result = _repository.Get("existinguser");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("existinguser", result.UserName);
        }
    }
}