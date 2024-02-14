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
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database name for each test method
        .Options;
            _context = new UserinfoAppDbContext(options);
            _repository = new AccountRepository(_context);
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

        [TestMethod()]
        public void GetById_ReturnsAccountIfExists()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                UserName = "existinguser",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            // Act
            var result = _repository.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod()]
        public void Exists_ReturnsTrueIfAccountExists()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                UserName = "existinguser",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            // Act
            var result = _repository.Exists(1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ExistsUserName_ReturnsTrueIfUserNameExists()
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
            var result = _repository.ExistsUserName("existinguser");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Delete_RemovesAccountIfExists()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                UserName = "existinguser",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            // Act
            _repository.Delete(1);

            // Assert
            Assert.IsFalse(_repository.Exists(1));
        }

        [TestMethod()]
        public void GetAll_ReturnsAllAccounts()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account
                {
                    UserName = "user1",
                    PasswordHash = new byte[] { 1, 2, 3 },
                    PasswordSalt = new byte[] { 4, 5, 6 }
                },
                new Account
                {
                    UserName = "user2",
                    PasswordHash = new byte[] { 4, 5, 6 },
                    PasswordSalt = new byte[] { 7, 8, 9 }
                }
            };
            _context.Accounts.AddRange(accounts);
            _context.SaveChanges();

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}