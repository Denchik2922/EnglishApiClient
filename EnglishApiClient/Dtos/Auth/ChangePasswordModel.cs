﻿using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Auth
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "User id is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Old password is required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewConfirmPassword { get; set; }
    }
}
