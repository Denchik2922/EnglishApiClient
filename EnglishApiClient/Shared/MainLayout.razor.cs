using EnglishApiClient.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Shared
{
    public partial class MainLayout : IDisposable
    {

        [Inject]
        private HttpInterceptorService _interceptor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _interceptor.RegisterEvent();
        }

        public void Dispose() => _interceptor.DisposeEvent();
    }
}
