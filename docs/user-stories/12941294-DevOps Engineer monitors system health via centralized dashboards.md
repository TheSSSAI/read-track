# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-107 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | DevOps Engineer monitors system health via central... |
| As A User Story | As a DevOps Engineer, I want a set of comprehensiv... |
| User Persona | DevOps Engineer responsible for system reliability... |
| Business Value | Improves system reliability and uptime by enabling... |
| Functional Area | System Operations & Monitoring |
| Story Theme | Observability and Operational Readiness |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

API Health Dashboard provides key performance metrics

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authorized DevOps Engineer logged into the AWS console

### 3.1.5 When

I navigate to the 'Production - API Health' CloudWatch Dashboard

### 3.1.6 Then

I can see widgets displaying P95 latency, 5xx error rate percentage, 4xx error rate percentage, and total request count for the API Gateway over selectable time periods (e.g., 1h, 6h, 24h).

### 3.1.7 Validation Notes

Verify by checking the CloudWatch dashboard in the production account. Ensure metrics align with API Gateway's own monitoring tab.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Database Health Dashboard shows core Aurora metrics

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am an authorized DevOps Engineer

### 3.2.5 When

I view the 'Production - Database Health' dashboard

### 3.2.6 Then

I can see widgets for Aurora Serverless v2 CPU Utilization, Database Connections, Read/Write IOPS, and Replica Lag.

### 3.2.7 Validation Notes

Verify against the metrics available in the RDS console for the production Aurora cluster.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Compute Health Dashboard displays Fargate and Lambda status

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am an authorized DevOps Engineer

### 3.3.5 When

I view the 'Production - Compute Health' dashboard

### 3.3.6 Then

I can see widgets for each Fargate service's CPU and Memory utilization, running task count, and key Lambda functions' invocation counts, error rates, and average duration.

### 3.3.7 Validation Notes

Verify against metrics in the ECS and Lambda consoles for production services.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Asynchronous System Health Dashboard monitors queue state

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am an authorized DevOps Engineer

### 3.4.5 When

I view the 'Production - Async Health' dashboard

### 3.4.6 Then

I can see widgets for primary SQS queues showing 'ApproximateNumberOfMessagesVisible' and 'ApproximateAgeOfOldestMessage'.

### 3.4.7 Validation Notes

Verify against metrics in the SQS console. Test by sending messages to the queue in a lower environment.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Business KPI Dashboard visualizes key user activity metrics

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am an authorized DevOps Engineer

### 3.5.5 When

I view the 'Production - Business KPIs' dashboard

### 3.5.6 Then

I can see widgets displaying custom metrics for Daily Active Users (DAU), new user sign-ups, and subscription conversion events, derived from structured application logs.

### 3.5.7 Validation Notes

Requires generating specific log events in the application and verifying that the CloudWatch Metric Filters correctly create data points.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Dashboards are separated by deployment environment

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

I have access to both Staging and Production environments

### 3.6.5 When

I list the available CloudWatch Dashboards

### 3.6.6 Then

I see separate, clearly named dashboards for the 'Staging' and 'Production' environments (e.g., 'Staging - API Health', 'Production - API Health').

### 3.6.7 Validation Notes

Verify in the AWS console that dashboards are created in both accounts/regions and are named according to convention.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Access to monitoring dashboards is restricted

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am a user with an IAM role that does not have CloudWatch read permissions

### 3.7.5 When

I attempt to access the system health CloudWatch Dashboards via a direct link or console navigation

### 3.7.6 Then

My access is denied with an appropriate IAM error message.

### 3.7.7 Validation Notes

Test by creating a temporary IAM user/role with restricted permissions and attempting to access the dashboard URLs.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- AWS CloudWatch Dashboards
- Graph/Metric Widgets
- Time range selectors

## 4.2.0 User Interactions

- Navigate between different dashboards
- Select different time ranges for viewing metrics
- Hover over graphs to see specific data points

## 4.3.0 Display Requirements

- Dashboards must be logically organized (e.g., a high-level overview dashboard linking to service-specific ones).
- All widgets must have clear, descriptive titles.
- Metrics should be displayed with appropriate units (e.g., ms, %, count).

