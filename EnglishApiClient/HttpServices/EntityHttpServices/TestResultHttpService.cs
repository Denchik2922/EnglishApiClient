using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices.EntityHttpServices
{
    public class TestResultHttpService : ITestResultHttpService
    {
        private readonly HttpClient _httpClient;
        public TestResultHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ICollection<TestResultForStatistic>> GetAllByDictionaryId(int dictionaryId)
        {
            var response = await _httpClient.GetAsync($"testresult/{dictionaryId}");
            return await response.Content.ReadFromJsonAsync<ICollection<TestResultForStatistic>>();
        }
    }
}
