import 'package:flutter/material.dart';
import '../../foundation/app_colors.dart';
import '../../foundation/app_radius.dart';
import '../../foundation/app_spacing.dart';
import '../../foundation/app_typography.dart';
import '../atoms/app_loader.dart';

/// A primary action button component that strictly adheres to the design system.
///
/// This component wraps [ElevatedButton] to provide standardized styling,
/// loading states, and accessibility features required by REQ-UI-002.
class PrimaryButton extends StatelessWidget {
  /// The text label displayed on the button.
  final String label;

  /// The callback invoked when the button is tapped.
  ///
  /// If null, the button will be rendered in a disabled state.
  /// If [isLoading] is true, this callback is effectively ignored in the UI
  /// as the button enters a loading state.
  final VoidCallback? onPressed;

  /// Whether the button is currently executing an asynchronous operation.
  ///
  /// When true, the [label] is replaced by an [AppLoader], and user interaction
  /// is disabled to prevent double-submissions.
  final bool isLoading;

  /// The explicit width of the button. If null, acts as an inline button
  /// (fitting its content plus padding), but still respecting min-width constraints.
  final double? width;

  /// Creates a Primary Button.
  const PrimaryButton({
    super.key,
    required this.label,
    this.onPressed,
    this.isLoading = false,
    this.width,
  });

  @override
  Widget build(BuildContext context) {
    // REQ-UI-002: Minimum touch target size calculation is handled by ElevatedButton's
    // default minimumSize, but we enforce it explicitly here for safety.
    final bool isDisabled = onPressed == null || isLoading;

    return SizedBox(
      width: width,
      height: 48.0, // Enforces comfortable touch target > 44px
      child: ElevatedButton(
        onPressed: isDisabled ? null : onPressed,
        style: ElevatedButton.styleFrom(
          backgroundColor: AppColors.primary,
          foregroundColor: AppColors.onPrimary,
          disabledBackgroundColor: AppColors.neutral300,
          disabledForegroundColor: AppColors.neutral500,
          padding: const EdgeInsets.symmetric(
            horizontal: AppSpacing.lg,
          ),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(AppRadius.md),
          ),
          elevation: 0,
          textStyle: AppTypography.labelLarge,
        ),
        child: isLoading
            ? const SizedBox(
                width: 24,
                height: 24,
                child: AppLoader(
                  color: AppColors.onPrimary,
                  size: 20,
                ),
              )
            : Text(
                label,
                textAlign: TextAlign.center,
                maxLines: 1,
                overflow: TextOverflow.ellipsis,
              ),
      ),
    );
  }
}