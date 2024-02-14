using System.ComponentModel.DataAnnotations;
using UserinfoApp.API.Validators;

namespace UserinfoApp.API.DTOs.Request
{
    public class ImageUploadRequestDto
    {
        ///// <summary>
        ///// Name of the image
        ///// </summary>
        //[Required]
        //[StringLength(100)]
        //public string Name { get; set; } = null!;

        ///// <summary>
        ///// Image description
        ///// </summary>
        //[Required]
        //[StringLength(1000)]
        //public string Description { get; set; } = null!;

        /// <summary>
        /// Image in bytes
        /// </summary>
        [MaxFileSize(15 * 1024 * 1024)]
        [AllowedExtensions([".png", ".jpg"])]
        public IFormFile Image { get; set; } = null!;
    }
}
