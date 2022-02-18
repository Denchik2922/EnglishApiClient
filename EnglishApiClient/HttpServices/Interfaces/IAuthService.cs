using EnglishApiClient.Dtos.Auth;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ChangePassword(ChangePasswordModel passwordModel);
        Task<bool> Login(LoginModel loginModel);
        Task<bool> LoginGoogle(ExternalAuthModel externalAuthModel);
        Task Logout();
        Task<bool> Register(RegisterModel registerModel);
        Task<string> RefreshToken();

    }
}