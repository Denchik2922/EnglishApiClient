using EnglishApiClient.Services.Interfaces;
using Models;
using System.Net.Http;

namespace EnglishApiClient.Services
{
    public class TagHttpService : GenericHttpService<Tag>, ITagHttpService
    {
        public TagHttpService(HttpClient httpClient) : base(httpClient, "tag") { }
    }
}
