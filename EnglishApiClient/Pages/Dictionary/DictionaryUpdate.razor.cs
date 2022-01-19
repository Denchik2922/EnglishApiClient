using Blazored.Toast.Services;
using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class DictionaryUpdate
    {
        [Parameter]
        public int Id { get; set; }
        private ICollection<Tag> _tags = new List<Tag>();
        private EnglishDictionary _dictionary = new EnglishDictionary();
        private int[] _selectedTags { get; set; } = new int[] { };

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private IDictionaryHttpService _dictionaryService { get; set; }

        [Inject]
        private ITagHttpService _tagService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetDictionaries();
            await GetTags();
            SetTags();
        }

        private void SetTags()
        {
            _selectedTags = _dictionary.Tags.Select(x => x.Id).ToArray();
        }

        private async Task GetDictionaries()
        {
            _dictionary = await _dictionaryService.GetById(Id);
        }

        private async Task GetTags()
        {
            _tags = await _tagService.GetAll();
        }

        private async void UpdateDictionary()
        {
            _dictionary.Tags = _selectedTags.Select(t => new Tag() { Id = t }).ToList();
            var result = await _dictionaryService.Update(_dictionary);
            if (result)
            {
                _toastService.ShowSuccess("Dictionary edited successfully!");
                _navigation.NavigateTo("/");
            }
        }
    }
}
