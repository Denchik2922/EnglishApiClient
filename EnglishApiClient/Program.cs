using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using EnglishApiClient.HttpServices;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure;
using EnglishApiClient.Infrastructure.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Tewr.Blazor.FileReader;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace EnglishApiClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/api/")
            }
            .EnableIntercept(sp));

            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddScoped<HttpInterceptorService>();

            builder.Services.AddMudServices();
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<RefreshTokenHelper>();

            builder.Services.AddScoped<IGoogleOAuthService, GoogleOAuthService>();

            builder.Services.AddScoped<IAudioTestHttpService, AudioTestHttpService>();
            builder.Services.AddScoped<ISpellingTestHttpService, SpellingTestHttpService>();
            builder.Services.AddScoped<IMatchingTestHttpService, MatchingTestHttpService>();
            builder.Services.AddScoped<ITagHttpService, TagHttpService>();
            builder.Services.AddScoped<IWordHttpService, WordHttpService>();
            builder.Services.AddScoped<IDictionaryHttpService, DictionaryHttpService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITestResultHttpService, TestResultHttpService>();
            builder.Services.AddScoped<ITypeOfTestingHttpService, TypeOfTestingHttpService>();
            builder.Services.AddScoped<IUserHttpService, UserHttpService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();



            await builder.Build().RunAsync();
        }
    }
}
