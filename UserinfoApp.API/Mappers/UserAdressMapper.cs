using System.Security.Claims;
using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.API.Mappers.Interfaces;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Mappers
{
    public class UserAdressMapper : IUserAdressMapper
    {
        private readonly int accountId;

        public UserAdressMapper(IHttpContextAccessor httpContextAccessor)
        {
            accountId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public UserAdressResultDto Map(UserAdress userAdress)
        {
            return new UserAdressResultDto
            {
                Id = userAdress.Id,
                City = userAdress.City,
                Street = userAdress.Street,
                HouseNumber = userAdress.HouseNumber,
                FlatNumber = userAdress.FlatNumber,
            };
        }

        public List<UserAdressResultDto> Map(IEnumerable<UserAdress> userAdresses)
        {
            return userAdresses.Select(Map).ToList();
        }

        public UserAdress Map(UserAdressRequestDto userAdress)
        {
            return new UserAdress
            {
                City = userAdress.City,
                Street = userAdress.Street,
                HouseNumber = userAdress.HouseNumber,
                FlatNumber = userAdress.FlatNumber,
                AccountId = accountId
            };
        }

        public void ProjectTo(UserAdressRequestDto from, UserAdress to)
        {
            to.City = from.City;
            to.Street = from.Street;
            to.HouseNumber = from.HouseNumber;
            to.FlatNumber = from.FlatNumber;
        }
    }
}
