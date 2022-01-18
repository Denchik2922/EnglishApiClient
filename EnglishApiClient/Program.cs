using Blazored.LocalStorage;
using EnglishApiClient.Infrastructure;
using EnglishApiClient.Services;
using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
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

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<HttpInterceptorService>();
            builder.Services.AddScoped<RefreshTokenService>();

            builder.Services.AddHttpClientInterceptor();
           

            builder.Services.AddScoped<ITagHttpService, TagHttpService>();
            builder.Services.AddScoped<IWordHttpService, WordHttpService>();
            builder.Services.AddScoped<IDictionaryHttpService, DictionaryHttpService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


            await builder.Build().RunAsync();
        }
    }
}
