using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReadTrack.Shared.Infrastructure.Abstractions.Persistence;
using ReadTrack.Shared.Infrastructure.BackgroundJobs;
using ReadTrack.Shared.Infrastructure.Logging;
using ReadTrack.Shared.Infrastructure.Persistence;
using ReadTrack.Shared.Infrastructure.Resilience;
using ReadTrack.Shared.Infrastructure.Services;

namespace ReadTrack.Shared.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for registering shared infrastructure services in the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers shared infrastructure services including resilience policies, background jobs, date time providers, and email services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration provider.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Configuration & Options
            // Binds the "Resilience" section to the ResilienceOptions object for DI injection.
            services.AddOptions<ResilienceOptions>()
                .Bind(configuration.GetSection("Resilience"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            // 2. Core Infrastructure Services
            // Singleton is appropriate for stateless system wrappers.
            services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
            
            // Transient for EmailService to ensure clean state per usage, though SmtpClient lifecycle might be managed internally.
            // Assuming SMTP settings are configured elsewhere or via other Options patterns not explicitly scoped here.
            services.AddTransient<IEmailService, SmtpEmailService>();

            // 3. Resilience
            // Registers the registry which holds the Polly policies.
            services.AddSingleton<IResiliencePolicyProvider, ResiliencePolicyRegistry>();
            // Also register the concrete type in case it's requested directly.
            services.AddSingleton<ResiliencePolicyRegistry>();

            // 4. Background Jobs
            // Registers the abstraction wrapper for Hangfire.
            // Note: The consuming application must call services.AddHangfire(...) to configure the server/storage.
            services.AddScoped<IBackgroundJobService, HangfireJobService>();

            // 5. Logging & Telemetry
            // Required for CorrelationIdEnricher to access HttpContext headers.
            services.AddHttpContextAccessor();
            // Register the Serilog enricher so it can be resolved with its dependencies.
            services.AddTransient<CorrelationIdEnricher>();

            return services;
        }

        /// <summary>
        /// Registers persistence services including Entity Framework Core DbContext, Repositories, and Unit of Work.
        /// </summary>
        /// <typeparam name="TContext">The concrete type of the DbContext.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="connectionString">The database connection string.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddPersistence<TContext>(this IServiceCollection services, string connectionString) 
            where TContext : DbContext
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }

            // 1. Register the concrete DbContext
            // Configures Npgsql with resilience (retry on failure) which is critical for cloud databases (Aurora).
            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    // Enable automatic retries for transient errors (e.g. network blips, failovers)
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                });

                // In development, detailed errors can be useful, but for shared infra we adhere to secure defaults.
                // Tracking behavior is default (TrackAll), as IReadRepository explicitly handles AsNoTracking.
            });

            // 2. Bind generic DbContext to the concrete TContext
            // This ensures that services requesting the base 'DbContext' (like EfRepository) receive the correctly configured 'TContext'.
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<TContext>());

            // 3. Register Generic Repositories
            // Scoped lifetime matches the DbContext lifetime.
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

            // 4. Register Unit of Work
            // Scoped to share the same DbContext instance within the request.
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            return services;
        }
    }
}