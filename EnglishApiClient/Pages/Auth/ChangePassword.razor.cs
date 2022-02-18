using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Auth;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EnglishApiClient.Pages.Auth
{
    public partial class ChangePassword
    {

        private ChangePasswordModel _passwordModel = new ChangePasswordModel();

        [Inject]
        private IAuthService AuthService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private AuthenticationStateProvider _authProvider { get; set; }

        private async void Change()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _passwordModel.UserId = userId;
            var result = await AuthService.ChangePassword(_passwordModel);
            if (result)
            {
                _toastService.ShowSuccess($"User password changed successfully!");
                _navigation.NavigateTo("/");
            }
        }
    }
}
