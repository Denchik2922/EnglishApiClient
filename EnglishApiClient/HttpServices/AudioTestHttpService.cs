using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;

namespace EnglishApiClient.HttpServices
{
    public class AudioTestHttpService : BaseTestHttpService<ParamsForAudioQuestion>, IAudioTestHttpService
    {
        public AudioTestHttpService(HttpClient client) : base(client, "AudioTest") { }
    }
}
