using Blazored.LocalStorage;
using Blazored.Toast;
using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<RefreshTokenService>();

            builder.Services.AddScoped<ISpellingTestHttpService, SpellingTestHttpService>();
            builder.Services.AddScoped<IMatchingTestHttpService, MatchingTestHttpService>();
            builder.Services.AddScoped<ITagHttpService, TagHttpService>();
            builder.Services.AddScoped<IWordHttpService, WordHttpService>();
            builder.Services.AddScoped<IDictionaryHttpService, DictionaryHttpService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


            await builder.Build().RunAsync();
        }
    }
}
