# 1 Deployment Model

| Property | Value |
|----------|-------|
| Model Type | CloudNativeContainerized |
| Description | A fully cloud-native deployment model leveraging s... |
| Architecture Style | Modular Monolith |
| Justification | This model directly aligns with the specified tech... |

# 2 Infrastructure Components

## 2.1 ContainerOrchestration

### 2.1.1 Component Id

compute-fargate-api

### 2.1.2 Name

AWS Fargate Service (API)

### 2.1.3 Type

🔹 ContainerOrchestration

### 2.1.4 Purpose

To run the containerized ASP.NET Core monolithic application. Provides serverless compute for the main API.

### 2.1.5 Configuration Notes

Configured with a minimum of 2 instances for high availability, scaling up to 10+ based on load. Runs in private subnets.

### 2.1.6 Requirements

- REQ-TEC-001
- REQ-SCA-001
- REQ-AVL-001

## 2.2.0 ContainerOrchestration

### 2.2.1 Component Id

compute-fargate-workers

### 2.2.2 Name

AWS Fargate Service (Workers)

### 2.2.3 Type

🔹 ContainerOrchestration

### 2.2.4 Purpose

To run the containerized Hangfire background job processors. Scales independently from the API.

### 2.2.5 Configuration Notes

Configured to scale based on SQS queue depth or job count. Can leverage Fargate Spot for cost optimization on non-time-critical jobs.

### 2.2.6 Requirements

- REQ-TEC-001
- REQ-FUNC-009
- REQ-FUNC-002

## 2.3.0 Database

### 2.3.1 Component Id

database-aurora

### 2.3.2 Name

Amazon Aurora Serverless v2 (PostgreSQL)

### 2.3.3 Type

🔹 Database

### 2.3.4 Purpose

Serves as the primary relational database for all application data.

### 2.3.5 Configuration Notes

Deployed in a multi-AZ configuration for high availability. Auto-scales capacity based on workload. Backups enabled with a 14-day PITR window (REQ-REL-001). Replicated to the DR region.

### 2.3.6 Requirements

- REQ-TEC-001
- REQ-SCA-001
- REQ-REL-001

## 2.4.0 ObjectStorage

### 2.4.1 Component Id

storage-s3

### 2.4.2 Name

Amazon S3

### 2.4.3 Type

🔹 ObjectStorage

### 2.4.4 Purpose

Stores user-generated data exports and other static assets.

### 2.4.5 Configuration Notes

Bucket policy will enforce private access. Data encrypted at rest using KMS. Lifecycle policies will automatically delete expired data exports after 24 hours (REQ-DAT-001).

### 2.4.6 Requirements

- REQ-TEC-001
- REQ-USR-001

## 2.5.0 Cache

### 2.5.1 Component Id

caching-redis

### 2.5.2 Name

Amazon ElastiCache for Redis

### 2.5.3 Type

🔹 Cache

### 2.5.4 Purpose

Provides a distributed in-memory cache to reduce database load and meet API latency requirements.

### 2.5.5 Configuration Notes

Deployed in a multi-AZ configuration. Used for caching dashboard data and other frequently accessed information.

### 2.5.6 Requirements

- REQ-TEC-001
- REQ-PER-001

## 2.6.0 Queue

### 2.6.1 Component Id

messaging-sqs

### 2.6.2 Name

Amazon SQS

### 2.6.3 Type

🔹 Queue

### 2.6.4 Purpose

Decouples the API from long-running tasks by queuing requests for background jobs.

### 2.6.5 Configuration Notes

A standard queue will be used for data export job requests, triggering the Fargate worker service.

### 2.6.6 Requirements

- REQ-TEC-001
- REQ-USR-001

## 2.7.0 LoadBalancer

### 2.7.1 Component Id

networking-alb

### 2.7.2 Name

Application Load Balancer (ALB)

### 2.7.3 Type

🔹 LoadBalancer

### 2.7.4 Purpose

Distributes incoming traffic across the Fargate API service instances.

### 2.7.5 Configuration Notes

Internet-facing, deployed across public subnets in multiple AZs. Manages TLS termination.

