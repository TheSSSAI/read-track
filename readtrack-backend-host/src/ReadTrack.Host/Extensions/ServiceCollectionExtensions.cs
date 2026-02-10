using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ReadTrack.Host.Configuration;
using ReadTrack.Host.Infrastructure;

namespace ReadTrack.Host.Extensions;

/// <summary>
/// Extension methods for configuring host-level infrastructure services.
/// Handles Cross-Cutting Concerns: Auth, CORS, Logging, Error Handling.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the core host infrastructure services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddHostInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Bind Configuration Options using the Options Pattern
        // Validates that required configuration sections exist at startup
        services.AddOptions<Auth0Configuration>()
            .Bind(configuration.GetSection("Auth0"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<CorsConfiguration>()
            .Bind(configuration.GetSection("AllowedOrigins"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // 2. Register Global Exception Handling (RFC 7807 Problem Details)
        // Uses the IExceptionHandler implementation from Level 1
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        // 3. Register Core ASP.NET Services
        services.AddHttpContextAccessor();
        services.AddHealthChecks();
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // 4. Configure Security (Auth & CORS)
        services.AddHostAuthentication(configuration);
        services.AddHostAuthorization();
        services.AddHostCors(configuration);

        return services;
    }

    private static IServiceCollection AddHostAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var auth0Section = configuration.GetSection("Auth0");
        var domain = auth0Section["Domain"];
        var audience = auth0Section["Audience"];

        if (string.IsNullOrWhiteSpace(domain) || string.IsNullOrWhiteSpace(audience))
        {
            throw new InvalidOperationException("Auth0 Domain and Audience must be configured.");
        }

        // Ensure domain starts with https
        var authority = domain.StartsWith("https://") ? domain : $"https://{domain}/";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.Audience = audience;
            
            // Validate the token signature and issuer
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authority,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

        return services;
    }

    private static IServiceCollection AddHostAuthorization(this IServiceCollection services)
    {
        // Define global authorization policies
        services.AddAuthorization(options =>
        {
            // By default, all endpoints require an authenticated user
            var defaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            options.FallbackPolicy = defaultPolicy;

            // Define specific policies based on Scopes or RBAC if needed in the future
            options.AddPolicy("PremiumUser", policy => 
                policy.RequireClaim("permissions", "access:premium_features"));
        });

        return services;
    }

    private static IServiceCollection AddHostCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("AllowedOrigins:Urls").Get<string[]>() ?? Array.Empty<string>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                if (allowedOrigins.Length > 0)
                {
                    builder.WithOrigins(allowedOrigins)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                }
                else
                {
                    // Fallback for development if no origins configured, though usually explicit is better
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }
            });
        });

        return services;
    }
}