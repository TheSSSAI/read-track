using ReadTrack.Host.Extensions;
using Serilog;

// Initialize the bootstrap logger to capture startup errors that might occur 
// before the Dependency Injection container and full logger configuration are ready.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Initializing ReadTrack.Host application startup sequence...");

    var builder = WebApplication.CreateBuilder(args);

    // -------------------------------------------------------------------------
    // Host Configuration
    // -------------------------------------------------------------------------
    
    // Replace the default logging provider with Serilog.
    // This enables structured logging, configuration from appsettings.json, and enrichment.
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName());

    // -------------------------------------------------------------------------
    // Service Registration (Dependency Injection Composition Root)
    // -------------------------------------------------------------------------

    // 1. Register Cross-Cutting Host Infrastructure
    // This includes Authentication (Auth0), CORS policies, Global Exception Handling,
    // Health Checks, and core ASP.NET MVC/API controllers.
    // Implemented in: src/ReadTrack.Host/Extensions/ServiceCollectionExtensions.cs
    builder.Services.AddHostInfrastructure(builder.Configuration);

    // 2. Register Business Domain Modules
    // This wires up the specific application modules (Users, Reading, Monetization, etc.)
    // enforcing the modular monolith architecture boundaries.
    // Implemented in: src/ReadTrack.Host/Extensions/ModuleRegistrationExtensions.cs
    builder.Services.AddBusinessModules(builder.Configuration);

    // 3. Register API Documentation (Swagger/OpenAPI)
    // Configures Swashbuckle to generate API specs from the aggregated controllers.
    // Implemented in: src/ReadTrack.Host/Extensions/SwaggerExtensions.cs
    builder.Services.AddSwaggerConfiguration();

    // Build the WebApplication instance
    var app = builder.Build();

    // -------------------------------------------------------------------------
    // Middleware Pipeline Configuration
    // -------------------------------------------------------------------------

    // Configure the HTTP request processing pipeline order.
    // Order matches: ExceptionHandler -> HSTS -> HttpsRedirection -> SerilogRequestLogging 
    // -> CORS -> Authentication -> Authorization -> Swagger -> MapControllers.
    // Implemented in: src/ReadTrack.Host/Extensions/MiddlewareExtensions.cs
    app.UseHostMiddleware();

    Log.Information("ReadTrack.Host application configuration successful. Starting Kestrel server...");

    // Start the application and listen for incoming HTTP requests
    await app.RunAsync();
}
catch (Exception ex)
{
    // Catch-all for any exceptions that escape the startup phase
    Log.Fatal(ex, "ReadTrack.Host application terminated unexpectedly due to a critical startup error.");
    throw;
}
finally
{
    // Ensure all buffered log messages are flushed to their sinks before the process exits
    await Log.CloseAndFlushAsync();
}