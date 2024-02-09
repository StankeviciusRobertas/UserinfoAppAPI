using UserinfoApp.DAL.Entities;

namespace UserinfoApp.DAL.Repositories.Interfaces
{
    public interface IUserAdressRepository : IRepository<UserAdress>
    {
        void Add(UserAdress entity);
        UserAdress? GetByAccountId(int accountId);
    }
}