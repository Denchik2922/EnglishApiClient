using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;

namespace EnglishApiClient.HttpServices.TestingHttpService
{
    public class AudioTestHttpService : BaseTestHttpService<AudioQuestion>, IAudioTestHttpService
    {
        public AudioTestHttpService(HttpClient client) : base(client, "AudioTest") { }
    }
}
