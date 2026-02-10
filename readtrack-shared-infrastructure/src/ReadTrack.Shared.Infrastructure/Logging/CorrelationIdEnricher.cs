using System;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace ReadTrack.Shared.Infrastructure.Logging
{
    /// <summary>
    /// A Serilog enricher that adds a Correlation ID to every log event.
    /// Retrieves the ID from the current HTTP context headers or generates a new one if missing.
    /// Satisfies REQ-MON-001 for distributed tracing.
    /// </summary>
    public class CorrelationIdEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CorrelationIdPropertyName = "CorrelationId";
        private const string CorrelationIdHeaderName = "X-Correlation-ID";

        public CorrelationIdEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Enriches the log event with the CorrelationId property.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating log properties.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (propertyFactory == null) throw new ArgumentNullException(nameof(propertyFactory));

            // Skip enrichment if we are outside of an HTTP context (e.g., startup or background threads without context)
            if (_httpContextAccessor.HttpContext == null)
            {
                return;
            }

            var correlationId = GetCorrelationId();

            if (!string.IsNullOrEmpty(correlationId))
            {
                var property = propertyFactory.CreateProperty(CorrelationIdPropertyName, correlationId);
                logEvent.AddPropertyIfAbsent(property);
            }
        }

        private string? GetCorrelationId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            // 1. Check if we already have it in the Items collection (processed by middleware)
            if (httpContext!.Items.TryGetValue(CorrelationIdPropertyName, out var correlationIdObj) && 
                correlationIdObj is string correlationIdItem)
            {
                return correlationIdItem;
            }

            // 2. Check the Request Headers
            if (httpContext.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var headerValue))
            {
                return headerValue.ToString();
            }

            // 3. Fallback to TraceIdentifier provided by ASP.NET Core
            return httpContext.TraceIdentifier;
        }
    }
}