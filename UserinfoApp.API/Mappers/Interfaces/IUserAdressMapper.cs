using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Mappers.Interfaces
{
    public interface IUserAdressMapper
    {
        UserAdressResultDto Map(UserAdress userAdress);
        List<UserAdressResultDto> Map(IEnumerable<UserAdress> userAdresses);
        UserAdress Map(UserAdressRequestDto userAdress);
        void ProjectTo(UserAdressRequestDto from, UserAdress to);
    }
}
