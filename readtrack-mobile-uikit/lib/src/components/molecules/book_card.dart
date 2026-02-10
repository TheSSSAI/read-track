import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import '../../foundation/app_colors.dart';
import '../../foundation/app_radius.dart';
import '../../foundation/app_spacing.dart';
import '../../foundation/app_typography.dart';

/// A card component displaying a book's cover, title, and author.
///
/// This molecule is designed for grid or list layouts and handles
/// image caching and accessibility semantics automatically.
class BookCard extends StatelessWidget {
  /// The title of the book.
  final String title;

  /// The author of the book.
  final String author;

  /// The URL of the book cover image.
  ///
  /// If null or if the image fails to load, a placeholder will be displayed.
  final String? coverUrl;

  /// Callback triggered when the card is tapped.
  final VoidCallback? onTap;

  /// The width of the card. Recommended to be constrained by the parent,
  /// but can be explicitly set if needed.
  final double? width;

  /// Creates a BookCard.
  const BookCard({
    super.key,
    required this.title,
    required this.author,
    this.coverUrl,
    this.onTap,
    this.width,
  });

  @override
  Widget build(BuildContext context) {
    // REQ-UI-002: Wrap in Semantics to ensure screen readers announce the full context
    // as a single interactive element rather than fragmented text nodes.
    return Semantics(
      label: 'Book: $title by $author',
      button: true,
      enabled: onTap != null,
      child: SizedBox(
        width: width,
        child: Card(
          margin: EdgeInsets.zero,
          elevation: 0,
          color: Colors.transparent,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(AppRadius.md),
          ),
          clipBehavior: Clip.antiAlias,
          child: InkWell(
            onTap: onTap,
            borderRadius: BorderRadius.circular(AppRadius.md),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.min,
              children: [
                _buildCoverImage(),
                const SizedBox(height: AppSpacing.xs),
                _buildDetails(),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildCoverImage() {
    return AspectRatio(
      aspectRatio: 0.66, // Standard book aspect ratio (2:3)
      child: Container(
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(AppRadius.sm),
          color: AppColors.surfaceVariant,
          border: Border.all(
            color: AppColors.outlineVariant,
            width: 1,
          ),
        ),
        clipBehavior: Clip.antiAlias,
        child: coverUrl != null && coverUrl!.isNotEmpty
            ? CachedNetworkImage(
                imageUrl: coverUrl!,
                fit: BoxFit.cover,
                placeholder: (context, url) => const Center(
                  child: Icon(
                    Icons.image,
                    color: AppColors.neutral400,
                  ),
                ),
                errorWidget: (context, url, error) => const Center(
                  child: Icon(
                    Icons.broken_image,
                    color: AppColors.neutral400,
                  ),
                ),
              )
            : const Center(
                child: Icon(
                  Icons.book,
                  color: AppColors.neutral400,
                  size: 32,
                ),
              ),
      ),
    );
  }

  Widget _buildDetails() {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          style: AppTypography.titleMedium.copyWith(
            fontWeight: FontWeight.w600,
            color: AppColors.onSurface,
          ),
          maxLines: 2,
          overflow: TextOverflow.ellipsis,
        ),
        const SizedBox(height: AppSpacing.xxs),
        Text(
          author,
          style: AppTypography.bodySmall.copyWith(
            color: AppColors.onSurfaceVariant,
          ),
          maxLines: 1,
          overflow: TextOverflow.ellipsis,
        ),
      ],
    );
  }
}