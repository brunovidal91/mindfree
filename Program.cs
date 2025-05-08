using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MindFree;
using MindFree.Connection;
using MindFree.Interfaces;
using MindFree.Utils;


var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Adicionando a rotina de cookies
builder.Services.AddScoped<ICookie, Cookie>();

builder.Services.AddScoped(IServiceProvider => new HttpClient { BaseAddress = new Uri(Api.Url) });


await builder.Build().RunAsync();
