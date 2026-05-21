using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Labs.StaticWebApp.Client.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ =>
{
    if (builder.HostEnvironment.IsDevelopment())
    {
        var configuredApiBaseAddress = builder.Configuration["ApiBaseAddress"];

        if (Uri.TryCreate(configuredApiBaseAddress, UriKind.Absolute, out var apiBaseUri))
            return new HttpClient { BaseAddress = apiBaseUri };
    }

    return new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
});

await builder.Build().RunAsync();