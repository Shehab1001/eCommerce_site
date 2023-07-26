using BlazorApp.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BlazorApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient("InventoryService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001/inventoryservice/");
});

builder.Services.AddHttpClient("ShoppingCartService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001/shoppingcartservice/");
});

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorApp.ServerAPI"));

builder.Services.AddMudServices();
builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
