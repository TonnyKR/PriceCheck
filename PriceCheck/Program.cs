using Microsoft.AspNetCore.Hosting;
using PriceCheck.API;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
// Add services to the container.
startup.AddServices(builder.Services);
var webApplication = builder.Build();

// Configure the HTTP request pipeline.
startup.Configure(webApplication, webApplication.Environment);

await webApplication.RunAsync();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();




