﻿using System.ComponentModel.DataAnnotations;
using UserinfoApp.API.Validators;

namespace UserinfoApp.API.DTOs.Request
{
    /// <summary>
    /// User account login request
    /// </summary>
    public record LoginRequestDto
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
    }
}
