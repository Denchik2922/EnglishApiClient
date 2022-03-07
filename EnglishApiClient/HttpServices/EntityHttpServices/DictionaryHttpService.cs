using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.Helpers;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace EnglishApiClient.HttpServices.EntityHttpServices
{
    public class DictionaryHttpService : GenericHttpService<EnglishDictionary>, IDictionaryHttpService
    {
        public DictionaryHttpService(HttpClient httpClient) : base(httpClient, "dictionary") { }

        public async Task<PagingResponse<EnglishDictionary>> GetPublicDictionaries(PaginationParameters parameters)
        {
            var queryStringParam = CustomQueryHelper.GetQueryString(parameters);

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString($"{requestString}/public-dictionaries", queryStringParam));
            
            return await CustomQueryHelper.GetPaginationResponse<EnglishDictionary>(response);
        }

        public async Task<PagingResponse<EnglishDictionary>> GetPrivateDictionaries(PaginationParameters parameters)
        {
            var queryStringParam = CustomQueryHelper.GetQueryString(parameters);

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString($"{requestString}/private-dictionaries", queryStringParam));
            
            return await CustomQueryHelper.GetPaginationResponse<EnglishDictionary>(response);
        }
    }
}
