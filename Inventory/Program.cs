using Application.Features.EmployeeFeatures.Queries;
using Application.Helpers;
using Application.Services.Abstract;
using Application.Services.Concrete;
using Domain.Models;
using Infrastructure.Models;
using Inventory.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters =
                       new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidIssuer = "OurIssuer",
                           ValidateIssuerSigningKey = true,
                           ValidAudience = "ValidAudience",
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uGJg'=8?Y8F68uv87)aw49M<.BM.YWS5V}B#s=>g*qr$ukca^j-~t8>vYr3\\397Aj4h'J*"))
                       };
              });

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddMediatR(typeof(AllEmployeesQueryHandler).Assembly);

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

SeedUsers(app);

app.MapControllers();

app.Run();


static void SeedUsers(WebApplication app)
{
    using var servicescope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    var db = servicescope.ServiceProvider.GetService<NorthwindContext>();

    var users = new List<User>()
    {
         User.CreateCustomer(BCrypt.Net.BCrypt.HashPassword("Password")),
         User.CreateAdmin(BCrypt.Net.BCrypt.HashPassword("Password"))
    };

    if (!db.Users.Any(x => users.Select(e => e.UserName).Contains(x.UserName)))
    {
        db.Set<User>().AddRange(users);

        db.SaveChanges();
    }
}

