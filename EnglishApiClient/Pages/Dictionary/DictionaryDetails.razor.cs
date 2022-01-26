using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class DictionaryDetails
    {
        [Parameter]
        public int Id { get; set; }

        private string CurrentUser = "";
        private EnglishDictionary _dictionary;

        private string SpellingTest
        {
            get
            {
                return _dictionary.SpellingTestUsers.FirstOrDefault(t => t.UserId == CurrentUser)?.Score.ToString();
            }
        }

        private string MatchingTest
        {
            get
            {
                return _dictionary.MatchingTestUsers.FirstOrDefault(t => t.UserId == CurrentUser)?.Score.ToString();
            }
        }


        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private AuthenticationStateProvider _authProvider { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private IDictionaryHttpService _dictionaryService { get; set; }

        private string IsDisabledTest()
        {
            if (_dictionary.Words.Count() < 4)
            {
                return "disabled";
            }
            else
            {
                return "";
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetDictionary();
            await GetCurrentUser();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private async Task GetDictionary()
        {
            _dictionary = await _dictionaryService.GetById(Id);
        }

        private void RouteEditWord(int wordId)
        {
            if (_dictionary.UserId.Contains(CurrentUser))
            {
                _navigation.NavigateTo($"edit-word/{wordId}");
            }
        }

        private async Task DeleteDictionary()
        {
            var confirmed = await _jsRuntime.InvokeAsync<bool>("confirm",
                                                                $"Are you sure you want to delete dictionary with name {_dictionary.Name}?");
            if (confirmed)
            {
                bool result = await _dictionaryService.Delete(Id);
                if (result)
                {
                    _toastService.ShowSuccess("Dictionary deleted successfully!");
                    _navigation.NavigateTo("/");
                }
                else
                {
                    _toastService.ShowError("Dictionary deleted failed!");
                }
            }
        }

        private async Task PlaySound(int id)
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", $"sound-{id}");
        }
    }
}
