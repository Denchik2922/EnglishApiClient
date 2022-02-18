using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EnglishApiClient.Pages.UserPages
{
    public partial class UserEdit
    {
        [Parameter]
        public Guid UserId { get; set; }

        private User _user;

        private ICollection<UserRole> _roles;

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IUserHttpService _userService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetUser();
            await GetRoles();
        }

        private async Task GetUser()
        {
            _user = await _userService.GetById(UserId.ToString());
        }

        private async Task GetRoles()
        {
            _roles = await _userService.GetUserRoles();
        }

        private Dictionary<string, object> ChangeRole(string role)
        {
            var dict = new Dictionary<string, object>();
            if (_user.Roles.Any(r => r == role))
            {
               dict.Add("checked", "checked");
            }
            return dict;
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

        private async Task DeleteUser()
        {
            var confirmed = await _jsRuntime.InvokeAsync<bool>("confirm",
                                                                $"Are you sure you want to delete User with name {_user.UserName}?");
            if (confirmed)
            {
                bool result = await _userService.Delete(UserId.ToString());
                if (result)
                {
                    _toastService.ShowSuccess($"User with name {_user.UserName} deleted successfully!");
                    _navigation.NavigateTo("/");
                }
                else
                {
                    _toastService.ShowError("User deleted failed!");
                }
            }
        }

        private async void EditUser()
        {
            var result = await _userService.Edit(_user);
            if (result)
            {
                _toastService.ShowSuccess($"User with name {_user.UserName} updated successfully!");
                _navigation.NavigateTo("/users");
            }
        }
    }
}
