**Software Requirements Specification**

**1. Introduction**

**1.1 Project Scope (REQ-SCP-001)**
*   The project shall include the design, development, and deployment of a cross-platform mobile application for Android and iOS.
*   The application shall provide user authentication via social providers.
*   The application shall provide a freemium subscription model.
*   The application shall provide personal reading tracking and library management.
*   The application shall provide flexible goal setting and monitoring.
*   The application shall provide a personalized dashboard.
*   The application shall provide daily reading tasks.
*   The application shall provide a curated library of reading tips.
*   The application shall provide interactive vocabulary-building games.
*   The application shall provide AI-powered book and content recommendations.
*   The project shall not include a web-based or desktop version of the application.
*   The project shall not include social features such as friend lists, book clubs, or activity feeds.
*   The project shall not include direct in-app purchasing of books.
*   The project shall not include support for audiobooks or e-reader device integration.
*   The project shall not include user-to-user messaging or forums.

**2. Overall Description**

**2.1 User Classes and Permissions (REQ-BUS-001)**
*   The system shall define a 'Guest (Unauthenticated)' user class.
    *   Guest users shall only be able to view the login screen.
    *   Guest users shall be prevented from accessing any other application features.
*   The system shall define a 'Free User' class with access to core functionality with specified limitations.
    *   Free Users shall be limited to tracking a maximum of 20 items (books/articles).
    *   Free Users shall be limited to one active goal.
    *   Free Users' access to vocabulary games shall be limited to a pre-populated word list.
    *   Free Users shall be limited to 5 AI suggestions per month.
    *   The system shall display advertisements to Free Users.
*   The system shall define a 'Premium User' class with unlimited access to all application features.
    *   Premium Users shall have unlimited tracking of books and articles.
    *   Premium Users shall be able to set unlimited, concurrent active goals.
    *   Premium Users shall have access to vocabulary games using an unlimited personal word list.
    *   Premium Users shall receive unlimited AI suggestions.
    *   The system shall not display advertisements to Premium Users.
    *   Premium Users shall have access to an advanced statistics and insights feature.
*   Upon expiration, cancellation, or refund of a Premium subscription, the user's account shall revert to the 'Free User' class. The system shall re-apply all Free User limitations, but shall not delete any existing data that exceeds the new limits (e.g., more than 20 books). The user will be prevented from adding new items until they are back within the free tier limits or upgrade again.
    *   The user interface shall clearly indicate when a limit has been reached and provide a non-intrusive prompt to either manage their existing items or upgrade their subscription.

**2.2 Operating Environment (REQ-OPE-001)**
*   The application shall support iOS version 14.0 and higher.
*   The application shall support Android version 7.0 (Nougat) and higher.
*   The application shall be performant on standard smartphones released within the last 4 years, with a minimum of 2GB of RAM.
*   The application shall require an internet connection for initial login, data synchronization, searching for new content, and receiving AI suggestions.
*   The application shall provide offline functionality for core features as defined in REQ-OFF-001.

**2.3 Design and Implementation Constraints (REQ-CON-001)**
*   The mobile application shall be developed using the Flutter framework with the Riverpod state management library (TC-001).
*   The entire backend infrastructure shall be deployed on Amazon Web Services (AWS) in the `eu-central-1` (Frankfurt) region to ensure GDPR compliance for data residency (TC-002).
*   User authentication shall be handled exclusively through third-party social providers (Google, Apple) via Auth0 (TC-003).
*   The system shall not implement a native email/password authentication system (TC-003).
*   All cloud infrastructure shall be defined and managed using the AWS Cloud Development Kit (CDK) with TypeScript (TC-004).
*   The application shall comply with GDPR and CCPA regulations, specifically regarding the user's right to data access, erasure, and portability (RC-001).
*   A formal data classification scheme shall be defined and applied to all user data, distinguishing between Personally Identifiable Information (PII), User-Generated Content, and system metadata to ensure appropriate handling and security controls are applied.

**2.4 Transition Requirements (REQ-TRN-001)**
*   **2.4.1 Implementation Approach**
    *   The initial release of the application shall follow a "Big Bang" deployment model, where the full application is made available to all new users simultaneously on the Apple App Store and Google Play Store.
