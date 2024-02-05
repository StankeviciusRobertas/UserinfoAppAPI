using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Mappers.Interfaces
{
    public interface IUserInfoMapper
    {
        UserInfoResultDto Map(UserInfo entity);
        List<UserInfoResultDto> Map(IEnumerable<UserInfo> entities);
        UserInfo Map(UserInfoRequestDto dto);
        void ProjectTo(UserInfoRequestDto from, UserInfo to);
    }
}
