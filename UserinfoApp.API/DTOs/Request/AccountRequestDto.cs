using System.ComponentModel.DataAnnotations;
using UserinfoApp.API.Validators;

namespace UserinfoApp.API.DTOs.Request
{
    // The main difference between a "public record" and a "public class" in this context
    // is the automatic generation of certain members for the record,
    // providing a more concise and expressive way to define immutable data structures.
    public record AccountRequestDto
    {
        /// <summary>
        /// Username of the account
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? UserName { get; set; }

        /// <summary>
        /// Password of the account
        /// </summary>
        [PasswordValidator]
        public string? Password { get; set; }

        ///// <summary>
        ///// Email of the account
        ///// </summary>
        //[EmailAddress]
        //public string? Email { get; set; }

        /// <summary>
        /// Role of the account
        /// </summary>
        [RoleValidator]
        public string? Role { get; set; }
    }
}
