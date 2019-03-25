using System;
using System.Net.Http;
using Blazor.Extensions.Logging;
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

            services.AddScoped<IMemoryCache>(service => new MemoryCache(new MemoryCacheOptions()));

            services.AddScoped<ScryfallApiClient>(service => {
                var uriHelper = service.GetRequiredService<IUriHelper>();
                var httpClient = service.GetRequiredService<HttpClient>();
                var logger = service.GetRequiredService<ILogger<ScryfallApiClient>>();
                var cache = service.GetRequiredService<IMemoryCache>();
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
