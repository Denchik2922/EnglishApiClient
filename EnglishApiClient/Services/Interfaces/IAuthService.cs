using Models;
using System.Threading.Tasks;

namespace EnglishApiClient.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(LoginModel loginModel);
        Task Logout();
        Task<bool> Register(RegisterModel registerModel);
        Task<string> RefreshToken();

    }
}