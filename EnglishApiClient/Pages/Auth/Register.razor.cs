using EnglishApiClient.Dtos.Auth;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.Auth
{
    public partial class Register
    {
        private RegisterModel _registerModel = new RegisterModel();

        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private async void RegisterUser()
        {
            var result = await AuthService.Register(_registerModel);
            if (result)
            {
                NavigationManager.NavigateTo("/login");
            }
        }
    }
}
