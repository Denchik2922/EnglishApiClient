using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EnglishApiClient.Components
{
    public partial class ListWords
    {
        [Parameter]
        public int DictionaryId { get; set; }

        private ICollection<WordModel> Words;
        public MetaData MetaData { get; set; } = new MetaData();
        private PaginationParameters parameters = new PaginationParameters() { PageSize = 7 };

        private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
        {
            {"Name", "name" },
            {"Name DESC", "name desc" }
        };

        [Inject]
        private IWordHttpService _wordService { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Parameter]
        public EventCallback<int> OnEditWord { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetWords();
        }

        private async Task GetWords()
        {
            var pagingResponse = await _wordService.GetWordsForDictionary(DictionaryId, parameters);
            Words = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            parameters.PageNumber = 1;
            parameters.SearchParameters.SearchTerm = searchTerm;
            await GetWords();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            parameters.OrderBy = orderBy;
            await GetWords();
        }

        private async Task SelectedPage(int page)
        {
            parameters.PageNumber = page;
            await GetWords();
        }

        private async Task PlaySound(int id)
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", $"sound-{id}");
        }
    }
}
