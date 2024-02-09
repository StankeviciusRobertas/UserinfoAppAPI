using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.API.Mappers.Interfaces;
using UserinfoApp.DAL.Repositories;
using UserinfoApp.DAL.Repositories.Interfaces;

namespace UserinfoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UserAdressController : ControllerBase
    {
        private readonly ILogger<UserAdressController> _logger;
        private readonly IUserAdressRepository _userAdressRepository;
        private readonly IUserAdressMapper _userAdressMapper;
        private readonly int _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAdressController(ILogger<UserAdressController> logger, IUserAdressRepository userAdressRepository, IUserAdressMapper userAdressMapper, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userAdressRepository = userAdressRepository;
            _userAdressMapper = userAdressMapper;
            _httpContextAccessor = httpContextAccessor;
            _userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        /// <summary>
        /// gets a user adress
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserAdressResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Getting userAdress with id {id} for user {_userId}");
            var entity = _userAdressRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User with id {id} not found");
                return NotFound();
            }
            var dto = _userAdressMapper.Map(entity);
            return Ok(dto);
        }

        /// <summary>
        /// creates a user adress for a user
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns> 

        [HttpPost("{userInfoId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post(UserAdressRequestDto req)
        {
            _logger.LogInformation($"Creating userAdress for user {_userId}");
            var entity = _userAdressMapper.Map(req);
            _userAdressRepository.Add(entity);

            return Created(nameof(Get), new { id = entity.Id });
        }

        /// <summary>
        /// updates a user adress
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
        public IActionResult Put(int accountId, UserAdressRequestDto req)
        {
            _logger.LogInformation($"Updating user adress with id {accountId} for user {_userId}");
            var entity = _userAdressRepository.GetByAccountId(accountId);
            if (entity == null)
            {
                _logger.LogInformation($"User with id {accountId} not found");
                return NotFound();
            }
            if (entity.AccountId != accountId)
            {
                _logger.LogInformation($"User with id {accountId} is forbidden");
                return Forbid();
            }


            _userAdressMapper.ProjectTo(req, entity);
            _userAdressRepository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// deletes a user adress
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
            var entity = _userAdressRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User with id  {id} not found");
                return NotFound();
            }
            if (entity.Id != _userId)
            {
                _logger.LogInformation($"User with id  {id} is forbidden");
                return Forbid();
            }
            _userAdressRepository.Delete(entity);
            return NoContent();
        }
    }
}