### 2.7.6 Requirements

- REQ-SCA-001
- REQ-AVL-001

## 2.8.0 APIGateway

### 2.8.1 Component Id

networking-apigw

### 2.8.2 Name

Amazon API Gateway

### 2.8.3 Type

🔹 APIGateway

### 2.8.4 Purpose

Acts as the primary, secure entry point for all client API requests. Integrates with Auth0 for JWT validation.

### 2.8.5 Configuration Notes

Configured as an HTTP proxy to the internal Application Load Balancer. Enforces request validation and throttling.

### 2.8.6 Requirements

- REQ-TEC-001
- REQ-SEC-001

## 2.9.0 SecretsManagement

### 2.9.1 Component Id

security-secrets-manager

### 2.9.2 Name

AWS Secrets Manager

### 2.9.3 Type

🔹 SecretsManagement

### 2.9.4 Purpose

Securely stores and manages all secrets, such as database credentials and third-party API keys.

### 2.9.5 Configuration Notes

Integrates with IAM roles to provide secrets to Fargate tasks at runtime, avoiding hardcoded credentials.

### 2.9.6 Requirements

- REQ-SEC-001

# 3.0.0 Cloud Strategy

| Property | Value |
|----------|-------|
| Hosting Model | CloudNative |
| Provider | AWS |
| Justification | The SRS explicitly mandates the use of AWS and a s... |

# 4.0.0 Region And Availability

## 4.1.0 Primary Region

| Property | Value |
|----------|-------|
| Region | eu-central-1 (Frankfurt) |
| Justification | Mandated by REQ-CON-001 for GDPR data residency co... |
| Availability Zones | 3 |
| Topology | Active-Active across AZs for high availability. |

## 4.2.0 Disaster Recovery Region

### 4.2.1 Region

eu-west-1 (Ireland)

### 4.2.2 Justification

Provides geographic separation from the primary region while remaining within a similar legal framework.

### 4.2.3 Strategy

Pilot Light

### 4.2.4 Rto

< 4 hours

### 4.2.5 Rpo

< 5 minutes

### 4.2.6 Implementation

Infrastructure is defined in CDK and can be rapidly deployed. Aurora Global Database provides continuous data replication to meet the RPO. Failover is a manual process initiated via runbook.

### 4.2.7 Requirements

- REQ-REL-001

# 5.0.0 Resource Allocation

## 5.1.0 Compute Scaling

### 5.1.1 Component Id

#### 5.1.1.1 Component Id

compute-fargate-api

#### 5.1.1.2 Strategy

Horizontal Auto Scaling

#### 5.1.1.3 Min Instances

2

#### 5.1.1.4 Max Instances

10

#### 5.1.1.5 Triggers

##### 5.1.1.5.1 Metric

###### 5.1.1.5.1.1 Metric

CPUUtilization

###### 5.1.1.5.1.2 Threshold

70%

##### 5.1.1.5.2.0 Metric

###### 5.1.1.5.2.1 Metric

RequestCountPerTarget

###### 5.1.1.5.2.2 Threshold

1000

### 5.1.2.0.0.0 Component Id

#### 5.1.2.1.0.0 Component Id

compute-fargate-workers

#### 5.1.2.2.0.0 Strategy

Horizontal Auto Scaling

#### 5.1.2.3.0.0 Min Instances

1

#### 5.1.2.4.0.0 Max Instances

5

#### 5.1.2.5.0.0 Triggers

- {'metric': 'SQSApproximateNumberOfMessagesVisible', 'threshold': '100'}

## 5.2.0.0.0.0 Database Scaling

| Property | Value |
|----------|-------|
| Component Id | database-aurora |
| Strategy | Automatic |
| Configuration | Amazon Aurora Serverless v2 scales automatically. ... |

# 6.0.0.0.0.0 Networking Topology

## 6.1.0.0.0.0 Vpc

| Property | Value |
|----------|-------|
| Name | app-vpc |
| Cidr Block | 10.0.0.0/16 |
| Description | A dedicated VPC to isolate all application resourc... |

## 6.2.0.0.0.0 Subnets

