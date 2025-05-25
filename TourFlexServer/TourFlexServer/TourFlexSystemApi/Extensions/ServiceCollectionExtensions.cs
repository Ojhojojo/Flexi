using Microsoft.AspNetCore.RateLimiting;
using System.Net.Http.Headers;
using System.Threading.RateLimiting;

namespace TourFlexSystem.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddHttpClient("XaiApi", client =>
        {
            var baseUrl = configuration["XaiApi:BaseUrl"];
            var apiKey = configuration["XaiApi:ApiKey"];
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }

    public static IServiceCollection AddGrokRateLimit(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("GrokApi", opt =>
            {
                opt.PermitLimit = 60; // 60 requests per minute
                opt.Window = TimeSpan.FromSeconds(60);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 0;
            });
        });

        return services;
    }
}
