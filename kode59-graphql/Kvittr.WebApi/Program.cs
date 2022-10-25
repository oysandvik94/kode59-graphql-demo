using System.Reflection;
using Keycloak.AuthServices.Authentication;
using Kvittr.Model;
using Kvittr.Model.GraphQL;
using Kvittr.Model.GraphQL.KvittQueries;
using Kvittr.Model.Models;
using Kvittr.WebApi.ViewModels;
using Mapster;
using MapsterMapper;
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
builder.Services.AddPooledDbContextFactory<KvittrDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var config = TypeAdapterConfig.GlobalSettings;
config.Default.MaxDepth(4);
            

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddWebApiTypes()
    .AddProjections()
    .AddSorting()
    .AddFiltering()
    .RegisterDbContext<KvittrDbContext>(DbContextKind.Pooled)
    ;

var app = builder.Build();

// Migrate on boot
await using var scope = app.Services.CreateAsyncScope();
var dbContextFactory = scope.ServiceProvider.GetService<IDbContextFactory<KvittrDbContext>>();
var db = await dbContextFactory.CreateDbContextAsync();
await db.Database.MigrateAsync();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();