﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountMapper _accountMapper;

        private readonly int _userId;

        public ImageController(ILogger<ImageController> logger,
                       IImageRepository imageRepository,
                                  IHttpContextAccessor httpContextAccessor,
                                             IImageMapper mapper,
                                                        IUserinfoRepository userinfoRepository, 
                                                                IAccountMapper accountMapper, 
                                                                    IAccountRepository accountRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userinfoRepository = userinfoRepository;
            _userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _accountMapper = accountMapper;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// get an image for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces(MediaTypeNames.Image.Png)]
        public IActionResult Get(int accountId)
        {
            _logger.LogInformation($"Getting image {accountId} for user {_userId}");
            var entity = _imageRepository.Get(accountId);
            if (entity == null)
            {
                _logger.LogInformation($"Image {accountId} not found for user {_userId}");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"Image {accountId} is forbidden for user {_userId}");
                return Forbid();
            }
            return File(entity.ImageBytes, "image/jpg");
        }

        /// <summary>
        /// creates an image for a user
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/Image/{accountId}/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromRoute] int accountId, [FromForm] ImageUploadRequestDto req)
        {
            _logger.LogInformation($"Creating image for user {_userId}");
            var accountEntity = _accountRepository.GetById(accountId);
            if (accountEntity == null)
            {
                _logger.LogInformation($"User with id {_userId} not found");
                return NotFound("User not found");
            }
            if (accountEntity.Id != _userId)
            {
                _logger.LogInformation($"For user {_userId} is forbidden");
                return Forbid();
            }
            var imageEntity = _imageRepository.Get(accountId);
            if (imageEntity != null)
            {
                _imageRepository.Delete(imageEntity);
            }
            var image = _mapper.Map(req, accountId);
            _imageRepository.AddImage(image);

            return Ok(image.Id);
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
            if (entity.Account.Id != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            _imageRepository.Delete(entity);
            return NoContent();
        }
    }
}
