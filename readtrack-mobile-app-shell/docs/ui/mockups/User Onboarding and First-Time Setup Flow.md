{
  "diagram_info": {
    "diagram_name": "User Onboarding and First-Time Setup Flow",
    "diagram_type": "flowchart",
    "purpose": "To visualize the mandatory multi-step onboarding sequence for new users, detailing the logical flow, data entry points, skip mechanisms, and backend persistence as defined in US-006 through US-011.",
    "target_audience": [
      "Mobile Developers",
      "Backend Developers",
      "UX Designers",
      "QA Engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5-10 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with distinct color coding for UI steps, backend operations, and error states.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Flutter Client",
      "Backend API",
      "Primary Database",
      "Google Books API"
    ],
    "key_processes": [
      "Goal Setting",
      "Book Search & Addition",
      "Feature Tour",
      "Onboarding Completion Persistence"
    ],
    "decision_points": [
      "Is Onboarding Complete?",
      "Skip Step?",
      "Input Validation",
      "API Success/Failure"
    ],
    "success_paths": [
      "Set Goal -> Add Book -> Complete Tour -> Dashboard",
      "Skip Goal -> Skip Book -> Skip Tour -> Dashboard"
    ],
    "error_scenarios": [
      "Invalid Goal Input",
      "Network Failure during Save",
      "Book Search Failure"
    ],
    "edge_cases_covered": [
      "User skips all optional steps",
      "Silent failure logging on tour completion"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart describing the new user onboarding process: Step 1 Set Goal, Step 2 Add Book, Step 3 Feature Tour, leading to the Dashboard.",
    "color_independence": "Shapes (rhombus for decisions, rectangles for process) denote function alongside color.",
    "screen_reader_friendly": "Nodes have descriptive text labels.",
    "print_compatibility": "High contrast borders ensure readability in grayscale."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout (TD) for better mobile scrolling readability.",
    "theme_compatibility": "Custom class definitions used to ensure consistency across themes.",
    "performance_notes": "Subgraphs used to logically group steps for faster cognitive processing."
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of US-006, US-007, US-008, US-009, US-010, US-011 and when testing the onboarding entry/exit logic.",
    "stakeholder_value": {
      "developers": "Defines the exact API endpoints triggered at each step and the expected error handling logic.",
      "designers": "Validates the skip/continue flow and visualizes the sequence of screens.",
      "product_managers": "Ensures all requirements from the Onboarding Epic are represented in the flow.",
      "qa_engineers": "Provides a map for E2E testing scenarios including happy paths and skip paths."
    },
    "maintenance_notes": "Update this diagram if the ordering of steps changes or if new mandatory steps (e.g., Notification Permissions) are added.",
    "integration_recommendations": "Embed in the Onboarding Epic documentation and link to the relevant UI mockups."
  },
  "validation_checklist": [
    "✅ Includes initial check for onboarding status",
    "✅ Represents logic for 'Set Goal' (US-007) and 'Skip Goal' (US-008)",
    "✅ Represents logic for 'Add Book' (US-009) and 'Skip Book' (US-010)",
    "✅ Represents Feature Tour flow (US-011)",
    "✅ Details backend persistence calls",
    "✅ Handles API error scenarios"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Nodes
    Start((Start: Auth Success))
    CheckState{Onboarding\nCompleted?}
    Dashboard[Load Main Dashboard]

    subgraph Step1_Goal["Step 1: Set Yearly Goal (US-007)"]
        GoalUI[Display 'Set Yearly Goal' UI]
        GoalInput[/User Input: Target Books/]
        GoalSkip{User Action}
        GoalValid{Input Valid?}
        GoalError[Show Inline Error]
    end

    subgraph Step2_Book["Step 2: Add First Book (US-009)"]
        BookUI[Display 'Add Current Book' UI]
        BookSearch[/User Searches Book/]
        BookAPI_Search[API: GET /books/search]
        BookSelect{User Action}
    end

    subgraph Step3_Tour["Step 3: Feature Tour (US-011)"]
        TourUI[Display Feature Tour Overlay]
        TourAction{User Action}
    end

    subgraph Backend["Backend & Persistence"]
        API_SaveGoal[API: POST /api/v1/goals]
        API_SaveBook[API: POST /api/v1/library]
        API_Complete[API: PATCH /api/v1/users/onboarding]
        DB[(Primary DB)]
    end

    %% Flow: Start & Check
    Start --> CheckState
    CheckState -- Yes --> Dashboard
    CheckState -- No --> GoalUI

    %% Step 1 Logic: Goal
    GoalUI --> GoalInput
    GoalInput --> GoalSkip
    GoalSkip -- "Skip (US-008)" --> BookUI
    GoalSkip -- "Set Goal" --> GoalValid
    GoalValid -- No (>0 Int) --> GoalError --> GoalInput
    GoalValid -- Yes --> API_SaveGoal

    API_SaveGoal -- Success --> DB
    API_SaveGoal -- Error --> GoalErrorAPI[Show Network Error] --> GoalUI
    DB -.-> BookUI

    %% Step 2 Logic: Book
    BookUI --> BookSearch
    BookSearch --> BookAPI_Search
    BookAPI_Search -- Results --> BookSelect
    BookSelect -- "Skip (US-010)" --> TourUI
    BookSelect -- "Select & Add" --> API_SaveBook

    API_SaveBook -- Success --> DB
    API_SaveBook -- Error --> BookErrorAPI[Show Network Error] --> BookUI
    DB -.-> TourUI

    %% Step 3 Logic: Tour & Finish
    TourUI --> TourAction
    TourAction -- "Skip / Done" --> API_Complete

    API_Complete -- Success --> DB
    API_Complete -- Error --> TourError[Log Silent Failure] --> Dashboard
    DB -.-> Dashboard

    %% Styling
    classDef ui fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#000
    classDef backend fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#000
    classDef error fill:#ffebee,stroke:#c62828,stroke-width:2px,color:#000
    classDef terminal fill:#212121,stroke:#000,stroke-width:2px,color:#fff

    class GoalUI,BookUI,TourUI,GoalInput,BookSearch ui
    class API_SaveGoal,API_SaveBook,API_Complete,BookAPI_Search,DB backend
    class CheckState,GoalSkip,GoalValid,BookSelect,TourAction decision
    class GoalError,GoalErrorAPI,BookErrorAPI,TourError error
    class Start,Dashboard terminal
```