import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

import 'routes.dart';
import '../../features/auth/data/auth_state.dart';
import '../../features/auth/data/auth_status.dart';
import '../../features/auth/presentation/controllers/auth_controller.dart';

// Implicitly referencing screen imports based on project structure.
// These files are expected to exist in the next dependency levels.
import '../../features/auth/presentation/screens/login_screen.dart';
import '../../features/onboarding/presentation/screens/onboarding_screen.dart';
// Assuming Dashboard module exists as per requirements, even if not explicitly in the current generation batch.
import '../../features/dashboard/presentation/screens/dashboard_screen.dart';

/// Global key for the root navigator, essential for overlay and dialog handling.
final _rootNavigatorKey = GlobalKey<NavigatorState>();

/// Provides the [GoRouter] configuration for the application.
/// 
/// This router includes:
/// - Reactive redirection based on [AuthController] state.
/// - Route definitions for Login, Onboarding, and Dashboard.
/// - Proper handling of loading and error states during navigation.
final appRouterProvider = Provider<GoRouter>((ref) {
  // We need a Listenable to trigger router refreshes when auth state changes.
  // We use a ValueNotifier that updates whenever the AuthController state changes.
  final authStateListenable = ValueNotifier<AsyncValue<AuthState>>(const AsyncLoading());
  
  ref.listen<AsyncValue<AuthState>>(
    authControllerProvider,
    (_, next) {
      authStateListenable.value = next;
    },
  );

  return GoRouter(
    navigatorKey: _rootNavigatorKey,
    initialLocation: Routes.dashboard,
    refreshListenable: authStateListenable,
    debugLogDiagnostics: true, // Useful for debugging navigation flows
    
    // Redirect logic determines access control based on authentication and user profile state.
    redirect: (BuildContext context, GoRouterState state) {
      // 1. Resolve Auth State
      final authState = ref.read(authControllerProvider);
      
      // If auth is still loading, we can stay on the splash (or return null to let the current route stay).
      // If we are in an error state, we might default to unauthenticated behavior for safety.
      if (authState.isLoading) {
        return null; 
      }

      final authData = authState.valueOrNull;
      final isLoggedIn = authData?.status == AuthStatus.authenticated;
      final isLoggingIn = state.matchedLocation == Routes.login;

      // 2. Handle Unauthenticated State
      if (!isLoggedIn) {
        return isLoggingIn ? null : Routes.login;
      }

      // 3. Handle Authenticated State
      // At this point, user is logged in. We check profile completeness.
      final user = authData?.user;
      
      // Safety check: if authenticated but user data is missing (rare), force login or loading.
      if (user == null) {
        return isLoggingIn ? null : Routes.login;
      }

      final hasCompletedOnboarding = user.hasCompletedOnboarding;
      final isOnboarding = state.matchedLocation == Routes.onboarding;

      // 4. Enforce Onboarding
      if (!hasCompletedOnboarding) {
        // If user hasn't finished onboarding, force them to the onboarding screen.
        return isOnboarding ? null : Routes.onboarding;
      }

      // 5. Navigate to Dashboard
      // If user is logged in AND has completed onboarding, they should be in the app.
      // If they are currently on Login or Onboarding screens, redirect to Dashboard.
      if (isLoggingIn || isOnboarding) {
        return Routes.dashboard;
      }

      // Allow navigation to other authenticated routes (e.g. settings, details).
      return null;
    },
    
    routes: [
      GoRoute(
        path: Routes.login,
        name: 'login',
        builder: (context, state) => const LoginScreen(),
      ),
      GoRoute(
        path: Routes.onboarding,
        name: 'onboarding',
        builder: (context, state) => const OnboardingScreen(),
      ),
      GoRoute(
        path: Routes.dashboard,
        name: 'dashboard',
        builder: (context, state) => const DashboardScreen(),
      ),
    ],
    
    // Optional: errorBuilder can be added here for 404s
  );
});