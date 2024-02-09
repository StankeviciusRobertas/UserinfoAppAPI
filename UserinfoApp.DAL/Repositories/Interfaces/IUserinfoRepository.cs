using UserinfoApp.DAL.Entities;

namespace UserinfoApp.DAL.Repositories.Interfaces
{
    public interface IUserinfoRepository : IRepository<UserInfo>
    {
        IQueryable<UserInfo> GetAll();
        UserInfo? GetById(int userId);
    }
}