using Blazored.Toast.Services;
using EnglishApiClient.Infrastructure.ErrorFeatures;
using EnglishApiClient.Infrastructure.Helpers;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Toolbelt.Blazor;

namespace EnglishApiClient.Infrastructure
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly RefreshTokenHelper _refreshTokenService;
        private IToastService _toastService;

        public HttpInterceptorService(HttpClientInterceptor interceptor,
                                      NavigationManager navigationManager,
                                      RefreshTokenHelper refreshTokenService,
                                      IToastService toastService)
        {
            _interceptor = interceptor;
            _navManager = navigationManager;
            _refreshTokenService = refreshTokenService;
            _toastService = toastService;
        }

        public void RegisterEvent()
        {
            _interceptor.AfterSendAsync += InterceptResponse;
            _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
        } 
        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            if (!absPath.Contains("auth") && !absPath.Contains("tag/all"))
            {
                var token = await _refreshTokenService.TryRefreshToken();
                if (!string.IsNullOrEmpty(token))
                {
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                }
            }
        }

        public async Task InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            if (!e.Response.IsSuccessStatusCode)
            {
                var statusCode = e.Response.StatusCode;
                var content = await e.Response.Content.ReadFromJsonAsync<ErrorResponse>();

                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404");
                        break;
                    case HttpStatusCode.Forbidden:
                        _navManager.NavigateTo("/403");
                        break;
                    case HttpStatusCode.Unauthorized:
                        _navManager.NavigateTo("/login");
                        break;
                }

                _toastService.ShowError(content.Message);
                throw new HttpResponseException(content.Message);
            }
        }

        public void DisposeEvent()
        {
            _interceptor.AfterSendAsync += InterceptResponse;
            _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
        } 
    }
}
