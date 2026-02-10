# 1 Id

REPO-BE-MOD-READING

# 2 Name

readtrack-reading-module

# 3 Description

This repository represents the core domain of the application: personal reading tracking and library management. Decomposed from the monolithic `readtrack-backend-api`, this module is singularly focused on all operations related to a user's library, including adding books/articles (REQ-TRK-001), managing virtual shelves ('Currently Reading', 'Read', etc.), and logging reading sessions. It owns the `LibraryItem` and `ReadingSession` data models and all the business logic governing them. It integrates with the Google Books API for metadata fetching. Isolating this functionality ensures the central value proposition of the app is robust, performant, and can be enhanced without being entangled with monetization or user management concerns.

# 4 Type

🔹 Business Logic

# 5 Namespace

ReadTrack.Reading

# 6 Output Path

solution/backend/modules/reading

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

ASP.NET Core 8, Entity Framework Core 8

# 10 Thirdparty Libraries

- Google.Apis.Books.v1

# 11 Layer Ids

- application
- domain
- infrastructure

# 12 Dependencies

- REPO-BE-LIB-CONTRACTS
- REPO-BE-LIB-INFRA
- REPO-BE-MOD-MONETIZATION

# 13 Requirements

- {'requirementId': 'REQ-TRK-001'}

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Clean Architecture Slice

# 17 Architecture Map

- application-layer-010

# 18 Components Map

*No items available*

# 19 Requirements Map

- REQ-TRK-001

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-BE-API

## 20.3 Decomposition Reasoning

The core reading experience is the heart of the application. Separating it into its own module clarifies the primary business domain and protects this critical functionality from unrelated changes. It allows developers to focus on improving the reading tracking features with a clear, bounded context.

## 20.4 Extracted Responsibilities

- Personal Library Management (Books & Articles)
- Virtual Shelf Logic
- Reading Session Logging and History
- Integration with Google Books API for metadata

## 20.5 Reusability Scope

- Domain models and logic could be a foundation for a more advanced reading platform in the future.

## 20.6 Development Benefits

- Clear ownership of the application's core feature set.
- Allows for optimization of database queries related to reading data without impacting other modules.
- Simplifies the development of new reading-related features.

# 21.0 Dependency Contracts

## 21.1 Repo-Be-Mod-Monetization

### 21.1.1 Required Interfaces

- {'interface': 'ISubscriptionService', 'methods': ['GetUserSubscriptionStatusAsync(Guid userId) : SubscriptionStatusDto'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Direct Service Call (via DI)

### 21.1.3 Communication Protocol

In-process

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'ILibraryService', 'methods': ['GetUserLibraryAsync(Guid userId, ShelfFilter filter) : List<LibraryItemDto>', 'AddBookToLibraryAsync(Guid userId, AddBookRequest request) : Result<LibraryItemDto>'], 'events': ['ReadingSessionLogged(Guid userId, Guid sessionId, int pagesRead, int minutesRead)'], 'properties': [], 'consumers': ['REPO-BE-MOD-ENGAGEMENT', 'REPO-BE-MOD-RECOMMENDATIONS']}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Consumes `ISubscriptionService` to enforce limits ... |
| Event Communication | Publishes the critical `ReadingSessionLogged` even... |
| Data Flow | Owns and manages the `LibraryItem` and `ReadingSes... |
| Error Handling | Handles errors from the external Google Books API ... |
| Async Patterns | All database and external API calls are asynchrono... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Organize logic around the core aggregates: Library... |
| Performance Considerations | Database queries for a user's library must be high... |
| Security Considerations | Authorization rules must ensure users can only acc... |
| Testing Approach | Unit test the business logic for moving books betw... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- CRUD operations for books and articles in a user's library.
- Logging, editing, and deleting reading sessions.

## 25.2.0 Must Not Implement

- Goal tracking logic.
- AI recommendation generation.
- Subscription status checks (it consumes this information, but does not manage it).

## 25.3.0 Extension Points

- Adding support for new item types (e.g., comics, papers).
- Integrating with other book metadata providers.

## 25.4.0 Validation Rules

- Validate input for logging a reading session (e.g., page numbers are sequential).
- Enforce library size limits for Free Users before adding a new item.

