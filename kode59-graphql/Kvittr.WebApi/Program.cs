using Keycloak.AuthServices.Authentication;
using Kvittr.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Services.AddKeycloakAuthentication(builder.Configuration, options =>
{
    options.Authority = builder.Configuration["Keycloak:authority"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new 
        TokenValidationParameters()
        {
            ValidateAudience = false
        };
});

builder.Services.AddAuthorization();

// Add DB
builder.Services.AddDbContext<KvittrDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Migrate on boot
await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<KvittrDbContext>();
await db.Database.MigrateAsync();

app.Run();