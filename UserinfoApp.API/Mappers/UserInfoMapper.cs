using System.Security.Claims;
using UserinfoApp.API.Mappers.Interfaces;

namespace UserinfoApp.API.Mappers
{
    public class UserInfoMapper : IUserInfoMapper
    {
        private readonly int accountId;

        public UserInfoMapper(IHttpContextAccessor httpContextAccessor)
        {
            accountId = int.Parse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

    }
}
