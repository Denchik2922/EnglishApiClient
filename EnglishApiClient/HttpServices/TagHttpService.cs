using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.HttpServices
{
    public class TagHttpService : GenericHttpService<Tag>, ITagHttpService
    {
        public TagHttpService(HttpClient httpClient) : base(httpClient, "tag") { }
    }
}
