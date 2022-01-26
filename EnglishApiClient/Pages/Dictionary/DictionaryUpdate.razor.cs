using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class DictionaryUpdate
    {
        [Parameter]
        public int Id { get; set; }

        private ICollection<Tag> _tags = new List<Tag>();
        private EnglishDictionary _dictionary = new EnglishDictionary();

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
                var itemToRemove = _dictionary.Tags.Single(t => t.Id == tag.Id);
                _dictionary.Tags.Remove(itemToRemove);
            }
            else
            {
                _dictionary.Tags.Add(tag);
            }
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
            var result = await _dictionaryService.Update(_dictionary);
            if (result)
            {
                _toastService.ShowSuccess("Dictionary edited successfully!");
                _navigation.NavigateTo("/");
            }
        }
    }
}
