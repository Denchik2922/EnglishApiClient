using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.PrivateDictionary
{
    public partial class PrivateDictionaries
    {
        private ICollection<EnglishDictionary> englishDictionaries;
        public MetaData MetaData { get; set; } = new MetaData();
        private PaginationParameters parameters = new PaginationParameters();

        [Inject]
        public IDictionaryHttpService DictionaryService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetDictionaries();
        }
        private async Task GetDictionaries()
        {
            var pagingResponse = await DictionaryService.GetPrivateDictionaries(parameters);
            englishDictionaries = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SelectedPage(int page)
        {
            parameters.PageNumber = page;
            await GetDictionaries();
        }
    }
}
