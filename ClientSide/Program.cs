using ClientSide.Configs;
using ClientSide.Data;
using ClientSide.Data.Implementations;
using ClientSide.Helpers;
using Flurl.Http;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IOrderService, OrderService>();

builder.Services.Configure<ApiConfigs>(builder.Configuration.GetSection("ApiConfigs"));

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

var app = builder.Build();

//using var servicescope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

//var authService = servicescope.ServiceProvider.GetService<ApiAuthenticationStateProvider>();

//await authService.GetAuthenticationStateAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
