﻿using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using System.Net.Http.Json;

namespace EnglishApiClient.HttpServices
{
    public class DictionaryHttpService : GenericHttpService<EnglishDictionary>, IDictionaryHttpService
    {
        public DictionaryHttpService(HttpClient httpClient) : base(httpClient, "dictionary") { }

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