using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Components
{
    public partial class DictionaryFilters
    {
        private Timer _timer;
        public SearchParameters SearchParam { get; set; } = new SearchParameters();

        [Parameter]
        public EventCallback<SearchParameters> OnSearchChanged { get; set; }
        public ICollection<Tag> Tags { get; set; }

        [Inject]
        private ITagHttpService _tagService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetTags();
        }

        private string StyleTag(string tagName)
        {
            if (SearchParam.SearchTags.Any(t => t == tagName))
            {
                return "btn-warning text-dark";

            }
            else
            {
                return "btn-primary text-white";
            }
        }

        private void SetOrRemoveTag(string tagName)
        {
            if (SearchParam.SearchTags.Any(t => t == tagName))
            {
                SearchParam.SearchTags.Remove(tagName);
            }
            else
            {
                SearchParam.SearchTags.Add(tagName);
            }
            SearchChanged();
        }

        private void SearchChanged()
        {
            if (_timer != null)
                _timer.Dispose();
            _timer = new Timer(OnTimerElapsed, null, 800, 0);
        }
        private void OnTimerElapsed(object sender)
        {
            OnSearchChanged.InvokeAsync(SearchParam);
            _timer.Dispose();
        }

        private async Task GetTags()
        {
            Tags = await _tagService.GetAllWithoutPage();
        }
    }
}
