using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MindFree;
using MindFree.Connection;
using MindFree.Interfaces;
using MindFree.Utils;


var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Adicionando a rotina de cookies
builder.Services.AddScoped<ICookie, Cookie>();
//builder.Services.AddScoped<JSRuntime, JSRuntime>();


builder.Services.AddScoped(IServiceProvider => new HttpClient { BaseAddress = new Uri(Api.Url) });
//builder.Services.AddScoped<Middleware>();
//builder.Services.AddScoped(IServiceProvider => new HttpClient(new AddHeadersDelegatingHandler())
//{
//    BaseAddress = new Uri(Api.Url)
//});

builder.Services.AddScoped<Middleware>();

await builder.Build().RunAsync();
