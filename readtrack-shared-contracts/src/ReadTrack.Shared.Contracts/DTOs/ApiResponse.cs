using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.DTOs;

/// <summary>
/// Represents a standardized envelope for all API responses, ensuring consistent structure
/// for both successful operations and error reporting across the application.
/// </summary>
/// <typeparam name="T">The type of the data payload.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; init; }

    /// <summary>
    /// A human-readable message describing the result of the operation.
    /// Primarily used for displaying success confirmations or error summaries.
    /// </summary>
    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; init; }

    /// <summary>
    /// The payload of the response. This will be null if the operation failed or if there is no return data.
    /// </summary>
    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Data { get; init; }

    /// <summary>
    /// A collection of specific validation errors or failure reasons.
    /// Populated only when IsSuccess is false.
    /// </summary>
    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string>? Errors { get; init; }

    /// <summary>
    /// The UTC timestamp when the response was generated. 
    /// Useful for debugging and client-side synchronization logic.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// A correlation ID to track the request through the system.
    /// </summary>
    [JsonPropertyName("correlationId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CorrelationId { get; init; }

    /// <summary>
    /// Default constructor for serialization purposes.
    /// </summary>
    [JsonConstructor]
    public ApiResponse() 
    {
        Timestamp = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Private constructor to enforce usage of factory methods.
    /// </summary>
    private ApiResponse(bool isSuccess, T? data, string? message, IEnumerable<string>? errors, string? correlationId)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        Errors = errors;
        CorrelationId = correlationId;
        Timestamp = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Creates a successful response with a data payload.
    /// </summary>
    /// <param name="data">The data to return.</param>
    /// <param name="message">Optional success message.</param>
    /// <param name="correlationId">Optional correlation ID for tracing.</param>
    /// <returns>A successful ApiResponse containing the data.</returns>
    public static ApiResponse<T> Success(T data, string? message = null, string? correlationId = null)
    {
        return new ApiResponse<T>(true, data, message, null, correlationId);
    }

    /// <summary>
    /// Creates a successful response indicating creation of a resource.
    /// </summary>
    /// <param name="data">The created resource data.</param>
    /// <param name="message">Optional success message (e.g., "Resource created").</param>
    /// <returns>A successful ApiResponse.</returns>
    public static ApiResponse<T> Created(T data, string message = "Resource created successfully")
    {
        return new ApiResponse<T>(true, data, message, null, null);
    }

    /// <summary>
    /// Creates a failed response with a general error message.
    /// </summary>
    /// <param name="message">Description of the failure.</param>
    /// <param name="correlationId">Optional correlation ID for tracing.</param>
    /// <returns>A failed ApiResponse.</returns>
    public static ApiResponse<T> Failure(string message, string? correlationId = null)
    {
        return new ApiResponse<T>(false, default, message, new[] { message }, correlationId);
    }

    /// <summary>
    /// Creates a failed response with a list of specific validation errors.
    /// </summary>
    /// <param name="errors">The collection of error messages.</param>
    /// <param name="message">A summary message for the failure.</param>
    /// <param name="correlationId">Optional correlation ID.</param>
    /// <returns>A failed ApiResponse.</returns>
    public static ApiResponse<T> Failure(IEnumerable<string> errors, string message = "Validation failed", string? correlationId = null)
    {
        return new ApiResponse<T>(false, default, message, errors, correlationId);
    }
}

/// <summary>
/// Non-generic helper class for creating responses without a data payload (e.g. void actions).
/// </summary>
public static class ApiResponse
{
    /// <summary>
    /// Creates a successful response with no data payload.
    /// </summary>
    public static ApiResponse<object> Success(string? message = null)
    {
        return ApiResponse<object>.Success(new object(), message);
    }

    /// <summary>
    /// Creates a failed response with no data payload type context.
    /// </summary>
    public static ApiResponse<object> Failure(string message)
    {
        return ApiResponse<object>.Failure(message);
    }
    
    /// <summary>
    /// Creates a failed response with multiple errors and no data payload type context.
    /// </summary>
    public static ApiResponse<object> Failure(IEnumerable<string> errors, string message = "One or more errors occurred")
    {
        return ApiResponse<object>.Failure(errors, message);
    }
}