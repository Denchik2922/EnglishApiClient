using EnglishApiClient.Dtos.Auth;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure;
using Microsoft.AspNetCore.WebUtilities;

namespace EnglishApiClient.HttpServices
{
    public class GoogleOAuthService : IGoogleOAuthService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        private readonly string _authEndpoint;
        private readonly string _tokenEndpoint;
        private readonly string _responseType;
        private readonly string _scope;
        private readonly string _challengeMethod;
        private readonly string _grantType;

        public GoogleOAuthService(IConfiguration config)
        {
            _clientId = config["GoogleAuth:ClientId"];
            _clientSecret = config["GoogleAuth:ClientSecret"];
            _authEndpoint = config["GoogleAuth:OAuthServerEndpoint"];
            _tokenEndpoint = config["GoogleAuth:TokenServerEndpoint"];
            _responseType = config["GoogleAuth:ResponseType"];
            _scope = config["GoogleAuth:Score"];
            _challengeMethod = config["GoogleAuth:ChallengeMethod"];
            _grantType = config["GoogleAuth:GrantType"];
        }

        public string GenerateOAuthRequestUrl(string redirectUrl, string codeChellange)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"client_id", _clientId},
                { "redirect_uri", redirectUrl },
                { "response_type", _responseType },
                { "scope", _scope },
                { "code_challenge", codeChellange },
                { "code_challenge_method", _challengeMethod }
            };

            var url = QueryHelpers.AddQueryString(_authEndpoint, queryParams);
            return url;
        }

        public async Task<GoogleJwtToken> ExchangeCodeOnTokenAsync(string code, string codeVerifier, string redirectUrl)
        {
            var authParams = new Dictionary<string, string>
            {
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "code", code },
                { "code_verifier", codeVerifier },
                { "grant_type", _grantType },
                { "redirect_uri", redirectUrl }
            };

            var tokenResult = await HttpHelper.SendPostRequest<GoogleJwtToken>(_tokenEndpoint, authParams);
            return tokenResult;
        }
    }
}