*   **2.4.2 Initial Data Seeding**
    *   Prior to launch, the production environment's Headless CMS (Contentful) shall be populated with the initial set of curated reading tips and the pre-populated word lists for Free User vocabulary games.
    *   No user data migration is required as this is a new system launch.
*   **2.4.3 User Training and Documentation**
    *   User training shall be delivered through the mandatory in-app onboarding flow as specified in REQ-ONB-001.
    *   A public-facing Help Center or FAQ page, with content managed in the Headless CMS, shall be available to all users, providing documentation on all application features.
*   **2.4.4 Go-Live and Cutover Plan**
    *   A detailed go-live checklist shall be executed, including:
        1.  Final deployment of the production-ready backend infrastructure via AWS CDK.
        2.  Execution of a full suite of automated end-to-end tests against the production environment.
        3.  Submission of the mobile application builds to the Apple App Store and Google Play Store for review and approval.
        4.  Coordinated release of the application on both stores upon approval.
        5.  Intensive post-launch monitoring of system health, performance metrics, and error rates for the first 72 hours.
    *   Go-live success shall be defined by: zero P1/P2 incidents within the first 24 hours, API P95 latency remaining below 200ms, and successful user sign-ups and subscription transactions.
*   **2.4.5 Fallback and Contingency Plan**
    *   In the event of a critical failure during the backend deployment, the CI/CD pipeline shall support an automated rollback to the previous stable version.
    *   If a critical bug is discovered post-launch, a hotfix release shall be prioritized and submitted to app stores for expedited review. The public status page shall be updated to inform users of the issue.

**3. System Features**

**3.1 Business Model: Freemium Tiers (REQ-FRE-001)**
*   The system shall enforce feature access controls based on the user's subscription tier (Free or Premium).
*   Given a Free User attempts to add the 21st book to their library, the system shall prevent the action and display a prompt to upgrade.
*   Given a Free User has one active goal, the option to create another goal shall be disabled or lead to an upgrade prompt.
*   Given a Free User has received 5 AI suggestions in the current month, any action to request another suggestion shall be blocked and the system shall present an upgrade prompt.
*   For a Premium User, all feature limits shall be removed.
*   The system shall allow users to view their subscription status and upgrade their plan.
*   The app shall contain a clearly accessible screen showing the user's current tier (Free/Premium), the benefits of the Premium Tier, and a button to initiate the upgrade process.
*   Tapping the upgrade button shall invoke the native in-app purchase flow for the Apple App Store or Google Play Store.
*   The backend shall listen for server-to-server webhooks from Apple/Google to reliably update the user's subscription status, including successful purchases, cancellations, and renewals.
*   The system shall handle subscription lifecycle events. If a user cancels their subscription, they shall retain Premium access until the end of the current billing period.

**3.2 Advertisements for Free Users (REQ-ADS-001)**
*   Advertisements shall be served to Free Users via the Google AdMob SDK.
*   The system shall display a banner ad unit on secondary screens, such as the statistics page, for Free Users.
*   The system shall display one full-screen interstitial ad after every third reading session is logged by a Free User.
*   Frequency and placement rules shall be configured to minimize disruption to the core user experience.
*   The system shall respect user consent choices regarding personalized advertising as required by platform policies and privacy regulations (e.g., App Tracking Transparency on iOS).

**3.3 User Onboarding (REQ-ONB-001)**
*   The system shall present a guided, multi-step onboarding flow to users upon their first login.
*   The onboarding flow shall prompt the user to set their first yearly reading goal, with an option to skip this step.
*   The onboarding flow shall guide the user to search for and add their 'Currently Reading' book to their library, with an option to skip this step.
*   The onboarding flow shall include a brief, dismissible tour highlighting the Dashboard, the button to log a reading session, and the Goals tab.
*   Upon completion of the onboarding flow, the user shall be directed to the main Dashboard.

