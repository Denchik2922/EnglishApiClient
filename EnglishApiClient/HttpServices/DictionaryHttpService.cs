using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace EnglishApiClient.HttpServices
{
    public class DictionaryHttpService : GenericHttpService<EnglishDictionary>, IDictionaryHttpService
    {
        public DictionaryHttpService(HttpClient httpClient) : base(httpClient, "dictionary") { }

        public async Task<PagingResponse<EnglishDictionary>> GetPublicDictionaries(PaginationParameters parameters)
        {

            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString()
            };
            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString($"{requestString}/public-dictionaries", queryStringParam));
            var pagingResponse = new PagingResponse<EnglishDictionary>
            {
                Items = await response.Content.ReadFromJsonAsync<List<EnglishDictionary>>(),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<EnglishDictionary>> GetPrivateDictionaries(PaginationParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString()
            };
            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString($"{requestString}/private-dictionaries", queryStringParam));
            var pagingResponse = new PagingResponse<EnglishDictionary>
            {
                Items = await response.Content.ReadFromJsonAsync<List<EnglishDictionary>>(),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

    }
}
