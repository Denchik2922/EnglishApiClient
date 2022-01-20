using EnglishApiClient.Dtos.Auth;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(LoginModel loginModel);
        Task Logout();
        Task<bool> Register(RegisterModel registerModel);
        Task<string> RefreshToken();

    }
}