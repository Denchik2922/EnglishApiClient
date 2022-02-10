using Blazored.LocalStorage;
using EnglishApiClient.Dtos.Auth;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure;
using EnglishApiClient.Infrastructure.ErrorFeatures;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", registerModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginGoogle(ExternalAuthModel externalAuthModel)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/external-login", externalAuthModel);
            var result = await response.Content.ReadFromJsonAsync<UserTokens>();

            if (!response.IsSuccessStatusCode)
            {
                return response.IsSuccessStatusCode;
            }

            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

            ((AuthStateProvider)_authenticationStateProvider).UserAuthentication(result.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Login(LoginModel loginModel)
        {

            var response = await _httpClient.PostAsJsonAsync("auth/login", loginModel);
            var result = await response.Content.ReadFromJsonAsync<UserTokens>();

            if (!response.IsSuccessStatusCode)
            {
                return response.IsSuccessStatusCode;
            }

            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

            ((AuthStateProvider)_authenticationStateProvider).UserAuthentication(result.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return response.IsSuccessStatusCode;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            ((AuthStateProvider)_authenticationStateProvider).UserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/refresh", new RefreshTokenDto { Token = token, RefreshToken = refreshToken });
                var result = await response.Content.ReadFromJsonAsync<UserTokens>();  
                
                await _localStorage.SetItemAsync("authToken", result.Token);
                await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

                Console.WriteLine(result.RefreshToken);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return result.Token;
            }
            catch (HttpResponseException)
            {
                await Logout();
                throw;
            }
            
        }

    }
}
