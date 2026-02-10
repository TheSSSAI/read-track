import 'package:flutter/material.dart';
import '../../foundation/app_colors.dart';
import '../../foundation/app_radius.dart';
import '../../foundation/app_spacing.dart';
import '../../foundation/app_typography.dart';

/// Defines the semantic type of the [InfoBanner].
enum InfoBannerType {
  /// Informational messages (Blue/Neutral).
  info,
  
  /// Success messages (Green).
  success,
  
  /// Warning messages (Yellow/Orange).
  warning,
  
  /// Error messages (Red).
  error,
}

/// A banner component used to display status messages, warnings, or errors.
///
/// Designed to be embedded within screen layouts (not a floating snackbar).
class InfoBanner extends StatelessWidget {
  /// The text message to display.
  final String message;

  /// The semantic type of the banner, determining styling.
  final InfoBannerType type;

  /// Optional callback for a trailing action button (e.g., "Dismiss" or "Retry").
  final VoidCallback? onAction;

  /// Label for the optional action button. Required if [onAction] is provided.
  final String? actionLabel;

  /// Optional leading icon override. If null, a default icon based on [type] is used.
  final IconData? icon;

  /// Creates an InfoBanner.
  const InfoBanner({
    super.key,
    required this.message,
    this.type = InfoBannerType.info,
    this.onAction,
    this.actionLabel,
    this.icon,
  }) : assert(onAction == null || actionLabel != null, 
             'actionLabel must be provided if onAction is not null');

  @override
  Widget build(BuildContext context) {
    final BannerStyle style = _getStyleForType(type);

    return Container(
      padding: const EdgeInsets.all(AppSpacing.md),
      decoration: BoxDecoration(
        color: style.backgroundColor,
        borderRadius: BorderRadius.circular(AppRadius.md),
        border: Border.all(
          color: style.borderColor,
          width: 1,
        ),
      ),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Icon(
            icon ?? style.defaultIcon,
            color: style.contentColor,
            size: 20,
          ),
          const SizedBox(width: AppSpacing.sm),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.min,
              children: [
                // Slight adjustment to align text vertically with the icon
                const SizedBox(height: 2), 
                Text(
                  message,
                  style: AppTypography.bodyMedium.copyWith(
                    color: style.contentColor,
                  ),
                ),
              ],
            ),
          ),
          if (onAction != null) ...[
            const SizedBox(width: AppSpacing.sm),
            GestureDetector(
              onTap: onAction,
              // Hit test expansion for accessibility
              behavior: HitTestBehavior.opaque,
              child: Padding(
                padding: const EdgeInsets.symmetric(horizontal: 4, vertical: 2),
                child: Text(
                  actionLabel!,
                  style: AppTypography.labelMedium.copyWith(
                    color: style.contentColor,
                    fontWeight: FontWeight.bold,
                    decoration: TextDecoration.underline,
                  ),
                ),
              ),
            ),
          ],
        ],
      ),
    );
  }

  BannerStyle _getStyleForType(InfoBannerType type) {
    switch (type) {
      case InfoBannerType.success:
        return BannerStyle(
          backgroundColor: AppColors.successContainer,
          contentColor: AppColors.onSuccessContainer,
          borderColor: Colors.transparent,
          defaultIcon: Icons.check_circle_outline,
        );
      case InfoBannerType.warning:
        return BannerStyle(
          backgroundColor: AppColors.warningContainer,
          contentColor: AppColors.onWarningContainer,
          borderColor: Colors.transparent,
          defaultIcon: Icons.warning_amber_rounded,
        );
      case InfoBannerType.error:
        return BannerStyle(
          backgroundColor: AppColors.errorContainer,
          contentColor: AppColors.onErrorContainer,
          borderColor: Colors.transparent,
          defaultIcon: Icons.error_outline,
        );
      case InfoBannerType.info:
      default:
        return BannerStyle(
          backgroundColor: AppColors.surfaceVariant,
          contentColor: AppColors.onSurfaceVariant,
          borderColor: AppColors.outlineVariant,
          defaultIcon: Icons.info_outline,
        );
    }
  }
}

/// Helper class to encapsulate style properties for the banner.
class BannerStyle {
  final Color backgroundColor;
  final Color contentColor;
  final Color borderColor;
  final IconData defaultIcon;

  BannerStyle({
    required this.backgroundColor,
    required this.contentColor,
    required this.borderColor,
    required this.defaultIcon,
  });
}