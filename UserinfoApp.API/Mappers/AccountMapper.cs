using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.Mappers.Interfaces;
using UserinfoApp.BLL.Services.Interfaces;
using UserinfoApp.DAL.Entities;

namespace UserinfoApp.API.Mappers
{
    public class AccountMapper : IAccountMapper
    {
        private readonly IAccountService _service;

        public AccountMapper(IAccountService service)
        {
            _service = service;
        }

        public Account Map(AccountRequestDto dto)
        {
            _service.CreatePasswordHash(dto.Password!, out var passwordHash, out var passwordSalt);
            return new Account
            {
                UserName = dto.UserName!,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = dto.Role!
            };
        }
    }
}
