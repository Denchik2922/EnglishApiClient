using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.Helpers;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace EnglishApiClient.HttpServices
{
        public abstract class GenericHttpService<T> : IGenericHttpService<T> where T : class
        {
            protected readonly HttpClient httpClient;
            protected readonly string requestString;
            public GenericHttpService(HttpClient client, string requestUri)
            {
                httpClient = client;
                requestString = requestUri;
            }

            public async Task<T> GetById(int id)
            {
                return await httpClient.GetFromJsonAsync<T>($"{requestString}/{id}");
            }

            public async Task<PagingResponse<T>> GetAll(PaginationParameters parameters)
            {
                var queryStringParam = CustomQueryHelper.GetQueryString(parameters);

                var response = await httpClient.GetAsync(QueryHelpers.AddQueryString(requestString, queryStringParam));

                var pagingResponse = new PagingResponse<T>
                {
                    Items = await response.Content.ReadFromJsonAsync<List<T>>(),
                    MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First())
                };
                return pagingResponse;
            }

            public virtual async Task<bool> Create(T entity)
            {
                var response = await httpClient.PostAsJsonAsync(requestString, entity);
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> Update( T entity)
            {
                var response = await httpClient.PutAsJsonAsync($"{requestString}", entity);
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> Delete(int id)
            {
                var response = await httpClient.DeleteAsync($"{requestString}/?id={id}");
                return response.IsSuccessStatusCode;
            }
        }
}
