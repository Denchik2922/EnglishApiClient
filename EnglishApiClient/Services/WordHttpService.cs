﻿using EnglishApiClient.Services.Interfaces;
using Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnglishApiClient.Services
{
    public class WordHttpService : IWordHttpService
    {
        private readonly HttpClient _client;
        public WordHttpService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Word>> GetWords()
        {
            return await _client.GetFromJsonAsync<List<Word>>("word");
        }
    }
}
