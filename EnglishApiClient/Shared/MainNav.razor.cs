using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace EnglishApiClient.Shared
{
    public partial class MainNav
    {
        [Inject]
        private IAuthService AuthenticationService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public async Task LogoutUser()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/login");
        }
    }
}
