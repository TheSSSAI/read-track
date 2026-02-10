namespace ReadTrack.Host.Configuration;

/// <summary>
/// Strongly typed configuration for Cross-Origin Resource Sharing (CORS) policies.
/// </summary>
public sealed class CorsConfiguration
{
    /// <summary>
    /// The configuration section name in appsettings.json.
    /// </summary>
    public const string SectionName = "AllowedOrigins";

    /// <summary>
    /// List of allowed origin URLs that can access the API.
    /// </summary>
    public string[] Urls { get; set; } = Array.Empty<string>();
}