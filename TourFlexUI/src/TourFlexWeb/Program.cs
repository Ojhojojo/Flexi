using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TourFlexWeb;
using TourFlexWeb.Pages.MockUps.ItineraryPlanner;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7154") });

builder.Services.AddSingleton<ItineraryService>();
builder.Services.AddSingleton<XaiApiService>();

// Add Authentication state and service
builder.Services.AddAuthorizationCore();

//builder.Services.AddDependencyInjections()
//    .AddHttpClients(builder.Configuration);

//builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
