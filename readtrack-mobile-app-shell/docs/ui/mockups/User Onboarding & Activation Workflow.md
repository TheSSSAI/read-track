{
  "diagram_info": {
    "diagram_name": "User Onboarding & Activation Workflow",
    "diagram_type": "flowchart",
    "purpose": "To visualize the mandatory multi-step onboarding process for new users, detailing user interactions, decision points (skips), and backend persistence states.",
    "target_audience": [
      "frontend developers",
      "backend developers",
      "product designers",
      "QA engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for vertical flow with clear separation between User UI and Backend/Database operations",
  "diagram_elements": {
    "actors_systems": [
      "New User",
      "Mobile App (Flutter)",
      "Backend API",
      "Primary Database"
    ],
    "key_processes": [
      "Goal Setting",
      "Book Addition",
      "Feature Tour",
      "State Persistence"
    ],
    "decision_points": [
      "Skip Goal?",
      "Skip Book?",
      "Search Successful?",
      "Tour Completed?"
    ],
    "success_paths": [
      "Complete Profile Setup -> Dashboard"
    ],
    "error_scenarios": [
      "API Failure during save",
      "Search returns no results"
    ],
    "edge_cases_covered": [
      "User skips all steps",
      "App restart mid-onboarding"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart showing the new user onboarding journey from login to dashboard, including steps for setting goals, adding a book, and viewing the feature tour.",
    "color_independence": "Steps, Decisions, and Data actions are distinguished by shape and stroke style.",
    "screen_reader_friendly": "Nodes follow a logical top-down sequence matching the screen reader order.",
    "print_compatibility": "High contrast black and white compatible."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout fits standard documentation viewports",
    "theme_compatibility": "Neutral styling works in light/dark modes",
    "performance_notes": "Standard complexity, renders instantly"
  },
  "usage_guidelines": {
    "when_to_reference": "During development of the 'OnboardingScreen' widget and 'CompleteUserOnboardingCommand' backend handler.",
    "stakeholder_value": {
      "developers": "Defines exact API triggers and state transitions.",
      "designers": "Maps the user journey and optional paths.",
      "product_managers": "Visualizes the activation funnel steps.",
      "QA_engineers": "Provides a map for testing skip logic and persistence."
    },
    "maintenance_notes": "Update if new steps are added to the onboarding sequence.",
    "integration_recommendations": "Include in the PRD for 'First Time User Experience' (FTUE)."
  },
  "validation_checklist": [
    "✅ Happy path (Goal + Book + Tour) included",
    "✅ Skip paths for Goal and Book steps included",
    "✅ Backend state update (`hasCompletedOnboarding`) included",
    "✅ Transition to Dashboard clearly marked",
    "✅ API interactions identified"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Nodes
    Start([User Logs In])
    CheckState{Is 'hasCompletedOnboarding' \n true?}
    
    subgraph UI_Client ["📱 Mobile Client (Flutter)"]
        NavDash[Navigate to Dashboard]
        
        %% Step 1: Goal
        ScreenGoal[Screen: Set Yearly Goal]
        InputGoal[/Input: Yearly Book Target/]
        DecideGoal{User Action}
        BtnSkipGoal[Click 'Skip']
        BtnSetGoal[Click 'Set Goal']
        
        %% Step 2: Add Book
        ScreenBook[Screen: Add 'Currently Reading']
        InputSearch[/Input: Search Query/]
        CallSearch>API: Search Books]
        ListResults[Display Results List]
        DecideBook{User Action}
        BtnSkipBook[Click 'Skip']
        BtnSelectBook[Select Book]
        
        %% Step 3: Tour
        ScreenTour[Screen: Feature Tour Overlay]
        TourSteps[Display: Dashboard -> Log Button -> Goals]
        DecideTour{User Action}
        BtnDismissTour[Click 'Finish' / 'Skip']
    end

    subgraph Backend_System ["☁️ Backend API & DB"]
        DBCreateGoal[(DB: Create Goal Record)]
        DBSearch[Google Books API Proxy]
        DBAddBook[(DB: Create LibraryItem)]
        DBUpdateProfile[(DB: Update User\nhasCompletedOnboarding=true)]
    end

    %% Flow
    Start --> CheckState
    CheckState -- Yes --> NavDash
    CheckState -- No --> ScreenGoal

    %% Goal Logic
    ScreenGoal --> InputGoal
    InputGoal --> DecideGoal
    DecideGoal -- Tap Skip --> BtnSkipGoal
    DecideGoal -- Tap Save --> BtnSetGoal
    BtnSetGoal --> DBCreateGoal
    DBCreateGoal --> ScreenBook
    BtnSkipGoal --> ScreenBook

    %% Book Logic
    ScreenBook --> InputSearch
    InputSearch --> CallSearch
    CallSearch --> DBSearch
    DBSearch -.-> ListResults
    ListResults --> DecideBook
    DecideBook -- Tap Skip --> BtnSkipBook
    DecideBook -- Select Item --> BtnSelectBook
    BtnSelectBook --> DBAddBook
    DBAddBook --> ScreenTour
    BtnSkipBook --> ScreenTour

    %% Tour Logic
    ScreenTour --> TourSteps
    TourSteps --> DecideTour
    DecideTour --> BtnDismissTour
    
    %% Completion
    BtnDismissTour --> DBUpdateProfile
    DBUpdateProfile --> NavDash

    %% Styling
    classDef highlight fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#000
    classDef database fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000
    classDef decision fill:#fff3e0,stroke:#ef6c00,stroke-width:2px,color:#000
    classDef screen fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#000

    class Start,NavDash highlight
    class DBCreateGoal,DBSearch,DBAddBook,DBUpdateProfile database
    class CheckState,DecideGoal,DecideBook,DecideTour decision
    class ScreenGoal,ScreenBook,ScreenTour screen
```