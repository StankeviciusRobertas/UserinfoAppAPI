using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserinfoApp.DAL.Entities;
using UserinfoApp.DAL.Repositories.Interfaces;

namespace UserinfoApp.DAL.Repositories
{
    public class UserAdressRepository : Repository<UserAdress>, IUserAdressRepository
    {
        public UserAdressRepository(UserinfoAppDbContext context) : base(context)
        {
        }

        // Override method to get all UserAdress entities from the 
        override public IQueryable<UserAdress> GetAll()
        {
            // Using AsQueryable() to ensure IQueryable return type
            return _context.UsersAdress.AsQueryable();
        }

        // Override method to get a specific UserAdress entity by its id, including the related UserInfo entity
        public override UserAdress? Get(int id)
        {
            // Using Include to eagerly load UserInfo navigation property
            return _context.UsersAdress.Find(id);
        }

        public UserAdress? GetByAccountId(int accountId)
        {
            // Using Include to eagerly load UserInfo navigation property
            return _context.UsersAdress.FirstOrDefault((UserAdress x) => x.AccountId == accountId);
        }

        public override void Add(UserAdress entity)
        {
            // Using AddRange to add a range of entities to the database
            _context.UsersAdress.AddRange(entity);
            _context.SaveChanges();
        }
    }
}
