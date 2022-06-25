using Application.Features.EmployeeFeatures.Queries;
using Application.Helpers;
using Application.Services.Abstract;
using Application.Services.Concrete;
using Domain.Models;
using HealthChecks.UI.Client;
using Infrastructure.Models;
using Inventory.Authorization;
using Inventory.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Shared.Extensions;
using Shared.Extensions.Swagger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    var cacheProfiles = builder.Configuration
            .GetSection("CacheProfiles")
            .GetChildren();
    foreach (var cacheProfile in cacheProfiles)
    {
        options.CacheProfiles
        .Add(cacheProfile.Key,
        cacheProfile.Get<CacheProfile>());
    }
});
builder.Services.AddResponseCaching();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiSwagger();

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddMediatR(typeof(AllEmployeesQueryHandler).Assembly);
builder.Services.AddVersioning();
builder.Services.AddCustomErrorHandling();
builder.Services.AddHealthChecks();
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecksUI(opt =>
{
    opt.AddHealthCheckEndpoint("default api", "/healthz"); //map health check api
})
        .AddInMemoryStorage();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = "ValidIssuer",
                         ValidAudience = "ValidAudience",
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("he4s$Y2$#d%%B^nxdvp*H76UA_M#53!E!Dzp"))
                     };
                 });

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
var env = app.Services.GetRequiredService<IHostEnvironment>();
// Configure the HTTP request pipeline.
app.UseApiSwaggerPage(env, provider);

app.UseMiddleware<JwtMiddleware>();
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseHttpLogging();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/healthz");
app.MapControllers();
app.UseRouting();
app.UseResponseCaching();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => false,
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report));
            await context.Response.Body.WriteAsync(bytes);
        }
    });

    //map healthcheck ui endpoing - default is /healthchecks-ui/
    endpoints.MapHealthChecksUI();

    endpoints.MapDefaultControllerRoute();
});
//SeedUsers(app);

app.Run();

static void SeedUsers(WebApplication app)
{
    using var servicescope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    var db = servicescope.ServiceProvider.GetService<NorthwindContext>();

    var users = new List<User>()
    {
        User.CreateSupplier(BCrypt.Net.BCrypt.HashPassword("Password")),
        User.CreateCustomer(BCrypt.Net.BCrypt.HashPassword("Password")),
        User.CreateAdmin(BCrypt.Net.BCrypt.HashPassword("Password"))
    };

    if (!db.Users.Any(x => users.Select(e => e.UserName).Contains(x.UserName)))
    {
        db.Set<User>().AddRange(users);
        db.SaveChanges();
    }
}

