using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.DAL.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        int Create(Account model);
        void Delete(int id);
        bool Exists(int id);
        Account? Get(string userName);
    }
}
