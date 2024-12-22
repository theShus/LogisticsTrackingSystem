using LogisticsTrackingSystem.Presentation;
using LogisticsTrackingSystem.Presentation.Configuration;
using LogisticsTrackingSystem.Presentation.Services;
using LogisticsTrackingSystem.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load configuration
builder.Services.AddSingleton<IConfiguration>(sp =>
{
    var config = new ConfigurationBuilder()
        .SetBasePath(builder.HostEnvironment.BaseAddress)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();
    return config;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();

builder.Services.Configure<MyAppSettings>(builder.Configuration.GetSection("MyAppSettings"));


await builder.Build().RunAsync();
