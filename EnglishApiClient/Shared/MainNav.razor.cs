using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

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
