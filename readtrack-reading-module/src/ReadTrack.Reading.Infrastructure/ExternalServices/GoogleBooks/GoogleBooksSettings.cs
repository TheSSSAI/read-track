namespace ReadTrack.Reading.Infrastructure.ExternalServices.GoogleBooks;

/// <summary>
/// Configuration settings for the Google Books API client.
/// </summary>
public sealed class GoogleBooksSettings
{
    /// <summary>
    /// The configuration section key.
    /// </summary>
    public const string SectionName = "GoogleBooks";

    /// <summary>
    /// Gets or sets the API Key for authenticating requests to Google Books.
    /// This should be stored securely (e.g., AWS Secrets Manager) in production.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base URL for the Google Books API.
    /// Default: https://www.googleapis.com/books/v1/
    /// </summary>
    public string BaseUrl { get; set; } = "https://www.googleapis.com/books/v1/";

    /// <summary>
    /// Gets or sets the timeout in seconds for API requests.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 10;

    /// <summary>
    /// Gets or sets the number of retry attempts for transient failures.
    /// </summary>
    public int RetryCount { get; set; } = 3;
}