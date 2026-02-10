using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ReadTrack.Host.Infrastructure;

/// <summary>
/// Centralized exception handler that processes unhandled exceptions from the middleware pipeline
/// and converts them into standardized RFC 7807 Problem Details responses.
/// Implements the .NET 8 IExceptionHandler pattern for improved performance and maintainability.
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for capturing exception details.</param>
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Attempts to handle the specified exception asynchronously.
    /// </summary>
    /// <param name="httpContext">The HTTP context associated with the request.</param>
    /// <param name="exception">The unhandled exception.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the exception was handled; otherwise, false.</returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(exception);

        // Capture the trace ID for distributed tracing correlation
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        // Log the exception with structured context
        _logger.LogError(
            exception,
            "An unhandled exception occurred while processing the request. TraceId: {TraceId}",
            traceId);

        // Determine the HTTP status code and title based on the exception type
        var (statusCode, title, detail) = MapExceptionToResponse(exception);

        // Configure the problem details object (RFC 7807)
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Type = GetRfcTypeUrl(statusCode),
            Instance = httpContext.Request.Path
        };

        // Add extension properties for observability
        problemDetails.Extensions.Add("traceId", traceId);
        problemDetails.Extensions.Add("timestamp", DateTime.UtcNow);

        // Write the response
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        // Return true to indicate the exception was handled and pipeline execution should stop here
        return true;
    }

    /// <summary>
    /// Maps specific exception types to appropriate HTTP status codes and user-friendly messages.
    /// </summary>
    private static (int StatusCode, string Title, string Detail) MapExceptionToResponse(Exception exception)
    {
        return exception switch
        {
            // Client Errors (4xx)
            BadHttpRequestException badReqEx => 
                (StatusCodes.Status400BadRequest, "Bad Request", badReqEx.Message),
            
            ArgumentNullException argNullEx => 
                (StatusCodes.Status400BadRequest, "Invalid Arguments", $"A required argument was missing: {argNullEx.ParamName}"),
            
            ArgumentException argEx => 
                (StatusCodes.Status400BadRequest, "Invalid Arguments", argEx.Message),
            
            KeyNotFoundException keyEx => 
                (StatusCodes.Status404NotFound, "Resource Not Found", keyEx.Message),
            
            UnauthorizedAccessException unauthorizedEx => 
                (StatusCodes.Status401Unauthorized, "Unauthorized", unauthorizedEx.Message),
            
            OperationCanceledException => 
                (499, "Client Closed Request", "The operation was canceled by the client."),

            // Server Errors (5xx)
            NotImplementedException => 
                (StatusCodes.Status501NotImplemented, "Not Implemented", "The requested functionality is not implemented."),
            
            TimeoutException => 
                (StatusCodes.Status504GatewayTimeout, "Timeout", "The operation timed out."),

            // Default Fallback
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred. Please contact support.")
        };
    }

    /// <summary>
    /// Returns a standard RFC reference URL for the given status code.
    /// </summary>
    private static string GetRfcTypeUrl(int statusCode)
    {
        return statusCode switch
        {
            400 => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            401 => "https://tools.ietf.org/html/rfc7235#section-3.1",
            403 => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            404 => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            408 => "https://tools.ietf.org/html/rfc7231#section-6.5.7",
            422 => "https://tools.ietf.org/html/rfc4918#section-11.2",
            500 => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            501 => "https://tools.ietf.org/html/rfc7231#section-6.6.2",
            504 => "https://tools.ietf.org/html/rfc7231#section-6.6.5",
            _ => "https://tools.ietf.org/html/rfc7231"
        };
    }
}