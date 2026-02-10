# 1 Id

REPO-FE-LIB-UIKIT

# 2 Name

readtrack-mobile-uikit

# 3 Description

A reusable Flutter package containing the application's design system and shared UI components. It was extracted from the original `readtrack-mobile-app` to create a centralized, independent library for all visual elements. This includes atomic components like themed buttons, text fields, and cards, as well as more complex, composite widgets. It also defines the application's theme, color palette, and typography (REQ-UIF-001). By managing the UI Kit in its own repository, we ensure visual consistency across the entire app, simplify the development of new features, and enable the design system to be versioned and potentially reused in future projects.

# 4 Type

🔹 Cross-Cutting Library

# 5 Namespace

readtrack_uikit

# 6 Output Path

solution/mobile/libs/uikit

# 7 Framework

Flutter 3.22+

# 8 Language

Dart

# 9 Technology

Flutter

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- shared-ui

# 12 Dependencies

*No items available*

# 13 Requirements

- {'requirementId': 'REQ-UIF-001'}

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Design System / Component Library

# 17 Architecture Map

*No items available*

# 18 Components Map

*No items available*

# 19 Requirements Map

- REQ-UIF-001

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-FE-MOB

## 20.3 Decomposition Reasoning

A dedicated UI Kit repository is a best practice for maintaining a consistent and scalable frontend. It decouples the visual implementation of components from the application's business logic, allowing designers and developers to collaborate on a shared, versioned set of building blocks. This accelerates development and makes rebranding or theme changes much easier to implement.

## 20.4 Extracted Responsibilities

- Core App Theme (Colors, Typography, Spacing)
- Atomic UI Components (Buttons, Inputs, Icons)
- Composite Widgets (BookCard, GoalProgressBar)
- Shared UI Utility Functions

## 20.5 Reusability Scope

- This library can be used by the main mobile app. It could also be used for any future Flutter applications developed by the company.

## 20.6 Development Benefits

- Enforces UI consistency.
- Speeds up feature development by providing ready-made components.
- Allows UI components to be developed and tested in isolation (e.g., using Storybook).

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

### 22.1.1 Interface

#### 22.1.1.1 Interface

AppTheme

#### 22.1.1.2 Methods

*No items available*

#### 22.1.1.3 Events

*No items available*

#### 22.1.1.4 Properties

- ThemeData lightTheme
- ThemeData darkTheme

#### 22.1.1.5 Consumers

- REPO-FE-APP

### 22.1.2.0 Interface

#### 22.1.2.1 Interface

PrimaryButton (Widget)

#### 22.1.2.2 Methods

*No items available*

#### 22.1.2.3 Events

*No items available*

#### 22.1.2.4 Properties

*No items available*

#### 22.1.2.5 Consumers

- REPO-FE-APP

# 23.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Not applicable. |
| Event Communication | Components may expose callbacks (e.g., `onPressed`... |
| Data Flow | Widgets are designed to be stateless where possibl... |
| Error Handling | Components should not handle business errors, but ... |
| Async Patterns | Not applicable. |

# 24.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Build widgets to be as generic and configurable as... |
| Performance Considerations | Ensure widgets are performant and do not cause unn... |
| Security Considerations | Not applicable. |
| Testing Approach | Each widget should have its own set of widget test... |

# 25.0.0.0 Scope Boundaries

## 25.1.0.0 Must Implement

- The application's visual design system.
- Generic, reusable UI components.

## 25.2.0.0 Must Not Implement

- Any business logic or state management.
- API calls or data fetching.
- Navigation logic.

## 25.3.0.0 Extension Points

- Adding new components to the design system.

## 25.4.0.0 Validation Rules

*No items available*

