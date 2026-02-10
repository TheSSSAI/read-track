# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-104 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Content Manager: Create and publish reading tip ar... |
| As A User Story | As a Content Manager, I want to create, manage, an... |
| User Persona | Content Manager (Internal role, not an app end-use... |
| Business Value | Enables the population of the 'Tips' feature with ... |
| Functional Area | Content Management |
| Story Theme | Reading Tips and Resources |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Content model for 'Reading Tip' is correctly defined in the Headless CMS

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A developer has administrative access to the project's Contentful space

### 3.1.5 When

The developer navigates to the 'Content model' section

### 3.1.6 Then

A content model named 'Reading Tip' exists and contains the following fields: 'title' (Short text, required), 'body' (Rich Text, required), 'category' (Short text, required, with validation for a predefined list of values), and 'coverImage' (Media, optional).

### 3.1.7 Validation Notes

Verify manually within the Contentful web UI. The predefined category list should be documented and agreed upon (e.g., 'Productivity', 'Focus', 'Book Discovery').

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Content Manager successfully creates and publishes a new reading tip

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The 'Reading Tip' content model is defined in Contentful

### 3.2.5 And

The content of the newly published tip is immediately available via the Contentful Delivery API.

### 3.2.6 When

The Content Manager creates a new 'Reading Tip' entry, fills all required fields, and clicks 'Publish'

### 3.2.7 Then

The entry's status is updated to 'Published' in the Contentful UI.

### 3.2.8 Validation Notes

Create a test tip in the staging environment. After publishing, use a tool like Postman with the Delivery API key to make a GET request for the new entry's ID and verify the JSON response is correct.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Content Manager is prevented from publishing a tip with missing required fields

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A Content Manager is creating a new 'Reading Tip' entry in the Contentful UI

### 3.3.5 When

The Content Manager leaves a required field, such as 'title', blank and attempts to publish the entry

### 3.3.6 Then

The Contentful UI displays a validation error message indicating which required field is missing.

### 3.3.7 And

The entry remains in a draft or changed state and is not published.

### 3.3.8 Validation Notes

Manually attempt to publish an incomplete entry in the Contentful staging environment to confirm the validation rule is enforced.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

API credentials for Contentful are securely stored and documented

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

The Contentful space has been provisioned for all environments (Testing, Staging, Production)

### 3.4.5 When

The API keys (Delivery API, Preview API) are generated

### 3.4.6 Then

The keys are stored as secrets in AWS Secrets Manager, following the project's naming conventions for each environment.

### 3.4.7 And

The Space ID, Environment ID, and the secret names are documented in the project's configuration guide for the backend team.

### 3.4.8 Validation Notes

Verify the existence of the secrets in AWS Secrets Manager for the staging environment. Review project documentation for completeness.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- This story's UI is the Contentful Web Application.
- Content Model Fields: Title (Text Input), Body (Rich Text Editor), Category (Dropdown or Tag Input), Cover Image (File Uploader).

## 4.2.0 User Interactions

- Creating a new content entry from a model.
- Editing text and rich text fields.
- Uploading and selecting media assets.
- Saving drafts and publishing content.

## 4.3.0 Display Requirements

- Clear validation messages for required fields within the Contentful UI.

## 4.4.0 Accessibility Needs

- N/A - Accessibility is managed by the third-party Headless CMS provider (Contentful).

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "A 'Reading Tip' must have a title, body, and category to be published.", 'enforcement_point': 'Contentful CMS, at the time of attempting to publish an entry.', 'violation_handling': 'The CMS will prevent the publish action and display a user-friendly validation error.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

*No items available*

## 6.2.0 Technical Dependencies

- A provisioned Contentful account and space.
- Provisioned AWS Secrets Manager service for storing API keys.

## 6.3.0 Data Dependencies

- An agreed-upon list of categories for reading tips.

## 6.4.0 External Dependencies

- This story is a prerequisite/blocker for any story related to fetching or displaying tips in the mobile app, specifically US-079 ('User browses the library of reading tips') and US-082 ('User reads a tip article').

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- While this story has no direct performance metrics, the chosen CMS (Contentful) must have a reliable API with low latency to support the performance requirements of the consuming backend service (as defined in REQ-PER-001).

## 7.2.0 Security

- Contentful API keys must be treated as sensitive secrets and stored exclusively in AWS Secrets Manager, never in code repositories or client-side code (NFR-SEC-005).
- The Content Manager role in Contentful should be configured with the principle of least privilege, granting only the necessary permissions to manage content.

## 7.3.0 Usability

- The content model in Contentful should be intuitive for a non-technical Content Manager to use.

## 7.4.0 Accessibility

- N/A

## 7.5.0 Compatibility

- N/A

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Low

## 8.2.0 Complexity Factors

- No custom code development is required for this story.
- Work consists of configuration within a third-party SaaS platform.
- Effort is primarily in setup, documentation, and coordination with the backend team.

## 8.3.0 Technical Risks

- Misconfiguration of the content model could require a migration or breaking changes for the consuming API later.
- Improper handling of API keys could lead to security vulnerabilities.

## 8.4.0 Integration Points

- The primary integration point is the Contentful Delivery API, which will be consumed by the application's backend service responsible for the 'Tips' feature.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Manual Validation

## 9.2.0 Test Scenarios

- Verify the 'Reading Tip' content model structure in Contentful.
- Create and publish a sample tip, ensuring it appears as expected via a direct API call.
- Attempt to publish a tip with a missing required field to verify validation.
- Confirm that the Contentful API keys are correctly stored in AWS Secrets Manager for the staging environment.

## 9.3.0 Test Data Needs

- At least one sample reading tip article with placeholder text and an optional image.

## 9.4.0 Testing Tools

- Contentful Web App
- Postman or cURL (for API validation)
- AWS Management Console (for verifying secrets)

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- The 'Reading Tip' content model is finalized and created in the Contentful staging environment.
- Contentful API keys and relevant IDs for the staging environment are securely stored in AWS Secrets Manager.
- A sample 'Reading Tip' has been successfully published in the staging environment.
- A direct API call to the Contentful Delivery API successfully retrieves the sample tip's data.
- All necessary identifiers (Space ID, Environment ID, API Key Secret Name) are documented and made available to the backend development team.
- Story deployed and verified in staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

1

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This story is a blocker for the entire 'Tips' feature. It must be completed before or in parallel with the backend story that implements the API endpoint to serve tips to the mobile client.

## 11.4.0 Release Impact

- Enables a key user-facing feature outlined in the project scope. Without this, the 'Tips' section of the app will be empty.

