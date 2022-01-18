﻿using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;

namespace EnglishApiClient.Pages.Auth
{
    public partial class Register
    {
        private RegisterModel _registerModel = new RegisterModel();
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
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
