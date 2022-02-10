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
        private PaginationParameters parameters = new PaginationParameters() { PageSize = 5 };

        private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
        {
            {"Name", "name" },
            {"Name DESC", "name desc" }
        };

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

        private async Task SearchChanged(SearchParameters searchParameters)
        {
            parameters.PageNumber = 1;
            parameters.SearchParameters = searchParameters;
            await GetDictionaries();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            parameters.OrderBy = orderBy;
            await GetDictionaries();
        }

        private async Task SelectedPage(int page)
        {
            parameters.PageNumber = page;
            await GetDictionaries();
        }
    }
}
