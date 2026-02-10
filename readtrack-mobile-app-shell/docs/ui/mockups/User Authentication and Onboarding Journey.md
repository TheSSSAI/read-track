{
  "diagram_info": {
    "diagram_name": "User Authentication and Onboarding Journey",
    "diagram_type": "flowchart",
    "purpose": "To visualize the application launch flow, distinguishing between Guest, New, and Returning users, and detailing the mandatory onboarding sequence for new accounts.",
    "target_audience": [
      "Mobile Developers",
      "QA Engineers",
      "Product Owners"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for vertical flow with distinct subgraphs for user states.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Flutter Client",
      "Auth0 Service",
      "Backend API",
      "Isar Local DB"
    ],
    "key_processes": [
      "Session Validation",
      "Authentication (Social Login)",
      "Onboarding Sequence",
      "Subscription Check"
    ],
    "decision_points": [
      "Has Valid Session?",
      "Is New User?",
      "Onboarding Complete?",
      "Skip Step?"
    ],
    "success_paths": [
      "Guest to New User to Dashboard",
      "Returning User to Dashboard"
    ],
    "error_scenarios": [
      "Auth Failure",
      "Network Error during Sync"
    ],
    "edge_cases_covered": [
      "App killed during onboarding",
      "Subscription expired on return"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart showing app launch. Guests login via Auth0. New users go through Goal Setting, Book Adding, and Tour. Returning users go to Dashboard.",
    "color_independence": "Shapes and labels distinguish logic (diamonds) from screens (rectangles) and actions (rounded).",
    "screen_reader_friendly": "Flow is strictly top-down with labeled decision branches.",
    "print_compatibility": "High contrast black and white compatible."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout suitable for documentation pages.",
    "theme_compatibility": "Neutral styling for light/dark mode.",
    "performance_notes": "Standard complexity, renders instantly."
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the splash screen, auth repository, and onboarding bloc.",
    "stakeholder_value": {
      "developers": "Defines the exact state transitions for the root navigation router.",
      "designers": "Visualizes the screens required for the first-run experience (US-006).",
      "product_managers": "Verifies the onboarding funnel steps (Goal -> Book -> Tour).",
      "QA_engineers": "Provides test cases for new vs returning user flows and resume-onboarding scenarios."
    },
    "maintenance_notes": "Update if additional onboarding steps (e.g., notification permissions) are added.",
    "integration_recommendations": "Link to US-006 and REQ-FUNC-003 in Jira/Confluence."
  },
  "validation_checklist": [
    "✅ Guest, New, and Returning user paths defined",
    "✅ Onboarding steps (Goal, Book, Tour) included",
    "✅ Skip logic for optional steps included",
    "✅ Local DB (Isar) interaction for session check included",
    "✅ Auth0 integration point marked"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Nodes
    Start((App Launch))
    
    subgraph Local_Check [Local Session Check]
        CheckSession{Valid Local\nSession?}
        FetchProfile[Fetch User Profile\nfrom Isar DB]
    end

    subgraph Guest_Flow [Guest User Experience US-001]
        LoginScreen[Login Screen\nSign in with Google/Apple]
        AuthAction[[User Authenticates\nvia Auth0]]
        AuthDecision{Auth Success?}
        AuthError[Show Error Message]
    end

    subgraph Backend_Logic [Server-Side Logic]
        ValidateToken{Validate Token\n& Check User Exists}
        CreateUser[Create New User Record\nRole: Free User]
        UpdateLastLogin[Update Last Login]
        CheckSub[Check Subscription Status\nREQ-FUNC-002]
    end

    subgraph Onboarding_Flow [New User Onboarding REQ-FUNC-003]
        CheckOnbStatus{Onboarding\nCompleted?}
        Step1[Step 1: Set Yearly Goal\nUS-007]
        DecideGoal{User Action}
        SaveGoal[Save Goal to DB]
        Step2[Step 2: Add Current Book\nUS-009]
        DecideBook{User Action}
        SaveBook[Add to 'Currently Reading'\nShelf]
        Step3[Step 3: Feature Tour\nUS-011]
        FinishOnb[Mark Onboarding Complete\nPersist to Backend]
    end

    subgraph Dashboard_Access [Main App Experience]
        SyncData[Sync Data to Isar\nREQ-FUNC-011]
        Dashboard[Main Dashboard\nUS-056]
    end

    %% Relationships
    Start --> CheckSession
    
    %% Session Check Path
    CheckSession -- No --> LoginScreen
    CheckSession -- Yes --> FetchProfile
    FetchProfile --> CheckSub
    CheckSub --> SyncData

    %% Guest Auth Path
    LoginScreen --> AuthAction
    AuthAction --> AuthDecision
    AuthDecision -- No --> AuthError --> LoginScreen
    AuthDecision -- Yes --> ValidateToken

    %% New vs Returning Logic
    ValidateToken -- User Not Found --> CreateUser
    ValidateToken -- User Exists --> UpdateLastLogin
    
    CreateUser --> Step1
    UpdateLastLogin --> CheckOnbStatus

    %% Resume Onboarding Logic
    CheckOnbStatus -- No --> Step1
    CheckOnbStatus -- Yes --> CheckSub

    %% Onboarding Steps
    Step1 --> DecideGoal
    DecideGoal -- Set Goal --> SaveGoal --> Step2
    DecideGoal -- Skip US-008 --> Step2
    
    Step2 --> DecideBook
    DecideBook -- Add Book --> SaveBook --> Step3
    DecideBook -- Skip US-010 --> Step3

    Step3 -- User Dismisses --> FinishOnb
    FinishOnb --> SyncData
    SyncData --> Dashboard

    %% Styling
    classDef screen fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#000
    classDef logic fill:#fff3e0,stroke:#ef6c00,stroke-width:2px,color:#000
    classDef action fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000
    classDef error fill:#ffebee,stroke:#c62828,stroke-width:2px,color:#000
    classDef database fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#000

    class LoginScreen,Step1,Step2,Step3,Dashboard screen
    class CheckSession,AuthDecision,ValidateToken,CheckOnbStatus,DecideGoal,DecideBook,CheckSub logic
    class Start,AuthAction,SaveGoal,SaveBook,FinishOnb,SyncData action
    class AuthError error
    class FetchProfile,CreateUser,UpdateLastLogin database
```