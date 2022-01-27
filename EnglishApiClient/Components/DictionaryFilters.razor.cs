using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Components
{
    public partial class DictionaryFilters
    {
        public ICollection<Tag> Tags { get; set; }

        [Inject]
        private ITagHttpService _tagService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetTags();
        }

        private async Task GetTags()
        {
            Tags = await _tagService.GetAllWithoutPage();
        }
    }
}
