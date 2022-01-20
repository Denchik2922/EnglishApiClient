using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Auth
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
