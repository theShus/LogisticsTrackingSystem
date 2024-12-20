using LogisticsTrackingSystem.Presentation;
using LogisticsTrackingSystem.Presentation.Services;
using LogisticsTrackingSystem.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5221") });
builder.Services.AddScoped<IShipmentService, ShipmentService>();

await builder.Build().RunAsync();
