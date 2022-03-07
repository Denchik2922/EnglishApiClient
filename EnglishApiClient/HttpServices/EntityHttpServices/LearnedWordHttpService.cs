using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices.EntityHttpServices
{
    public class LearnedWordHttpService : ILearnedWordHttpService
    {
        private readonly HttpClient _httpClient;
        public LearnedWordHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SetLearned(LearnedWord word)
        {
            var response = await _httpClient.PutAsJsonAsync("learnedword", word);
            return response.IsSuccessStatusCode;
        }

        public async Task<ICollection<LearnedWord>> GetAllByDictionaryId(int dictionaryId)
        {
            var response = await _httpClient.GetAsync($"learnedword/{dictionaryId}");
            return await response.Content.ReadFromJsonAsync<ICollection<LearnedWord>>();
        }
    }
}
