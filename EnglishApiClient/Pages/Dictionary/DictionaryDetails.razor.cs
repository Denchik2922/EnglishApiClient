using Blazored.Toast.Services;
using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class DictionaryDetails
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private IDictionaryHttpService _dictionaryService { get; set; }

        private EnglishDictionary _dictionary = new EnglishDictionary();

        protected override async Task OnInitializedAsync()
        {
            await GetDictionary();
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

        private void AddNewWord()
        {
            _navigation.NavigateTo($"/add-word/{Id}");
        }

        private void UpdateDictionary()
        {
            _navigation.NavigateTo($"/dictionary-update/{Id}");
        }

        private async Task PlaySound()
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", "roar");
        }
    }
}
