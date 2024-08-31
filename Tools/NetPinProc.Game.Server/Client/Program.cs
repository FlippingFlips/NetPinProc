using AKSoftware.Localization.MultiLanguages;
using AKSoftware.Localization.MultiLanguages.Providers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NetPinProc.Game.Manager.Client;
using System.Globalization;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

//set up localization 
builder.Services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Resources");

await builder.Build().RunAsync();
