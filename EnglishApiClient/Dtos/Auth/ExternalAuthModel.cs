namespace EnglishApiClient.Dtos.Auth
{
    public class ExternalAuthModel
    {
        public string Provider { get; set; } = "google";
        public string Token { get; set; }
    }
}
