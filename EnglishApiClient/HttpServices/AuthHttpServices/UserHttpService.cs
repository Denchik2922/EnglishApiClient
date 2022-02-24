using EnglishApiClient.Dtos.Auth;
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
        private readonly string requestString = "user";
        private readonly HttpClient _httpClient;
        public UserHttpService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<PagingResponse<User>> GetAll(PaginationParameters parameters)
        {
            var queryStringParam = CustomQueryHelper.GetQueryString(parameters);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(requestString, queryStringParam));

            var pagingResponse = new PagingResponse<User>
            {
                Items = await response.Content.ReadFromJsonAsync<List<User>>(),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public async Task<User> GetById(string id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"{requestString}/{id}");
        }

        public async Task<ICollection<UserRole>> GetUserRoles()
        {
            var response = await _httpClient.GetAsync($"{requestString}/roles");
            return await response.Content.ReadFromJsonAsync<ICollection<UserRole>>();
        }

        public async Task<bool> Create(UserCreateModel entity)
        {
            var response = await _httpClient.PostAsJsonAsync(requestString, entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var response = await _httpClient.DeleteAsync($"{requestString}/?id={id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(User entity)
        {
            var response = await _httpClient.PutAsJsonAsync(requestString, entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SetNewPassword(SetPasswordModel passwordModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"{requestString}/set-password", passwordModel);
            return response.IsSuccessStatusCode;
        }
    }
}
