using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.Helpers;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tewr.Blazor.FileReader;

namespace EnglishApiClient.HttpServices.EntityHttpServices
{
    public class WordHttpService : GenericHttpService<WordModel>, IWordHttpService
    {
        public WordHttpService(HttpClient httpClient) : base(httpClient, "word") { }

        public async Task<PagingResponse<WordModel>> GetWordsForDictionary(int dictionaryId, PaginationParameters parameters)
        {
            var queryStringParam = CustomQueryHelper.GetQueryString(parameters);

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString($"{requestString}/dictionary-words/{dictionaryId}", queryStringParam));

            return await CustomQueryHelper.GetPaginationResponse<WordModel>(response);
        }

        public async Task<WordInformation> GenerateWordInformation(string wordName)
        {
            return await httpClient.GetFromJsonAsync<WordInformation>($"{requestString}/generate-word/{wordName}");            
        }

        public async Task<ICollection<WordPhoto>> GetWordPictures(string wordName)
        {
            return await httpClient.GetFromJsonAsync<ICollection<WordPhoto>>($"{requestString}/word-pictures/{wordName}");
        }

        public async Task<string> UploadWordImage(IFileReference file)
        {
            var fileInfo = await file.ReadFileInfoAsync();
            using (var ms = await file.CreateMemoryStreamAsync(4 * 1024))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(ms.Length)), "image", fileInfo.Name);

                var response = await httpClient.PostAsync("upload", content);
                var imgUrl = await response.Content.ReadAsStringAsync();
                return imgUrl;
            }
        }
    }
}
