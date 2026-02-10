/// Represents the current authentication state of the user within the application.
///
/// This enum is used by the Authentication State Management system to determine
/// routing logic (redirects) and UI rendering (conditional widgets).
enum AuthStatus {
  /// The initial state when the application is launching and checking for
  /// persisted sessions. The UI should typically show a splash screen.
  initial,

  /// The state when the user has successfully logged in and holds a valid
  /// session token.
  authenticated,

  /// The state when the user is explicitly logged out or no valid session
  /// exists.
  unauthenticated,

  /// The state when an error occurred during the authentication check or
  /// login process (e.g., network failure, invalid credentials).
  failure;

  /// Returns true if the user is authenticated.
  bool get isAuthenticated => this == AuthStatus.authenticated;

  /// Returns true if the app is still initializing auth state.
  bool get isInitial => this == AuthStatus.initial;
}