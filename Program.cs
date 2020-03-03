using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazor.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Blazor.Extensions.Storage;
using Microsoft.Extensions.Caching.Memory;
using ScryfallApi.Client;
using System.Net.Http;
using Blazor.DragDrop.Core;

namespace deckdiff
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Disabled until https://github.com/BlazorExtensions/Logging/issues/41
            // builder.Services.AddLogging(builder => builder
            //        .AddBrowserConsole()
            //        .SetMinimumLevel(LogLevel.Trace)
            //    );


            builder.Services.AddStorage();

            builder.Services.AddScoped<IMemoryCache>(service => new MemoryCache(new MemoryCacheOptions()));

            builder.Services.AddSingleton<ScryfallApiClient>(service => {
                var logger = service.GetRequiredService<ILogger<ScryfallApiClient>>();
                var cache = service.GetRequiredService<IMemoryCache>();
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://api.scryfall.com/");
                return new ScryfallApiClient(httpClient, logger, cache);
            });

            builder.Services.AddBlazorDragDrop();

            await builder.Build().RunAsync();
        }

    }
}