# ReadTrack UI Kit

A centralized, enterprise-grade Design System library for the ReadTrack mobile application. This package provides the foundational design tokens (Colors, Typography, Spacing) and reusable atomic/molecular components to ensure consistency, accessibility, and maintainability across the application.

## 🏗 Architecture

This library follows **Atomic Design** principles:

- **Foundation**: Raw design tokens (Colors, Typography, Spacing, Radius).
- **Atoms**: Indivisible UI components (Buttons, Inputs, Loaders).
- **Molecules**: Composite components (Book Cards, Progress Bars).
- **Theme**: Flutter `ThemeData` configuration.

## 🚀 Installation

Add this package to your `pubspec.yaml`:

```yaml
dependencies:
  readtrack_uikit:
    path: ../readtrack_uikit # Or git dependency
```

## 🎨 Usage

### Foundation

```dart
import 'package:readtrack_uikit/readtrack_uikit.dart';

// Use AppColors
Color primary = AppColors.primary;

// Use Spacing
Padding(padding: EdgeInsets.all(AppSpacing.md));
```

### Components

```dart
PrimaryButton(
  label: 'Get Started',
  onPressed: () {},
)
```

## 🧪 Testing

This package enforces strict quality standards. Run tests using:

```bash
flutter test
```

## ♿ Accessibility

All components are built to meet WCAG 2.1 Level AA standards:
- Minimum touch targets (44x44).
- High contrast color schemes.
- Dynamic Type support.
- Semantic labels.