using Microsoft.Identity.Client;
using System.Security.Claims;
using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.API.Mappers.Interfaces;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Mappers
{
    public class UserInfoMapper : IUserInfoMapper
    {
        private readonly int accountId;

        public UserInfoMapper(IHttpContextAccessor httpContextAccessor)
        {
            accountId = int.Parse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public UserInfoResultDto Map(UserInfo entity)
        {
            return new UserInfoResultDto
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                PersonalCode = entity.PersonalCode,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                ImageIds = entity.Images.Any() ? entity.Images.Select(i => i.Id) : null,
            };
        }

        public List<UserInfoResultDto> Map(IEnumerable<UserInfo> entities)
        {
            return entities.Select(Map).ToList();
        }

        public UserInfo Map(UserInfoRequestDto dto)
        {
            return new UserInfo
            {
                Name = dto.Name,
                LastName = dto.LastName,
                PersonalCode = dto.PersonalCode,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                AccountId = accountId
            };
        }

        public void ProjectTo(UserInfoRequestDto from, UserInfo to)
        {
            to.Name = from.Name;
            to.LastName = from.LastName;
            to.PersonalCode = from.PersonalCode;
            to.PhoneNumber = from.PhoneNumber;
            to.Email = from.Email;
        }
    }
}
