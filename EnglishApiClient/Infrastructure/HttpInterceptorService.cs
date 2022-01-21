using EnglishApiClient.HttpServices;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using Toolbelt.Blazor;

namespace EnglishApiClient.Infrastructure
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly RefreshTokenService _refreshTokenService;
        public HttpInterceptorService(HttpClientInterceptor interceptor,
                                      NavigationManager navigationManager,
                                      RefreshTokenService refreshTokenService)
        {
            _interceptor = interceptor;
            _navManager = navigationManager;
            _refreshTokenService = refreshTokenService;
        }

        public void RegisterEvent()
        {
            _interceptor.AfterSend += InterceptResponse;
            _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
        } 
        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            if (!absPath.Contains("refresh") && !absPath.Contains("auth"))
            {
                var token = await _refreshTokenService.TryRefreshToken();
                if (!string.IsNullOrEmpty(token))
                {
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                }
            }
        }

        public void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            string message = string.Empty;
            if (!e.Response.IsSuccessStatusCode)
            {
                var statusCode = e.Response.StatusCode;
                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404");
                        message = "The requested resorce was not found.";
                        break;
                    case HttpStatusCode.Forbidden:
                        _navManager.NavigateTo("/403");
                        message = "You don`t have permission to access on this server.";
                        break;
                    case HttpStatusCode.Unauthorized:
                        _navManager.NavigateTo("/login");
                        message = "User is not authorized";
                        break;
                    default:
                        message = "Something went wrong, please contact Administrator";
                        break;
                }
                throw new HttpResponseException(message);
            }
        }

        public void DisposeEvent()
        {
            _interceptor.AfterSend += InterceptResponse;
            _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
        } 
    }
}