### 6.2.1.0.0.0 Public

#### 6.2.1.1.0.0 Type

🔹 Public

#### 6.2.1.2.0.0 Purpose

Hosts the Application Load Balancer and NAT Gateways. Spans multiple AZs.

#### 6.2.1.3.0.0 Count

3

### 6.2.2.0.0.0 Private

#### 6.2.2.1.0.0 Type

🔹 Private

#### 6.2.2.2.0.0 Purpose

Hosts all application compute (Fargate) and data stores (Aurora, ElastiCache). Instances here have no direct internet access. Spans multiple AZs.

#### 6.2.2.3.0.0 Count

3

## 6.3.0.0.0.0 Gateways

### 6.3.1.0.0.0 Internet Gateway

#### 6.3.1.1.0.0 Type

🔹 Internet Gateway

#### 6.3.1.2.0.0 Purpose

Provides internet access for resources in public subnets.

### 6.3.2.0.0.0 NAT Gateway

#### 6.3.2.1.0.0 Type

🔹 NAT Gateway

#### 6.3.2.2.0.0 Purpose

Allows resources in private subnets to initiate outbound connections (e.g., to OpenAI API) while blocking inbound connections.

### 6.3.3.0.0.0 API Gateway

#### 6.3.3.1.0.0 Type

🔹 API Gateway

#### 6.3.3.2.0.0 Purpose

Serves as the single, managed entry point for all mobile client requests, proxying to the internal ALB.

## 6.4.0.0.0.0 Dns

### 6.4.1.0.0.0 Service

Amazon Route 53

### 6.4.2.0.0.0 Purpose

Manages the public domain and routes traffic to the API Gateway endpoint.

# 7.0.0.0.0.0 Security And Compliance

## 7.1.0.0.0.0 Network Security

### 7.1.1.0.0.0 Control

#### 7.1.1.1.0.0 Control

Security Groups

#### 7.1.1.2.0.0 Description

Stateful firewalls applied to Fargate services, Aurora, and ElastiCache instances to enforce least-privilege network access. For example, the API Fargate service can only accept traffic from the ALB on the application port.

### 7.1.2.0.0.0 Control

#### 7.1.2.1.0.0 Control

Network ACLs

#### 7.1.2.2.0.0 Description

Stateless firewalls applied at the subnet level as a secondary layer of defense.

## 7.2.0.0.0.0 Secrets Management

### 7.2.1.0.0.0 Service

AWS Secrets Manager

### 7.2.2.0.0.0 Strategy

All credentials (DB connection strings, API keys) are stored in Secrets Manager. IAM roles for Fargate tasks are granted permission to retrieve specific secrets at runtime.

### 7.2.3.0.0.0 Requirements

- REQ-SEC-001

## 7.3.0.0.0.0 Identity And Access

### 7.3.1.0.0.0 Service

AWS IAM

### 7.3.2.0.0.0 Strategy

IAM Roles are used extensively to grant granular, service-to-service permissions (e.g., Fargate task role allowing access to SQS and S3) following the principle of least privilege.

## 7.4.0.0.0.0 Data Protection

### 7.4.1.0.0.0 At Rest

#### 7.4.1.1.0.0 Strategy

Encryption enabled for all data stores (Aurora, S3, ElastiCache) using AWS Key Management Service (KMS).

#### 7.4.1.2.0.0 Requirements

- REQ-SEC-001

### 7.4.2.0.0.0 In Transit

#### 7.4.2.1.0.0 Strategy

All communication is encrypted using TLS 1.2+. This is enforced at the API Gateway and ALB.

#### 7.4.2.2.0.0 Requirements

- REQ-CIF-001
- REQ-SEC-001

# 8.0.0.0.0.0 Project Specific Deployment

## 8.1.0.0.0.0 Deployment Automation

### 8.1.1.0.0.0 Tool

AWS CDK with TypeScript

### 8.1.2.0.0.0 Description

The entire infrastructure is defined as code, enabling repeatable, automated, and version-controlled deployments for all environments (Testing, Staging, Production) as required by REQ-MNT-001 and REQ-CON-001.

