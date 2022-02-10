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

        private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
        {
            {"Name", "name" },
            {"Name DESC", "name desc" },
            {"Id", "id" },
            {"Id DESC", "id desc" }
        };

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

        private async Task SearchChanged(string searchTerm)
        {
            parameters.PageNumber = 1;
            parameters.SearchParameters.SearchTerm = searchTerm;
            await GetTags();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            parameters.OrderBy = orderBy;
            await GetTags();
        }

        private async Task SelectedPage(int page)
        {
            parameters.PageNumber = page;
            await GetTags();
        }
    }
}
