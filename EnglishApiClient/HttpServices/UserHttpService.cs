using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.Helpers;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace EnglishApiClient.HttpServices
{
    public class UserHttpService : IUserHttpService
    {
        private readonly HttpClient _httpClient;
        public UserHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagingResponse<User>> GetAll(PaginationParameters parameters)
        {
            var queryStringParam = CustomQueryHelper.GetQueryString(parameters);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("user", queryStringParam));

            var pagingResponse = new PagingResponse<User>
            {
                Items = await response.Content.ReadFromJsonAsync<List<User>>(),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }
    }
}
