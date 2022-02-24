using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;

namespace EnglishApiClient.HttpServices.TestingHttpService
{
    public class SpellingTestHttpService : BaseTestHttpService<SpellingQuestion>, ISpellingTestHttpService
    {
        public SpellingTestHttpService(HttpClient client) : base(client, "SpellingTest") { }
    }
}
