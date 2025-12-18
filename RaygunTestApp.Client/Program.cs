using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Raygun.Blazor.WebAssembly.Extensions;
using RaygunTestApp.Client;
using RaygunTestApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<CounterService>();

// Configure Raygun if the API key is provided
var raygunApiKey = builder.Configuration.GetValue("Raygun:ApiKey", defaultValue: "${RAYGUN_API_KEY}");
Console.WriteLine($"Raygun API Key: \"{raygunApiKey}\"");
if (!string.IsNullOrWhiteSpace(raygunApiKey) && raygunApiKey != "${RAYGUN_API_KEY}")
{
    Console.WriteLine("Configuring Raygun");
    builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, RaygunLoggerProvider>());
    builder.UseRaygunBlazor();
}

await builder.Build().RunAsync();
