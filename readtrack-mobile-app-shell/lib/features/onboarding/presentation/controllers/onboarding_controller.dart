import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../../core/providers/dependency_providers.dart';
import '../../../auth/presentation/controllers/auth_controller.dart';

/// Manages the state of the onboarding process.
/// 
/// Uses [AsyncNotifier] to handle the asynchronous nature of updating the user profile
/// and propagating the state change to the authentication controller.
class OnboardingController extends AsyncNotifier<void> {
  @override
  Future<void> build() async {
    // Initial state is void (idle), waiting for user action.
    return;
  }

  /// Marks the user's onboarding as complete in the backend and refreshes local auth state.
  /// 
  /// This method performs the following steps:
  /// 1. Sets the controller state to [AsyncLoading].
  /// 2. Calls the API client to update the user profile.
  /// 3. Upon success, triggers a refresh of the [AuthController] to update the global [User] object.
  ///    This helps the [AppRouter] detect the change in `hasCompletedOnboarding` and redirect.
  /// 4. Handles any errors by setting the state to [AsyncError].
  Future<void> completeOnboarding() async {
    state = const AsyncLoading();
    
    state = await AsyncValue.guard(() async {
      final apiClient = ref.read(apiClientProvider);
      
      // Update the user profile on the backend to reflect onboarding completion.
      // We assume the API client exposes a method for partial profile updates.
      await apiClient.updateUserProfile(
        hasCompletedOnboarding: true,
      );

      // Refresh the AuthController. This is critical as the Router listens to the AuthController.
      // Updating the AuthController will fetch the fresh UserProfile (with onboarding=true)
      // and trigger the redirect logic to the Dashboard.
      await ref.read(authControllerProvider.notifier).refresh();
    });
  }
}

/// Global provider for the [OnboardingController].
final onboardingControllerProvider = AsyncNotifierProvider<OnboardingController, void>(() {
  return OnboardingController();
});