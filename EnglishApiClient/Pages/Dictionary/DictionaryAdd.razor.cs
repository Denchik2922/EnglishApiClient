using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class DictionaryAdd
    {
        private EnglishDictionary _dictionary = new EnglishDictionary();
        private ICollection<Tag> _tags = new List<Tag>();

        [Inject]
        private IDictionaryHttpService _dictionaryService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

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

        private string StyleTag(Tag tag)
        {
            if (_dictionary.Tags.Any(t => t.Name == tag.Name))
            {
                return "btn-warning text-dark";
                
            }
            else
            {
                return "btn-primary text-white";
            }
        }

        private void SetOrRemoveTag(Tag tag)
        {
            if (_dictionary.Tags.Any(t => t.Name == tag.Name))
            {
                _dictionary.Tags.Remove(tag);
            }
            else
            {
                _dictionary.Tags.Add(tag);
            }
        }

        private async Task GetTags()
        {
            _tags = await _tagService.GetAllWithoutPage();
        }

        private async void AddDictionary()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _dictionary.UserId = userId;

            var result = await _dictionaryService.Create(_dictionary);
            if (result)
            {
                _toastService.ShowSuccess($"Dictionary with name {_dictionary.Name} added successfully!");
                await JSRuntime.InvokeVoidAsync("history.back");
            }
        }
    }
}
