using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Auth;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.UserPages
{
    public partial class ChangeUserPassword
    {
        [Parameter]
        public Guid UserId { get; set; }

        private SetPasswordModel _user = new SetPasswordModel();

        [Inject]
        private IUserHttpService _userService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        private async void SetPassword()
        {
            _user.UserId = UserId.ToString();
            var result = await _userService.SetNewPassword(_user);
            if (result)
            {
                _toastService.ShowSuccess($"User with id {_user.UserId} set password successfully!");
                _navigation.NavigateTo("/users");
            }
        }
    }
}
