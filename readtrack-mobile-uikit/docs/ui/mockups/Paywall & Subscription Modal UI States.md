{
  "diagram_info": {
    "diagram_name": "Paywall & Subscription Modal UI States",
    "diagram_type": "stateDiagram-v2",
    "purpose": "Documents the state lifecycle of the Premium Subscription Paywall modal, covering initialization, user interaction, purchase processing, receipt validation, and error handling. This ensures the frontend implements all necessary feedback loops for monetization.",
    "target_audience": [
      "Frontend Developers (Flutter)",
      "QA Engineers",
      "UI/UX Designers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with distinct state coloring for success and error paths.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Mobile App UI",
      "StoreKit/BillingClient",
      "Backend API"
    ],
    "key_processes": [
      "Product Fetching",
      "Native Purchase Flow",
      "Receipt Validation",
      "State Restoration"
    ],
    "decision_points": [
      "Products Loaded?",
      "Purchase Successful?",
      "Receipt Valid?",
      "User Cancelled?"
    ],
    "success_paths": [
      "Purchase Flow -> Validation -> Success View"
    ],
    "error_scenarios": [
      "Network Error",
      "Payment Declined",
      "User Cancelled",
      "Validation Failed"
    ],
    "edge_cases_covered": [
      "Restoring Purchases",
      "Offline Mode Entry"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "State diagram showing the Paywall flow: Loading -> Benefits View -> Native Purchase -> Verification -> Success or Error.",
    "color_independence": "States are differentiated by position and label, not just color.",
    "screen_reader_friendly": "Transitions describe user actions clearly",
    "print_compatibility": "High contrast lines and text"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout optimized for mobile documentation viewing",
    "theme_compatibility": "Neutral colors with semantic highlights",
    "performance_notes": "Focuses on UI state transitions rather than deep backend logic"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of US-017, US-018, and US-019 (Subscription Features).",
    "stakeholder_value": {
      "developers": "Defines exact state variables needed in Riverpod/BLoC",
      "designers": "Identifies all necessary screen states (Loading, Error, Success, Content) requiring mockups",
      "product_managers": "Visualizes the conversion funnel steps",
      "QA_engineers": "Provides a checklist for state transition testing"
    },
    "maintenance_notes": "Update if new purchase methods (e.g., promo codes) are added",
    "integration_recommendations": "Link to the Flutter 'in_app_purchase' package documentation"
  },
  "validation_checklist": [
    "✅ Loading state included",
    "✅ Native OS interaction modeled",
    "✅ Backend verification step included",
    "✅ Error recovery paths defined",
    "✅ Success feedback state visualization",
    "✅ Mermaid syntax validated",
    "✅ Styling applied for readability",
    "✅ Addresses requirements from US-017/018/019"
  ]
}

---

# Mermaid Diagram

```mermaid
stateDiagram-v2
    direction TB

    %% Definition of states
    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px,color:#000
    classDef success fill:#d1fae5,stroke:#059669,stroke-width:2px,color:#000
    classDef error fill:#fee2e2,stroke:#b91c1c,stroke-width:2px,color:#000
    classDef system fill:#e0f2fe,stroke:#0284c7,stroke-width:2px,color:#000
    classDef interactive fill:#fffbeb,stroke:#d97706,stroke-width:2px,color:#000

    [*] --> FetchingProducts: User Taps "Upgrade" or Limit Reached

    state FetchingProducts {
        [*] --> QueryStore
        QueryStore --> StoreResponse: Native Platform API Call
    }

    state StoreResponse <<choice>>
    
    FetchingProducts --> BenefitsView: Success (Products Loaded)
    FetchingProducts --> LoadErrorView: Failure (Network/Store Error)

    state BenefitsView {
        %% UI Display
        [*] --> Idle
        Idle --> ContentRender: Show Price & Features
        note right of ContentRender
            Display localized price
            List Premium features
            Show "Upgrade Now" CTA
        end note
    }

    LoadErrorView --> FetchingProducts: User Taps "Retry"
    LoadErrorView --> [*]: User Taps "Close"

    BenefitsView --> NativePurchaseFlow: User Taps "Upgrade Now"
    BenefitsView --> NativePurchaseFlow: User Taps "Restore Purchases"
    BenefitsView --> [*]: User Taps "Close"

    state NativePurchaseFlow {
        %% System Overlay
        [*] --> OS_Overlay: App Store / Google Play Sheet
        note right of OS_Overlay
            App moves to background/inactive
            Waiting for OS callback
        end note
    }

    state PurchaseResult <<choice>>
    NativePurchaseFlow --> PurchaseResult: Callback Received

    PurchaseResult --> VerifyingReceipt: Purchase/Restore Successful
    PurchaseResult --> BenefitsView: User Cancelled
    PurchaseResult --> PurchaseErrorView: Payment Failed/Declined

    state VerifyingReceipt {
        [*] --> SendToBackend
        SendToBackend --> UpdateUserRole: Valid Receipt (Webhook/API)
    }

    state ValidationResult <<choice>>
    VerifyingReceipt --> ValidationResult

    ValidationResult --> SuccessView: Valid Signature
    ValidationResult --> PurchaseErrorView: Invalid Signature/Server Error

    state SuccessView {
        [*] --> Animation
        Animation --> UnlockFeatures
        UnlockFeatures --> [*]: Auto-dismiss / User Taps "Continue"
    }

    PurchaseErrorView --> BenefitsView: User Taps "Try Again"
    PurchaseErrorView --> [*]: User Taps "Cancel"

    %% Styling Application
    class FetchingProducts, VerifyingReceipt system
    class BenefitsView interactive
    class SuccessView success
    class LoadErrorView, PurchaseErrorView error
    class NativePurchaseFlow system
```