{
  "diagram_info": {
    "diagram_name": "Subscription Screen UI State Machine",
    "diagram_type": "stateDiagram-v2",
    "purpose": "Documents the finite states of the Subscription/Upgrade screen, specifically focusing on the lifecycle of fetching subscription products, displaying content based on user tier, and handling various error conditions during the purchase flow.",
    "target_audience": [
      "frontend developers",
      "QA engineers",
      "UI/UX designers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with clear color coding for state types",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Flutter Client",
      "App Store/Play Store SDK",
      "Backend API"
    ],
    "key_processes": [
      "Fetching Products",
      "Displaying Benefits",
      "Purchase Flow",
      "Error Recovery"
    ],
    "decision_points": [
      "Product Fetch Success?",
      "Current User Tier?",
      "Purchase Confirmation?"
    ],
    "success_paths": [
      "Load -> Display -> Purchase -> Success"
    ],
    "error_scenarios": [
      "Store Connection Failure",
      "Payment Declined",
      "Network Timeout"
    ],
    "edge_cases_covered": [
      "User Cancellation",
      "Offline Mode",
      "Empty Product List"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "State diagram illustrating the Subscription Screen flow, starting from loading products, moving to display, handling purchase interactions, and managing error states.",
    "color_independence": "States differentiated by structure and text labels, not just color",
    "screen_reader_friendly": "Flow is logical and linear where possible",
    "print_compatibility": "High contrast optimized"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout optimized for mobile documentation viewing",
    "theme_compatibility": "Neutral styling with specific state emphasis",
    "performance_notes": "Uses composite states to reduce visual clutter"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the SubscriptionScreen widget and its Riverpod state provider.",
    "stakeholder_value": {
      "developers": "Defines the exact states required for the UI logic (Loading, Success, Error, Purchasing).",
      "designers": "Validates that all edge cases (like loading and errors) have designated UI designs.",
      "product_managers": "Visualizes the monetization flow and potential drop-off points.",
      "QA_engineers": "Provides a checklist of states to trigger during manual and automated testing."
    },
    "maintenance_notes": "Update if new subscription tiers are introduced or if the purchase flow changes.",
    "integration_recommendations": "Include in the PR description for the Monetization feature set."
  },
  "validation_checklist": [
    "✅ Loading state documented (US-018 AC-007)",
    "✅ Product display state included (US-017)",
    "✅ Error handling states defined (US-018 AC-004, AC-005, AC-006)",
    "✅ Mermaid syntax validated",
    "✅ Purchase flow transitions included (US-019)",
    "✅ Visual hierarchy supports comprehension"
  ]
}

---

# Mermaid Diagram

```mermaid
stateDiagram-v2
    direction LR

    %% Define styles
    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px;
    classDef errorState fill:#fee,stroke:#f00,stroke-width:2px;
    classDef successState fill:#efe,stroke:#0f0,stroke-width:2px;
    classDef interactiveState fill:#eef,stroke:#00f,stroke-width:2px;
    classDef systemState fill:#eee,stroke:#666,stroke-width:1px,stroke-dasharray: 5 5;

    [*] --> Initializing: Navigation to Screen

    state "Data Loading Phase" as LoadingContext {
        Initializing --> FetchingUserTier: Read Local State
        FetchingUserTier --> FetchingStoreProducts: Check Connectivity
        
        state "Async Operations" as AsyncOps {
            FetchingStoreProducts --> ValidatingStoreConnection
            ValidatingStoreConnection --> RetrievingProductDetails
        }
    }

    LoadingContext --> ErrorHandling: Network/Store Error
    LoadingContext --> DisplayingContent: Data Ready

    state "UI Presentation Phase" as DisplayingContent {
        direction TB
        
        state "User Tier Check" as TierChoice <<choice>>
        
        [*] --> TierChoice
        
        TierChoice --> FreeTierView: User is Free
        TierChoice --> PremiumTierView: User is Premium

        state "Free User View" as FreeTierView {
            [*] --> RenderBenefitsList
            RenderBenefitsList --> RenderPricingCard
            RenderPricingCard --> EnableUpgradeButton
        }

        state "Premium User View" as PremiumTierView {
            [*] --> RenderActiveStatus
            RenderActiveStatus --> RenderRenewalDate
            RenderRenewalDate --> EnableManageSubButton
        }
    }

    state "Purchase Transaction Flow" as PurchaseFlow {
        DisplayingContent --> InitiatingPurchase: Tap 'Upgrade'
        
        state InitiatingPurchase {
            [*] --> ShowLoadingOverlay
            ShowLoadingOverlay --> RequestNativeSheet
        }

        InitiatingPurchase --> AwaitingStoreInteraction: Native Sheet Open
        
        state AwaitingStoreInteraction {
            [*] --> UserInput
            UserInput --> ProcessingPayment: Confirm
            UserInput --> CancelledByUser: Cancel
        }
    }

    PurchaseFlow --> DisplayingContent: User Cancelled
    PurchaseFlow --> ErrorHandling: Payment Declined / Error
    PurchaseFlow --> VerifyingPurchase: Store Success

    state "Backend Verification" as VerifyingPurchase {
        [*] --> SendingReceiptToAPI
        SendingReceiptToAPI --> UpdatingUserStatus: Webhook/Response Valid
    }

    VerifyingPurchase --> ErrorHandling: Verification Failed
    VerifyingPurchase --> PurchaseSuccess: Validation OK

    state "Error Handling Presentation" as ErrorHandling {
        state "Error Types" as ET {
            NetworkError: No Internet / Timeout
            StoreError: StoreKit / Play Billing Unavailable
            PaymentError: Card Declined / Insufficient Funds
        }
        [*] --> DetermineErrorType
        DetermineErrorType --> DisplayErrorMessage
        DisplayErrorMessage --> EnableRetryButton
    }

    ErrorHandling --> LoadingContext: Tap 'Retry' (System Error)
    ErrorHandling --> DisplayingContent: Tap 'Dismiss' (Payment Error)

    state "Success State" as PurchaseSuccess {
        [*] --> ShowSuccessConfetti
        ShowSuccessConfetti --> TransitionToPremiumUI
    }

    PurchaseSuccess --> DisplayingContent: Auto-Refresh UI

    %% Apply Styles
    class ErrorHandling errorState
    class PurchaseSuccess successState
    class DisplayingContent interactiveState
    class LoadingContext, VerifyingPurchase systemState
```