using UserinfoApp.API.DTOs.Request;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Mappers.Interfaces
{
    public interface IAccountMapper
    {
        Account Map(AccountRequestDto dto);
    }
}
