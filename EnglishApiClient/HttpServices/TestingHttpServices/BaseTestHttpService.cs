using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices.TestingHttpService
{
    public abstract class BaseTestHttpService<T> : IBaseTestHttpService<T> where T : class
    {
        protected readonly HttpClient httpClient;
        protected readonly string requestString;
        public BaseTestHttpService(HttpClient client, string requestUri)
        {
            httpClient = client;
            requestString = requestUri;
        }

        public async Task<TestParameters> StartTest(int Dictionaryid)
        {
            return await httpClient.GetFromJsonAsync<TestParameters>($"{requestString}/{Dictionaryid}");
        }

        public async Task<T> GetPartOfTest(TestParameters parameters)
        {
            var response = await httpClient.PostAsJsonAsync($"{requestString}/part-of-test", parameters);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<ParamsForCheck> CheckQuestion(ParamsFoAnswer parameters)
        {
            var response = await httpClient.PostAsJsonAsync($"{requestString}/check-answer", parameters);
            return await response.Content.ReadFromJsonAsync<ParamsForCheck>();
        }

        public async Task<bool> FinishTest(TestResult result)
        {
            var response = await httpClient.PostAsJsonAsync($"{requestString}/finish-test", result);
            return response.IsSuccessStatusCode;
        }
    }
}
