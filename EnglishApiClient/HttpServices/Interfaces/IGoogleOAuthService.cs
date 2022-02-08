using EnglishApiClient.Dtos.Auth;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IGoogleOAuthService
    {
        Task<GoogleJwtToken> ExchangeCodeOnTokenAsync(string code, string codeVerifier, string redirectUrl);
        string GenerateOAuthRequestUrl(string redirectUrl, string codeChellange);
    }
}