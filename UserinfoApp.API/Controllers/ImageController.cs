using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using UserinfoApp.API.DTOs.Request;
using UserinfoApp.API.DTOs.Results;
using UserinfoApp.API.Mappers.Interfaces;
using UserinfoApp.DAL.Repositories.Interfaces;

namespace UserinfoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageRepository _imageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageMapper _mapper;
        private readonly IUserinfoRepository _userinfoRepository;

        private readonly int _userId;

        public ImageController(ILogger<ImageController> logger,
                       IImageRepository imageRepository,
                                  IHttpContextAccessor httpContextAccessor,
                                             IImageMapper mapper,
                                                        IUserinfoRepository userinfoRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userinfoRepository = userinfoRepository;
            _userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        /// <summary>
        /// get an image for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces(MediaTypeNames.Image.Png)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Getting image {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.UserInfo.AccountId != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            return File(entity.ImageBytes, $"image/png");
        }

        /// <summary>
        /// creates an image for a user
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/UserInfo/{userId}/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromRoute] int userinfoId, [FromForm] ImageUploadRequestDto req)
        {
            _logger.LogInformation($"Creating image for user {_userId}");
            var userinfoEntity = _userinfoRepository.Get(userinfoId);
            if (userinfoEntity == null)
            {
                _logger.LogInformation($"User with id {_userId} not found");
                return NotFound("User not found");
            }
            if (userinfoEntity.AccountId != _userId)
            {
                _logger.LogInformation($"For user {_userId} is forbidden");
                return Forbid();
            }

            var image = _mapper.Map(req, userinfoId);
            _imageRepository.Add(image);

            return Created(nameof(Get), new { id = image.Id });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Deleting image {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.UserInfo.AccountId != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            _imageRepository.Delete(entity);
            return NoContent();
        }
    }
}
