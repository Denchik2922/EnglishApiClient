using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.TagPage
{
    public partial class Tags
    {
        private ICollection<Tag> _tags = new List<Tag>();
        public MetaData MetaData { get; set; } = new MetaData();
        private PaginationParameters parameters = new PaginationParameters() { PageSize = 10 };

        [Inject]
        private ITagHttpService _tagService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetTags();
        }
        private async Task GetTags()
        {
            var pagingResponse = await _tagService.GetAll(parameters);
            _tags = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SelectedPage(int page)
        {
            parameters.PageNumber = page;
            await GetTags();
        }
    }
}
