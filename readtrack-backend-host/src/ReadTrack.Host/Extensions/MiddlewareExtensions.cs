using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ReadTrack.Host.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring the ASP.NET Core HTTP request processing pipeline.
    /// This orchestrates the order of middleware components (Security, Logging, Routing, etc.).
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configures the complete HTTP request pipeline for the application.
        /// </summary>
        /// <param name="app">The web application instance to configure.</param>
        /// <returns>The configured web application.</returns>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // 1. Global Exception Handling
            // This must be one of the first middleware components to catch exceptions from all subsequent middleware.
            // Using the empty lambda triggers the IExceptionHandler implementation registered in the DI container (GlobalExceptionHandler).
            app.UseExceptionHandler(options => { });

            // 2. HTTP Strict Transport Security (HSTS)
            // Enforces HTTPS in production environments to prevent downgrade attacks.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // 3. HTTPS Redirection
            // Redirects HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            // 4. Structured Request Logging (Serilog)
            // Logs HTTP requests with rich structured data. Placed early to capture timings of the entire pipeline
            // but after exception handling so that 500s are logged with context.
            app.UseSerilogRequestLogging(options =>
            {
                // Customize the message template to be concise and machine-readable
                options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
                
                // Don't log health check requests to avoid log noise in production
                options.GetLevel = (httpContext, elapsed, ex) =>
                {
                    if (httpContext.Request.Path.Value?.Contains("/health") == true && ex == null)
                    {
                        return Serilog.Events.LogEventLevel.Verbose;
                    }
                    return Serilog.Events.LogEventLevel.Information;
                };
            });

            // 5. Swagger / OpenAPI Documentation
            // Exposes the API definition. Typically restricted to Development, but can be enabled in Staging if needed.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReadTrack API v1");
                    // Serve the Swagger UI at the app's root
                    c.RoutePrefix = string.Empty;
                });
            }

            // 6. Cross-Origin Resource Sharing (CORS)
            // Must be applied before Response Caching and Authorization.
            // Uses the policy named "ReadTrackCorsPolicy" configured in ServiceCollectionExtensions.
            app.UseCors("ReadTrackCorsPolicy");

            // 7. Authentication
            // Validates the JWT token and constructs the User Principal.
            app.UseAuthentication();

            // 8. Authorization
            // Enforces policies based on the User Principal (e.g., Premium vs Free).
            app.UseAuthorization();

            // 9. Health Checks
            // Exposes endpoints for infrastructure monitoring (e.g., AWS Load Balancer, Kubernetes).
            
            // Liveness Probe: Simply returns 200 if the app process is running.
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false
            });

            // Readiness Probe: Returns 200 only if critical dependencies (DB, Auth0) are responsive.
            // Assumes readiness checks are tagged with "ready" during registration.
            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready")
            });

            // 10. Controller Routing
            // Maps attribute-routed controllers to the request pipeline.
            app.MapControllers();

            return app;
        }
    }
}