﻿using Blazored.LocalStorage;
using EnglishApiClient.Infrastructure;
using EnglishApiClient.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnglishApiClient.Services
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

        public async Task<bool> Login(LoginModel loginModel)
        {

            var response = await _httpClient.PostAsJsonAsync("auth/login", loginModel);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

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
            var response = await _httpClient.PostAsJsonAsync("auth/refresh", new { Token = token, RefreshToken = refreshToken });
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong during the refresh token action");

            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return result.Token;
        }

    }
}
