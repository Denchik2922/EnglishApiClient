namespace EnglishApiClient.Dtos.Auth
{
    public class ExternalLoginsDto
    {
        public string ReturnUrl { get; set; }
        public IList<ProviderModel> ExternalLogins { get; set; }
    }
}
