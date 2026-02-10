using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReadTrack.Users.Application.DTOs;
using ReadTrack.Users.Application.Interfaces;
using ReadTrack.Users.Infrastructure.Configuration;

namespace ReadTrack.Users.Infrastructure.Services
{
    /// <summary>
    /// Service responsible for validating social login tokens and retrieving user profile information
    /// via Auth0 or direct provider verification.
    /// This implementation assumes an interaction with Auth0's authentication endpoints to exchange 
    /// or validate social tokens.
    /// </summary>
    public class Auth0TokenValidationService : ITokenValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly Auth0Settings _auth0Settings;
        private readonly ILogger<Auth0TokenValidationService> _logger;

        public Auth0TokenValidationService(
            HttpClient httpClient,
            IOptions<Auth0Settings> auth0Settings,
            ILogger<Auth0TokenValidationService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _auth0Settings = auth0Settings?.Value ?? throw new ArgumentNullException(nameof(auth0Settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Validates a social ID token and retrieves the associated user profile.
        /// </summary>
        /// <param name="provider">The social provider (google-oauth2, apple, etc.)</param>
        /// <param name="idToken">The OIDC ID Token from the provider</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>UserProfileDto containing basic user info</returns>
        public async Task<UserProfileDto> ValidateSocialTokenAsync(string provider, string idToken, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(provider)) throw new ArgumentException("Provider is required", nameof(provider));
            if (string.IsNullOrWhiteSpace(idToken)) throw new ArgumentException("ID Token is required", nameof(idToken));

            _logger.LogInformation("Validating social token for provider: {Provider}", provider);

            // In a production scenario using Auth0, you typically verify the token signature 
            // or use the /userinfo endpoint if you have an access token.
            // For social login flows where the mobile client sends a provider token, 
            // we often treat this as a "Token Exchange" or verification step.
            // 
            // NOTE: This implementation simulates the extraction of claims from a token 
            // or an upstream call to Auth0's /userinfo or a token verification endpoint.
            // Actual implementation details depend heavily on the specific Auth0 configuration
            // (e.g. OIDC Conformant, Social Connections setup).

            try
            {
                // This is a simplified representation. In a real Auth0 implementation, 
                // you might verify the JWT locally using JWKS or call Auth0 API.
                // Assuming we are validating the token via a secure back-channel call 
                // or have verified the signature locally.
                
                // For demonstration, we assume logic here that parses the verified token 
                // or calls an introspection endpoint.
                
                // Mocking extraction for structure validity (Replace with actual JWT handler or API call)
                // var principal = _jwtHandler.ValidateToken(idToken, ...);
                
                // If we assume the mobile app authenticated with Auth0 and sent an Auth0 Access Token:
                // We call /userinfo.
                
                // If we assume the mobile app authenticated with Google directly and sent a Google ID Token:
                // We might need to exchange it or verify it with Google keys.
                
                // Based on "Auth0TokenValidationService", we assume interaction with Auth0.
                
                // Placeholder logic for retrieving profile from Auth0 using the token
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://{_auth0Settings.Domain}/userinfo");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", idToken);

                var response = await _httpClient.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to validate token with Auth0. Status: {Status}", response.StatusCode);
                    throw new UnauthorizedAccessException("Invalid token provided.");
                }

                var userInfo = await response.Content.ReadFromJsonAsync<Auth0UserInfo>(cancellationToken: cancellationToken);

                if (userInfo == null)
                {
                    throw new InvalidOperationException("Failed to deserialize user info from Auth0.");
                }

                return new UserProfileDto(
                    Guid.Empty, // Id will be resolved by the Command Handler
                    userInfo.Name ?? userInfo.Nickname ?? "User",
                    userInfo.Email ?? ""
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during token validation for provider {Provider}", provider);
                throw;
            }
        }

        // Helper class to deserialize Auth0 /userinfo response
        private class Auth0UserInfo
        {
            public string Sub { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Nickname { get; set; } = string.Empty;
            public string Picture { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public bool EmailVerified { get; set; }
        }
    }
}