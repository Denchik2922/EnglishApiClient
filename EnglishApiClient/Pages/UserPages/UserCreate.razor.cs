using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Auth;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.UserPages
{
    public partial class UserCreate
    {
        private UserCreateModel _user = new UserCreateModel();

        private ICollection<UserRole> _roles;

        [Inject]
        private IUserHttpService _userService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetRoles();
        }

        private async Task GetRoles()
        {
            _roles = await _userService.GetUserRoles();
        }

        private void SetOrRemoveRole(string role)
        {
            if (_user.Roles.Any(r => r == role))
            {
                _user.Roles.Remove(role);
            }
            else
            {
                _user.Roles.Add(role);
            }
        }

        private async void AddUser()
        {
            var result = await _userService.Create(_user);
            if (result)
            {
                _toastService.ShowSuccess($"User with name {_user.UserName} added successfully!");
                _navigation.NavigateTo("/users");
            }
        }
    }
}
