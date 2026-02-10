using System.ComponentModel.DataAnnotations;

namespace ReadTrack.Host.Configuration;

/// <summary>
/// Strongly typed configuration for Auth0 Identity Provider settings.
/// Implements the Options pattern for binding from configuration providers.
/// </summary>
public sealed class Auth0Configuration
{
    /// <summary>
    /// The configuration section name in appsettings.json.
    /// </summary>
    public const string SectionName = "Auth0";

    /// <summary>
    /// The Auth0 tenant domain (e.g., tenant.auth0.com).
    /// </summary>
    [Required(ErrorMessage = "Auth0 Domain is required")]
    [Url(ErrorMessage = "Auth0 Domain must be a valid URL")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// The API Identifier (Audience) defined in the Auth0 dashboard.
    /// </summary>
    [Required(ErrorMessage = "Auth0 Audience is required")]
    public string Audience { get; set; } = string.Empty;
}