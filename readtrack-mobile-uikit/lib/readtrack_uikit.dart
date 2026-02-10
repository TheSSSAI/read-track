library readtrack_uikit;

// Foundation
// Core design tokens that define the visual language of the application.
// These should be used to maintain consistency across all UI components.
export 'src/foundation/app_colors.dart';
export 'src/foundation/app_radius.dart';
export 'src/foundation/app_spacing.dart';
export 'src/foundation/app_typography.dart';

// Theme
// Configuration for the overall application theme (Light/Dark mode).
// This factory uses the foundation tokens to build the Flutter ThemeData.
export 'src/theme/app_theme.dart';

// Components - Atoms
// The smallest, indivisible building blocks of the UI.
export 'src/components/atoms/app_loader.dart';
export 'src/components/atoms/custom_text_field.dart';
export 'src/components/atoms/primary_button.dart';

// Components - Molecules
// Composite components built from atoms and foundation elements.
// These represent specific domain concepts or complex UI patterns.
export 'src/components/molecules/book_card.dart';
export 'src/components/molecules/goal_progress_bar.dart';
export 'src/components/molecules/info_banner.dart';