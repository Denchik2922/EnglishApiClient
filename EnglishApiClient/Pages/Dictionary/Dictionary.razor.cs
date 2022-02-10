using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.Dictionary
{
    public partial class Dictionary
    {
        private List<EnglishDictionary> _englishDictionaries = new List<EnglishDictionary>();
        public MetaData MetaData { get; set; } = new MetaData();
        private PaginationParameters parameters = new PaginationParameters() { PageSize = 5 };

        private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
        {
            {"Name", "name" },
            {"Name DESC", "name desc" }
        };

        [Inject]
        private IDictionaryHttpService _dictionaryService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetDictionaries();

        }

        private async Task GetDictionaries()
        {
            var pagingResponse = await _dictionaryService.GetPublicDictionaries(parameters);
            _englishDictionaries = pagingResponse.Items;
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