## 8.2.0.0.0.0 Ci Cd Pipeline

### 8.2.1.0.0.0 Tool

GitHub Actions

### 8.2.2.0.0.0 Description

The pipeline automates building the .NET application, containerizing it, pushing the image to Amazon ECR (Elastic Container Registry), and deploying the new version to Fargate using the AWS CDK.

### 8.2.3.0.0.0 Requirements

- REQ-TEC-001
- REQ-TRN-001

# 9.0.0.0.0.0 Implementation Priority

## 9.1.0.0.0.0 Component

### 9.1.1.0.0.0 Component

Core Infrastructure (VPC, IAM, S3, Secrets Manager)

### 9.1.2.0.0.0 Priority

🔴 high

### 9.1.3.0.0.0 Dependencies

*No items available*

### 9.1.4.0.0.0 Estimated Effort

Medium

### 9.1.5.0.0.0 Risk Level

low

## 9.2.0.0.0.0 Component

### 9.2.1.0.0.0 Component

Primary Data Stores (Aurora, ElastiCache)

### 9.2.2.0.0.0 Priority

🔴 high

### 9.2.3.0.0.0 Dependencies

- Core Infrastructure

### 9.2.4.0.0.0 Estimated Effort

Medium

### 9.2.5.0.0.0 Risk Level

medium

## 9.3.0.0.0.0 Component

### 9.3.1.0.0.0 Component

Application Deployment (Fargate, ALB, ECR, API Gateway)

### 9.3.2.0.0.0 Priority

🔴 high

### 9.3.3.0.0.0 Dependencies

- Core Infrastructure
- Primary Data Stores

### 9.3.4.0.0.0 Estimated Effort

High

### 9.3.5.0.0.0 Risk Level

medium

## 9.4.0.0.0.0 Component

### 9.4.1.0.0.0 Component

Disaster Recovery Setup (Cross-region replication, DR CDK stack)

### 9.4.2.0.0.0 Priority

🟡 medium

### 9.4.3.0.0.0 Dependencies

- Application Deployment

### 9.4.4.0.0.0 Estimated Effort

Medium

### 9.4.5.0.0.0 Risk Level

low

# 10.0.0.0.0.0 Risk Assessment

## 10.1.0.0.0.0 Risk

### 10.1.1.0.0.0 Risk

Over-provisioning of resources leading to excessive costs.

### 10.1.2.0.0.0 Impact

medium

### 10.1.3.0.0.0 Probability

medium

### 10.1.4.0.0.0 Mitigation

Utilize serverless and auto-scaling services (Fargate, Aurora Serverless). Start with conservative scaling thresholds and adjust based on production monitoring data. Use Fargate Spot for worker nodes.

## 10.2.0.0.0.0 Risk

### 10.2.1.0.0.0 Risk

Insecure network configuration (e.g., misconfigured Security Groups).

### 10.2.2.0.0.0 Impact

high

### 10.2.3.0.0.0 Probability

low

### 10.2.4.0.0.0 Mitigation

Define all security rules via Infrastructure as Code (CDK) for peer review. Employ automated security scanning tools in the CI/CD pipeline to check for common misconfigurations.

# 11.0.0.0.0.0 Recommendations

## 11.1.0.0.0.0 Category

### 11.1.1.0.0.0 Category

🔹 Automation

### 11.1.2.0.0.0 Recommendation

Strictly enforce that all infrastructure changes are deployed via the AWS CDK pipeline.

### 11.1.3.0.0.0 Justification

This aligns with REQ-MNT-001 and prevents configuration drift, ensuring consistency across environments and improving reliability.

### 11.1.4.0.0.0 Priority

🔴 high

## 11.2.0.0.0.0 Category

### 11.2.1.0.0.0 Category

🔹 Operations

### 11.2.2.0.0.0 Recommendation

Develop and regularly test operational runbooks for key failure scenarios, especially the manual failover process to the DR region.

### 11.2.3.0.0.0 Justification

Ensures the team is prepared to meet the RTO/RPO targets defined in REQ-REL-001 in a real disaster scenario.

### 11.2.4.0.0.0 Priority

🟡 medium

