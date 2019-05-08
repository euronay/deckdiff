using System;
using System.Net.Http;
using Blazor.Extensions.Logging;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScryfallApi.Client;

namespace deckdiff
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                    .AddBrowserConsole() 
                    .SetMinimumLevel(LogLevel.Trace)
                );

            services.AddStorage();

            services.AddScoped<IMemoryCache>(service => new MemoryCache(new MemoryCacheOptions()));


            services.AddSingleton<ScryfallApiClient>(service => {
                var logger = service.GetRequiredService<ILogger<ScryfallApiClient>>();
                var cache = service.GetRequiredService<IMemoryCache>();
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://api.scryfall.com/");
                return new ScryfallApiClient(httpClient, logger,  cache);
            });
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
