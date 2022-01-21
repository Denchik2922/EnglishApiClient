using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.TagPage
{
    public partial class Tags
    {
        private ICollection<Tag> _tags = new List<Tag>();

        [Inject]
        private ITagHttpService _tagService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetTags();
        }
        private async Task GetTags()
        {
            _tags = await _tagService.GetAll();
        }
    }
}
