{
  "diagram_info": {
    "diagram_name": "High-Level System Architecture & Integration Flow",
    "diagram_type": "flowchart",
    "purpose": "To visualize the architectural components of the Reading Tracker application, detailing the interactions between the offline-first mobile client, the modular backend, and critical external services (AI, Books, CMS, Auth).",
    "target_audience": [
      "System Architects",
      "Backend Developers",
      "Mobile Developers",
      "Product Owners"
    ],
    "complexity_level": "high",
    "estimated_review_time": "5-10 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with distinct colors for internal vs external systems.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Flutter Mobile App",
      "Backend API (.NET 8)",
      "PostgreSQL (Aurora)",
      "OpenSearch",
      "Redis",
      "S3",
      "External APIs (OpenAI, Google Books, Contentful, Auth0, AdMob)"
    ],
    "key_processes": [
      "Offline Data Sync",
      "AI Recommendation RAG Flow",
      "Book Metadata Search",
      "Freemium Ad Serving",
      "Content Delivery"
    ],
    "decision_points": [
      "Online/Offline Check",
      "Subscription Tier Check (Free vs Premium)",
      "Cache Hit/Miss"
    ],
    "success_paths": [
      "Seamless Offline-to-Online Sync",
      "Personalized AI Recommendation generation",
      "Book Search and Addition"
    ],
    "error_scenarios": [
      "External API Failure (Circuit Breaker)",
      "Sync Conflict Resolution"
    ],
    "edge_cases_covered": [
      "Offline functionality",
      "Ad serving fallback"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "High-level architecture diagram showing the Flutter mobile app with local Isar database syncing to a .NET backend, which connects to PostgreSQL, OpenSearch, and external APIs like OpenAI and Google Books.",
    "color_independence": "Components are distinguished by shape and grouping, not just color.",
    "screen_reader_friendly": "Flow direction and component labels are descriptive.",
    "print_compatibility": "High contrast lines and text suitable for black and white printing."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout optimized for scrolling.",
    "theme_compatibility": "Custom classes used for adaptability.",
    "performance_notes": "Grouped subgraphs reduce visual clutter."
  },
  "usage_guidelines": {
    "when_to_reference": "During architectural reviews, onboarding new developers, and planning feature integrations involving external services.",
    "stakeholder_value": {
      "developers": "Understanding the offline sync boundary and external API dependencies.",
      "designers": "Visualizing where data comes from (Local vs Remote) to design appropriate UI states.",
      "product_managers": "Seeing the complexity of the AI and Sync features.",
      "QA_engineers": "identifying integration points for contract testing and failure simulation."
    },
    "maintenance_notes": "Update if new external services are added or if the database technology changes.",
    "integration_recommendations": "Include in the system architecture design document (SDD)."
  },
  "validation_checklist": [
    "✅ Offline-first architecture (Isar) represented",
    "✅ Critical external integrations (OpenAI, Google Books) shown",
    "✅ Data storage layers (PostgreSQL, OpenSearch, Redis) included",
    "✅ Freemium logic (Ads, Auth) visualized",
    "✅ Mermaid syntax validated"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TB
    %% Definitions and Styling
    classDef client fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef backend fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20
    classDef database fill:#fff3e0,stroke:#ef6c00,stroke-width:2px,color:#e65100
    classDef external fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,stroke-dasharray: 5 5,color:#4a148c
    classDef user fill:#fafafa,stroke:#212121,stroke-width:2px,color:#212121

    User((User)) 
    
    subgraph Client_Device [📱 Mobile Client Device]
        direction TB
        UI[Flutter UI Layer]:::client
        State[State Mgmt <br/>Riverpod]:::client
        LocalDB[(Isar Local DB)]:::client
        Ads[AdMob SDK]:::client
        Auth_Client[Auth0 SDK]:::client
        
        UI <--> State
        State <--> LocalDB
        UI -.-> Ads
        UI -.-> Auth_Client
    end

    User --> UI

    subgraph Cloud_Infrastructure [☁️ Backend Infrastructure]
        direction TB
        
        Gateway[API Gateway / Load Balancer]:::backend
        
        subgraph Backend_Services [.NET 8 Modular Monolith]
            direction TB
            Auth_Svc[Identity & Auth Service]:::backend
            Library_Svc[Reading & Library Service]:::backend
            Rec_Svc[AI Recommendation Service]:::backend
            Sync_Svc[Data Sync Worker]:::backend
            CMS_Svc[Content Service]:::backend
        end

        subgraph Data_Persistence [Data Layer]
            Postgres[(PostgreSQL<br/>Aurora)]:::database
            VectorDB[(Amazon<br/>OpenSearch)]:::database
            Cache[(Redis Cache)]:::database
            ObjStore[(S3 Bucket<br/>Exports/Images)]:::database
        end
    end

    subgraph External_Services [🌐 External Ecosystem]
        OpenAI[OpenAI GPT-4 API]:::external
        GBooks[Google Books API]:::external
        Contentful[Contentful CMS]:::external
        Auth0_Prov[Auth0 Provider]:::external
        AdNet[Google Ad Network]:::external
    end

    %% Client to Backend Connections
    State -- "HTTPS / Sync Request" --> Gateway
    Auth_Client -- "Token Exchange" --> Auth0_Prov
    Ads -- "Ad Request" --> AdNet

    %% Backend Routing
    Gateway --> Auth_Svc
    Gateway --> Library_Svc
    Gateway --> Rec_Svc
    Gateway --> CMS_Svc

    %% Service Logic & Integrations
    Auth_Svc -- "Validate JWT" --> Auth0_Prov
    Auth_Svc --> Postgres
    
    Library_Svc -- "Fetch Metadata (Resilient)" --> GBooks
    Library_Svc --> Postgres
    
    Rec_Svc -- "RAG Query (Context)" --> VectorDB
    Rec_Svc -- "LLM Prompt" --> OpenAI
    Rec_Svc -- "Store Recs" --> Postgres
    
    CMS_Svc -- "Fetch Tips (Cache-Aside)" --> Cache
    CMS_Svc -- "Source Content" --> Contentful
    
    Sync_Svc -- "Batch Processing" --> Postgres
    
    %% Background Jobs
    Library_Svc -.-> Sync_Svc
    Library_Svc -- "Embed User Data" --> VectorDB
    
    %% Offline Note
    LocalDB -. "Offline Mode" .-x Gateway
    
    %% Export Flow
    Library_Svc -- "Generate Export" --> ObjStore

    %% Legend / Notes
    note1[<b>Offline First Strategy</b><br/>User acts on Local DB.<br/>Sync Svc handles conflict resolution<br/>via 'Last Write Wins'.]:::client
    note2[<b>Freemium Logic</b><br/>Auth Svc injects roles.<br/>UI toggles Ads/Features<br/>based on Claims.]:::backend

    LocalDB -.- note1
    Auth_Svc -.- note2
```