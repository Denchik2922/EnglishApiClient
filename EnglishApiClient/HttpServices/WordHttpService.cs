using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices
{
    public class WordHttpService : GenericHttpService<WordModel>, IWordHttpService
    {
        private const string API_BASE_URL = "https://localhost:5001";

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

        public async Task<string> UploadWordImage(MultipartFormDataContent content)
        {
            var postResult = await httpClient.PostAsync("upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (String.IsNullOrEmpty(postContent))
            {
                throw new ApplicationException(postContent);
            }
            else
            {

                var imgUrl = Path.Combine(API_BASE_URL, postContent);
                return imgUrl;
            }
        }
    }
}
