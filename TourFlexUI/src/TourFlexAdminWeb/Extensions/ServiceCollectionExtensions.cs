namespace TourFlexWeb.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add all the options used by the application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfigurableOptions(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        return services;
    }

    /// <summary>
    /// Add jwt authentication used by the application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, ConfigurationManager configuration)
    {

        return services;
    }

    //    /// <summary>
    //    /// Add all the http clients used by the application
    //    /// </summary>
    //    /// <param name="services"></param>
    //    /// <param name="configuration"></param>
    //    /// <returns></returns>
    //    public static IServiceCollection AddHttpClients(this IServiceCollection services,
    //        WebAssemblyHostConfiguration configuration)
    //    {
    //        services.AddScoped<SavvyPayAuthorizationMessageHandler>();

    //        // TODO: Use configurable value here
    //        services.AddHttpClient<ISavvyPayClient, SavvyPayClient>(client =>
    //            client.BaseAddress = new Uri("https://localhost:44122/")
    //            // Use tunnel for mobile apps
    //            //                client.BaseAddress = new Uri("https://kwjc1zsc-44122.aue.devtunnels.ms/")
    //            )
    //            .AddHttpMessageHandler<SavvyPayAuthorizationMessageHandler>();

    //        return services;
    //    }

    //    /// <summary>
    //    /// Add all the dependency injections used by the application
    //    /// </summary>
    //    /// <param name="services"></param>
    //    /// <returns></returns>
    //    public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
    //    {
    //        services.AddScoped<ICustomAccessTokenProvider, CustomAccessTokenProvider>();
    //        services.AddScoped<CustomAuthenticationStateProvider>();
    //        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

    //        services.AddScoped<IAuthenticationService, AuthenticationService>();

    //        // Shared service for all pages
    //        services.AddSingleton<AccountsService>();
    //        services.AddSingleton<IUserService, UserService>();
    //        services.AddSingleton<IQrManagerService, QrManagerService>();
    //        services.AddSingleton<IMediaPickerService, MediaPickerService>();

    //        return services;
    //    }
}
