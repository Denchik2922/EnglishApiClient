using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices
{
    public class WordHttpService : GenericHttpService<WordModel>, IWordHttpService
    {
        public WordHttpService(HttpClient httpClient) : base(httpClient, "word") { }

        public async Task<PagingResponse<WordModel>> GetWordsForDictionary(int dictionaryId, PaginationParameters parameters)
        {
            var queryStringParam = GetQueryString(parameters);

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString($"{requestString}/dictionary-words/{dictionaryId}", queryStringParam));

            return await GetPaginationResponse(response);
        }

        public async Task<WordInformation> GenerateWordInformation(string wordName)
        {
            return await httpClient.GetFromJsonAsync<WordInformation>($"{requestString}/generate-word/{wordName}");            
        }

        public async Task<ICollection<WordPhoto>> GetWordPictures(string wordName)
        {
            return await httpClient.GetFromJsonAsync<ICollection<WordPhoto>>($"{requestString}/word-pictures/{wordName}");
        }
    }
}
