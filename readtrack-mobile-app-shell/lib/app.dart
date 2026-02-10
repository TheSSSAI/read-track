import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'config/theme/app_theme.dart';
import 'core/providers/dependency_providers.dart';

/// The root widget of the ReadTrack application.
///
/// This widget acts as the composition root for the UI tree, configuring the
/// [MaterialApp] with routing, theming, and localization.
///
/// It extends [ConsumerWidget] to integrate with Riverpod for dependency injection,
/// specifically to access the [GoRouter] instance provided by [appRouterProvider].
///
/// Architectural Role: Presentation Layer - Application Shell
/// Dependencies:
/// - [AppRouter] (via [appRouterProvider]): Handles navigation and deep linking.
/// - [AppTheme]: Provides visual styling for Light and Dark modes.
class ReadTrackApp extends ConsumerWidget {
  /// Creates the root [ReadTrackApp] widget.
  ///
  /// This constructor is constant to allow for optimization by the Flutter framework.
  const ReadTrackApp({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch the router provider to obtain the GoRouter configuration.
    // This allows the app to react to changes in the routing configuration if necessary,
    // although typically the router instance is stable.
    // The provider is defined in level 2 (lib/core/providers/dependency_providers.dart).
    final goRouter = ref.watch(appRouterProvider);

    return MaterialApp.router(
      // Application Title
      title: 'ReadTrack',

      // Routing Configuration via GoRouter
      // This delegates all navigation logic to the configured GoRouter instance.
      routerConfig: goRouter,

      // Debug Configuration
      // Disables the "DEBUG" banner in the top-right corner for a cleaner UI.
      debugShowCheckedModeBanner: false,

      // Theme Configuration
      // Implements REQ-UI-001: The application must support both light and dark themes.
      // AppTheme.lightTheme and AppTheme.darkTheme are defined in level 1.
      theme: AppTheme.lightTheme,
      darkTheme: AppTheme.darkTheme,
      
      // Theme Mode
      // By default, adopts the system's theme setting (light/dark) to match OS preference.
      // This can be overridden by watching a specific themeModeProvider if manual switching is implemented later.
      themeMode: ThemeMode.system,

      // Localization Configuration
      // Sets up standard material localizations to ensure widgets like DatePicker
      // and text selection handles appear correctly in the user's language.
      localizationsDelegates: const [
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
      ],
      supportedLocales: const [
        Locale('en', 'US'), // English (United States)
        // Additional locales can be added here as per requirements.
      ],

      // Global Builder
      // Wraps the entire application to apply global behaviors or constraints.
      builder: (context, child) {
        // Error boundary fallback if child is somehow null (should not happen in valid flow)
        final widget = child ?? const SizedBox.shrink();

        // REQ-UI-002: Accessibility & Dynamic Type
        // We wrap the app in a MediaQuery to ensure text scaling behaves reasonably.
        // While we support dynamic type, we may clamp the scale factor to prevent
        // extreme layouts from breaking, while still maintaining accessibility standards.
        return MediaQuery(
          data: MediaQuery.of(context).copyWith(
            textScaler: MediaQuery.of(context).textScaler.clamp(
                  minScaleFactor: 1.0,
                  maxScaleFactor: 2.0, // Allow up to 200% text size for accessibility
                ),
          ),
          child: widget,
        );
      },
    );
  }
}