## 4.4.0 Accessibility Needs

- N/A (UI is provided by AWS CloudWatch, which is responsible for its own accessibility compliance).

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'Monitoring dashboards must exist for all production services before go-live.', 'enforcement_point': 'Go-Live Checklist (REQ-TRN-001)', 'violation_handling': 'Go-live is blocked until monitoring is in place and verified.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-106', 'dependency_reason': 'The infrastructure (Fargate, Aurora, SQS, etc.) must be defined and deployed via IaC before dashboards can be created to monitor it.'}

## 6.2.0 Technical Dependencies

- AWS CloudWatch for metrics and dashboards.
- AWS CDK (TypeScript) for defining dashboards as Infrastructure as Code (REQ-CON-001).
- Application services must be configured to emit structured JSON logs (REQ-MON-001) to enable creation of custom business metrics via Metric Filters.

## 6.3.0 Data Dependencies

- Services must be actively running and generating metrics for the dashboards to populate with data.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- CloudWatch dashboards should load within 5 seconds.

## 7.2.0 Security

- Access to all monitoring dashboards must be restricted via AWS IAM to authorized personnel only.

## 7.3.0 Usability

- Dashboards should be organized intuitively, allowing an engineer to quickly assess overall system health and drill down into specific service issues.

## 7.4.0 Accessibility

- N/A - Handled by AWS.

## 7.5.0 Compatibility

- Dashboards must be viewable in standard modern web browsers (Chrome, Firefox, Safari, Edge).

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- All dashboards and widgets must be defined as Infrastructure as Code using AWS CDK, which can be verbose and requires specific expertise.
- Creating custom metrics for business KPIs (DAU, etc.) requires defining CloudWatch Metric Filters on top of structured application logs, adding a layer of configuration.
- Requires careful planning to ensure dashboards are insightful and not just a collection of disconnected metrics.

## 8.3.0 Technical Risks

- Misconfiguration of metric dimensions in CDK could lead to widgets showing 'No Data'.
- Metric filters for business KPIs might be brittle if the application log structure changes without updating the filter patterns.

## 8.4.0 Integration Points

- AWS CloudWatch
- AWS API Gateway
- AWS Fargate
- AWS Lambda
- Amazon Aurora
- Amazon SQS

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Manual Verification
- Visual Inspection

## 9.2.0 Test Scenarios

- Verify each dashboard in the 'Staging' environment by generating synthetic load and checking if metrics appear as expected.
- Confirm that dashboard access is denied for an unauthorized IAM user.
- After production deployment, perform a final visual inspection to ensure all dashboards are populated with live user data.

## 9.3.0 Test Data Needs

- Requires active services in a deployed environment (Staging) to generate metrics.
- For business KPIs, specific user actions (e.g., sign-up, login) must be performed to generate the necessary log entries.

## 9.4.0 Testing Tools

- AWS Management Console (CloudWatch section)

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing in both Staging and Production environments.
- AWS CDK code for all dashboards is peer-reviewed, adheres to standards, and is merged into the main branch.
- Unit tests are not applicable for this story type.
- Integration testing is completed via manual verification of deployed dashboards.
- User interface (the dashboard layout and content) is reviewed and approved by the engineering team.
- Performance requirements (dashboard load time) are verified.
- Security requirements (IAM access control) are validated.
- Documentation is updated in the team's runbook to include links to the new dashboards and a brief description of each.
- Story is deployed and verified in the production environment.

# 11.0.0 Planning Information

## 11.1.0 Story Points

8

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a critical-path story for operational readiness and must be completed before the 'Go-Live' date.
- Should be worked on by an engineer with strong AWS CDK and CloudWatch experience.
- Tightly coupled with US-108 (Alerting); may be beneficial to plan them in the same or consecutive sprints.

## 11.4.0 Release Impact

- Enables the 'Intensive post-launch monitoring' requirement specified in the Go-Live plan (REQ-TRN-001). Without this, the team cannot effectively monitor the health of the application after release.

