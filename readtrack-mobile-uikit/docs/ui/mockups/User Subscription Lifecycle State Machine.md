{
  "diagram_info": {
    "diagram_name": "User Subscription Lifecycle State Machine",
    "diagram_type": "stateDiagram",
    "purpose": "Documents the lifecycle states of a user account subscription, including transitions between Free and Premium tiers, cancellation grace periods, and payment failure handling as defined in US-019, US-020, and US-022.",
    "target_audience": [
      "backend developers",
      "mobile developers",
      "QA engineers",
      "product managers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid stateDiagram-v2 syntax verified",
  "rendering_notes": "Optimized for both light and dark themes with clear state grouping",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "App Store/Google Play",
      "Backend System",
      "Scheduled Job"
    ],
    "key_processes": [
      "Upgrade",
      "Cancellation",
      "Expiration",
      "Payment Failure"
    ],
    "decision_points": [
      "Payment Success?",
      "Period Ended?",
      "Resubscribed?"
    ],
    "success_paths": [
      "Free -> Premium -> Renewal",
      "Premium -> Cancelled -> Free"
    ],
    "error_scenarios": [
      "Payment Failure (Past Due)",
      "Involuntary Churn"
    ],
    "edge_cases_covered": [
      "Resubscribing during cancellation period",
      "Grace period expiration"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "State diagram showing user transitions between Free Tier, Premium Active, Premium Cancelled (Grace Period), and Payment Issue states.",
    "color_independence": "States differentiated by structure and labels",
    "screen_reader_friendly": "Transitions explicitly labeled with event triggers",
    "print_compatibility": "High contrast rendering"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout optimized for scrolling",
    "theme_compatibility": "Neutral colors with semantic status indicators",
    "performance_notes": "Standard rendering complexity"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of subscription webhook handlers and client-side entitlement checks.",
    "stakeholder_value": {
      "developers": "Defines exact database states and trigger events for webhooks",
      "designers": "Clarifies UI states needed (e.g., 'Resubscribe' button vs 'Upgrade' button)",
      "product_managers": "Visualizes the churn flow and retention windows",
      "QA_engineers": "Provides test cases for state transitions (e.g., verifying access during 'Cancelled' state)"
    },
    "maintenance_notes": "Update if new subscription tiers or pause functionality is introduced.",
    "integration_recommendations": "Link to US-020 (Revert to Free) and US-022 (Retain Access)"
  },
  "validation_checklist": [
    "✅ Free Tier limitations noted",
    "✅ Premium Active state included",
    "✅ Cancelled but access retained state (US-022) included",
    "✅ Expiration logic (US-020) included",
    "✅ Payment failure/grace period included",
    "✅ Resubscribe path included",
    "✅ Webhook triggers identified"
  ]
}

---

# Mermaid Diagram

```mermaid
stateDiagram-v2
    direction TB

    %% Initial State
    [*] --> FreeUser : Account Creation

    %% Free Tier State
    state FreeUser {
        [*] --> FreeActive
        FreeActive --> FreeActive : Usage (Ads Shown)
        note right of FreeActive
            LIMITATIONS:
            - Max 20 Books
            - Max 1 Active Goal
            - Max 5 AI Suggestions
            - Ads Enabled
        end note
    }

    %% Transition to Premium
    FreeUser --> PremiumUser : Upgrade Purchase (IAP) \n[Webhook: SUBSCRIBED]

    %% Premium Tier Composite State
    state PremiumUser {
        [*] --> Active

        state Active {
            [*] --> AutoRenewOn
            note right of AutoRenewOn
                BENEFITS:
                - Unlimited Books/Goals
                - No Ads
                - Adv. Stats & AI
            end note
        }

        state CancelledButActive {
            [*] --> AutoRenewOff
            note right of AutoRenewOff
                Status: Pending Expiry
                Access: FULL PREMIUM
                (Until CurrentPeriodEnd)
            end note
        }

        state PaymentIssue {
            [*] --> InGracePeriod
            note right of InGracePeriod
                Status: Past Due
                Access: FULL PREMIUM
                (Platform Retry Logic)
            end note
        }

        %% Internal Premium Transitions
        Active --> CancelledButActive : User Cancels \n[Webhook: DID_CHANGE_RENEWAL_STATUS]
        CancelledButActive --> Active : User Resubscribes \n[Webhook: DID_RECOVER/RENEW]
        
        Active --> PaymentIssue : Payment Failed \n[Webhook: DID_FAIL_TO_RENEW]
        PaymentIssue --> Active : Payment Recovered \n[Webhook: DID_RECOVER]
    }

    %% Downgrade Transitions
    CancelledButActive --> FreeUser : Subscription Expired \n[Webhook: EXPIRED / Daily Job]
    PaymentIssue --> FreeUser : Grace Period Ended \n[Webhook: EXPIRED]

    %% Styling
    classDef free fill:#f1f5f9,stroke:#64748b,stroke-width:2px,color:#0f172a
    classDef premium fill:#fff7ed,stroke:#f97316,stroke-width:2px,color:#7c2d12
    classDef error fill:#fef2f2,stroke:#ef4444,stroke-width:2px,color:#7f1d1d
    classDef warning fill:#fffbeb,stroke:#f59e0b,stroke-width:2px,color:#78350f

    class FreeUser free
    class PremiumUser, Active, CancelledButActive premium
    class PaymentIssue error
    class CancelledButActive warning
```