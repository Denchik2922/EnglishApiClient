using EnglishApiClient.Services.Interfaces;
using Models;
using Models.Dictionary;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EnglishApiClient.Services
{
    public class DictionaryHttpService : GenericHttpService<EnglishDictionary>, IDictionaryHttpService
    {
        public DictionaryHttpService(HttpClient httpClient) : base(httpClient, "dictionary") { }

        public async Task<bool> Create(DictionaryAddModel entity)
        {
            var response = await httpClient.PostAsJsonAsync(requestString, entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<ICollection<EnglishDictionary>> GetPublicDictionaries()
        {
            return await httpClient.GetFromJsonAsync<List<EnglishDictionary>>($"{requestString}/public-dictionaries");
        }

        public async Task<ICollection<EnglishDictionary>> GetPrivateDictionaries()
        {
            return await httpClient.GetFromJsonAsync<List<EnglishDictionary>>($"{requestString}/private-dictionaries");
        }

    }
}
