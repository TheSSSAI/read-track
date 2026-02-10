import 'package:flutter/material.dart';

/// Defines the spatial system for the application.
/// 
/// This class provides consistent spacing values for margins, padding, and
/// gaps to ensure visual rhythm and consistency across the UI.
/// 
/// Usage:
/// ```dart
/// Padding(padding: EdgeInsets.all(AppSpacing.md))
/// SizedBox(height: AppSpacing.lg)
/// ```
@immutable
class AppSpacing {
  const AppSpacing._();

  // ---------------------------------------------------------------------------
  // Raw Values (Logical Pixels)
  // ---------------------------------------------------------------------------

  /// 2.0
  static const double xxs = 2.0;
  
  /// 4.0
  static const double xs = 4.0;
  
  /// 8.0
  static const double sm = 8.0;
  
  /// 12.0
  static const double md = 12.0;
  
  /// 16.0
  static const double lg = 16.0;
  
  /// 24.0
  static const double xl = 24.0;
  
  /// 32.0
  static const double xxl = 32.0;
  
  /// 48.0
  static const double xxxl = 48.0;

  /// 64.0
  static const double section = 64.0;

  // ---------------------------------------------------------------------------
  // EdgeInsets Presets
  // ---------------------------------------------------------------------------

  /// EdgeInsets.all(8.0)
  static const EdgeInsets paddingSm = EdgeInsets.all(sm);
  
  /// EdgeInsets.all(12.0)
  static const EdgeInsets paddingMd = EdgeInsets.all(md);
  
  /// EdgeInsets.all(16.0)
  static const EdgeInsets paddingLg = EdgeInsets.all(lg);

  /// EdgeInsets.symmetric(horizontal: 16.0)
  static const EdgeInsets paddingHrzLg = EdgeInsets.symmetric(horizontal: lg);
  
  /// EdgeInsets.symmetric(vertical: 16.0)
  static const EdgeInsets paddingVrtLg = EdgeInsets.symmetric(vertical: lg);

  /// EdgeInsets.symmetric(horizontal: 12.0, vertical: 8.0)
  /// Ideal for buttons or chips
  static const EdgeInsets paddingBtn = EdgeInsets.symmetric(horizontal: md, vertical: sm);
}