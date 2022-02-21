using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
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
        public int CountWords { get; set; }
        public ICollection<TypeOfTesting> _testingTypes;
        private string CurrentUser = "";
        private EnglishDictionary _dictionary;

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private ITypeOfTestingHttpService _typeService { get; set; }

        [Inject]
        private AuthenticationStateProvider _authProvider { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private IDictionaryHttpService _dictionaryService { get; set; }

        private bool IsCurrentUser
        {
            get {
                return _dictionary.UserId.Contains(CurrentUser);
            }
        }

        private string IsDisabledTest()
        {
            if (CountWords < 4)
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
            await GetTypeOfTesting();
        }

        private async Task GetTypeOfTesting()
        {
            _testingTypes = await _typeService.GetAllWithoutPage();
        }

        private void SetCountWords(int count)
        {
            CountWords = count;
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
    }
}
