using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;

namespace EnglishApiClient.HttpServices
{
    public class SpellingTestHttpService : BaseTestHttpService<ParamsForSpellingQuestion>, ISpellingTestHttpService
    {
        public SpellingTestHttpService(HttpClient client) : base(client, "SpellingTest") { }
    }
}
