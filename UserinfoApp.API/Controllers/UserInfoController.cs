using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.API.Mappers;
using UserinfoApp.API.Mappers.Interfaces;
using UserinfoApp.BLL.Services.Interfaces;
using UserinfoApp.DAL.Repositories;
using UserinfoApp.DAL.Repositories.Interfaces;

namespace UserinfoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UserInfoController : ControllerBase
    {
        private readonly ILogger<UserInfoController> _logger;
        private readonly IUserinfoRepository _userinfoRepository;
        private readonly IUserInfoMapper _userInfoMapper;
        private readonly IEmailService _emailService;
        private readonly int _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAdressMapper _userAdressMapper;
        private readonly IUserAdressRepository _userAdressRepository;

        public UserInfoController(ILogger<UserInfoController> logger,
            IUserinfoRepository userinfoRepository,
            IUserInfoMapper userInfoMapper,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor,
            IUserAdressMapper userAdressMapper,
            IUserAdressRepository userAdressRepository)
        {
            _logger = logger;
            _userinfoRepository = userinfoRepository;
            _userInfoMapper = userInfoMapper;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _userAdressMapper = userAdressMapper;
            _userAdressRepository = userAdressRepository;
        }

        /// <summary>
        /// gets a userinfo
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(UserInfoResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get(int accountId)
        {
            _logger.LogInformation($"Getting userInfo with id {accountId} for user {_userId}");
            var entity = _userinfoRepository.Get(accountId);
            if (entity == null)
            {
                _logger.LogInformation($"User with id {accountId} not found");
                return NotFound();
            }
            var dto = _userInfoMapper.Map(entity);
            return Ok(dto);
        }

        ///// <summary>
        ///// creates userinfo
        ///// </summary>
        ///// <param name="req"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Produces(MediaTypeNames.Application.Json)]
        //[Consumes(MediaTypeNames.Application.Json)]
        //public IActionResult Post(UserInfoRequestDto req)
        //{
        //    _logger.LogInformation($"Creating userInfo for user {_userId} with Title {req.Name}");
        //    var entity = _userInfoMapper.Map(req);
        //    _userinfoRepository.Add(entity);

        //    var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
        //    var isSent = _emailService.SendEmail(email, $"Created new UserInfo: {entity.Name}");
        //    if (!isSent)
        //    {
        //        _logger.LogError($"Failed to send email to {email}");
        //    }

        //    return Created(nameof(Get), new { id = entity.Id });
        //}

        /// <summary>
        /// creates userinfo and adress
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("{accountId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post(UserInfoRequestDto req)
        {
            _logger.LogInformation($"Creating userInfo for user {_userId} with Title {req.Name}");
            var userInfoEntity = _userInfoMapper.Map(req);
            _userinfoRepository.Add(userInfoEntity);

            var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            var isSent = _emailService.SendEmail(email, $"Created new UserInfo: {userInfoEntity.Name}");
            if (!isSent)
            {
                _logger.LogError($"Failed to send email to {email}");
            }

            return Created(nameof(Get), new { id = userInfoEntity.Id });
        }

        /// <summary>
        /// updates a user info
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("{accountId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int accountId, UserInfoRequestDto req)
        {
            _logger.LogInformation($"Updating user info for user {_userId}");
            var entity = _userinfoRepository.Get(accountId);
            if (entity == null)
            {
                _logger.LogInformation($"User with id {accountId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User with id {accountId} is forbidden");
                return Forbid();
            }


            _userInfoMapper.ProjectTo(req, entity);
            _userinfoRepository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// deletes a user info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Deleting user info with id {id} for user {_userId}");
            var entity = _userinfoRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User with id  {id} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User with id  {id} is forbidden");
                return Forbid();
            }
            _userinfoRepository.Delete(entity);
            return NoContent();
        }
    }
}
