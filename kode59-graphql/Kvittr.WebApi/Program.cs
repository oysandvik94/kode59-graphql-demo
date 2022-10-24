using Keycloak.AuthServices.Authentication;
using Kvittr.Model;
using Kvittr.Model.GraphQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Services.AddKeycloakAuthentication(builder.Configuration, options =>
{
    options.Authority = builder.Configuration["Configuration:Keycloak:authority"];
    options.TokenValidationParameters = new 
        TokenValidationParameters()
        {
            ValidateAudience = false
        };
});

// Add DB
builder.Services.AddDbContext<KvittrDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .RegisterDbContext<KvittrDbContext>()
    ;

var app = builder.Build();

// Migrate on boot
await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<KvittrDbContext>();
await db.Database.MigrateAsync();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();