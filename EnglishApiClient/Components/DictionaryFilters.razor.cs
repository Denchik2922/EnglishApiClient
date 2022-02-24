using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Components
{
    public partial class DictionaryFilters
    {
        private Timer _timer;

        [Parameter]
        public EventCallback<ICollection<string>> OnTagsChanged { get; set; }

        public ICollection<string> SelectedTags { get; set; } = new List<string>();
        public ICollection<Tag> Tags { get; set; }

        [Inject]
        private ITagHttpService _tagService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetTags();
        }

        private string StyleTag(string tagName)
        {
            if (SelectedTags.Any(t => t == tagName))
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
            if (SelectedTags.Any(t => t == tagName))
            {
                SelectedTags.Remove(tagName);
            }
            else
            {
                SelectedTags.Add(tagName);
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
            OnTagsChanged.InvokeAsync(SelectedTags);
            _timer.Dispose();
        }

        private async Task GetTags()
        {
            Tags = await _tagService.GetAllWithoutPage();
        }
    }
}
