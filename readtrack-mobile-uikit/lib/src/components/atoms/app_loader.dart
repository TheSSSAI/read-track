import 'package:flutter/material.dart';
import '../../foundation/app_colors.dart';

/// A standardized loading indicator for the application.
///
/// Wraps the native [CircularProgressIndicator] with application-specific
/// styling defaults to ensure consistency across the app.
class AppLoader extends StatelessWidget {
  /// The color of the progress indicator.
  ///
  /// If null, defaults to [AppColors.primary].
  final Color? color;

  /// The size (width and height) of the loader.
  ///
  /// Defaults to 24.0 logical pixels.
  final double size;

  /// The width of the line used to draw the circle.
  ///
  /// Defaults to 3.0.
  final double strokeWidth;

  /// Creates a standardized application loader.
  const AppLoader({
    super.key,
    this.color,
    this.size = 24.0,
    this.strokeWidth = 3.0,
  });

  @override
  Widget build(BuildContext context) {
    return Center(
      child: SizedBox(
        width: size,
        height: size,
        child: CircularProgressIndicator(
          color: color ?? AppColors.primary,
          strokeWidth: strokeWidth,
          strokeCap: StrokeCap.round,
        ),
      ),
    );
  }
}