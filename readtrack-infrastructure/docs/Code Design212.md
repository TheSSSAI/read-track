# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | readtrack-infrastructure |
| Validation Timestamp | 2025-10-27T14:30:00Z |
| Original Component Count Claimed | 4 |
| Original Component Count Actual | 4 |
| Gaps Identified Count | 5 |
| Components Added Count | 18 |
| Final Component Count | 22 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic architectural analysis against AWS CDK ... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance with IaC responsibilities, but required decomposition of monolithic definitions into modular Stacks and Constructs.

#### 2.2.1.2 Gaps Identified

- Missing OIDC Identity Provider configuration for GitHub Actions (US-106)
- Missing comprehensive CloudWatch Monitoring dashboards and alarms (US-107, US-108)
- Missing Secrets Manager definitions for external API keys
- Missing S3 Bucket Lifecycle and Object Lock policies for Audit Logs (US-111)
- Lack of centralized configuration management for multi-environment deployment

#### 2.2.1.3 Components Added

- IdentityStack
- MonitoringStack
- SecretsConstruct
- AuditStorageConstruct
- ConfigLoader
- SecurityAspect

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100% (Security, Resilience, Observability)

#### 2.2.2.3 Missing Requirement Components

- Auto-scaling configuration for Fargate (REQ-PERF-001)
- WAF/Shield configuration for API Gateway/ALB (REQ-SEC-001)
- Multi-AZ configuration details for Database (REQ-REL-002)

#### 2.2.2.4 Added Requirement Components

- AutoScalingConfig
- WafWebAclConstruct
- MultiAzDatabaseConfig

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Construct Library pattern implemented; Stack Composition pattern applied.

#### 2.2.3.2 Missing Pattern Components

- Aspect-based tagging and security enforcement
- Type-safe environment configuration

#### 2.2.3.3 Added Pattern Components

- SecurityAspect
- TaggingAspect
- IEnvConfig

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

N/A - Infrastructure defines storage engines, not schemas.

#### 2.2.4.2 Missing Database Components

- Parameter Groups for Redis
- Access Policies for OpenSearch

#### 2.2.4.3 Added Database Components

- DatabaseConfigFactory
- OpenSearchAccessPolicy

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Infrastructure supports all defined sequences (Events, Queues, APIs).

#### 2.2.5.2 Missing Interaction Components

- Dead Letter Queues for SQS resilience
- SNS Topics for Alerting

#### 2.2.5.3 Added Interaction Components

