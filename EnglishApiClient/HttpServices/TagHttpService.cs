using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;

namespace EnglishApiClient.HttpServices
{
    public class TagHttpService : GenericHttpService<Tag>, ITagHttpService
    {
        public TagHttpService(HttpClient httpClient) : base(httpClient, "tag") { }
    }
}
