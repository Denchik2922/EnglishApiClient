using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Pages.UserPages
{
    public partial class Users
    {
        private ICollection<User> _users = new List<User>();
        public MetaData MetaData { get; set; } = new MetaData();
        private PaginationParameters parameters = new PaginationParameters() { PageSize = 10 };

        private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
        {
            {"Name", "username" },
            {"Name DESC", "username desc" },
            {"Id", "id" },
            {"Id DESC", "id desc" }
        };

        [Inject]
        private IUserHttpService _userService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetUsers();
        }
        private async Task GetUsers()
        {
            var pagingResponse = await _userService.GetAll(parameters);
            _users = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            parameters.PageNumber = 1;
            parameters.SearchParameters.SearchTerm = searchTerm;
            await GetUsers();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            parameters.OrderBy = orderBy;
            await GetUsers();
        }

        private async Task SelectedPage(int page)
        {
            parameters.PageNumber = page;
            await GetUsers();
        }
    }
}
