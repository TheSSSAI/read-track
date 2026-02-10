/// Defines the routing constants for the application.
/// 
/// This class centralizes all route names and paths to prevent magic strings
/// and ensure type safety when navigating with GoRouter.
class Routes {
  Routes._();

  // -- Route Paths (URI segments) --

  /// Path: /
  static const String rootPath = '/';

  /// Path: /login
  static const String loginPath = '/login';

  /// Path: /onboarding
  static const String onboardingPath = '/onboarding';

  /// Path: /dashboard
  static const String dashboardPath = '/dashboard';

  /// Path: /settings
  static const String settingsPath = '/settings';

  /// Path: /profile
  static const String profilePath = '/profile';

  /// Path: /library
  static const String libraryPath = '/library';

  // -- Route Names (Internal Identifiers) --

  /// Name: splash
  static const String splashName = 'splash';

  /// Name: login
  static const String loginName = 'login';

  /// Name: onboarding
  static const String onboardingName = 'onboarding';

  /// Name: dashboard
  static const String dashboardName = 'dashboard';

  /// Name: settings
  static const String settingsName = 'settings';

  /// Name: profile
  static const String profileName = 'profile';

  /// Name: library
  static const String libraryName = 'library';

  // -- Sub-routes or Parameterized Routes --

  /// Path: details/:id
  static const String bookDetailsPath = 'details/:id';
  
  /// Name: bookDetails
  static const String bookDetailsName = 'bookDetails';

  /// Helper to build path with parameters
  static String bookDetails(String id) => '/library/details/$id';
}