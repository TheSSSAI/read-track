using System;

namespace ReadTrack.Monetization.Application.Interfaces
{
    /// <summary>
    /// Factory for resolving the appropriate payment provider service strategy.
    /// </summary>
    public interface IPaymentProviderFactory
    {
        /// <summary>
        /// Returns the service implementation for the specified provider.
        /// </summary>
        /// <param name="provider">The provider key (e.g., "apple", "google").</param>
        /// <returns>The payment provider service instance.</returns>
        /// <exception cref="ArgumentException">Thrown if the provider is not supported.</exception>
        IPaymentProviderService GetProvider(string provider);
    }
}