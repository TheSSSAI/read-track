import 'dart:async';
import 'package:flutter_test/flutter_test.dart';

/// Global configuration for Flutter tests in the UI Kit.
/// This ensures consistent setup for all widget tests, including font loading
/// and HTTP overriding if necessary for image caching components.
Future<void> testExecutable(FutureOr<void> Function() testMain) async {
  // Global setUp for all tests
  setUpAll(() {
    // In a real scenario, we might load fonts here using a FontLoader
    // if we were doing precise golden tests.
    // For now, we ensure the binding is initialized.
    TestWidgetsFlutterBinding.ensureInitialized();
  });

  await testMain();
}