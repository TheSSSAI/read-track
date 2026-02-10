# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-LIB-UIKIT |
| Extraction Timestamp | 2025-01-27T14:45:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-UI-001

#### 1.2.1.2 Requirement Text

The application's user interface must support both a light theme and a dark theme.

#### 1.2.1.3 Validation Criteria

- Color palettes for both themes must be defined and consistently applied.
- Application must expose ThemeData configurations for both modes.

#### 1.2.1.4 Implementation Implications

- Define semantic color tokens (e.g., 'surface', 'onSurface', 'primary') in `AppColors`.
- Implement `AppTheme` factory to generate Flutter `ThemeData` dynamically.

#### 1.2.1.5 Extraction Reasoning

This repository is the single source of truth for the application's visual language and theming logic.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-UI-002

#### 1.2.2.2 Requirement Text

The application's user interface must adhere to accessibility best practices, targeting WCAG 2.1 Level AA compliance.

#### 1.2.2.3 Validation Criteria

- Interactive elements must have a minimum size of 44x44 logical pixels.
- Contrast ratios must meet AA standards.
- Support for Dynamic Type (text scaling).

#### 1.2.2.4 Implementation Implications

- Components like `PrimaryButton` must enforce `minHeight: 44` and `minWidth: 44` in their layout constraints.
- Typography definitions in `AppTypography` must use `sp` units for font sizes.
- Semantics widgets must wrap visual-only elements.

#### 1.2.2.5 Extraction Reasoning

Accessibility compliance is enforced at the atomic component level to ensure system-wide adherence.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-FUNC-010

#### 1.2.3.2 Requirement Text

The system shall display advertisements to 'Free User' tier users via the Google AdMob SDK.

#### 1.2.3.3 Validation Criteria

- Layouts must handle ad loading states without jarring layout shifts.

#### 1.2.3.4 Implementation Implications

- Provide a `BannerAdPlaceholder` widget that reserves screen real estate with correct dimensions to prevent layout shifts (CLS) when ads load.

#### 1.2.3.5 Extraction Reasoning

While ad logic is in the app shell, the UI Kit must provide the structural containers to ensure visual stability.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

AppThemeFactory

#### 1.3.1.2 Component Specification

Static factory class generating Flutter ThemeData for Light and Dark modes based on atomic design tokens.

#### 1.3.1.3 Implementation Requirements

- Map `AppColors` to Flutter's `ColorScheme`.
- Map `AppTypography` to Flutter's `TextTheme`.
- Configure component themes (e.g., `ElevatedButtonThemeData`, `InputDecorationTheme`).

#### 1.3.1.4 Architectural Context

Presentation Layer / Design System Root

#### 1.3.1.5 Extraction Reasoning

Centralizes the theming integration point for the consuming application shell.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

AtomicWidgets

#### 1.3.2.2 Component Specification

Indivisible UI components (Buttons, Inputs, Loaders) implementing accessibility and design standards.

#### 1.3.2.3 Implementation Requirements

- Must be stateless where possible.
- Must accept `VoidCallback` for interactions.
- Must support `isLoading` and `isDisabled` states visually.

#### 1.3.2.4 Architectural Context

Presentation Layer / Atoms

#### 1.3.2.5 Extraction Reasoning

Provides the building blocks for feature screens.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

MolecularWidgets

#### 1.3.3.2 Component Specification

Composite widgets representing domain concepts (Book Cards, Progress Bars, Ad Containers).

#### 1.3.3.3 Implementation Requirements

- Combine atoms into cohesive units.
- Handle asset loading (e.g., Book Covers) using `CachedNetworkImage`.
- Expose semantic events (e.g., `onTap`) rather than raw pointer events.

#### 1.3.3.4 Architectural Context

Presentation Layer / Molecules

#### 1.3.3.5 Extraction Reasoning

Standardizes complex UI patterns used across multiple features.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Design System Foundation

#### 1.4.1.2 Layer Responsibilities

Defines primitive design tokens (Colors, Typography, Spacing, Radius, Shadows).

#### 1.4.1.3 Layer Constraints