**3.4 User Management (REQ-USR-001)**
*   User registration and login shall be handled exclusively via 'Sign in with Google' and 'Sign in with Apple' options.
*   Selecting a sign-in option shall initiate the platform-native OAuth 2.0/OIDC flow.
*   Upon successful authentication, the system shall create a new user account for a first-time user or log in an existing user.
*   The backend shall securely validate the token from the provider and issue its own JWT for session management. The JWT access token shall have a short expiry (e.g., 15 minutes), and a long-lived refresh token shall be used to obtain new access tokens without requiring the user to log in again.
*   The system shall handle cases where a user revokes the application's permissions from their social provider's account settings, invalidating their session and requiring re-authentication.
*   Users shall be able to change their display name and profile picture.
*   A settings screen shall provide separate toggles to enable/disable 'Goal Reminders' and 'Daily Task Reminders' push notifications, managed via Amazon Simple Notification Service (SNS).
*   Users shall have a clear and accessible option to permanently delete their account and all associated data within the app's account settings.
    *   Initiating deletion shall present a confirmation dialog explaining that the action is irreversible.
    *   Upon confirmation, the backend shall trigger a process to perform a hard delete of all PII and user-generated content from primary data stores. This process must also include a step to find and anonymize the user's PII (e.g., user ID, IP address) within any application logs that are subject to the 90-day retention policy.
    *   The account deletion event itself shall be logged for audit purposes, containing a non-identifiable, unique user ID and a timestamp, but no PII.
*   Users shall be able to request and download an export of their personal reading data.
    *   An option to 'Export My Data' shall be available in the account settings.
    *   Triggering the export shall queue an asynchronous job on the backend via Amazon SQS.
    *   Upon completion, the user shall be notified via Amazon SES with a secure, time-limited link (valid for 24 hours) to download their data from Amazon S3.
    *   The exported file shall be in a machine-readable format (JSON) and contain their library list and reading session history.

**3.5 Reading Tracking (REQ-TRK-001)**
*   Users shall be able to manage a personal library of books and articles organized into distinct virtual shelves: 'Currently Reading,' 'Want to Read,' 'Read,' and 'Did Not Finish (DNF).'\n*   Users shall be able to move an item between shelves. Moving an item to 'Read' or 'DNF' shall prompt for a completion date.
*   To add a book, the user shall be able to search by Title, Author, or ISBN. The search shall query the Google Books API.
*   To add an article, the user shall be able to paste a URL and provide a custom title.
*   Users shall be able to record reading sessions for items in their 'Currently Reading' shelf.
*   Users shall be able to log progress by entering the page number they have read up to. For items without a defined page count, users shall be able to log progress by percentage.
*   Users shall be able to use a start/stop timer or manually enter the time spent reading for a session.
*   The system shall calculate and store the pages read and minutes spent for the session.
*   Users shall be able to view, edit, and delete past reading sessions.
*   For any book, the user shall be able to view a history of all logged reading sessions.
*   A search bar shall allow users to find items in their library by title or author.
*   Filter options shall be available to view items by their shelf status.

**3.5.1 Data Model**
*   The system shall implement a data model with the following core entities and relationships:
    *   **User**: Represents a registered user. Contains user preferences, subscription status, and a unique identifier.
    *   **LibraryItem**: Represents a book or article. Attributes include type (book/article), title, author(s), ISBN (for books), URL (for articles), page count, cover image URL, and current shelf status. Each LibraryItem belongs to one User.
    *   **ReadingSession**: Represents a single reading event. Attributes include start time, duration in minutes, pages read or percentage completed, and a timestamp. Each ReadingSession belongs to one LibraryItem.
    *   **Goal**: Represents a user-defined goal. Attributes include goal type (books, pages, time), target value, and time period (day, week, month, year). Each Goal belongs to one User.
    *   **VocabularyWord**: Represents a word saved by a Premium User. Attributes include the word, its definition, and an example sentence. Each VocabularyWord belongs to one User.

**3.6 Statistics and Insights**
*   **3.6.1 Basic Statistics (REQ-STA-000)**
    *   The app shall display statistics for all users, such as total pages read, total time spent, and total books completed.
    *   The app shall calculate and display the user's average reading speed (pages per hour).
