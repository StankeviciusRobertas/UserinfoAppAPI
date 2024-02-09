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
        string Create(Account model);
        void Delete(int id);
        bool Exists(int id);
        bool ExistsUserName(string userName);
        Account? Get(string userName);
        Account? GetById(int id);
    }
}
