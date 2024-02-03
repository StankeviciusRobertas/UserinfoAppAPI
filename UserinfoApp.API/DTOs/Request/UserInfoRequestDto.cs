using System.ComponentModel.DataAnnotations;
using UserinfoApp.API.Validators;

namespace UserinfoApp.API.DTOs.Request
{
    public class UserInfoRequestDto
    {
        /// <summary>
        /// User First name
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string? Name { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string? LastName { get; set; }

        /// <summary>
        /// User personal code
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(10000000000, 99999999999, ErrorMessage = "Please enter a valid {0}.")]
        public int? PersonalCode { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [Phone]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid {0}.")]
        public string? Email { get; set; }
    }
}
