using System.ComponentModel.DataAnnotations;

namespace ReadTrack.Users.Infrastructure.Configuration
{
    /// <summary>
    /// Strongly-typed configuration class for Auth0 settings.
    /// Maps to the "Auth0" section in appsettings.json.
    /// </summary>
    public class Auth0Settings
    {
        public const string SectionName = "Auth0";

        /// <summary>
        /// The Auth0 tenant domain (e.g., "readtrack.auth0.com").
        /// </summary>
        [Required(ErrorMessage = "Auth0 Domain is required.")]
        [Url(ErrorMessage = "Auth0 Domain must be a valid URL.")]
        public string Domain { get; set; } = string.Empty;

        /// <summary>
        /// The API Audience identifier.
        /// </summary>
        [Required(ErrorMessage = "Auth0 Audience is required.")]
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// The Client ID for the Management API interaction (if needed for profile updates).
        /// </summary>
        [Required(ErrorMessage = "Auth0 ClientId is required.")]
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// The Client Secret for the Management API interaction.
        /// </summary>
        [Required(ErrorMessage = "Auth0 ClientSecret is required.")]
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// Checks if the provided configuration is valid.
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Domain) &&
                   !string.IsNullOrWhiteSpace(Audience) &&
                   !string.IsNullOrWhiteSpace(ClientId) &&
                   !string.IsNullOrWhiteSpace(ClientSecret);
        }
    }
}