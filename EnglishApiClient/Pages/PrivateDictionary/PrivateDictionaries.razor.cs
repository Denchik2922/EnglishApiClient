using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Models;

namespace EnglishApiClient.Pages.PrivateDictionary
{
    public partial class PrivateDictionaries
    {
        private ICollection<EnglishDictionary> englishDictionaries;

        private ICollection<Tag> tags;

        [Inject]
        public ITagHttpService TagService { get; set; }
        [Inject]
        public IDictionaryHttpService DictionaryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetDictionaries();
            await GetTags();
        }

        private async Task GetDictionaries()
        {
            englishDictionaries = await DictionaryService.GetPrivateDictionaries();
        }

        private async Task GetTags()
        {
            tags = await TagService.GetAll();
        }
        public void NavigateToDictionary(int id)
        {
            NavigationManager.NavigateTo($"dictionary/{id}");
        }
    }
}
