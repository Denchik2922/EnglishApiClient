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

        [Parameter]
        public bool IsCurrentUser { get; set; }

        [Parameter]
        public EventCallback<int> EditCountWords { get; set; }

        private ICollection<WordModel> Words;
        public MetaData MetaData { get; set; } = new MetaData();
        private PaginationParameters parameters = new PaginationParameters() { PageSize = 7 };

        private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
        {
            {"Name", "name" },
            {"Name DESC", "name desc" }
        };

        private string GetPercent(int num)
        {
            double score = ((double)num / 10) * 100;
            return Math.Round(score, 0).ToString();
        }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private IWordHttpService _wordService { get; set; }

        [Inject]
        private ILearnedWordHttpService _learnedWordService { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetWords();
        }

        private async Task SetLearnedWord(LearnedWord learnedWord)
        {
            learnedWord.IsLearned = !learnedWord.IsLearned;
            await _learnedWordService.SetLearned(learnedWord);
            await GetWords();
        }

        private async Task GetNotLearnedWords()
        {
           var notLearnedWords = await _learnedWordService.GetAllForUser(DictionaryId);
            var unlearnedWords = notLearnedWords.Count(l => l.IsLearned == false);
            await EditCountWords.InvokeAsync(unlearnedWords);
        }

        private async Task GetWords()
        {
            var pagingResponse = await _wordService.GetWordsForDictionary(DictionaryId, parameters);
            Words = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            await GetNotLearnedWords();
        }

        private async Task SearchChanged(string searchTerm)
        {
            parameters.PageNumber = 1;
            parameters.SearchParameters.SearchTerm = searchTerm;
            await GetWords();
        }

        private async Task SortChanged(string orderBy)
        {
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
