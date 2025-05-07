using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MindFree;
using MindFree.Interfaces;
using MindFree.Utils;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Adicionando a rotina de cookies
builder.Services.AddScoped<ICookie, Cookie>();




await builder.Build().RunAsync();
