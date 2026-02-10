import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'app_colors.dart';

/// Defines the typography system for the application using [GoogleFonts].
/// 
/// This class configures the [TextTheme] consistent with Material 3 type scale
/// but customized for the reading application's readability requirements.
@immutable
class AppTypography {
  const AppTypography._();

  /// The primary font family used throughout the application.
  /// Using 'Lato' for a clean, humanist sans-serif look optimized for reading.
  static final String? _fontFamily = GoogleFonts.lato().fontFamily;

  /// Returns the configured [TextTheme] based on the current [AppColors].
  /// 
  /// The [isDark] parameter determines the default color of the text
  /// (High contrast white for dark mode, dark grey for light mode).
  static TextTheme getTextTheme({required bool isDark}) {
    final Color mainTextColor = isDark ? AppColors.white : const Color(0xFF111827); // neutral900
    final Color secondaryTextColor = isDark ? const Color(0xFFD1D5DB) : const Color(0xFF4B5563); // neutral300 : neutral600

    return TextTheme(
      // Display: Large text for intros/splash
      displayLarge: GoogleFonts.lato(
        fontSize: 57,
        fontWeight: FontWeight.w400,
        height: 1.12, // 64px
        letterSpacing: -0.25,
        color: mainTextColor,
      ),
      displayMedium: GoogleFonts.lato(
        fontSize: 45,
        fontWeight: FontWeight.w400,
        height: 1.16, // 52px
        color: mainTextColor,
      ),
      displaySmall: GoogleFonts.lato(
        fontSize: 36,
        fontWeight: FontWeight.w400,
        height: 1.22, // 44px
        color: mainTextColor,
      ),

      // Headline: Section headers
      headlineLarge: GoogleFonts.lato(
        fontSize: 32,
        fontWeight: FontWeight.w400,
        height: 1.25, // 40px
        color: mainTextColor,
      ),
      headlineMedium: GoogleFonts.lato(
        fontSize: 28,
        fontWeight: FontWeight.w400,
        height: 1.29, // 36px
        color: mainTextColor,
      ),
      headlineSmall: GoogleFonts.lato(
        fontSize: 24,
        fontWeight: FontWeight.w400,
        height: 1.33, // 32px
        color: mainTextColor,
      ),

      // Title: Component headers (Cards, Dialogs)
      titleLarge: GoogleFonts.lato(
        fontSize: 22,
        fontWeight: FontWeight.w500, // Medium
        height: 1.27, // 28px
        color: mainTextColor,
      ),
      titleMedium: GoogleFonts.lato(
        fontSize: 16,
        fontWeight: FontWeight.w500,
        height: 1.5, // 24px
        letterSpacing: 0.15,
        color: mainTextColor,
      ),
      titleSmall: GoogleFonts.lato(
        fontSize: 14,
        fontWeight: FontWeight.w500,
        height: 1.43, // 20px
        letterSpacing: 0.1,
        color: secondaryTextColor,
      ),

      // Body: Primary reading content
      bodyLarge: GoogleFonts.lato(
        fontSize: 16,
        fontWeight: FontWeight.w400,
        height: 1.5, // 24px
        letterSpacing: 0.5,
        color: mainTextColor,
      ),
      bodyMedium: GoogleFonts.lato(
        fontSize: 14,
        fontWeight: FontWeight.w400,
        height: 1.43, // 20px
        letterSpacing: 0.25,
        color: mainTextColor,
      ),
      bodySmall: GoogleFonts.lato(
        fontSize: 12,
        fontWeight: FontWeight.w400,
        height: 1.33, // 16px
        letterSpacing: 0.4,
        color: secondaryTextColor,
      ),

      // Label: Buttons, Inputs, Captions
      labelLarge: GoogleFonts.lato(
        fontSize: 14,
        fontWeight: FontWeight.w500,
        height: 1.43, // 20px
        letterSpacing: 0.1,
        color: mainTextColor,
      ),
      labelMedium: GoogleFonts.lato(
        fontSize: 12,
        fontWeight: FontWeight.w500,
        height: 1.33, // 16px
        letterSpacing: 0.5,
        color: secondaryTextColor,
      ),
      labelSmall: GoogleFonts.lato(
        fontSize: 11,
        fontWeight: FontWeight.w500,
        height: 1.45, // 16px
        letterSpacing: 0.5,
        color: secondaryTextColor,
      ),
    );
  }
}