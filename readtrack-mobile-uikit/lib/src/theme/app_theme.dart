import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import '../foundation/app_colors.dart';
import '../foundation/app_radius.dart';
import '../foundation/app_spacing.dart';
import '../foundation/app_typography.dart';

/// Factory for creating the application's [ThemeData].
///
/// This class centralizes all theme configuration logic, ensuring that
/// the application maintains a consistent visual identity across both
/// Light and Dark modes. It maps the foundation tokens (Colors, Typography,
/// Spacing, Radius) to Flutter's Material 3 theme properties.
class AppTheme {
  // Private constructor to prevent instantiation
  AppTheme._();

  /// Returns the configured [ThemeData] for Light Mode.
  static ThemeData get light {
    final colorScheme = AppColors.lightScheme;
    return _buildTheme(
      brightness: Brightness.light,
      colorScheme: colorScheme,
      scaffoldBackgroundColor: AppColors.backgroundLight,
    );
  }

  /// Returns the configured [ThemeData] for Dark Mode.
  static ThemeData get dark {
    final colorScheme = AppColors.darkScheme;
    return _buildTheme(
      brightness: Brightness.dark,
      colorScheme: colorScheme,
      scaffoldBackgroundColor: AppColors.backgroundDark,
    );
  }

  /// Internal builder method to construct the base theme and apply component overrides.
  static ThemeData _buildTheme({
    required Brightness brightness,
    required ColorScheme colorScheme,
    required Color scaffoldBackgroundColor,
  }) {
    final baseTheme = ThemeData(
      useMaterial3: true,
      brightness: brightness,
      colorScheme: colorScheme,
      scaffoldBackgroundColor: scaffoldBackgroundColor,
      fontFamily: 'Inter', // Assuming Inter based on modern app standards, or derived from Typography
      textTheme: AppTypography.textTheme,
    );

    return baseTheme.copyWith(
      appBarTheme: _buildAppBarTheme(baseTheme, colorScheme),
      elevatedButtonTheme: _buildElevatedButtonTheme(colorScheme),
      outlinedButtonTheme: _buildOutlinedButtonTheme(colorScheme),
      textButtonTheme: _buildTextButtonTheme(colorScheme),
      inputDecorationTheme: _buildInputDecorationTheme(colorScheme),
      cardTheme: _buildCardTheme(colorScheme),
      dividerTheme: _buildDividerTheme(colorScheme),
      bottomNavigationBarTheme: _buildBottomNavigationBarTheme(colorScheme),
    );
  }

  static AppBarTheme _buildAppBarTheme(ThemeData theme, ColorScheme colors) {
    return AppBarTheme(
      backgroundColor: colors.surface,
      foregroundColor: colors.onSurface,
      elevation: 0,
      centerTitle: true,
      scrolledUnderElevation: 0,
      systemOverlayStyle: SystemUiOverlayStyle(
        statusBarColor: Colors.transparent,
        statusBarIconBrightness:
            theme.brightness == Brightness.light ? Brightness.dark : Brightness.light,
        statusBarBrightness: theme.brightness,
      ),
      titleTextStyle: AppTypography.titleLarge.copyWith(
        color: colors.onSurface,
        fontWeight: FontWeight.w600,
      ),
    );
  }

  static ElevatedButtonThemeData _buildElevatedButtonTheme(ColorScheme colors) {
    return ElevatedButtonThemeData(
      style: ElevatedButton.styleFrom(
        backgroundColor: colors.primary,
        foregroundColor: colors.onPrimary,
        elevation: 0,
        minimumSize: const Size(double.infinity, 48),
        padding: const EdgeInsets.symmetric(
          horizontal: AppSpacing.lg,
          vertical: AppSpacing.sm,
        ),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(AppRadius.md),
        ),
        textStyle: AppTypography.labelLarge.copyWith(
          fontWeight: FontWeight.w600,
        ),
      ),
    );
  }

  static OutlinedButtonThemeData _buildOutlinedButtonTheme(ColorScheme colors) {
    return OutlinedButtonThemeData(
      style: OutlinedButton.styleFrom(
        foregroundColor: colors.primary,
        side: BorderSide(color: colors.outline),
        minimumSize: const Size(double.infinity, 48),
        padding: const EdgeInsets.symmetric(
          horizontal: AppSpacing.lg,
          vertical: AppSpacing.sm,
        ),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(AppRadius.md),
        ),
        textStyle: AppTypography.labelLarge.copyWith(
          fontWeight: FontWeight.w600,
        ),
      ),
    );
  }

  static TextButtonThemeData _buildTextButtonTheme(ColorScheme colors) {
    return TextButtonThemeData(
      style: TextButton.styleFrom(
        foregroundColor: colors.primary,
        padding: const EdgeInsets.symmetric(
          horizontal: AppSpacing.sm,
          vertical: AppSpacing.xs,
        ),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(AppRadius.sm),
        ),
        textStyle: AppTypography.labelLarge.copyWith(
          fontWeight: FontWeight.w600,
        ),
      ),
    );
  }

  static InputDecorationTheme _buildInputDecorationTheme(ColorScheme colors) {
    final border = OutlineInputBorder(
      borderRadius: BorderRadius.circular(AppRadius.md),
      borderSide: BorderSide(color: colors.outline, width: 1),
    );

    return InputDecorationTheme(
      filled: true,
      fillColor: colors.surfaceContainer, // M3 Container color
      contentPadding: const EdgeInsets.symmetric(
        horizontal: AppSpacing.md,
        vertical: AppSpacing.md,
      ),
      border: border,
      enabledBorder: border,
      focusedBorder: border.copyWith(
        borderSide: BorderSide(color: colors.primary, width: 2),
      ),
      errorBorder: border.copyWith(
        borderSide: BorderSide(color: colors.error, width: 1),
      ),
      focusedErrorBorder: border.copyWith(
        borderSide: BorderSide(color: colors.error, width: 2),
      ),
      disabledBorder: border.copyWith(
        borderSide: BorderSide(color: colors.onSurface.withOpacity(0.12), width: 1),
      ),
      labelStyle: AppTypography.bodyMedium.copyWith(color: colors.onSurfaceVariant),
      hintStyle: AppTypography.bodyMedium.copyWith(color: colors.onSurfaceVariant.withOpacity(0.7)),
      errorStyle: AppTypography.labelSmall.copyWith(color: colors.error),
    );
  }

  static CardTheme _buildCardTheme(ColorScheme colors) {
    return CardTheme(
      color: colors.surfaceContainerLow,
      elevation: 0,
      margin: EdgeInsets.zero,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(AppRadius.lg),
        side: BorderSide(color: colors.outlineVariant, width: 1),
      ),
      clipBehavior: Clip.antiAlias,
    );
  }

  static DividerThemeData _buildDividerTheme(ColorScheme colors) {
    return DividerThemeData(
      color: colors.outlineVariant,
      thickness: 1,
      space: 1,
    );
  }

  static BottomNavigationBarThemeData _buildBottomNavigationBarTheme(ColorScheme colors) {
    return BottomNavigationBarThemeData(
      backgroundColor: colors.surface,
      selectedItemColor: colors.primary,
      unselectedItemColor: colors.onSurfaceVariant,
      type: BottomNavigationBarType.fixed,
      elevation: 8,
      selectedLabelStyle: AppTypography.labelSmall.copyWith(fontWeight: FontWeight.w600),
      unselectedLabelStyle: AppTypography.labelSmall,
    );
  }
}