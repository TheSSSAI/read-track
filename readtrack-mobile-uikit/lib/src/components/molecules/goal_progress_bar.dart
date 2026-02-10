import 'package:flutter/material.dart';
import '../../foundation/app_colors.dart';
import '../../foundation/app_radius.dart';
import '../../foundation/app_spacing.dart';
import '../../foundation/app_typography.dart';

/// A component that visualizes progress towards a specific numeric goal.
///
/// Displays a label (current/target) and a linear progress indicator.
/// Handles edge cases like zero targets to avoid division errors.
class GoalProgressBar extends StatelessWidget {
  /// The current progress value (e.g., pages read).
  final int current;

  /// The target value (e.g., total pages).
  final int target;

  /// Optional label describing the unit (e.g., "pages").
  final String? unitLabel;

  /// Whether to show the numeric label above the bar. Defaults to true.
  final bool showLabel;

  /// Creates a GoalProgressBar.
  const GoalProgressBar({
    super.key,
    required this.current,
    required this.target,
    this.unitLabel,
    this.showLabel = true,
  });

  @override
  Widget build(BuildContext context) {
    // Business logic: Ensure we don't divide by zero and clamp percentage between 0 and 1.
    // If target is 0, we consider progress 0 unless current > 0, but logically 0/0 is 0 progress.
    final double percentage = target > 0 ? (current / target).clamp(0.0, 1.0) : 0.0;
    
    // Accessibility: Calculate percentage string for screen readers
    final int percentageInt = (percentage * 100).toInt();
    final String semanticsLabel = 'Progress: $percentageInt percent, $current of $target ${unitLabel ?? ""}';

    return Semantics(
      label: semanticsLabel,
      value: '$percentageInt%',
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisSize: MainAxisSize.min,
        children: [
          if (showLabel) ...[
            _buildLabelRow(),
            const SizedBox(height: AppSpacing.xs),
          ],
          _buildProgressBar(percentage),
        ],
      ),
    );
  }

  Widget _buildLabelRow() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Text(
          '$current / $target',
          style: AppTypography.labelMedium.copyWith(
            color: AppColors.onSurface,
            fontWeight: FontWeight.w600,
          ),
        ),
        if (unitLabel != null)
          Text(
            unitLabel!,
            style: AppTypography.labelSmall.copyWith(
              color: AppColors.onSurfaceVariant,
            ),
          ),
      ],
    );
  }

  Widget _buildProgressBar(double percentage) {
    return ClipRRect(
      borderRadius: BorderRadius.circular(AppRadius.full),
      child: LinearProgressIndicator(
        value: percentage,
        minHeight: 8.0,
        backgroundColor: AppColors.surfaceVariant,
        // Use primary color for the bar, could be themed/customized if needed
        color: AppColors.primary,
        semanticsLabel: 'Progress bar',
      ),
    );
  }
}