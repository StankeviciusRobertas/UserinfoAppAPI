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
    public class UserinfoRepository : Repository<UserInfo>, IUserinfoRepository 
    {
        public UserinfoRepository(UserinfoAppDbContext context) : base(context)
        {
            
        }

        // Override method to get all UserInfo entities from the database, including related Images entities
        override public IQueryable<UserInfo> GetAll()
        {
            // Using Include to eagerly load Images navigation property
            return _context.UsersInfo.Include(x => x.Images);
        }

        // Override method to get a specific UserInfo entity by its id, including the related Images entities
        public override UserInfo? Get(int id)
        {
            // Using Include to eagerly load Images navigation property
            return _context.UsersInfo.Include(x => x.Images).FirstOrDefault(x => x.Id == id);
        }
    }
}