- ResilientQueueConstruct
- AlertingTopicConstruct

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | readtrack-infrastructure |
| Technology Stack | AWS CDK v2, TypeScript, Node.js 20+ |
| Technology Guidance Integration | AWS Well-Architected Framework (Security & Reliabi... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 22 |
| Specification Methodology | Modular Stack Architecture with L3 Constructs |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Construct Library
- Stack Composition
- Aspect-Oriented Programming (Tagging/Security)
- Context-based Configuration
- Least Privilege IAM

#### 2.3.2.2 Directory Structure Source

CDK Best Practices

#### 2.3.2.3 Naming Conventions Source

PascalCase for Constructs/Stacks, camelCase for props

#### 2.3.2.4 Architectural Patterns Source

Infrastructure as Code patterns

#### 2.3.2.5 Performance Optimizations Applied

- Aurora Serverless v2 for auto-scaling
- Provisioned Concurrency for critical Lambdas
- VPC Endpoints for internal traffic routing

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

.editorconfig

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- .editorconfig

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.gitattributes

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- .gitattributes

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.github/workflows/backend-ci.yml

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- backend-ci.yml

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.github/workflows/infra-deploy.yml

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- infra-deploy.yml

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

.github/workflows/mobile-ci.yml

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- mobile-ci.yml

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

.gitignore

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- .gitignore

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

.vscode/extensions.json

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- extensions.json

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

.vscode/launch.json

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- launch.json

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

.vscode/settings.json

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- settings.json

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

backend/.gitignore

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- .gitignore

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

backend/Directory.Build.props

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- Directory.Build.props

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

backend/Directory.Packages.props

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- Directory.Packages.props

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

backend/docker-compose.dev.yml

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- docker-compose.dev.yml

###### 2.3.3.1.13.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.13.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

backend/global.json

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- global.json

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

backend/nuget.config

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- nuget.config

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

backend/ReadTrack.sln

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- ReadTrack.sln

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

backend/src/ReadTrack.Host/appsettings.Development.json

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- appsettings.Development.json

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

backend/src/ReadTrack.Host/Dockerfile

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- Dockerfile

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

bin

###### 2.3.3.1.19.2 Purpose

CDK App entry point

###### 2.3.3.1.19.3 Contains Files

- readtrack-infra.ts

###### 2.3.3.1.19.4 Organizational Reasoning

Instantiates the App and orchestrates Stacks based on environment context.

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard CDK entry point

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

config

###### 2.3.3.1.20.2 Purpose

Environment-specific configuration

###### 2.3.3.1.20.3 Contains Files

- config.ts
- environments.ts

###### 2.3.3.1.20.4 Organizational Reasoning

Separates configuration data from infrastructure code (12-Factor App).

###### 2.3.3.1.20.5 Framework Convention Alignment

Configuration injection

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

infrastructure/cdk.json

###### 2.3.3.1.21.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.21.3 Contains Files

- cdk.json

###### 2.3.3.1.21.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.21.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.22.0 Directory Path

###### 2.3.3.1.22.1 Directory Path

infrastructure/jest.config.js

###### 2.3.3.1.22.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.22.3 Contains Files

- jest.config.js

###### 2.3.3.1.22.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.22.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.23.0 Directory Path

###### 2.3.3.1.23.1 Directory Path

infrastructure/package.json

###### 2.3.3.1.23.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.23.3 Contains Files

- package.json

###### 2.3.3.1.23.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.23.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.24.0 Directory Path

###### 2.3.3.1.24.1 Directory Path

lib/constructs

###### 2.3.3.1.24.2 Purpose

Reusable L3 constructs (Custom Resource abstractions)

###### 2.3.3.1.24.3 Contains Files

- StandardVpc.ts
- AuroraCluster.ts
- FargateService.ts
- SecureBucket.ts
- ResilientQueue.ts
- OpenSearchDomain.ts
- AuditStorageConstruct.ts

###### 2.3.3.1.24.4 Organizational Reasoning

Encapsulates configuration best practices and security defaults.

###### 2.3.3.1.24.5 Framework Convention Alignment

Construct Library pattern

##### 2.3.3.1.25.0 Directory Path

###### 2.3.3.1.25.1 Directory Path

lib/stacks

###### 2.3.3.1.25.2 Purpose

Top-level deployment units

###### 2.3.3.1.25.3 Contains Files

- NetworkStack.ts
- DatabaseStack.ts
- ComputeStack.ts
- StorageStack.ts
- MonitoringStack.ts
- IdentityStack.ts

###### 2.3.3.1.25.4 Organizational Reasoning

Separates resources by lifecycle and domain (Stateful vs Stateless vs Observability).

###### 2.3.3.1.25.5 Framework Convention Alignment

Stack-per-service pattern

##### 2.3.3.1.26.0 Directory Path

###### 2.3.3.1.26.1 Directory Path

lib/types

###### 2.3.3.1.26.2 Purpose

TypeScript interfaces for configuration

###### 2.3.3.1.26.3 Contains Files

- index.ts
- config-types.ts

###### 2.3.3.1.26.4 Organizational Reasoning

Ensures type safety for Stack definitions and Config files.

###### 2.3.3.1.26.5 Framework Convention Alignment

TypeScript best practices

##### 2.3.3.1.27.0 Directory Path

###### 2.3.3.1.27.1 Directory Path

mobile/.gitignore

###### 2.3.3.1.27.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.27.3 Contains Files

- .gitignore

###### 2.3.3.1.27.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.27.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.28.0 Directory Path

###### 2.3.3.1.28.1 Directory Path

mobile/analysis_options.yaml

###### 2.3.3.1.28.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.28.3 Contains Files

- analysis_options.yaml

###### 2.3.3.1.28.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.28.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.29.0 Directory Path

###### 2.3.3.1.29.1 Directory Path

mobile/android/fastlane/Fastfile

###### 2.3.3.1.29.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.29.3 Contains Files

- Fastfile

###### 2.3.3.1.29.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.29.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.30.0 Directory Path

###### 2.3.3.1.30.1 Directory Path

mobile/build.yaml

###### 2.3.3.1.30.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.30.3 Contains Files

- build.yaml

###### 2.3.3.1.30.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.30.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.31.0 Directory Path

###### 2.3.3.1.31.1 Directory Path

mobile/ios/fastlane/Fastfile

###### 2.3.3.1.31.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.31.3 Contains Files

- Fastfile

###### 2.3.3.1.31.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.31.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.32.0 Directory Path

###### 2.3.3.1.32.1 Directory Path

mobile/pubspec.yaml

###### 2.3.3.1.32.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.32.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.32.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.32.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.33.0 Directory Path

###### 2.3.3.1.33.1 Directory Path

src/aspects

###### 2.3.3.1.33.2 Purpose

Cross-cutting policy enforcement

###### 2.3.3.1.33.3 Contains Files

- SecurityAspect.ts
- TaggingAspect.ts

###### 2.3.3.1.33.4 Organizational Reasoning

Applies global rules (encryption, tagging) to all resources.

###### 2.3.3.1.33.5 Framework Convention Alignment

CDK Aspects

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReadTrack.Infra |
| Namespace Organization | Stacks \| Constructs \| Aspects |
| Naming Conventions | PascalCase for Classes, camelCase for properties |
| Framework Alignment | CDK Construct ID conventions |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

NetworkStack

##### 2.3.4.1.2.0 File Path

lib/stacks/NetworkStack.ts

##### 2.3.4.1.3.0 Class Type

Stack

##### 2.3.4.1.4.0 Inheritance

cdk.Stack

##### 2.3.4.1.5.0 Purpose

Provisions the base networking layer: VPC, Subnets, and core Security Groups.

##### 2.3.4.1.6.0 Dependencies

- StandardVpc
- Config

##### 2.3.4.1.7.0 Framework Specific Attributes

- StackProps

##### 2.3.4.1.8.0 Technology Integration Notes

Exports VPC and Subnets via public readonly properties for consumption by other stacks.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

vpc

####### 2.3.4.1.9.1.2 Property Type

ec2.IVpc

####### 2.3.4.1.9.1.3 Access Modifier

public readonly

####### 2.3.4.1.9.1.4 Purpose

Exposes the created VPC.

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Multi-AZ (min 2)

####### 2.3.4.1.9.1.7 Implementation Notes

Must include Isolated subnets for DBs.

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

defaultSecurityGroup

####### 2.3.4.1.9.2.2 Property Type

ec2.ISecurityGroup

####### 2.3.4.1.9.2.3 Access Modifier

public readonly

####### 2.3.4.1.9.2.4 Purpose

Base SG for internal communication.

####### 2.3.4.1.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.2.6 Framework Specific Configuration



####### 2.3.4.1.9.2.7 Implementation Notes



##### 2.3.4.1.10.0.0 Methods

- {'method_name': 'constructor', 'method_signature': 'constructor(scope: Construct, id: string, props: NetworkStackProps)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'scope', 'parameter_type': 'Construct', 'is_nullable': 'false', 'purpose': 'App scope'}, {'parameter_name': 'props', 'parameter_type': 'NetworkStackProps', 'is_nullable': 'false', 'purpose': 'Configuration'}], 'implementation_logic': 'Initialize StandardVpc construct. Provision VPC Endpoints (S3, SecretsManager, ECR, CloudWatch) to keep traffic private. Export IDs to SSM.', 'exception_handling': 'CDK Synth-time validation.', 'performance_considerations': 'Use Gateway Endpoints for S3/DynamoDB to save NAT costs.', 'validation_requirements': 'CIDR non-overlap check.', 'technology_integration_details': 'aws-ec2'}

##### 2.3.4.1.11.0.0 Implementation Notes

Foundation for all other stacks.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

DatabaseStack

##### 2.3.4.2.2.0.0 File Path

lib/stacks/DatabaseStack.ts

##### 2.3.4.2.3.0.0 Class Type

Stack

##### 2.3.4.2.4.0.0 Inheritance

cdk.Stack

##### 2.3.4.2.5.0.0 Purpose

Provisions stateful resources: Aurora, Redis, OpenSearch.

##### 2.3.4.2.6.0.0 Dependencies

- NetworkStack
- AuroraCluster
- OpenSearchDomain

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Receives NetworkStack as a dependency prop.

##### 2.3.4.2.9.0.0 Properties

###### 2.3.4.2.9.1.0 Property Name

####### 2.3.4.2.9.1.1 Property Name

dbSecret

####### 2.3.4.2.9.1.2 Property Type

secretsmanager.ISecret

####### 2.3.4.2.9.1.3 Access Modifier

public readonly

####### 2.3.4.2.9.1.4 Purpose

Exposes DB credentials.

####### 2.3.4.2.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.1.6 Framework Specific Configuration

Generated by RDS

####### 2.3.4.2.9.1.7 Implementation Notes

Needed by ComputeStack.

###### 2.3.4.2.9.2.0 Property Name

####### 2.3.4.2.9.2.1 Property Name

openSearchEndpoint

####### 2.3.4.2.9.2.2 Property Type

string

####### 2.3.4.2.9.2.3 Access Modifier

public readonly

####### 2.3.4.2.9.2.4 Purpose

Exposes OpenSearch URL.

####### 2.3.4.2.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.2.6 Framework Specific Configuration



####### 2.3.4.2.9.2.7 Implementation Notes

Export as CfnOutput.

##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'constructor', 'method_signature': 'constructor(scope: Construct, id: string, props: DatabaseStackProps)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'props', 'parameter_type': 'DatabaseStackProps', 'is_nullable': 'false', 'purpose': 'Includes VPC ref'}], 'implementation_logic': 'Instantiate AuroraCluster (Serverless v2). Instantiate OpenSearchDomain. Instantiate ElastiCache Redis. Configure Security Groups to allow ingress from Compute SG.', 'exception_handling': 'None', 'performance_considerations': 'Aurora V2 auto-scaling config.', 'validation_requirements': 'VPC must be valid.', 'technology_integration_details': 'aws-rds, aws-opensearchservice'}

##### 2.3.4.2.11.0.0 Implementation Notes

Stores endpoints in SSM for app discovery.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

IdentityStack

##### 2.3.4.3.2.0.0 File Path

lib/stacks/IdentityStack.ts

##### 2.3.4.3.3.0.0 Class Type

Stack

##### 2.3.4.3.4.0.0 Inheritance

cdk.Stack

##### 2.3.4.3.5.0.0 Purpose

Provisions OIDC Provider and Roles for GitHub Actions.

##### 2.3.4.3.6.0.0 Dependencies

- aws-iam

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Implements US-106.

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

- {'method_name': 'constructor', 'method_signature': 'constructor(scope: Construct, id: string, props: StackProps)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'props', 'parameter_type': 'StackProps', 'is_nullable': 'false', 'purpose': 'Props'}], 'implementation_logic': 'Create OpenIdConnectProvider for GitHub. Create IAM Role with trust policy condition `StringLike: token.actions.githubusercontent.com:sub`: `repo:org/repo:*`. Attach policies for CDK deployment (CloudFormation, S3, ECR).', 'exception_handling': 'None', 'performance_considerations': 'None', 'validation_requirements': 'GitHub Org/Repo must be configured.', 'technology_integration_details': 'aws-iam'}

##### 2.3.4.3.11.0.0 Implementation Notes

Enables keyless CI/CD.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

MonitoringStack

##### 2.3.4.4.2.0.0 File Path

lib/stacks/MonitoringStack.ts

##### 2.3.4.4.3.0.0 Class Type

Stack

##### 2.3.4.4.4.0.0 Inheritance

cdk.Stack

##### 2.3.4.4.5.0.0 Purpose

Centralized observability stack (US-107, US-108).

##### 2.3.4.4.6.0.0 Dependencies

- ComputeStack
- DatabaseStack

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

Aggregates metrics.

##### 2.3.4.4.9.0.0 Properties

*No items available*

##### 2.3.4.4.10.0.0 Methods

- {'method_name': 'constructor', 'method_signature': 'constructor(scope: Construct, id: string, props: MonitoringStackProps)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'props', 'parameter_type': 'MonitoringStackProps', 'is_nullable': 'false', 'purpose': 'Resource references'}], 'implementation_logic': 'Create SNS Topic for critical alerts. Create CloudWatch Alarms: API 5xx > 1%, P95 Latency > 200ms, DB CPU > 80%, SQS DLQ Depth > 0. Create Dashboard with widgets.', 'exception_handling': 'None', 'performance_considerations': 'None', 'validation_requirements': 'Thresholds from config.', 'technology_integration_details': 'aws-cloudwatch'}

##### 2.3.4.4.11.0.0 Implementation Notes

Alarms trigger SNS.

#### 2.3.4.5.0.0.0 Class Name

##### 2.3.4.5.1.0.0 Class Name

AuditStorageConstruct

##### 2.3.4.5.2.0.0 File Path

lib/constructs/AuditStorageConstruct.ts

##### 2.3.4.5.3.0.0 Class Type

Construct

##### 2.3.4.5.4.0.0 Inheritance

Construct

##### 2.3.4.5.5.0.0 Purpose

Provisions immutable S3 storage for audit logs (US-111).

##### 2.3.4.5.6.0.0 Dependencies

- aws-s3

##### 2.3.4.5.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.5.8.0.0 Technology Integration Notes

Object Lock enabled.

##### 2.3.4.5.9.0.0 Properties

- {'property_name': 'bucket', 'property_type': 's3.Bucket', 'access_modifier': 'public readonly', 'purpose': 'Bucket instance.', 'validation_attributes': [], 'framework_specific_configuration': '', 'implementation_notes': ''}

##### 2.3.4.5.10.0.0 Methods

- {'method_name': 'constructor', 'method_signature': 'constructor(scope: Construct, id: string)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'scope', 'parameter_type': 'Construct', 'is_nullable': 'false', 'purpose': 'Scope'}, {'parameter_name': 'id', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'ID'}], 'implementation_logic': 'Create Bucket. Enable Versioning. Enable Object Lock (Governance/Compliance mode). Enable Encryption (KMS). Block Public Access. Add Lifecycle Rule (Transition to Glacier after 90 days, Retain 7 years).', 'exception_handling': 'None', 'performance_considerations': 'Lifecycle rules for cost.', 'validation_requirements': 'Object Lock requires Versioning.', 'technology_integration_details': 'aws-s3'}

##### 2.3.4.5.11.0.0 Implementation Notes

Compliance requirement.

### 2.3.5.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0 Interface Name

##### 2.3.5.1.1.0.0 Interface Name

NetworkStackProps

##### 2.3.5.1.2.0.0 File Path

lib/types/index.ts

##### 2.3.5.1.3.0.0 Purpose

Configuration definition for Network Stack.

##### 2.3.5.1.4.0.0 Generic Constraints

None

##### 2.3.5.1.5.0.0 Framework Specific Inheritance

cdk.StackProps

##### 2.3.5.1.6.0.0 Method Contracts

*No items available*

##### 2.3.5.1.7.0.0 Property Contracts

###### 2.3.5.1.7.1.0 Property Name

####### 2.3.5.1.7.1.1 Property Name

vpcCidr

####### 2.3.5.1.7.1.2 Property Type

string

####### 2.3.5.1.7.1.3 Getter Contract

CIDR block

####### 2.3.5.1.7.1.4 Setter Contract

Readonly

###### 2.3.5.1.7.2.0 Property Name

####### 2.3.5.1.7.2.1 Property Name

maxAzs

####### 2.3.5.1.7.2.2 Property Type

number

####### 2.3.5.1.7.2.3 Getter Contract

AZ Count

####### 2.3.5.1.7.2.4 Setter Contract

Readonly

##### 2.3.5.1.8.0.0 Implementation Guidance

Derived from IEnvConfig.

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

DatabaseStackProps

##### 2.3.5.2.2.0.0 File Path

lib/types/index.ts

##### 2.3.5.2.3.0.0 Purpose

Configuration for Database Stack.

##### 2.3.5.2.4.0.0 Generic Constraints

None

##### 2.3.5.2.5.0.0 Framework Specific Inheritance

cdk.StackProps

##### 2.3.5.2.6.0.0 Method Contracts

*No items available*

##### 2.3.5.2.7.0.0 Property Contracts

###### 2.3.5.2.7.1.0 Property Name

####### 2.3.5.2.7.1.1 Property Name

vpc

####### 2.3.5.2.7.1.2 Property Type

ec2.IVpc

####### 2.3.5.2.7.1.3 Getter Contract

VPC Instance

####### 2.3.5.2.7.1.4 Setter Contract

Readonly

###### 2.3.5.2.7.2.0 Property Name

####### 2.3.5.2.7.2.1 Property Name

isProd

####### 2.3.5.2.7.2.2 Property Type

boolean

####### 2.3.5.2.7.2.3 Getter Contract

Production flag

####### 2.3.5.2.7.2.4 Setter Contract

Readonly

##### 2.3.5.2.8.0.0 Implementation Guidance

Used to toggle deletion protection and instance sizes.

#### 2.3.5.3.0.0.0 Interface Name

##### 2.3.5.3.1.0.0 Interface Name

IEnvConfig

##### 2.3.5.3.2.0.0 File Path

lib/types/config-types.ts

##### 2.3.5.3.3.0.0 Purpose

Contract for environment configuration files.

##### 2.3.5.3.4.0.0 Generic Constraints

None

##### 2.3.5.3.5.0.0 Framework Specific Inheritance

None

##### 2.3.5.3.6.0.0 Method Contracts

*No items available*

##### 2.3.5.3.7.0.0 Property Contracts

###### 2.3.5.3.7.1.0 Property Name

####### 2.3.5.3.7.1.1 Property Name

account

####### 2.3.5.3.7.1.2 Property Type

string

####### 2.3.5.3.7.1.3 Getter Contract

AWS Account ID

####### 2.3.5.3.7.1.4 Setter Contract

Readonly

###### 2.3.5.3.7.2.0 Property Name

####### 2.3.5.3.7.2.1 Property Name

region

####### 2.3.5.3.7.2.2 Property Type

string

####### 2.3.5.3.7.2.3 Getter Contract

AWS Region

####### 2.3.5.3.7.2.4 Setter Contract

Readonly

###### 2.3.5.3.7.3.0 Property Name

####### 2.3.5.3.7.3.1 Property Name

vpc

####### 2.3.5.3.7.3.2 Property Type

{ cidr: string; maxAzs: number }

####### 2.3.5.3.7.3.3 Getter Contract

Network config

####### 2.3.5.3.7.3.4 Setter Contract

Readonly

##### 2.3.5.3.8.0.0 Implementation Guidance

Ensures type safety in config files.

### 2.3.6.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0 Dto Specifications

*No items available*

### 2.3.8.0.0.0.0 Configuration Specifications

#### 2.3.8.1.0.0.0 Configuration Name

##### 2.3.8.1.1.0.0 Configuration Name

cdk.json

##### 2.3.8.1.2.0.0 File Path

cdk.json

##### 2.3.8.1.3.0.0 Purpose

CDK CLI context and settings.

##### 2.3.8.1.4.0.0 Framework Base Class

N/A

##### 2.3.8.1.5.0.0 Configuration Sections

###### 2.3.8.1.5.1.0 Section Name

####### 2.3.8.1.5.1.1 Section Name

app

####### 2.3.8.1.5.1.2 Properties

- {'property_name': 'command', 'property_type': 'string', 'default_value': 'npx ts-node --prefer-ts-exts bin/readtrack-infra.ts', 'required': 'true', 'description': 'Execution command'}

###### 2.3.8.1.5.2.0 Section Name

####### 2.3.8.1.5.2.1 Section Name

context

####### 2.3.8.1.5.2.2 Properties

- {'property_name': 'githubRepo', 'property_type': 'string', 'default_value': 'readtrack/infrastructure', 'required': 'true', 'description': 'For OIDC'}

##### 2.3.8.1.6.0.0 Validation Requirements

Valid JSON

##### 2.3.8.1.7.0.0 Validation Notes

Feature flags should be enabled.

#### 2.3.8.2.0.0.0 Configuration Name

##### 2.3.8.2.1.0.0 Configuration Name

EnvConfig

##### 2.3.8.2.2.0.0 File Path

config/config.ts

##### 2.3.8.2.3.0.0 Purpose

Runtime configuration loader.

##### 2.3.8.2.4.0.0 Framework Base Class

N/A

##### 2.3.8.2.5.0.0 Configuration Sections

- {'section_name': 'Configs', 'properties': [{'property_name': 'dev', 'property_type': 'IEnvConfig', 'default_value': '...', 'required': 'true', 'description': 'Dev environment config'}, {'property_name': 'prod', 'property_type': 'IEnvConfig', 'default_value': '...', 'required': 'true', 'description': 'Prod environment config'}]}

##### 2.3.8.2.6.0.0 Validation Requirements

Must match IEnvConfig interface.

##### 2.3.8.2.7.0.0 Validation Notes

Loaded by bin/main.ts based on context.

### 2.3.9.0.0.0.0 Dependency Injection Specifications

*No items available*

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

GitHub Actions

##### 2.3.10.1.2.0.0 Integration Type

OIDC

##### 2.3.10.1.3.0.0 Required Client Classes

- IdentityStack

##### 2.3.10.1.4.0.0 Configuration Requirements

GitHub Org/Repo in cdk.json

##### 2.3.10.1.5.0.0 Error Handling Requirements

None

##### 2.3.10.1.6.0.0 Authentication Requirements

IAM Trust Policy

##### 2.3.10.1.7.0.0 Framework Integration Patterns

OpenIdConnectProvider

##### 2.3.10.1.8.0.0 Validation Notes

Enables secure deployment.

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

AWS Services (Internal)

##### 2.3.10.2.2.0.0 Integration Type

VPC Endpoints

##### 2.3.10.2.3.0.0 Required Client Classes

- NetworkStack

##### 2.3.10.2.4.0.0 Configuration Requirements

None

##### 2.3.10.2.5.0.0 Error Handling Requirements

None

##### 2.3.10.2.6.0.0 Authentication Requirements

IAM Policy

##### 2.3.10.2.7.0.0 Framework Integration Patterns

InterfaceVpcEndpoint

##### 2.3.10.2.8.0.0 Validation Notes

Enhances security by keeping traffic off public internet.

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 3 |
| Total Enums | 0 |
| Total Dtos | 0 |
| Total Configurations | 2 |
| Total External Integrations | 2 |
| Grand Total Components | 19 |
| Phase 2 Claimed Count | 4 |
| Phase 2 Actual Count | 4 |
| Validation Added Count | 15 |
| Final Validated Count | 19 |

