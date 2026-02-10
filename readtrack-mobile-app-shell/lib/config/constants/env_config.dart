/// Configuration class for environment-specific variables injected at build time.
/// 
/// Uses [String.fromEnvironment] to read values passed via `--dart-define`.
/// This ensures sensitive keys and URLs are not hardcoded in the codebase.
class EnvConfig {
  EnvConfig._();

  /// The base URL for the backend API.
  /// 
  /// Defaults to localhost for development if not provided.
  static const String apiUrl = String.fromEnvironment(
    'API_URL',
    defaultValue: 'http://localhost:5000/api/v1',
  );

  /// The current environment name (e.g., 'dev', 'staging', 'prod').
  static const String environment = String.fromEnvironment(
    'ENVIRONMENT',
    defaultValue: 'dev',
  );

  /// Whether the app is running in debug mode based on the environment flag.
  static const bool isDebug = environment == 'dev';

  /// Timeout duration for API connection attempts in milliseconds.
  static const int connectionTimeoutMs = int.fromEnvironment(
    'CONN_TIMEOUT',
    defaultValue: 30000,
  );

  /// Timeout duration for API receive attempts in milliseconds.
  static const int receiveTimeoutMs = int.fromEnvironment(
    'RECV_TIMEOUT',
    defaultValue: 30000,
  );

  /// API Key for third-party integrations if required at the app level.
  static const String apiKey = String.fromEnvironment(
    'API_KEY',
    defaultValue: '',
  );

  /// Feature flag to enable verbose logging.
  static const bool enableLogging = bool.fromEnvironment(
    'ENABLE_LOGGING',
    defaultValue: true,
  );
}