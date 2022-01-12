using Blazored.LocalStorage;
using EnglishApiClient.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EnglishApiClient.Services
{
    public class AuthService
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
            var response = await _httpClient.PostAsJsonAsync("register", registerModel);
            return response.IsSuccessStatusCode;
            
        }


        public async Task<AuthResponse> Login(LoginModel loginModel)
        {
            
            var response = await _httpClient.PostAsJsonAsync("login", loginModel);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            
            if (!response.IsSuccessStatusCode)
            {
                return result;
            }

            await _localStorage.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)_authenticationStateProvider).UserAuthentication(result.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return result;
        }


    }
}
