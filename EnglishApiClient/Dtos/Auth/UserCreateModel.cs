using EnglishApiClient.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Auth
{
    public class UserCreateModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [EnsureMinimumElements(1, ErrorMessage = "At least one role is required")]
        public ICollection<string> Roles { get; set; } = new List<string>();

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
