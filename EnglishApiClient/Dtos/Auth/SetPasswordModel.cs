using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Auth
{
    public class SetPasswordModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewConfirmPassword { get; set; }
    }
}
