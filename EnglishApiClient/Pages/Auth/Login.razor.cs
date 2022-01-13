using EnglishApiClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;

namespace EnglishApiClient.Pages.Auth
{
    public partial class Login
    {
        private LoginModel _loginModel = new LoginModel();
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
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
