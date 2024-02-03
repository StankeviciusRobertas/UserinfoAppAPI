using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserinfoApp.DAL.Entities;
using UserinfoApp.DAL.Repositories.Interfaces;

namespace UserinfoApp.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserinfoAppDbContext _context;

        public AccountRepository(UserinfoAppDbContext context)
        {
            _context = context;
        }

        // Method to create a new account in the database
        public int Create(Account model)
        {
            _context.Accounts.Add(model);
            _context.SaveChanges();
            return model.Id;
        }

        // Method to retrieve an account based on the provided username
        public Account? Get(string userName)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserName == userName);
        }

        // Method to check if an account with the given id exists in the database
        public bool Exists(int id)
        {
            return _context.Accounts.Any(x => x.Id == id);
        }

        // Method to delete an account from the database based on the provided id
        public void Delete(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }
    }
}
