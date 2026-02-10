using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ReadTrack.Host.Extensions;

/// <summary>
/// Extension methods for configuring Swagger/OpenAPI documentation.
/// Configures JWT Bearer authentication support within the Swagger UI.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Registers and configures Swagger generation services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ReadTrack API",
                Version = "v1",
                Description = "Modular Monolith API for ReadTrack Application",
                Contact = new OpenApiContact
                {
                    Name = "ReadTrack Engineering",
                    Email = "engineering@readtrack.com"
                }
            });

            // Configure JWT Bearer Authentication for Swagger UI
            // This allows developers to click "Authorize", enter a token, and execute protected endpoints.
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition("Bearer", securityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    securityScheme,
                    new List<string>()
                }
            });

            // Group actions by their module name if controllers are namespaced correctly
            options.TagActionsBy(api =>
            {
                if (api.GroupName != null)
                {
                    return new[] { api.GroupName };
                }

                if (api.ActionDescriptor.RouteValues.TryGetValue("controller", out var controllerName))
                {
                    return new[] { controllerName };
                }

                return new[] { "General" };
            });

            options.DocInclusionPredicate((name, api) => true);
        });

        return services;
    }
}