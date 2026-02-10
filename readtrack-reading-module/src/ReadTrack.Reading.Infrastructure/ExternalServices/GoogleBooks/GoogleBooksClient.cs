using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Infrastructure.ExternalServices.GoogleBooks;

/// <summary>
/// Infrastructure implementation for communicating with the Google Books API.
/// </summary>
public class GoogleBooksClient : IGoogleBooksClient
{
    private readonly HttpClient _httpClient;
    private readonly GoogleBooksSettings _settings;
    private readonly ILogger<GoogleBooksClient> _logger;

    public GoogleBooksClient(
        HttpClient httpClient,
        IOptions<GoogleBooksSettings> settings,
        ILogger<GoogleBooksClient> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Basic validation of settings
        if (string.IsNullOrEmpty(_settings.ApiKey))
        {
            _logger.LogWarning("Google Books API Key is missing.");
        }
    }

    public async Task<List<BookMetadata>> SearchBooksAsync(string query, CancellationToken cancellationToken)
    {
        try
        {
            var url = $"{_settings.BaseUrl}volumes?q={Uri.EscapeDataString(query)}&key={_settings.ApiKey}&maxResults=10";
            
            var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GoogleBooksSearchResponse>(cancellationToken: cancellationToken);

            if (result?.Items == null)
            {
                return new List<BookMetadata>();
            }

            return result.Items
                .Select(item => MapToDomain(item))
                .ToList();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP Error searching Google Books for query '{Query}'", query);
            throw new Exception("Failed to contact book search provider.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error searching Google Books");
            throw;
        }
    }

    public async Task<BookMetadata?> GetBookDetailsAsync(string googleBookId, CancellationToken cancellationToken)
    {
        try
        {
            var url = $"{_settings.BaseUrl}volumes/{googleBookId}?key={_settings.ApiKey}";

            var response = await _httpClient.GetAsync(url, cancellationToken);
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            var item = await response.Content.ReadFromJsonAsync<GoogleBookItem>(cancellationToken: cancellationToken);

            if (item == null) return null;

            return MapToDomain(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching book details for ID {Id}", googleBookId);
            throw;
        }
    }

    // Mapping Logic
    private static BookMetadata MapToDomain(GoogleBookItem item)
    {
        var info = item.VolumeInfo;
        
        return new BookMetadata(
            Title: info.Title ?? "Unknown Title",
            Authors: info.Authors ?? new List<string>(),
            Isbn: GetIsbn13(info.IndustryIdentifiers),
            PageCount: info.PageCount ?? 0,
            CoverUrl: info.ImageLinks?.Thumbnail?.Replace("http:", "https:"), // Ensure SSL
            GoogleBookId: item.Id,
            Description: info.Description,
            PublishedDate: info.PublishedDate
        );
    }

    private static string? GetIsbn13(List<IndustryIdentifier>? identifiers)
    {
        return identifiers?.FirstOrDefault(id => id.Type == "ISBN_13")?.Identifier 
               ?? identifiers?.FirstOrDefault(id => id.Type == "ISBN_10")?.Identifier;
    }

    // Private DTOs for Deserialization
    private class GoogleBooksSearchResponse
    {
        public List<GoogleBookItem>? Items { get; set; }
    }

    private class GoogleBookItem
    {
        public string Id { get; set; } = string.Empty;
        public VolumeInfo VolumeInfo { get; set; } = new();
    }

    private class VolumeInfo
    {
        public string? Title { get; set; }
        public List<string>? Authors { get; set; }
        public string? Description { get; set; }
        public int? PageCount { get; set; }
        public string? PublishedDate { get; set; }
        public ImageLinks? ImageLinks { get; set; }
        public List<IndustryIdentifier>? IndustryIdentifiers { get; set; }
    }

    private class ImageLinks
    {
        public string? Thumbnail { get; set; }
    }

    private class IndustryIdentifier
    {
        public string Type { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
    }
}