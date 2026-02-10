import 'package:flutter/material.dart';

/// Defines the border radius system for the application.
/// 
/// These constants ensure consistent rounding of corners across components
/// like buttons, cards, and input fields.
@immutable
class AppRadius {
  const AppRadius._();

  // ---------------------------------------------------------------------------
  // Raw Values
  // ---------------------------------------------------------------------------

  /// 4.0
  static const double sm = 4.0;
  
  /// 8.0
  static const double md = 8.0;
  
  /// 12.0
  static const double lg = 12.0;
  
  /// 16.0
  static const double xl = 16.0;
  
  /// 24.0
  static const double xxl = 24.0;

  // ---------------------------------------------------------------------------
  // BorderRadius Objects
  // ---------------------------------------------------------------------------

  /// BorderRadius.circular(4.0)
  static const BorderRadius roundedSm = BorderRadius.all(Radius.circular(sm));
  
  /// BorderRadius.circular(8.0)
  static const BorderRadius roundedMd = BorderRadius.all(Radius.circular(md));
  
  /// BorderRadius.circular(12.0)
  static const BorderRadius roundedLg = BorderRadius.all(Radius.circular(lg));
  
  /// BorderRadius.circular(16.0)
  static const BorderRadius roundedXl = BorderRadius.all(Radius.circular(xl));
  
  /// BorderRadius.circular(999.0) - For pill shapes
  static const BorderRadius roundedFull = BorderRadius.all(Radius.circular(999.0));
}