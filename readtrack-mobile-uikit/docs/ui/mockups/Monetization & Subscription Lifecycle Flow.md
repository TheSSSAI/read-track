{
  "diagram_info": {
    "diagram_name": "Monetization & Subscription Lifecycle Flow",
    "diagram_type": "stateDiagram-v2",
    "purpose": "Documents the complete user journey through subscription states, visual enforcement of limits, purchasing, cancellation, and expiration logic.",
    "target_audience": [
      "Product Managers",
      "Frontend Developers",
      "QA Engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "diagram_elements": {
    "actors_systems": [
      "Free User",
      "Premium User",
      "StoreKit/Billing",
      "Backend"
    ],
    "key_processes": [
      "Limit Enforcement",
      "Upgrading",
      "Cancellation",
      "Expiration"
    ],
    "decision_points": [
      "Check Limit",
      "Verify Purchase",
      "Check Subscription Status"
    ],
    "success_paths": [
      "Free -> Purchase -> Premium",
      "Premium -> Cancel -> Expire -> Free"
    ],
    "error_scenarios": [
      "Purchase Failed",
      "Restore Failed"
    ],
    "edge_cases_covered": [
      "Cancelled but still active",
      "Limit reached trigger"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "State diagram showing transition from Free to Premium user via purchase, handling of limits via paywalls, and the lifecycle of cancellation and expiration.",
    "color_independence": "States differentiated by structure and labels",
    "screen_reader_friendly": "Logical flow from state to state",
    "print_compatibility": "High contrast optimized"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout for mobile readability",
    "theme_compatibility": "Adaptive colors",
    "performance_notes": "Standard rendering"
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing the PaywallModal, SubscriptionScreen, or backend webhook handlers.",
    "stakeholder_value": {
      "developers": "Defines exact triggers for UI state changes",
      "designers": "Maps out required screens (Sales vs Management) and Modal variants",
      "qa_engineers": "Provides a checklist of state transitions to test (Upgrade, Cancel, Expire)"
    },
    "maintenance_notes": "Update if new tiers or limits are introduced.",
    "integration_recommendations": "Link to US-018 and US-022"
  },
  "validation_checklist": [
    "✅ Free Tier limits visually defined",
    "✅ Purchase flow transitions included",
    "✅ Cancellation 'grace period' state modeled",
    "✅ Reversion to Free state documented",
    "✅ UI components mapped to states"
  ]
}

---

# Mermaid Diagram

```mermaid
stateDiagram-v2
    classDef free fill:#e3f2fd,stroke:#1e88e5,color:#0d47a1
    classDef premium fill:#fff8e1,stroke:#ffb300,color:#bf360c
    classDef ui fill:#f3e5f5,stroke:#8e24aa,color:#4a148c
    classDef logic fill:#e0f2f1,stroke:#00897b,color:#004d40

    [*] --> FreeUser_State

    state "Free User Tier" as FreeUser_State {
        direction LR
        [*] --> Browsing
        
        state "Visual State" as VisualFree {
            note right of VisualFree
                - Banner Ads: Visible
                - Premium Features: Locked Icon
                - Settings: 'Upgrade to Premium' Button
            end note
        }

        state "Limit Enforcement Logic" as Limits {
            direction TB
            state "Action: Add 21st Book" as LimitBook
            state "Action: Add 2nd Goal" as LimitGoal
            state "Action: 6th AI Request" as LimitAI
            
            LimitBook --> TriggerPaywall
            LimitGoal --> TriggerPaywall
            LimitAI --> TriggerPaywall
        }
    }

    state "Paywall / Upsell UI" as PaywallUI {
        state "PaywallModal (Variant: Limit Reached)" as Modal
        state "SubscriptionScreen (Variant: Sales Page)" as SalesPage
        
        TriggerPaywall --> Modal: Show Limit Message
        Modal --> SalesPage: User Taps 'Upgrade'
        FreeUser_State --> SalesPage: User Taps 'Upgrade' in Settings
    }

    state "Purchase Transaction" as Transaction {
        SalesPage --> StoreKit: Initiate Purchase
        StoreKit --> Backend: Validate Receipt/Webhook
    }

    Transaction --> PremiumUser_State: Success
    Transaction --> PaywallUI: Failure/Cancel (Return to Sales)

    state "Premium User Tier" as PremiumUser_State {
        direction TB
        
        state "Active Subscription" as Active {
            note right of Active
                - Ads: Hidden
                - Features: Unlocked
                - Settings: 'Manage Subscription' Button
            end note
        }

        state "Cancelled (Grace Period)" as Cancelled {
            note right of Cancelled
                - Status: Active until Period End
                - Settings: 'Resubscribe' Button & End Date displayed
                - Access: Full Premium Access
            end note
        }

        Active --> Cancelled: User cancels in App Store
        Cancelled --> Active: User Resubscribes
    }

    state "Subscription Lifecycle" as Lifecycle {
        Cancelled --> ExpirationEvent: Period End Date Reached
        ExpirationEvent --> FreeUser_State: Downgrade Account Role
    }

    class FreeUser_State free
    class PremiumUser_State premium
    class PaywallUI ui
    class Transaction logic
    class Lifecycle logic
```