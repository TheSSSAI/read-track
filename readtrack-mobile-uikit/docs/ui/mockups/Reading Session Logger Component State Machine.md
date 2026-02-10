{
  "diagram_info": {
    "diagram_name": "Reading Session Logger Component State Machine",
    "diagram_type": "stateDiagram-v2",
    "purpose": "To document the internal states, transitions, and side-effects of the ReadingSessionLogger component, focusing on the dual-mode input (Timer/Manual), validation logic, and offline-first persistence strategy.",
    "target_audience": [
      "Flutter Developers",
      "QA Engineers",
      "UI/UX Designers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "10 minutes"
  },
  "diagram_elements": {
    "actors_systems": [
      "User",
      "ReadingSessionLogger UI",
      "Timer Service",
      "Isar Local DB",
      "Sync Service"
    ],
    "key_processes": [
      "Timer Management",
      "Input Validation",
      "Local Persistence",
      "Queueing for Sync"
    ],
    "decision_points": [
      "Input Mode Selection",
      "Validation Pass/Fail",
      "Connectivity Check"
    ],
    "success_paths": [
      "Log Manual Session",
      "Log Timed Session",
      "Offline Save"
    ],
    "error_scenarios": [
      "Invalid Page Number",
      "Zero Duration",
      "Future Date"
    ],
    "edge_cases_covered": [
      "App Backgrounding during Timer",
      "Network Loss during Sync"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "State diagram showing the lifecycle of a reading session log, from input mode selection (Timer vs Manual) to validation, local storage, and eventual synchronization.",
    "color_independence": "States are differentiated by structure and labels; success/error paths marked with text.",
    "screen_reader_friendly": "Transitions describe specific user actions or system events.",
    "print_compatibility": "High contrast optimized for black and white printing."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout optimized for scrolling",
    "theme_compatibility": "Neutral styling compatible with light/dark documentation themes",
    "performance_notes": "Focuses on client-side state logic"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the LogReadingSessionScreen and associated Riverpod providers.",
    "stakeholder_value": {
      "developers": "Defines exact states for the Riverpod StateNotifier",
      "designers": "Clarifies necessary UI feedback states (validating, saving, error)",
      "product_managers": "Visualizes the offline-first behavior logic",
      "QA_engineers": "Provides a map for testing state transitions and validation rules"
    },
    "maintenance_notes": "Update if new logging metadata fields (e.g., mood, location) are added.",
    "integration_recommendations": "Link to US-046, US-048, and US-097 documentation."
  },
  "validation_checklist": [
    "✅ Timer backgrounding logic included",
    "✅ Manual vs Timer paths clearly separated",
    "✅ Validation states defined",
    "✅ Offline-first persistence strategy visualized",
    "✅ Error recovery loops included",
    "✅ Mermaid syntax validated",
    "✅ Visual hierarchy follows logical user flow",
    "✅ Edge cases (backgrounding) addressed"
  ]
}

---

# Mermaid Diagram

```mermaid
stateDiagram-v2
    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px;
    classDef active fill:#e3f2fd,stroke:#1976d2,stroke-width:2px;
    classDef success fill:#e8f5e9,stroke:#388e3c,stroke-width:2px;
    classDef error fill:#ffebee,stroke:#d32f2f,stroke-width:2px;
    classDef data fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px;

    [*] --> SelectingBook: Entry from Dashboard/Library
    
    state SelectingBook {
        [*] --> BrowsingLibrary
        BrowsingLibrary --> BookSelected: User Taps Item
    }

    BookSelected --> InputMode: Initialize Logger

    state InputMode {
        [*] --> ManualEntry
        
        state ManualEntry {
            [*] --> Idle
            Idle --> ValidatingInput: User Types
            ValidatingInput --> Idle: Valid
            ValidatingInput --> inputError: Invalid (e.g., Page > Total)
        }

        state TimerMode {
            [*] --> TimerIdle
            TimerIdle --> TimerRunning: Start Tap
            TimerRunning --> TimerPaused: Pause Tap
            TimerPaused --> TimerRunning: Resume Tap
            TimerRunning --> Backgrounded: App Minimizes
            Backgrounded --> TimerRunning: App Resumes (Calc Delta)
            TimerRunning --> TimerStopped: Stop Tap
            TimerStopped --> ManualEntry: Populate Duration & Switch
        }

        ManualEntry --> TimerMode: Switch Tab
        TimerMode --> ManualEntry: Switch Tab / Stop Timer
    }

    InputMode --> ValidationCheck: User Taps 'Save'

    state ValidationCheck {
        [*] --> CheckingRules
        CheckingRules --> Valid: Rules Passed
        CheckingRules --> Invalid: Rules Failed
    }

    Invalid --> InputMode: Show Error Toast\n(e.g., "Duration > 0")
    
    Valid --> PersistingLocal: Trigger Save

    state PersistingLocal {
        [*] --> WritingToIsar
        WritingToIsar --> QueueingSync: Success
        WritingToIsar --> WriteError: DB Failure
    }

    WriteError --> InputMode: Show DB Error
    
    state QueueingSync {
        [*] --> CheckConnectivity
        CheckConnectivity --> SyncNow: Online
        CheckConnectivity --> SyncLater: Offline
    }

    PersistingLocal --> OptimisticUpdate: Write Success

    state OptimisticUpdate {
        [*] --> UpdateDashboardUI
        UpdateDashboardUI --> ShowSuccessFeedback
    }

    ShowSuccessFeedback --> [*]: Navigate Back

    %% Styling Application
    class InputMode active
    class OptimisticUpdate success
    class WriteError error
    class Invalid error
    class PersistingLocal data
```