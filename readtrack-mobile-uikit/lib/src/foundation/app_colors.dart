import 'package:flutter/material.dart';

/// Defines the semantic color palette and Material [ColorScheme]s for the application.
/// 
/// This class separates primitive hex definitions (private) from semantic tokens (public).
/// It ensures strict adherence to accessibility contrast ratios (WCAG 2.1 AA) via
/// carefully selected schemes.
@immutable
class AppColors {
  const AppColors._();

  // ---------------------------------------------------------------------------
  // Primitive Tokens (Private)
  // Based on a "Reading/Paper" aesthetic with strong contrast.
  // ---------------------------------------------------------------------------
  
  // Primary Brand (Deep Indigo/Blue)
  static const Color _brandPrimary = Color(0xFF4A6572);
  static const Color _brandPrimaryLight = Color(0xFF7893A0);
  static const Color _brandPrimaryDark = Color(0xFF1E3B47);

  // Secondary/Accent (Warm Orange/Amber for highlights)
  static const Color _brandSecondary = Color(0xFFF9AA33);
  static const Color _brandSecondaryLight = Color(0xFFFFDC69);
  static const Color _brandSecondaryDark = Color(0xFFC17B00);

  // Neutrals (Light)
  static const Color _neutralWhite = Color(0xFFFFFFFF);
  static const Color _neutral50 = Color(0xFFF9FAFB);
  static const Color _neutral100 = Color(0xFFF3F4F6);
  static const Color _neutral200 = Color(0xFFE5E7EB);
  static const Color _neutral300 = Color(0xFFD1D5DB);

  // Neutrals (Dark)
  static const Color _neutral900 = Color(0xFF111827);
  static const Color _neutral800 = Color(0xFF1F2937);
  static const Color _neutral700 = Color(0xFF374151);
  static const Color _neutral600 = Color(0xFF4B5563);

  // Functional
  static const Color _success = Color(0xFF10B981);
  static const Color _warning = Color(0xFFF59E0B);
  static const Color _error = Color(0xFFEF4444);
  static const Color _info = Color(0xFF3B82F6);

  // ---------------------------------------------------------------------------
  // Semantic Tokens (Public)
  // ---------------------------------------------------------------------------

  // Brand
  static const Color primary = _brandPrimary;
  static const Color secondary = _brandSecondary;

  // Feedback
  static const Color success = _success;
  static const Color warning = _warning;
  static const Color error = _error;
  static const Color info = _info;

  // Surfaces
  static const Color white = _neutralWhite;
  static const Color transparent = Colors.transparent;

  // ---------------------------------------------------------------------------
  // Color Schemes
  // ---------------------------------------------------------------------------

  /// The Material 3 [ColorScheme] for Light Mode.
  static const ColorScheme lightScheme = ColorScheme(
    brightness: Brightness.light,
    
    // Primary
    primary: _brandPrimary,
    onPrimary: _neutralWhite,
    primaryContainer: _brandPrimaryLight,
    onPrimaryContainer: _neutral900,

    // Secondary
    secondary: _brandSecondary,
    onSecondary: _neutral900,
    secondaryContainer: _brandSecondaryLight,
    onSecondaryContainer: _neutral900,

    // Error
    error: _error,
    onError: _neutralWhite,
    errorContainer: Color(0xFFFEE2E2), // Light red
    onErrorContainer: Color(0xFF991B1B), // Dark red

    // Background & Surface
    surface: _neutralWhite,
    onSurface: _neutral900,
    surfaceTint: _brandPrimary,
    
    // Outline & Variants
    outline: _neutral300,
    outlineVariant: _neutral200,
    inverseSurface: _neutral800,
    onInverseSurface: _neutralWhite,
  );

  /// The Material 3 [ColorScheme] for Dark Mode.
  static const ColorScheme darkScheme = ColorScheme(
    brightness: Brightness.dark,
    
    // Primary (Lighter for dark mode visibility)
    primary: _brandPrimaryLight,
    onPrimary: _neutral900,
    primaryContainer: _brandPrimaryDark,
    onPrimaryContainer: _neutral100,

    // Secondary
    secondary: _brandSecondary,
    onSecondary: _neutral900,
    secondaryContainer: _brandSecondaryDark,
    onSecondaryContainer: _neutral100,

    // Error
    error: Color(0xFFF87171), // Lighter red for dark mode
    onError: _neutral900,
    errorContainer: Color(0xFF991B1B),
    onErrorContainer: Color(0xFFFEE2E2),

    // Background & Surface
    surface: _neutral900,
    onSurface: _neutral100,
    surfaceTint: _brandPrimaryLight,
    
    // Outline & Variants
    outline: _neutral600,
    outlineVariant: _neutral700,
    inverseSurface: _neutral100,
    onInverseSurface: _neutral900,
  );
}