*   **3.6.2 Advanced Statistics (Premium Users) (REQ-STA-001)**
    *   The system shall provide a dedicated \"Insights\" section for Premium Users.
    *   This section shall display the user's current and longest reading streaks (consecutive days with a logged session).
    *   It shall include charts showing reading pace trends over time (e.g., pages per week). All charts must be clearly labeled and use color-blind accessible palettes.
    *   It shall provide a breakdown of reading activity by time of day and day of the week.
    *   It shall list the user's most-read authors and genres (based on completed books).
    *   It shall provide a comparison of current yearly goal progress against the progress of previous years, where data is available.

**3.7 Goal Management (REQ-GOL-001)**
*   Users shall be able to set various types of reading goals.
*   When creating a goal, users shall be able to choose from types: Number of books per year, Number of pages per day/week/month, and Amount of time (minutes) per day/week/month.
*   Users shall be able to set a target value for their chosen goal type.
*   Users shall be able to edit the target value or delete an active goal.
*   The UI shall display progress bars, percentage completion, or other visual cues against all active goals.
*   Progress shall update automatically after each reading session is logged.
*   At the end of a goal's period, the app shall present a 'Year in Review' summary of achievements.
*   Following the summary, the app shall prompt the user to set a new goal for the upcoming period.

**3.8 Dashboard (REQ-DSH-001)**
*   The dashboard shall be the primary landing screen after login.
*   The dashboard shall display the book(s) on the 'Currently Reading' shelf.
*   The dashboard shall show a summary of progress toward all active goals.
*   The dashboard shall provide a prominent shortcut to log a new reading session.
*   The dashboard shall intelligently display a reminder for any incomplete daily tasks for the current day.

**3.9 Daily Tasks (REQ-TSK-001)**
*   Users shall be able to create custom daily tasks (e.g., 'Read one chapter').
*   Users shall be able to configure tasks to be recurring on specific days of the week.
*   The app shall suggest daily tasks based on the user's active goals.
*   Users shall be able to mark tasks as complete.

**3.10 Tips (REQ-TIP-001)**
*   The system shall provide a library of articles offering reading strategies and study techniques.
*   The app shall have a dedicated section for 'Tips'.
*   Content for the tips shall be fetched via an API call to a Headless CMS (Contentful).
*   The app shall cache the content to reduce load times and support offline viewing of previously loaded tips.
*   The 'Tips' section shall include categorization and a search function to help users find relevant content.

**3.11 Vocabulary Games (REQ-VOC-001)**
*   The app shall include at least two game types: Flashcards and a Multiple-Choice Quiz.
*   Games for Free Users shall be populated from a pre-populated word list.
*   Premium Users shall be able to manually add words to a personal vocabulary list by entering a word, its definition, and an example sentence.
*   Games for Premium Users shall be populated from their personal vocabulary list.
*   Pre-populated word lists for Free Tier users shall be managed in and fetched from the Headless CMS (Contentful).
*   The system shall track and display user performance history for the games to show progress over time.

**3.12 AI Suggestions (REQ-AIS-001)**
*   The app shall provide intelligent book recommendations by sending user data as prompts to a third-party LLM API (OpenAI GPT-4).
*   The backend shall have a service that constructs a detailed prompt based on user data. This service will periodically generate vector embeddings from the user's reading history, stated goals, and vocabulary. These embeddings will be stored in Amazon OpenSearch Serverless.
*   To generate a recommendation, the service shall perform a k-NN similarity search against the vector store to retrieve the most relevant context, which will then be included in the prompt sent to the LLM.
*   The service shall securely call the external LLM API with the prompt. Calls to the LLM API shall be rate-limited to control costs.
*   The service shall parse the LLM's response and format it as a user-facing recommendation.
*   The recommendation process shall be asynchronous to avoid blocking the user interface.
*   Users shall be able to provide feedback on suggestions (e.g., thumbs up/down, report as irrelevant) to improve future recommendations.
*   Each recommendation shall have interactive elements allowing the user to dismiss it or save it to their 'Want to Read' shelf.
*   This feedback shall be stored and included in future prompts to the LLM to refine its output.

