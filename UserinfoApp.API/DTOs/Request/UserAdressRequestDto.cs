using System.ComponentModel.DataAnnotations;

namespace UserinfoApp.API.DTOs.Request
{
    public class UserAdressRequestDto
    {
        /// <summary>
        /// User City name
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string? City { get; set; }

        /// <summary>
        /// User street name
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string? Street { get; set; }

        /// <summary>
        /// User house number
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string? HouseNumber { get; set; }

        /// <summary>
        /// User flat number
        /// </summary>
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string? FlatNumber { get; set; }
    }
}
