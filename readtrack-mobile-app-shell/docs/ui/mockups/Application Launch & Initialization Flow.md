{
  "diagram_info": {
    "diagram_name": "Application Launch & Initialization Flow",
    "diagram_type": "flowchart",
    "purpose": "To document the critical logic flow upon application startup, determining the initial user experience and routing based on authentication status, token validity, and onboarding completion state.",
    "target_audience": [
      "Mobile Developers",
      "QA Engineers",
      "Product Owners"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with distinct colors for decision points and UI screens.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Mobile App (Flutter)",
      "Secure Storage",
      "Backend API"
    ],
    "key_processes": [
      "Service Initialization",
      "Token Validation",
      "User Profile Fetch",
      "Routing Logic"
    ],
    "decision_points": [
      "Has Stored Token?",
      "Is Token Valid/Refreshable?",
      "Is Onboarding Complete?"
    ],
    "success_paths": [
      "Direct to Dashboard (Returning User)",
      "To Onboarding (New User)",
      "To Login (Guest)"
    ],
    "error_scenarios": [
      "Token Refresh Failed",
      "Network Unavailable (Offline Mode handling)"
    ],
    "edge_cases_covered": [
      "First launch",
      "Expired session",
      "Incomplete onboarding"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart showing the app launch sequence: initializing services, checking for secure tokens, validating session, checking onboarding status, and routing to either Login, Onboarding, or Dashboard.",
    "color_independence": "Shapes (diamonds for decisions, rectangles for processes) differentiate logic from screens.",
    "screen_reader_friendly": "Flow follows a logical top-down progression.",
    "print_compatibility": "High contrast borders and text."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout optimized for mobile documentation viewing.",
    "theme_compatibility": "Neutral color palette with semantic coloring for success/fail paths.",
    "performance_notes": "Represents the 'Warm Start' logic mentioned in REQ-PERF-002."
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing the `main.dart` initialization logic or debugging startup routing issues.",
    "stakeholder_value": {
      "developers": "Defines exact conditions for routing to Login vs Dashboard.",
      "product_managers": "Clarifies the user journey for new vs returning users.",
      "qa_engineers": "Provides test cases for app states (logged out, logged in, partially onboarded)."
    },
    "maintenance_notes": "Update if new mandatory startup checks (e.g., forced update, maintenance mode) are added.",
    "integration_recommendations": "Include in the 'Authentication & Routing' technical documentation section."
  },
  "validation_checklist": [
    "✅ Authentication check logic included",
    "✅ Onboarding status check included",
    "✅ Login screen fallback defined",
    "✅ Dashboard success path defined",
    "✅ Offline consideration noted",
    "✅ Consistent styling applied"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Node Definitions
    Start((App Launch))
    InitServices[Initialize Core Services\n(Isar DB, Secure Storage, Analytics)]
    
    %% Data Checks
    CheckAuth{Has Stored\nAuth Token?}
    ValidateToken{Is Token Valid\nor Refreshable?}
    CheckOnboarding{Is 'onboardingCompleted'\nFlag True?}
    
    %% Actions/Processes
    RefreshProcess[Attempt Token Refresh\n(Background)]
    FetchProfile[Fetch/Sync User Profile]
    ClearSession[Clear Local Session Data]
    
    %% UI Screens
    LoginScreen([Login Screen\n(US-001)])
    Dashboard([Main Dashboard\n(REQ-FUNC-001)])
    Onboarding([Onboarding Flow\n(REQ-FUNC-003)])
    
    %% Relationships
    Start --> InitServices
    InitServices --> CheckAuth
    
    %% Auth Logic
    CheckAuth -- No --> LoginScreen
    CheckAuth -- Yes --> ValidateToken
    
    ValidateToken -- No / Failed --> ClearSession
    ClearSession --> LoginScreen
    
    ValidateToken -- Yes --> FetchProfile
    
    %% Offline Handling Note
    subgraph Offline_Handling [Resilience Logic]
        direction TB
        FetchProfile -.-> |If Offline, use cached profile| CheckOnboarding
        FetchProfile --> |If Online, sync latest| CheckOnboarding
    end
    
    %% Routing Logic
    CheckOnboarding -- Yes --> Dashboard
    CheckOnboarding -- No --> Onboarding
    
    %% Post-Flow Connections
    LoginScreen -.-> |Success| CheckOnboarding
    Onboarding -.-> |Complete| Dashboard

    %% Styling
    classDef process fill:#e1f5fe,stroke:#01579b,stroke-width:2px,color:#000
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#000
    classDef screen fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000,stroke-dasharray: 5 5
    classDef terminator fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#000
    classDef logic fill:#f5f5f5,stroke:#616161,stroke-width:1px,color:#000

    class InitServices,RefreshProcess,FetchProfile,ClearSession process
    class CheckAuth,ValidateToken,CheckOnboarding decision
    class LoginScreen,Dashboard,Onboarding screen
    class Start terminator
    class Offline_Handling logic
```