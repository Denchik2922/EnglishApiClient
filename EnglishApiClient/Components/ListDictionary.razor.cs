using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Components
{
    public partial class ListDictionary
    {

        [Parameter]
        public bool IsPrivateDictionaries { get; set; }

        public ICollection<EnglishDictionary> Dictionaries;
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
            PagingResponse<EnglishDictionary> pagingResponse;
            if(IsPrivateDictionaries == true)
            {
                pagingResponse = await _dictionaryService.GetPrivateDictionaries(parameters);
            }
            else
            {
                pagingResponse = await _dictionaryService.GetPublicDictionaries(parameters);
            }
            Dictionaries = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            parameters.PageNumber = 1;
            parameters.SearchParameters.SearchTerm = searchTerm;
            await GetDictionaries();
        }

        private async Task TagsChanged(ICollection<string> searchTags)
        {
            parameters.PageNumber = 1;
            parameters.SearchParameters.SearchTags = searchTags;
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
