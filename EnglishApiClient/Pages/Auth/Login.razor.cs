using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;

namespace EnglishApiClient.Pages.Auth
{
    public partial class Login
    {
        private LoginModel _loginModel = new LoginModel();

        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private async void LoginUser()
        {
            var result = await AuthService.Login(_loginModel);
            if (result)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
