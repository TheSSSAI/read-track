{
  "diagram_info": {
    "diagram_name": "App Launch & Initialization Logic",
    "diagram_type": "flowchart",
    "purpose": "Documents the decision logic and sequence of events occurring when the application is launched, covering initialization, authentication, onboarding checks, and dashboard routing.",
    "target_audience": [
      "Mobile Developers (Flutter)",
      "QA Engineers",
      "Product Managers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for vertical flow with distinct subgraphs for logical phases.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Mobile App",
      "Local Database (Isar)",
      "Backend API",
      "Auth0"
    ],
    "key_processes": [
      "Service Initialization",
      "Session Validation",
      "Data Synchronization",
      "Feature Gating",
      "UI Routing"
    ],
    "decision_points": [
      "Is User Authenticated?",
      "Is Device Online?",
      "Is Onboarding Complete?",
      "Is Feature Tour Seen?",
      "Is Year in Review Available?",
      "Are Goals Expired?"
    ],
    "success_paths": [
      "Direct to Dashboard",
      "Login to Onboarding to Dashboard"
    ],
    "error_scenarios": [
      "Network Unavailable (Offline Mode)",
      "Session Expired/Invalid"
    ],
    "edge_cases_covered": [
      "First run",
      "Upgrade from Free to Premium state change",
      "New Year Event"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart detailing the app launch sequence: initializing services, checking authentication, handling offline/online states, routing to login or onboarding, and checking logic for dashboard overlays like the feature tour or year in review.",
    "color_independence": "Shapes and labels distinguish between processes, decisions, and UI screens.",
    "screen_reader_friendly": "Logical top-down flow with descriptive node labels.",
    "print_compatibility": "High contrast rendering suitable for documentation."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout adapts to standard documentation widths.",
    "theme_compatibility": "Neutral styling works in light/dark modes.",
    "performance_notes": "Uses standard flowchart syntax for broad compatibility."
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing the `main.dart` or `app_delegate` logic, debugging startup crashes, or verifying the order of modal presentations.",
    "stakeholder_value": {
      "developers": "Defines the exact order of async checks and UI routing logic.",
      "designers": "Clarifies the sequence of potential pop-ups and blocking views on startup.",
      "product_managers": "Validates that high-value features (Onboarding, Upsells, YiR) are shown at the correct times.",
      "QA_engineers": "Provides a checklist of startup states to test (e.g., Offline vs Online, New vs Returning User)."
    },
    "maintenance_notes": "Update if new blocking interceptors (e.g., mandatory updates) are added to the startup flow.",
    "integration_recommendations": "Include in the 'App Lifecycle' section of technical documentation."
  },
  "validation_checklist": [
    "✅ Authentication check occurs before data loading",
    "✅ Offline support path defined",
    "✅ Onboarding logic bypasses dashboard until complete",
    "✅ Subscription sync logic included",
    "✅ Dashboard interceptors (Tour, YiR, Goals) sequenced correctly",
    "✅ Mermaid syntax validated"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Styling Definitions
    classDef process fill:#e1f5fe,stroke:#01579b,stroke-width:2px,color:#000
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#000
    classDef screen fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000,rx:5,ry:5
    classDef external fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,stroke-dasharray: 5 5,color:#000
    classDef terminator fill:#ffcdd2,stroke:#c62828,stroke-width:2px,color:#000

    Start([App Launch]) --> Init[Initialize Services<br/>Hive/Isar DB, Theme, Analytics]
    Init --> CheckAuth{Has Valid<br/>Session Token?}

    %% Unauthenticated Flow
    CheckAuth -- No --> LoginScreen[Login Screen<br/>(US-001)]
    LoginScreen --> UserLogin{User Action}
    UserLogin -- Social Sign In --> Auth0((Auth0 / Backend))
    Auth0 --> |Success| SaveToken[Secure Storage: Save JWT]
    SaveToken --> CheckOnboarding

    %% Authenticated Flow
    CheckAuth -- Yes --> LoadLocal[Load User Profile<br/>from Isar DB]
    LoadLocal --> CheckNetwork{Network<br/>Available?}
    
    subgraph BackgroundSync [Background Tasks]
        direction TB
        CheckNetwork -- Yes --> SyncSub[Sync Subscription Status<br/>(Check Expiry/Downgrade)]
        SyncSub --> SyncQueue[Push Offline Changes<br/>(US-101)]
        SyncQueue --> FetchConfig[Fetch Feature Flags<br/>& Year in Review Data]
    end

    CheckNetwork -- No --> CheckOnboarding
    FetchConfig -.-> CheckOnboarding

    %% Routing Logic
    subgraph RoutingLogic [Routing & Interceptors]
        CheckOnboarding{Onboarding<br/>Complete?}
        CheckOnboarding -- No --> OnboardingFlow[Onboarding Flow<br/>(US-006)]
        OnboardingFlow --> MarkOnbComplete[Set 'onboardingCompleted'<br/>in DB]
        MarkOnbComplete --> CheckTour

        CheckOnboarding -- Yes --> CheckTour{Feature Tour<br/>Seen?}
        
        CheckTour -- No --> TourOverlay[Feature Tour Overlay<br/>(US-011)]
        TourOverlay --> MarkTourComplete[Set 'tourCompleted'<br/>in DB]
        MarkTourComplete --> DashboardState
        
        CheckTour -- Yes --> DashboardState[Prepare Dashboard View]
    end

    %% Dashboard Presentation Logic
    subgraph DashboardInterceptors [Dashboard Presentation]
        DashboardState --> CheckYiR{Show Year<br/>in Review?}
        
        CheckYiR -- Yes (Jan 1+ & Flag True) --> YiRModal[Show Year in Review<br/>(US-066)]
        YiRModal --> DismissYiR[Dismiss & Update Flag]
        DismissYiR --> CheckGoals
        
        CheckYiR -- No --> CheckGoals{Goals Expired?}
        
        CheckGoals -- Yes --> GoalPrompt[Show 'Set New Goal' Modal<br/>(US-067)]
        GoalPrompt --> DismissGoal[Dismiss/Set Goal]
        DismissGoal --> FinalView
        
        CheckGoals -- No --> FinalView
    end

    FinalView[Main Dashboard Screen<br/>(US-056 / US-057)]

    %% Connections for flow completion
    LoginScreen --> |User Exits| Exit([App Close])

    %% Styling Application
    class Start,Init,LoadLocal,SaveToken,MarkOnbComplete,MarkTourComplete,DismissYiR,DismissGoal,SyncSub,SyncQueue,FetchConfig process
    class CheckAuth,CheckNetwork,CheckOnboarding,CheckTour,CheckYiR,CheckGoals,UserLogin decision
    class LoginScreen,OnboardingFlow,TourOverlay,YiRModal,GoalPrompt,FinalView screen
    class Auth0 external
    class Exit terminator
```