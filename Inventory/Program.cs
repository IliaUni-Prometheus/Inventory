using Application;
using Application.Features.EmployeeFeatures.Queries;
using Application.Helpers;
using Application.Services.Abstract;
using Application.Services.Concrete;
using Domain.Models;
using Domain.Models.Abstraction;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Inventory.Authorization;
using Inventory.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Description = "Test-Description",
            Title = "Test-Title"
        });
    s.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Test Desc",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header
    });

    s.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
           new List<string>()
        }
    });
});

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<NorthwindContext>();

builder.Services.AddMediatR(typeof(IApplication).Assembly)
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddMediatR(typeof(GetEmployeesQuery).Assembly);

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

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpGlobalExceptionFilter>();
}).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

SeedUsers(app);

app.Run();

static void SeedUsers(WebApplication app)
{
    using var servicescope = app.Services.CreateScope();

    var db = servicescope.ServiceProvider.GetRequiredService<NorthwindContext>();

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

