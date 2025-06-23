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
        options.RequireHttpsMetadata = false; // nếu test HTTP
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ServiceAScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "serviceA.scope");
    });
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/secure-data", [Microsoft.AspNetCore.Authorization.Authorize(Policy = "ServiceAScope")] () => "Service A Protected Data");

app.Run();
