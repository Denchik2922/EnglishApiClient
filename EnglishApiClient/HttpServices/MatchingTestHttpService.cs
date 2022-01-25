using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;

namespace EnglishApiClient.HttpServices
{
    public class MatchingTestHttpService : BaseTestHttpService<ParamsForMatchingQuestion>, IMatchingTestHttpService
    {
        public MatchingTestHttpService(HttpClient client) : base(client, "MatchingTest") { }
    }
}
