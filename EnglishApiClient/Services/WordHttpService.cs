using EnglishApiClient.Services.Interfaces;
using Models;
using System.Net.Http.Json;

namespace EnglishApiClient.Services
{
    public class WordHttpService : GenericHttpService<WordModel>, IWordHttpService
    {
        public WordHttpService(HttpClient httpClient) : base(httpClient, "word") { }

        public async Task<WordInformation> GenerateWordInformation(string wordName)
        {
            return await httpClient.GetFromJsonAsync<WordInformation>($"{requestString}/generate-word/{wordName}");            
        }
    }
}