**3.13 Offline Support (REQ-OFF-001)**
*   The application shall use the Isar database, a fast, cross-platform, and object-oriented local database, to store user data for offline access.
*   Users shall be able to perform the following actions while offline:
    *   Log new reading sessions for books already in their library.
    *   View their entire library and reading history.
    *   Move items between shelves.
    *   View cached 'Tips' content.
*   The application shall implement a background synchronization mechanism.
*   When network connectivity is restored, the app shall automatically push any local changes to the backend and fetch the latest data from the server.
*   The synchronization mechanism shall use a 'last write wins' conflict resolution strategy, where the most recent change (based on a client-side timestamp) overwrites older data.

**4. External Interface Requirements**

**4.1 User Interfaces (REQ-UIF-001)**
*   The UI shall be modern, clean, and visually engaging.
*   The UI shall use a consistent color palette and typography suitable for the target demographic (Students age group 17-28).
*   The UI shall adapt gracefully to various screen sizes and densities across supported iOS and Android devices.
*   The application shall support both light and dark themes, respecting the user's OS-level preference by default.
*   The application shall support dynamic type, allowing UI text to scale according to the user's OS-level font size settings.
*   The application shall strive to meet Web Content Accessibility Guidelines (WCAG) 2.1 Level AA standards.
*   All user-facing strings shall be managed in resource files and externalized from the application code to facilitate future localization, although the initial release will only support English (en-US).

**4.2 Software Interfaces (REQ-SIF-001)**
*   The app shall integrate with the native SDKs for Google Sign-In and Sign in with Apple (SI-001).
*   The backend shall integrate with Auth0 for managing the OIDC/OAuth2 protocol and issuing JWTs (SI-002).
*   The backend shall make secure REST API calls to the OpenAI GPT-4 API for recommendations (SI-003).
*   The client application and backend shall fetch content via a REST API from a Headless CMS provider (Contentful) (SI-004).
*   The backend shall integrate with the Google Books API to fetch book metadata (SI-005).
*   The backend shall receive and process server-to-server notifications (webhooks) from the Apple App Store and Google Play Billing for subscription lifecycle events (SI-006).

**4.3 Communication Interfaces (REQ-CIF-001)**
*   All communication between the mobile client and the backend API shall use HTTPS over TCP/IP.
*   The API shall use JSON for all request and response bodies.
*   The API shall be versioned (e.g., `/api/v1/...`) to ensure backward compatibility for older clients.
*   Communication shall be secured using TLS 1.2 or higher.
*   Client requests to protected endpoints shall include a JWT in the `Authorization: Bearer <token>` header, managed via the Dio HTTP client library's interceptors.

**5. Non-Functional Requirements**

**5.1 Performance (REQ-PER-001)**
*   P95 latency for all core API endpoints shall be less than 200ms (NFR-PERF-001).
*   The mobile application's main Dashboard screen shall load and become interactive in under 1.5 seconds on a standard 4G network connection (NFR-PERF-002).
*   The application's cold start time shall be under 2 seconds on a mid-range device (NFR-PERF-003).
*   Asynchronous operations shall not block the UI and shall provide feedback to the user that a process is underway (NFR-PERF-004).

**5.2 Security (REQ-SEC-001)**
*   All user authentication shall be delegated to secure third-party providers (Google, Apple) via Auth0 (NFR-SEC-001).
*   The system shall not store passwords or other sensitive credentials (NFR-SEC-001).
*   The system shall implement Role-Based Access Control (RBAC) (NFR-SEC-002).
*   A user's role (`free_user`, `premium_user`) shall be encoded in their JWT and used by the API Gateway and backend services to enforce feature access (NFR-SEC-002).
*   All user data stored in the primary database (Aurora) and file storage (S3) shall be encrypted at rest using AWS KMS (NFR-SEC-003).
*   All data transmitted between the client and server, and between internal services, shall be encrypted using TLS 1.2+ (NFR-SEC-004).
*   All third-party API keys shall be stored securely in AWS Secrets Manager and never exposed on the client side (NFR-SEC-005).
*   Backend services shall perform input validation on all incoming data to protect against common vulnerabilities such as injection attacks (OWASP Top 10) (NFR-SEC-006).
*   The system shall undergo regular, automated vulnerability scanning and a third-party penetration test annually (NFR-SEC-007).
*   The CI/CD pipeline shall include automated dependency scanning to detect and report known vulnerabilities in third-party libraries.

