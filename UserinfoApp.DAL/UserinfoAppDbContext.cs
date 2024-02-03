using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.DAL
{
    public class UserinfoAppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserInfo> UsersInfo { get; set; }
        public DbSet<UserAdress> UsersAdress { get; set; }
        public DbSet<Image> Images { get; set; }

        public UserinfoAppDbContext(DbContextOptions<UserinfoAppDbContext> options) : base(options)
        {

        }
    }
}
