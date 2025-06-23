using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddTestUsers(Config.TestUsers);

var app = builder.Build();

app.UseIdentityServer();
app.Run();

public static class Config
{
    public static IEnumerable<Client> Clients => new[]
    {
        new Client
        {
            ClientId = "client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Duende.IdentityServer.Models.Secret("secret".Sha256()) },
            AllowedScopes = { "openid", "profile", "serviceA.scope", "serviceB.scope" },
            AllowOfflineAccess = true
        }
    };

    public static IEnumerable<ApiScope> ApiScopes => new[]
    {
        new ApiScope("serviceA.scope", "Service A Access"),
        new ApiScope("serviceB.scope", "Service B Access"),
    };

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };

    public static List<TestUser> TestUsers => new()
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "test",
            Password = "password"
        }
    };
}
