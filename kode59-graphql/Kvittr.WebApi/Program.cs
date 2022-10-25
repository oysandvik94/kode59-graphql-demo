using Keycloak.AuthServices.Authentication;
using Kvittr.Model;
using Kvittr.Model.GraphQL;
using Kvittr.WebApi;
using Mapster;
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
builder.Services.AddPooledDbContextFactory<KvittrDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var config = TypeAdapterConfig.GlobalSettings;
config.Default.MaxDepth(4);
            

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddMutationConventions()
    .AddWebApiTypes()
    .AddProjections()
    .AddSorting()
    .AddFiltering()
    .AddHttpRequestInterceptor<RequestInterceptor>()
    .RegisterDbContext<KvittrDbContext>(DbContextKind.Pooled)
    ;

var app = builder.Build();

// Migrate on boot
await using var scope = app.Services.CreateAsyncScope();
var dbContextFactory = scope.ServiceProvider.GetService<IDbContextFactory<KvittrDbContext>>();
var db = await dbContextFactory.CreateDbContextAsync();
await db.Database.MigrateAsync();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();