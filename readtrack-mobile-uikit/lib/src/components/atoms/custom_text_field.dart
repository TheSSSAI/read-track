import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import '../../foundation/app_colors.dart';
import '../../foundation/app_radius.dart';
import '../../foundation/app_spacing.dart';
import '../../foundation/app_typography.dart';

/// A reusable, standardized text input field component.
///
/// This component encapsulates the design system's styling for text inputs,
/// providing consistent borders, padding, typography, and error states.
/// It wraps the Flutter [TextField] widget.
class CustomTextField extends StatelessWidget {
  /// The controller for the text field.
  final TextEditingController? controller;

  /// The label text displayed above or inside the field.
  final String? label;

  /// The hint text displayed inside the field when empty.
  final String? hintText;

  /// The error text displayed below the field.
  final String? errorText;

  /// Whether the text should be obscured (e.g., for passwords).
  final bool obscureText;

  /// The type of keyboard to display.
  final TextInputType? keyboardType;

  /// The action button on the keyboard (e.g., Done, Next).
  final TextInputAction? textInputAction;

  /// Callback when the text changes.
  final ValueChanged<String>? onChanged;

  /// Callback when the user submits the field.
  final ValueChanged<String>? onSubmitted;

  /// A widget to display before the input area.
  final Widget? prefixIcon;

  /// A widget to display after the input area (e.g., password toggle).
  final Widget? suffixIcon;

  /// Whether the field is enabled.
  final bool enabled;

  /// The maximum number of lines.
  ///
  /// Defaults to 1. If null, the field grows vertically.
  final int? maxLines;

  /// Input formatters to validate or format the text as it is typed.
  final List<TextInputFormatter>? inputFormatters;

  /// Creates a [CustomTextField] with standardized styling.
  const CustomTextField({
    super.key,
    this.controller,
    this.label,
    this.hintText,
    this.errorText,
    this.obscureText = false,
    this.keyboardType,
    this.textInputAction,
    this.onChanged,
    this.onSubmitted,
    this.prefixIcon,
    this.suffixIcon,
    this.enabled = true,
    this.maxLines = 1,
    this.inputFormatters,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final colorScheme = theme.colorScheme;

    // We define local styles to ensure atomic consistency regardless of parent theme context,
    // but we default to using the theme's colors where appropriate.
    
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      mainAxisSize: MainAxisSize.min,
      children: [
        if (label != null) ...[
          Text(
            label!,
            style: AppTypography.labelMedium.copyWith(
              color: enabled ? colorScheme.onSurface : colorScheme.onSurface.withOpacity(0.38),
              fontWeight: FontWeight.w600,
            ),
          ),
          const SizedBox(height: AppSpacing.xs),
        ],
        TextField(
          controller: controller,
          enabled: enabled,
          obscureText: obscureText,
          keyboardType: keyboardType,
          textInputAction: textInputAction,
          onChanged: onChanged,
          onSubmitted: onSubmitted,
          maxLines: maxLines,
          inputFormatters: inputFormatters,
          style: AppTypography.bodyLarge.copyWith(
            color: enabled ? colorScheme.onSurface : colorScheme.onSurface.withOpacity(0.38),
          ),
          cursorColor: AppColors.primary,
          decoration: InputDecoration(
            hintText: hintText,
            errorText: errorText,
            prefixIcon: prefixIcon,
            suffixIcon: suffixIcon,
            isDense: true,
            // Explicitly defining borders here guarantees the atomic design
            // even if the global inputDecorationTheme is overridden elsewhere.
            border: OutlineInputBorder(
              borderRadius: BorderRadius.circular(AppRadius.md),
              borderSide: BorderSide(
                color: AppColors.outline,
                width: 1.0,
              ),
            ),
            enabledBorder: OutlineInputBorder(
              borderRadius: BorderRadius.circular(AppRadius.md),
              borderSide: BorderSide(
                color: AppColors.outline,
                width: 1.0,
              ),
            ),
            focusedBorder: OutlineInputBorder(
              borderRadius: BorderRadius.circular(AppRadius.md),
              borderSide: BorderSide(
                color: AppColors.primary,
                width: 2.0,
              ),
            ),
            errorBorder: OutlineInputBorder(
              borderRadius: BorderRadius.circular(AppRadius.md),
              borderSide: BorderSide(
                color: AppColors.error,
                width: 1.0,
              ),
            ),
            focusedErrorBorder: OutlineInputBorder(
              borderRadius: BorderRadius.circular(AppRadius.md),
              borderSide: BorderSide(
                color: AppColors.error,
                width: 2.0,
              ),
            ),
            disabledBorder: OutlineInputBorder(
              borderRadius: BorderRadius.circular(AppRadius.md),
              borderSide: BorderSide(
                color: colorScheme.onSurface.withOpacity(0.12),
                width: 1.0,
              ),
            ),
            contentPadding: const EdgeInsets.symmetric(
              horizontal: AppSpacing.md,
              vertical: AppSpacing.md,
            ),
            filled: true,
            fillColor: enabled 
                ? colorScheme.surfaceContainer 
                : colorScheme.onSurface.withOpacity(0.04),
          ),
        ),
      ],
    );
  }
}