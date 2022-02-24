using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;

namespace EnglishApiClient.HttpServices.TestingHttpService
{
    public class MultipleMatchingTestHttpService : BaseTestHttpService<MultipleMatchingQuestion>, IMultipleMatchingTestHttpService
    {
        public MultipleMatchingTestHttpService(HttpClient client) : base(client, "MultipleMatchingTest") { }
    }
}