- No dependencies on widgets.
- Pure Dart constants/classes.

#### 1.4.1.4 Implementation Patterns

- Singleton/Static Constants
- Theme Extensions

#### 1.4.1.5 Extraction Reasoning

Foundational layer required by all UI components.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Component Library

#### 1.4.2.2 Layer Responsibilities

Implements reusable Widgets based on the Foundation layer.

#### 1.4.2.3 Layer Constraints

- Must be decoupled from business logic (BLoC/Riverpod providers).
- Must rely on parameters for data injection.

#### 1.4.2.4 Implementation Patterns

- StatelessWidget
- Composition

#### 1.4.2.5 Extraction Reasoning

The core deliverable of this repository.

## 1.5.0.0 Dependency Interfaces

- {'interface_name': 'CachedNetworkImage', 'source_repository': 'cached_network_image (pub.dev)', 'method_contracts': [{'method_name': 'CachedNetworkImage', 'method_signature': 'Widget CachedNetworkImage({required String imageUrl, Widget Function? placeholder, Widget Function? errorWidget})', 'method_purpose': 'Efficiently loads and caches remote image assets (book covers).', 'integration_context': 'Used within `BookCard` and `ArticleCard` molecules.'}], 'integration_pattern': 'Flutter Package Import', 'communication_protocol': 'In-Process', 'extraction_reasoning': 'Essential for performance; prevents re-downloading images in lists.'}

## 1.6.0.0 Exposed Interfaces

- {'interface_name': 'ReadTrackUiKit', 'consumer_repositories': ['REPO-FE-APP'], 'method_contracts': [{'method_name': 'AppTheme.light', 'method_signature': 'ThemeData get light', 'method_purpose': 'Provides the Light Mode theme configuration for the MaterialApp root.', 'implementation_requirements': 'Must be assigned to `MaterialApp.theme`.'}, {'method_name': 'AppTheme.dark', 'method_signature': 'ThemeData get dark', 'method_purpose': 'Provides the Dark Mode theme configuration for the MaterialApp root.', 'implementation_requirements': 'Must be assigned to `MaterialApp.darkTheme`.'}, {'method_name': 'PrimaryButton', 'method_signature': 'const PrimaryButton({required String label, VoidCallback? onPressed, bool isLoading = false})', 'method_purpose': 'Renders the standard CTA button.', 'implementation_requirements': 'Handles loading state internally by showing a spinner.'}, {'method_name': 'BookCard', 'method_signature': 'const BookCard({required String title, required String author, String? coverUrl, VoidCallback? onTap})', 'method_purpose': 'Renders a standard book summary card.', 'implementation_requirements': 'Handles missing cover URLs with a local asset placeholder.'}, {'method_name': 'GoalProgressBar', 'method_signature': 'const GoalProgressBar({required int current, required int target})', 'method_purpose': 'Visualizes progress towards a numeric goal.', 'implementation_requirements': 'Calculates percentage and handles division by zero.'}], 'service_level_requirements': ['Zero-layout shift on first render.', '60fps rendering performance for all components.'], 'implementation_constraints': ['All widgets must extend `StatelessWidget` or `StatefulWidget`.', 'No direct dependency on Riverpod or BLoC.'], 'extraction_reasoning': 'These are the public contracts consumed by the App Shell to build screens.'}

## 1.7.0.0 Technology Context

### 1.7.1.0 Framework Requirements

Flutter 3.22+, Dart 3.4+

### 1.7.2.0 Integration Technologies

- Flutter Package System
- Dart Exports

### 1.7.3.0 Performance Constraints

Components must use `const` constructors where possible to enable widget canonicalization and reduce rebuild costs.

### 1.7.4.0 Security Requirements

External image loading must support HTTPS.

## 1.8.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All core UI components referenced in User Stories ... |
| Cross Reference Validation | Confirmed `BookCard` aligns with `BookDto` data av... |
| Implementation Readiness Assessment | High. Component specifications are detailed and de... |
| Quality Assurance Confirmation | Accessibility and Theming requirements are strictl... |

