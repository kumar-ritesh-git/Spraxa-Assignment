using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MedicinesApi.ServiceExtensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthenticationWithGoogleJwt(this IServiceCollection services, IConfiguration configuration)
        {
            // Authentication: accept Google-issued JWTs (ID tokens / access tokens) for API calls.
            // Keep cookie + external Google for interactive flows if needed, but default to JWT Bearer for API auth.
            services.AddAuthentication(options =>
            {
                // APIs should use Bearer tokens by default
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Google OpenID Connect / OAuth2 token issuer
                options.Authority = "https://accounts.google.com";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuers = new[] { "accounts.google.com", "https://accounts.google.com" },
                    ValidateAudience = true,
                    // Audience should be your Google OAuth client ID (set in appsettings or env)
                    ValidAudience = configuration["Google:ClientId"],
                    ValidateLifetime = true
                };

                // Optional: for debugging on non-HTTPS dev environments
                // options.RequireHttpsMetadata = false;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Google:ClientId"];
                options.ClientSecret = configuration["Google:ClientSecret"];
                // Keep for interactive flows (optional); not required for API token validation
            });


            return services;

        }
    }
}
