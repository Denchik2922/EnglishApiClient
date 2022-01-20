using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class DictionaryAdd
    {
        private EnglishDictionary _dictionary = new EnglishDictionary();
        private ICollection<Tag> _tags = new List<Tag>();
        private int[] _selectedTags { get; set; } = new int[] {};

        [Inject]
        private IDictionaryHttpService _dictionaryService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private ITagHttpService _tagService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private AuthenticationStateProvider _authProvider { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetTags();
        }

        private async Task GetTags()
        {
            _tags = await _tagService.GetAll();
        }

        private async void AddDictionary()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            _dictionary.UserId= userId;
            _dictionary.Tags = _selectedTags.Select(t => new Tag() { Id = t }).ToList();
            var result = await _dictionaryService.Create(_dictionary);
            if (result)
            {
                _toastService.ShowSuccess($"Dictionary with name {_dictionary.Name} added successfully!");
                _navigation.NavigateTo("/");
            }
        }
    }
}