**5.3 Data Management and Retention (REQ-DAT-001)**
*   A formal data retention policy shall be implemented to comply with privacy regulations.
*   Structured application logs (e.g., CloudWatch Logs) shall be retained for 90 days.
*   User accounts that have been inactive for 2 years shall be considered dormant and all associated PII and user-generated content will be permanently purged.
*   Data export files generated by user request shall be automatically and permanently deleted from storage 24 hours after their creation.
*   All data received by the backend API shall be validated against a defined schema to ensure type safety, format correctness, and integrity before being processed or stored.

**5.4 Reliability and Disaster Recovery (REQ-REL-001)**
*   The system architecture shall leverage highly available managed AWS services to minimize single points of failure (NFR-REL-002).
*   The primary database shall be configured with automated, continuous backups and Point-In-Time Recovery (PITR) enabled for a recovery window of at least 14 days (NFR-REL-001).
    *   Database restoration procedures shall be tested automatically on a quarterly basis to validate the integrity of backups and the recovery process.
*   A comprehensive disaster recovery plan shall be documented and tested periodically, with the following objectives:
    *   Recovery Time Objective (RTO): < 4 hours.
    *   Recovery Point Objective (RPO): < 5 minutes.
*   The plan shall detail a multi-region failover strategy for all critical AWS services.
*   The system shall implement resilience patterns for all critical external API integrations (LLM, Book Database, CMS).
    *   A circuit breaker pattern shall be used to prevent cascading failures from an unresponsive API.
    *   Automated health checks shall continuously monitor the status of all internal services and external dependencies.
    *   Defined fallback behaviors shall be implemented:
        *   If the Book Database API is down, the system shall allow users to add a book by manually entering its Title and Author.
        *   If the LLM recommendation service fails, the UI shall display a graceful message to the user.
        *   If the Headless CMS is unavailable, the application shall serve stale content from its local cache.

**5.5 Availability (REQ-AVL-001)**
*   The backend services shall have a target uptime of 99.9%, excluding planned maintenance (NFR-A-001).
*   Planned maintenance windows shall be scheduled during periods of low user activity (NFR-A-002).
    *   Users shall be notified of significant planned maintenance at least 48 hours in advance via an in-app notification.
*   A public-facing status page shall be maintained to communicate system uptime, incidents, and planned maintenance to users (NFR-A-003).

**5.6 Scalability (REQ-SCA-001)**
*   The backend architecture shall be able to scale horizontally to support an initial target of 10,000 concurrent users (NFR-S-001).
*   The database solution shall support read-heavy workloads through the use of read replicas and scale write capacity automatically (NFR-S-002).
*   Stateless services shall scale automatically based on request volume (NFR-S-003).

**5.7 Maintainability (REQ-MNT-001)**
*   The backend shall be designed as a set of loosely coupled microservices (NFR-M-001).
*   All infrastructure shall be managed via Infrastructure as Code (AWS CDK) for repeatability and version control (NFR-M-002).
*   The codebase shall adhere to defined linting rules, and all new code shall be accompanied by unit and integration tests, with a target of 80% minimum code coverage (NFR-M-003).
    *   Backend tests shall be written using the Jest testing framework.
    *   Flutter unit and widget tests shall use `flutter_test`, and end-to-end tests shall use the `integration_test` package.
*   Backend API documentation shall be automatically generated and maintained in the OpenAPI 3.0 format (NFR-M-004).

**5.8 Regulatory and Legal Compliance (REQ-REG-001)**
*   **5.8.1 Privacy Regulations (GDPR & CCPA)**
    *   The system shall provide mechanisms for users to exercise their rights as defined by GDPR and CCPA, implemented through the 'Export My Data' and 'Delete Account' features specified in REQ-USR-001.
    *   User consent for data processing shall be obtained explicitly upon registration. The consent text shall clearly state what data is collected and for what purpose.
    *   Data Processing Agreements (DPAs) shall be in place with all third-party sub-processors (e.g., Auth0, OpenAI, AWS).
