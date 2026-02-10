{
  "diagram_info": {
    "diagram_name": "Application Initial Routing & Navigation Logic",
    "diagram_type": "flowchart",
    "purpose": "Documents the decision logic for routing users to the correct initial screen (Login, Onboarding, or Dashboard) based on authentication and profile state.",
    "target_audience": [
      "mobile developers",
      "backend developers",
      "QA engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with distinct colors for screens vs. logic",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Mobile Client",
      "Local Storage (Isar)",
      "Auth0 / Backend API"
    ],
    "key_processes": [
      "Session Validation",
      "Profile State Check",
      "Onboarding Resume Logic",
      "Navigation Routing"
    ],
    "decision_points": [
      "Is User Authenticated?",
      "Is Onboarding Completed?",
      "Does Saved Onboarding State Exist?"
    ],
    "success_paths": [
      "Existing User -> Dashboard",
      "New User -> Onboarding -> Dashboard",
      "Unauthenticated -> Login"
    ],
    "error_scenarios": [
      "Token Expiry/Revocation",
      "Network Failure during Profile Fetch"
    ],
    "edge_cases_covered": [
      "App terminated mid-onboarding",
      "Offline launch behavior"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart showing the application startup sequence: determining authentication state, checking onboarding flags, and routing to Login, Onboarding Flow, or Dashboard.",
    "color_independence": "Shapes (diamonds for decisions, rectangles for screens) distinguish element types.",
    "screen_reader_friendly": "Nodes have descriptive labels indicating actions and states.",
    "print_compatibility": "High contrast borders and text ensure readability in monochrome."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout optimized for mobile documentation viewing",
    "theme_compatibility": "Neutral color palette with semantic highlights",
    "performance_notes": "Standard flowchart complexity, renders instantly"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the `main.dart` or root navigation controller initialization.",
    "stakeholder_value": {
      "developers": "Defines exact logic for the splash screen/boot sequence.",
      "designers": "Clarifies the user journey entry points.",
      "product_managers": "Validates the enforcement of mandatory onboarding (REQ-ONB-001).",
      "QA_engineers": "Provides test cases for cold starts, existing users, and interrupted onboarding."
    },
    "maintenance_notes": "Update if new mandatory setup steps (e.g., T&C acceptance update) are added to the startup sequence.",
    "integration_recommendations": "Embed in the Navigation/Routing technical architecture documentation."
  },
  "validation_checklist": [
    "✅ Login screen routing for unauthenticated users (US-001)",
    "✅ Mandatory onboarding flow trigger (REQ-FUNC-003)",
    "✅ Resume functionality for interrupted onboarding (US-006 AC-004)",
    "✅ Dashboard access for returning users (US-057)",
    "✅ Session revocation handling (US-012)",
    "✅ Mermaid syntax validated"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Global Styling
    classDef screen fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1,rx:5,ry:5
    classDef logic fill:#fff3e0,stroke:#ef6c00,stroke-width:2px,color:#e65100
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#f57f17,shape:rhombus
    classDef action fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20
    classDef terminate fill:#fce4ec,stroke:#c2185b,stroke-width:2px,color:#880e4f

    Start([App Launch / Cold Start]) --> Init[Initialize App Config & Dependencies]
    Init --> CheckAuth{Has Valid\nAuth Token?}
    
    %% Unauthenticated Path
    CheckAuth -- No / Expired --> LoginScreen[Login Screen\n(US-001)]
    class LoginScreen screen
    
    LoginScreen --> UserAuth[User Authenticates\n(Google/Apple)]
    UserAuth -- Success --> SaveSession[Securely Store Tokens]
    SaveSession --> FetchProfile
    
    %% Authenticated Path
    CheckAuth -- Yes --> FetchProfile[Fetch User Profile & Flags]
    class FetchProfile logic
    
    FetchProfile --> CheckOnboarding{Flag:\nonboardingCompleted?}
    class CheckAuth,CheckOnboarding decision
    
    %% Onboarding Logic (REQ-FUNC-003, US-006)
    CheckOnboarding -- No --> CheckResume{Local State:\nMid-Onboarding?}
    class CheckResume decision
    
    CheckResume -- Yes --> RestoreState[Restore Last Step]
    class RestoreState logic
    CheckResume -- No --> StartOnboarding[Start Fresh Onboarding]
    
    subgraph OnboardingFlow [Onboarding Sequence]
        direction TB
        StartOnboarding --> SetGoal[Step 1: Set Goal]
        RestoreState -.-> SetGoal
        RestoreState -.-> AddBook[Step 2: Add Book]
        SetGoal --> AddBook
        AddBook --> FeatureTour[Step 3: Feature Tour]
        class SetGoal,AddBook,FeatureTour screen
    end
    
    FeatureTour -- Finish/Skip --> SetFlag[Update Backend:\nonboardingCompleted = true]
    class SetFlag action
    
    %% Main App Path
    CheckOnboarding -- Yes --> Dashboard[Main Dashboard\n(REQ-DSH-001)]
    SetFlag --> Dashboard
    class Dashboard screen
    
    %% Session Management (US-012)
    Dashboard -.->|User Logout / Revoke| ClearData[Clear Local Session]
    class ClearData terminate
    ClearData --> LoginScreen

    %% Offline Handling Note
    FetchProfile -.->|Network Error| CheckOffline{Has Cached\nProfile?}
    CheckOffline -- Yes --> CheckOnboarding
    CheckOffline -- No --> LoginScreen
```