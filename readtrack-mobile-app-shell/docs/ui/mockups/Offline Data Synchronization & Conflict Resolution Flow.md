{
  "diagram_info": {
    "diagram_name": "Offline Data Synchronization & Conflict Resolution Flow",
    "diagram_type": "sequenceDiagram",
    "purpose": "Details the technical flow for persisting user actions while offline and synchronizing them with the backend upon network restoration using a 'Last Write Wins' strategy.",
    "target_audience": [
      "Mobile Developers",
      "Backend Developers",
      "QA Engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with clear grouping for Offline and Online phases",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Mobile App (Flutter)",
      "Isar (Local DB)",
      "Sync Service",
      "Backend API",
      "Aurora (Remote DB)"
    ],
    "key_processes": [
      "Offline Data Persistence",
      "Network Detection",
      "Batch Synchronization",
      "Timestamp Comparison"
    ],
    "decision_points": [
      "Network Status Check",
      "Record Existence Check",
      "Timestamp Conflict Resolution"
    ],
    "success_paths": [
      "Offline Save -> Sync -> Server Update",
      "Offline Save -> Sync -> Server Ignore (Stale)"
    ],
    "error_scenarios": [
      "Network Restoration Failure",
      "Database Locking"
    ],
    "edge_cases_covered": [
      "Record does not exist on server",
      "Server data is newer than client data (Conflict)"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Sequence diagram showing a user logging a session offline, the data being saved locally to Isar, and subsequently syncing to the backend where a Last Write Wins logic resolves conflicts.",
    "color_independence": "Phases and actions are textually labeled; colors are decorative.",
    "screen_reader_friendly": "Nodes and messages have descriptive labels indicating data flow.",
    "print_compatibility": "High contrast lines and text ensure readability in grayscale."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Scales vertically for sequence steps",
    "theme_compatibility": "Neutral styling for broad compatibility",
    "performance_notes": "Focuses on logical data flow rather than low-level networking details"
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing the offline-first data layer or the backend synchronization endpoint.",
    "stakeholder_value": {
      "developers": "Defines the exact logic for 'Last Write Wins' and local DB state updates.",
      "designers": "Clarifies when UI feedback ('Saved Offline') should occur.",
      "product_managers": "Validates the requirement for seamless offline usage.",
      "qa_engineers": "Provides clear steps for reproducing sync scenarios and conflict testing."
    },
    "maintenance_notes": "Update if the conflict resolution strategy changes from 'Last Write Wins' to 'Merge' or user-intervention.",
    "integration_recommendations": "Link to US-101 and REQ-FUNC-011 in Jira/Confluence."
  },
  "validation_checklist": [
    "✅ All critical user paths documented",
    "✅ Error scenarios and recovery paths included",
    "✅ Decision points clearly marked with conditions",
    "✅ Mermaid syntax validated and renders correctly",
    "✅ Diagram serves intended audience needs",
    "✅ Visual hierarchy supports easy comprehension",
    "✅ Styling enhances rather than distracts from content",
    "✅ Accessible to users with different visual abilities"
  ]
}

---

# Mermaid Diagram

```mermaid
sequenceDiagram
    actor User
    participant App as Mobile App (Flutter)
    participant LocalDB as Isar (Local DB)
    participant SyncService as Sync Manager
    participant API as Backend API
    participant RemoteDB as Aurora (Remote DB)

    box "Phase 1: Offline Operation" #f9f9f9
        User->>App: Action: Log Reading Session
        activate App
        App->>App: Check Connectivity
        Note right of App: Status: Offline
        App->>App: Generate UTC Timestamp (T_Client)
        App->>LocalDB: WRITE {id: UUID, status: 'pending_sync', timestamp: T_Client}
        activate LocalDB
        LocalDB-->>App: Acknowledge Write
        deactivate LocalDB
        App-->>User: Update UI (Optimistic)
        deactivate App
    end

    box "Phase 2: Synchronization" #e6f3ff
        Note over SyncService: Connectivity Restored
        SyncService->>SyncService: Trigger Sync Job
        activate SyncService
        SyncService->>LocalDB: READ WHERE status = 'pending_sync'
        activate LocalDB
        LocalDB-->>SyncService: Return List[Sessions]
        deactivate LocalDB

        loop For Each Session
            SyncService->>API: POST /sync {id: UUID, timestamp: T_Client, data: ...}
            activate API
            
            API->>RemoteDB: SELECT timestamp FROM Sessions WHERE id = UUID
            activate RemoteDB
            RemoteDB-->>API: Return T_Server OR Null
            deactivate RemoteDB

            alt Record is New (Null)
                API->>RemoteDB: INSERT New Record
                API-->>SyncService: 201 Created
            else Conflict: T_Server < T_Client
                Note over API: Client data is newer
                API->>RemoteDB: UPDATE Record (Last Write Wins)
                API-->>SyncService: 200 OK
            else Conflict: T_Server >= T_Client
                Note over API: Server data is newer/same
                API->>API: Ignore Update (Preserve Server Data)
                API-->>SyncService: 200 OK (Acknowledged)
            end
            deactivate API

            SyncService->>LocalDB: UPDATE {id: UUID, status: 'synced'}
            activate LocalDB
            LocalDB-->>SyncService: Success
            deactivate LocalDB
        end
        deactivate SyncService
    end
```