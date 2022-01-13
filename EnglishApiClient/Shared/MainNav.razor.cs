using EnglishApiClient.Infrastructure;
using EnglishApiClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EnglishApiClient.Shared
{
    public partial class MainNav : IDisposable
    {
        [Inject]
        public IAuthService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }

        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
        }

        public async Task LogoutUser()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/login");
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
