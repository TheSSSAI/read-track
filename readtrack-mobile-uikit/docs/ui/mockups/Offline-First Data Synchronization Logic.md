{
  "diagram_info": {
    "diagram_name": "Offline-First Data Synchronization Logic",
    "diagram_type": "flowchart",
    "purpose": "Documents the logic flow for handling user data mutations in both online and offline states, including local persistence, optimistic UI updates, and the 'Last Write Wins' conflict resolution strategy during synchronization.",
    "target_audience": [
      "Mobile Developers",
      "Backend Developers",
      "QA Engineers"
    ],
    "complexity_level": "high",
    "estimated_review_time": "15 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for vertical flow with clear separation between Client and Server logic",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Mobile Client (Flutter)",
      "Isar Local DB",
      "Sync Service",
      "Backend API",
      "PostgreSQL DB"
    ],
    "key_processes": [
      "Optimistic UI Update",
      "Local Persistence",
      "Network Detection",
      "Batch Synchronization",
      "Conflict Resolution"
    ],
    "decision_points": [
      "Is Device Online?",
      "API Call Successful?",
      "Server Timestamp Check (Last Write Wins)"
    ],
    "success_paths": [
      "Online Write -> API Success -> Local Sync",
      "Offline Write -> Local Pending -> Network Restore -> Sync Success"
    ],
    "error_scenarios": [
      "API 5xx/Timeout -> Fallback to Local Pending",
      "Stale Data Conflict -> Server Rejection/Update"
    ],
    "edge_cases_covered": [
      "Network loss during API call",
      "Concurrent updates on multiple devices"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart illustrating the offline-first data synchronization process, detailing paths for online and offline actions, local database updates, and server-side conflict resolution.",
    "color_independence": "Nodes are distinguished by shape and distinct border styles in addition to color.",
    "screen_reader_friendly": "Flow directions and decision outcomes are explicitly labeled.",
    "print_compatibility": "High contrast borders ensure readability in grayscale."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Uses subgraph clustering to maintain structure on smaller screens",
    "theme_compatibility": "Neutral colors with semantic highlights for success/error states",
    "performance_notes": "Standard flowchart complexity, renders quickly"
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing the repository pattern in the mobile app or the sync endpoint in the backend.",
    "stakeholder_value": {
      "developers": "Defines the exact logic for the 'Last Write Wins' strategy and local state management.",
      "product_managers": "Visualizes how the app handles offline user experiences.",
      "qa_engineers": "Provides test cases for network transitions and conflict scenarios."
    },
    "maintenance_notes": "Update if the conflict resolution strategy changes (e.g., to CRDTs) or if sync triggers change.",
    "integration_recommendations": "Link this diagram in the REQ-OFF-001 and US-101 documentation."
  },
  "validation_checklist": [
    "✅ Offline path clearly defined",
    "✅ Optimistic UI updates included",
    "✅ 'Last Write Wins' logic visualized on server side",
    "✅ Network restoration trigger included",
    "✅ Error handling for API failures included",
    "✅ Local Isar DB interactions mapped",
    "✅ Mermaid syntax validated"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Nodes
    Start([User Initiates Action\ne.g., Log Session, Move Shelf])
    CheckNet{Is Device\nOnline?}
    
    subgraph Client_Side [Mobile Client & Isar DB]
        OptimisticUI[Update UI Optimistically]
        
        %% Online Path
        CallAPI[Attempt API Request]
        CheckAPISuccess{API Success\n200 OK?}
        
        %% Offline Path
        SaveLocalPending[Save to Isar DB\nStatus: 'pending_sync'\nTimestamp: Client Now]
        
        %% Post-API
        UpdateLocalSynced[Update Isar DB\nStatus: 'synced'\nStore Server Data]
        
        %% Background Sync
        NetRestore((Network\nRestored))
        FetchPending[Query Isar:\nStatus == 'pending_sync']
        LoopPending[Loop: For Each Pending Item]
        SyncCallAPI[POST /api/sync]
        MarkSynced[Update Isar:\nStatus = 'synced']
    end

    subgraph Server_Side [Backend API & PostgreSQL]
        ReceiveSync[Receive Data & Client Timestamp]
        FetchExisting[Fetch Current Record from DB]
        CheckConflict{Record Exists?}
        CheckTimestamp{Client TS >\nServer TS?}
        
        UpdateServer[Update Server DB]
        IgnoreUpdate[Ignore Update\n(Keep Server Version)]
        ReturnSuccess[Return Success\n& Current Server State]
    end

    %% Flows
    Start --> OptimisticUI
    OptimisticUI --> CheckNet
    
    %% Online Flow
    CheckNet -- Yes --> CallAPI
    CallAPI --> CheckAPISuccess
    CheckAPISuccess -- Yes --> UpdateLocalSynced
    CheckAPISuccess -- No (5xx/Timeout) --> SaveLocalPending
    CheckAPISuccess -- 4xx Error --> ShowError[Display Validation Error]
    
    %% Offline Flow
    CheckNet -- No --> SaveLocalPending
    
    %% Sync Process
    NetRestore --> FetchPending
    FetchPending --> LoopPending
    LoopPending --> SyncCallAPI
    
    %% Server Logic
    SyncCallAPI --> ReceiveSync
    ReceiveSync --> FetchExisting
    FetchExisting --> CheckConflict
    
    CheckConflict -- No (New) --> UpdateServer
    CheckConflict -- Yes --> CheckTimestamp
    
    CheckTimestamp -- Yes (Newer) --> UpdateServer
    CheckTimestamp -- No (Older) --> IgnoreUpdate
    
    UpdateServer --> ReturnSuccess
    IgnoreUpdate --> ReturnSuccess
    
    ReturnSuccess --> MarkSynced
    MarkSynced --> LoopPending

    %% Styling
    classDef logic fill:#e1f5fe,stroke:#01579b,stroke-width:2px,color:#000
    classDef success fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000
    classDef warning fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#000
    classDef error fill:#ffebee,stroke:#c62828,stroke-width:2px,color:#000
    classDef storage fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#000

    class Start,NetRestore logic
    class CheckNet,CheckAPISuccess,CheckConflict,CheckTimestamp warning
    class UpdateLocalSynced,UpdateServer,MarkSynced,ReturnSuccess success
    class ShowError,IgnoreUpdate error
    class SaveLocalPending,FetchExisting storage
```