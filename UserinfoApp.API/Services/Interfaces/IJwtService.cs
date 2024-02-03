using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Services.Interfaces
{
    public interface IJwtService
    {
        string GetJwtToken(Account account);
    }
}
