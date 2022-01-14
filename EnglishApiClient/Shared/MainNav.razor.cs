using EnglishApiClient.Infrastructure;
using EnglishApiClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EnglishApiClient.Shared
{
    public partial class MainNav
    {
        [Inject]
        public IAuthService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }
        public async Task LogoutUser()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/login");
        }
    }
}
