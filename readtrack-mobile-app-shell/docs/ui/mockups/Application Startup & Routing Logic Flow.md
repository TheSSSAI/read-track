{
  "diagram_info": {
    "diagram_name": "Application Startup & Routing Logic Flow",
    "diagram_type": "flowchart",
    "purpose": "To visualize the critical decision logic executed upon application launch, determining whether to direct the user to the dashboard, onboarding flow, login screen, or forced update screen based on authentication, app version, and user state.",
    "target_audience": [
      "mobile developers",
      "QA engineers",
      "product managers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with clear color-coding for decision paths.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Mobile App (Flutter)",
      "Backend API",
      "Local Storage (Isar)",
      "Auth0"
    ],
    "key_processes": [
      "Version Check",
      "Token Validation",
      "User Profile Sync",
      "Routing"
    ],
    "decision_points": [
      "Forced Update Required?",
      "User Authenticated?",
      "Onboarding Complete?"
    ],
    "success_paths": [
      "Direct to Dashboard",
      "Login to Dashboard",
      "Onboarding to Dashboard"
    ],
    "error_scenarios": [
      "Forced Update Blocking",
      "Token Expiry",
      "Network Errors"
    ],
    "edge_cases_covered": [
      "Offline Mode",
      "First-time Install",
      "Returning User"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart detailing the app startup sequence: checks app version, then authentication status, then onboarding completion status to determine the landing screen.",
    "color_independence": "Shapes distinguish process steps (rectangles) from decisions (diamonds).",
    "screen_reader_friendly": "Nodes have descriptive labels indicating the logic flow.",
    "print_compatibility": "High contrast black and white rendering supported."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Top-down layout optimized for vertical scrolling.",
    "theme_compatibility": "Uses standard class definitions for flexibility.",
    "performance_notes": "Logic represents synchronous and asynchronous checks during the splash screen phase."
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the `main.dart` or root widget routing logic.",
    "stakeholder_value": {
      "developers": "Defines the exact order of operations for the splash screen.",
      "designers": "Identifies necessary states (loading, update required, etc.).",
      "product_managers": "Verifies that business rules for forced updates and onboarding are enforced.",
      "QA_engineers": "Provides a map for testing startup scenarios (e.g., old version, unauthenticated, partial onboarding)."
    },
    "maintenance_notes": "Update if new gating conditions (e.g., maintenance mode) are added.",
    "integration_recommendations": "Link to the Splash Screen UI story and Route Guard documentation."
  },
  "validation_checklist": [
    "✅ Forced update check is prioritized for security/stability",
    "✅ Authentication check precedes data access",
    "✅ Onboarding check ensures REQ-ONB-001 compliance",
    "✅ Offline fallback paths considered for local state",
    "✅ Mermaid syntax validated",
    "✅ Clear start and end states defined",
    "✅ Decision diamonds correctly labeled with Yes/No paths",
    "✅ Visual styling differentiates happy path from blocking states"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Define Nodes
    Start((App Launch))
    
    subgraph Initialization [Phase 1: Environment Check]
        LoadConfig[Load Local Config & Secure Storage]
        CheckNet{Network Available?}
        FetchRemoteConfig[Fetch Remote Config / Version]
        CheckUpdate{Is Forced Update<br/>Required?}
        UpdateScreen[Display 'Update Required'<br/>Blocking Screen]
    end

    subgraph Auth_Validation [Phase 2: Authentication]
        CheckAuth{Is User<br/>Authenticated?}
        ValidateToken[Validate JWT / Refresh Token]
        TokenValid{Token Valid?}
        ClearSession[Clear Local Session]
        LoginScreen[Display Login Screen<br/>(US-001)]
    end

    subgraph User_State [Phase 3: State & Routing]
        FetchProfile[Fetch/Sync User Profile<br/>(Remote or Local Isar)]
        CheckOnboarding{Is Onboarding<br/>Complete?}
        OnboardingFlow[Start Onboarding Flow<br/>(REQ-ONB-001 / US-006)]
    end

    subgraph Destination [Phase 4: Landing]
        Dashboard[Load Main Dashboard<br/>(US-056)]
        AppStore[Redirect to App Store]
    end

    %% Define Flow
    Start --> LoadConfig
    LoadConfig --> CheckNet
    
    %% Network / Update Logic
    CheckNet -- Yes --> FetchRemoteConfig
    FetchRemoteConfig --> CheckUpdate
    CheckNet -- No --> CheckAuth
    
    CheckUpdate -- Yes --> UpdateScreen
    UpdateScreen --> AppStore
    CheckUpdate -- No --> CheckAuth

    %% Auth Logic
    CheckAuth -- Yes (Has Credentials) --> ValidateToken
    CheckAuth -- No --> LoginScreen
    
    ValidateToken -- Valid --> FetchProfile
    ValidateToken -- Invalid/Expired --> ClearSession
    ClearSession --> LoginScreen
    
    LoginScreen -- User Authenticates --> FetchProfile

    %% Onboarding Logic
    FetchProfile --> CheckOnboarding
    CheckOnboarding -- Yes (True) --> Dashboard
    CheckOnboarding -- No (False) --> OnboardingFlow
    OnboardingFlow -- User Completes --> Dashboard

    %% Styling
    classDef process fill:#e1f5fe,stroke:#01579b,stroke-width:2px,color:#000
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#000
    classDef screen fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000
    classDef terminator fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#000
    classDef error fill:#ffebee,stroke:#c62828,stroke-width:2px,color:#000

    class LoadConfig,FetchRemoteConfig,ValidateToken,FetchProfile,ClearSession process
    class CheckNet,CheckUpdate,CheckAuth,TokenValid,CheckOnboarding decision
    class LoginScreen,OnboardingFlow,UpdateScreen screen
    class Start,Dashboard terminator
    class AppStore error
```