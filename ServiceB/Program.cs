using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ServiceBScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "serviceB.scope");
    });
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/secure-data", [Microsoft.AspNetCore.Authorization.Authorize(Policy = "ServiceBScope")] () => "Service B Protected Data");

app.Run();
