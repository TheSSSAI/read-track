using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using ReadTrack.Recommendations.Application.Interfaces;
using ReadTrack.Recommendations.Application.Interfaces.Infrastructure;
using ReadTrack.Recommendations.Application.Services;
using ReadTrack.Recommendations.Infrastructure.AI;
using ReadTrack.Recommendations.Infrastructure.Configuration;
using ReadTrack.Recommendations.Infrastructure.Persistence;

namespace ReadTrack.Recommendations.Infrastructure
{
    /// <summary>
    /// Dependency Injection configuration for the Recommendations module infrastructure layer.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddRecommendationsModule(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Database Configuration (PostgreSQL)
            var connectionString = configuration.GetConnectionString("RecommendationsConnection");
            services.AddDbContext<RecommendationsDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(RecommendationsDbContext).Assembly.FullName)));

            // 2. Configuration Options
            services.Configure<OpenAIOptions>(configuration.GetSection("OpenAI"));
            services.Configure<OpenSearchOptions>(configuration.GetSection("OpenSearch"));

            // 3. Application Services & Repositories
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddScoped<IRecommendationProcessService, RecommendationProcessService>();
            services.AddSingleton<IPromptBuilder, PromptBuilder>();
            
            // Note: IReadingHistoryProvider and IUnitOfWork are expected to be registered 
            // either via shared kernel or the host application if they are cross-module dependencies.
            // If RecommendationRepository implements IUnitOfWork implicitly via DbContext, we ensure it's handled.
            // For this module scope, we register the internal logic.

            // 4. AI & Vector Clients with Resilience Policies (Polly)
            
            // Register OpenAI Client with Resilience
            services.AddHttpClient<ILLMClient, OpenAIClientAdapter>(client =>
            {
                var options = configuration.GetSection("OpenAI").Get<OpenAIOptions>();
                // Base address could be set here if using a custom endpoint, otherwise the Adapter handles it via SDK
                client.Timeout = TimeSpan.FromSeconds(60); // LLM calls can be slow
            })
            .AddStandardResilienceHandler(options =>
            {
                // Customize retry for 429 Too Many Requests and 5xx errors
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.BackoffType = DelayBackoffType.Exponential;
                options.Retry.Delay = TimeSpan.FromSeconds(2);
                
                // Customize circuit breaker
                options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
                options.CircuitBreaker.FailureRatio = 0.5; // Break if 50% fail
                options.CircuitBreaker.MinimumThroughput = 5;
            });

            // Register OpenSearch Client with Resilience
            services.AddHttpClient<IVectorStoreClient, OpenSearchClientAdapter>(client =>
            {
                var options = configuration.GetSection("OpenSearch").Get<OpenSearchOptions>();
                if (options != null && !string.IsNullOrEmpty(options.Endpoint))
                {
                    client.BaseAddress = new Uri(options.Endpoint);
                }
                client.Timeout = TimeSpan.FromSeconds(10);
            })
            .AddStandardResilienceHandler(options => 
            {
                options.Retry.MaxRetryAttempts = 3;
                options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(15);
            });

            // 5. Interface Forwarding for Embedding Generator
            // Since OpenAIClientAdapter implements both ILLMClient and IEmbeddingGenerator,
            // we can register IEmbeddingGenerator to resolve the same instance/type as ILLMClient
            // or register it as a separate HttpClient usage if they have different configs.
            // Here we assume distinct usage patterns, so we register it similarly but pointing to the same class.
            services.AddHttpClient<IEmbeddingGenerator, OpenAIClientAdapter>()
                .AddStandardResilienceHandler(); // Reuse standard policy

            // 6. MediatR Registration
            // Registers all handlers in the Application assembly
            var applicationAssembly = typeof(Application.Features.Recommendations.GenerateRecommendationsCommand).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            return services;
        }
    }
}