*   **5.8.2 Legal Documentation**
    *   A comprehensive Privacy Policy and Terms of Service document shall be drafted by legal counsel.
    *   Users must explicitly accept the Terms of Service and acknowledge the Privacy Policy during the registration process before an account can be created.
    *   Links to the Privacy Policy and Terms of Service shall be readily accessible from within the application's settings menu.
*   **5.8.3 Industry Standards**
    *   The system shall adhere to the security best practices outlined in the OWASP Application Security Verification Standard (ASVS).
    *   Payment processing for subscriptions shall be handled exclusively by the native Apple App Store and Google Play Store billing systems, ensuring PCI DSS compliance is managed by the platform providers.

**6. Other Requirements**

**6.1 Technology Stack (REQ-TEC-001)**
*   **Cloud Provider**: Amazon Web Services (AWS)
*   **Infrastructure as Code**: AWS CDK with TypeScript
*   **CI/CD**: GitHub Actions

*   **Mobile Frontend**
    *   Framework: Flutter 3.x
    *   State Management: Riverpod
    *   Local Database: Isar
    *   HTTP Client: Dio

*   **Backend**
    *   Language/Framework: Node.js (v18+) with TypeScript and Fastify
    *   Database ORM: Prisma
    *   Logging: Pino for structured JSON logging
    *   Container Orchestration: AWS Fargate
    *   Serverless Compute: AWS Lambda

*   **Data & Storage**
    *   Primary Database: Amazon Aurora (PostgreSQL compatible) Serverless v2
    *   Caching: Amazon ElastiCache for Redis
    *   File Storage: Amazon S3
    *   Vector Storage: Amazon OpenSearch Serverless with k-NN support

*   **Networking & Messaging**
    *   API Gateway: Amazon API Gateway
    *   Message Queue: Amazon SQS
    *   Push Notifications: Amazon SNS
    *   Email Service: Amazon SES

*   **Third-Party Services**
    *   Identity Provider: Auth0
    *   AI Recommendations: OpenAI GPT-4 API
    *   Book Metadata: Google Books API
    *   Ad Platform: Google AdMob SDK
    *   Headless CMS: Contentful

*   **Monitoring & Observability**
    *   Metrics & Alarms: AWS CloudWatch
    *   Distributed Tracing: AWS X-Ray
    *   Client-side Error Reporting: Sentry

**6.2 Deployment Environments (REQ-DEP-001)**
*   A multi-environment strategy shall be implemented to support the development lifecycle.
*   The AWS CDK configuration shall support the automated provisioning of distinct environments.
*   `Development`: Developers will use ephemeral, containerized environments for local feature development.
*   `Testing`: A dedicated environment used for automated QA and integration tests as part of the CI/CD pipeline.
*   `Staging`: A near-replica of the production environment for user acceptance testing (UAT) and pre-release validation.
*   `Production`: The live environment serving end-users.

**6.3 Reporting, Monitoring, and Logging (REQ-MON-001)**
*   Business-level dashboards shall be created to track key metrics such as Daily Active Users (DAU), Monthly Active Users (MAU), subscription conversion rates, and feature engagement.
*   Key performance indicators for all backend services shall be collected using Amazon CloudWatch Metrics.
*   All services and functions shall output structured JSON logs to Amazon CloudWatch Logs using the Pino logger for centralized aggregation, searching, and analysis.
    *   All logs associated with a single request shall include a unique correlation ID to enable distributed tracing across services.
*   AWS X-Ray shall be enabled to trace requests as they travel through the distributed system.
*   The application shall report client-side crashes, non-fatal errors, and performance metrics to Sentry for analysis.
*   CloudWatch Alarms shall be configured to automatically notify the development team of critical issues.
    *   Alerts shall be triggered if API 5xx error rates exceed 1%.
    *   Alerts shall be triggered if P99 latency surpasses 1 second.
    *   Alerts shall be triggered for critical failures in asynchronous jobs.
*   **Audit Logging**: The system shall maintain a separate, immutable audit trail for security-sensitive events, including user login success and failure, subscription changes, and account deletion requests. Audit logs shall be retained for a minimum of 1 year.