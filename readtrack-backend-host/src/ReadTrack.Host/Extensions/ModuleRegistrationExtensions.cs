using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReadTrack.Host.Extensions;

/// <summary>
/// Extension methods for registering business modules in the dependency injection container.
/// This acts as the Composition Root for the Modular Monolith, aggregating service registration
/// from all isolated business domains.
/// </summary>
public static class ModuleRegistrationExtensions
{
    /// <summary>
    /// Registers all business modules and their specific dependencies.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddBusinessModules(this IServiceCollection services, IConfiguration configuration)
    {
        // NOTE: In a full Modular Monolith implementation, this class references the 
        // Extension methods from the specific Module projects (e.g., ReadTrack.Modules.Users).
        // Since those projects are external to this Host repository context, we define the 
        // structure here. When the module projects are referenced in the .csproj, 
        // their specific Add[Module] methods would be invoked here.

        // Example Integration Pattern:
        // services.AddUsersModule(configuration);
        // services.AddReadingModule(configuration);
        // services.AddMonetizationModule(configuration);
        // services.AddEngagementModule(configuration);
        // services.AddRecommendationsModule(configuration);

        // Register Shared Infrastructure logic if required by modules
        // This ensures that any cross-module communication (like MediatR pipelines) 
        // is properly wired up.
        
        return services;
    }
}