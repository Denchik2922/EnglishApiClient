using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;

namespace EnglishApiClient.HttpServices.TestingHttpService
{
    public class MatchingTestHttpService : BaseTestHttpService<MatchingQuestion>, IMatchingTestHttpService
    {
        public MatchingTestHttpService(HttpClient client) : base(client, "MatchingTest") { }
    }
}
