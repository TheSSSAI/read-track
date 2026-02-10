using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReadTrack.Monetization.Application.Interfaces;
using ReadTrack.Monetization.Domain.Exceptions;
using ReadTrack.Monetization.Domain.ValueObjects;

namespace ReadTrack.Monetization.Infrastructure.Services;

/// <summary>
/// Factory implementation for resolving the appropriate IPaymentProviderService strategy.
/// Supports selection of Apple or Google providers at runtime.
/// </summary>
public class PaymentProviderFactory : IPaymentProviderFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PaymentProviderFactory> _logger;

    public PaymentProviderFactory(IServiceProvider serviceProvider, ILogger<PaymentProviderFactory> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves the payment provider service implementation for the specified provider type.
    /// </summary>
    /// <param name="provider">The type of provider (Apple/Google) to retrieve.</param>
    /// <returns>The concrete implementation of IPaymentProviderService.</returns>
    /// <exception cref="PaymentProviderException">Thrown when the provider cannot be resolved.</exception>
    public IPaymentProviderService GetProvider(Provider provider)
    {
        try
        {
            // We get all registered services and filter. 
            // Alternatively, with .NET 8 Keyed Services, we could use _serviceProvider.GetRequiredKeyedService<IPaymentProviderService>(provider.ToString());
            // Here we stick to a standard IEnumerable resolution for broader compatibility if Keyed Services aren't configured in startup.
            
            var providers = _serviceProvider.GetServices<IPaymentProviderService>();
            var selectedProvider = providers.FirstOrDefault(p => p.Provider == provider);

            if (selectedProvider == null)
            {
                _logger.LogError("No service implementation registered for provider type: {Provider}", provider);
                throw new PaymentProviderException($"Payment provider implementation for {provider} not found.");
            }

            return selectedProvider;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error resolving payment provider service.");
            throw new PaymentProviderException("Dependency injection error resolving payment provider.", ex);
        }
    }
}