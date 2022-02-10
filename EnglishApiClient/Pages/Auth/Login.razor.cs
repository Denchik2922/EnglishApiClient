using Blazored.SessionStorage;
using EnglishApiClient.Dtos.Auth;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using EnglishApiClient.Infrastructure.Helpers;

namespace EnglishApiClient.Pages.Auth
{
    public partial class Login
    {
        private LoginModel _loginModel = new LoginModel();
        private string RedirectUrl;
        private string PkceSessionKey;

        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ISessionStorageService sessionStorage {get;set;}

        [Inject]
        public IGoogleOAuthService googleOAuth { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        protected override void OnInitialized()
        {
            RedirectUrl = Configuration.GetSection("GoogleAuth")["RedirectUrl"];
            PkceSessionKey = Configuration.GetSection("GoogleAuth")["PkceSessionKey"];    
        }

        public async void GoogleLogIn()
        {
            var codeVerifier = Guid.NewGuid().ToString();
            var codeChellange = HashHelper.ComputeHash(codeVerifier);

            await sessionStorage.SetItemAsStringAsync(PkceSessionKey, codeVerifier);

            var url = googleOAuth.GenerateOAuthRequestUrl(RedirectUrl, codeChellange);
            NavigationManager.NavigateTo(url);
        }

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
