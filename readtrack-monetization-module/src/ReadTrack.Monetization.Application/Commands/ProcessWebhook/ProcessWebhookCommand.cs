using MediatR;

namespace ReadTrack.Monetization.Application.Commands.ProcessWebhook
{
    /// <summary>
    /// CQRS Command to process an incoming webhook from a payment provider.
    /// </summary>
    /// <param name="Provider">The provider name (e.g., "apple", "google").</param>
    /// <param name="Payload">The raw JSON or JWS payload received.</param>
    /// <param name="Signature">The signature header value (optional, depending on provider).</param>
    public record ProcessWebhookCommand(
        string Provider,
        string Payload,
        string? Signature
    ) : IRequest<bool>; 
    // Returning bool to indicate success/failure to the controller for HTTP status code determination.
    // In complex systems, this might return a rich Result<T> object.
}