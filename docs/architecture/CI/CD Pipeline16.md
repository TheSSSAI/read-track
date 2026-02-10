# 1 Pipelines

## 1.1 pipeline-backend-aws

### 1.1.1 Id

pipeline-backend-aws

### 1.1.2 Name

Backend API & Infrastructure Deployment

### 1.1.3 Description

Builds, tests, scans, and deploys the backend Node.js application and its AWS infrastructure using the AWS CDK, as required by REQ-CON-001 and REQ-TRN-001.

### 1.1.4 Stages

#### 1.1.4.1 Build & Unit Test

##### 1.1.4.1.1 Name

Build & Unit Test

##### 1.1.4.1.2 Steps

- npm ci
- npm run lint
- npm test -- --coverage

##### 1.1.4.1.3 Environment

###### 1.1.4.1.3.1 Node Version

18

##### 1.1.4.1.4.0 Quality Gates

- {'name': 'Code Coverage Check', 'criteria': ['coverage >= 80%'], 'blocking': True}

#### 1.1.4.2.0.0 Security Scan

##### 1.1.4.2.1.0 Name

Security Scan

##### 1.1.4.2.2.0 Steps

- npm audit --audit-level=high
- trivy fs .

##### 1.1.4.2.3.0 Environment

###### 1.1.4.2.3.1 Fail On

critical

##### 1.1.4.2.4.0 Quality Gates

- {'name': 'Vulnerability Check', 'criteria': ['zero critical CVEs'], 'blocking': True}

#### 1.1.4.3.0.0 Package & Push Container

##### 1.1.4.3.1.0 Name

Package & Push Container

##### 1.1.4.3.2.0 Steps

- docker build -t backend-service .
- aws ecr get-login-password --region eu-central-1 | docker login --username AWS --password-stdin <aws_account_id>.dkr.ecr.eu-central-1.amazonaws.com
- docker push <aws_account_id>.dkr.ecr.eu-central-1.amazonaws.com/backend-service:$CI_COMMIT_SHA

##### 1.1.4.3.3.0 Environment

###### 1.1.4.3.3.1 Aws Region

eu-central-1

#### 1.1.4.4.0.0 Deploy to Staging

##### 1.1.4.4.1.0 Name

Deploy to Staging

##### 1.1.4.4.2.0 Steps

- npm install -g aws-cdk
- cdk deploy StagingStack --require-approval never

##### 1.1.4.4.3.0 Environment

###### 1.1.4.4.3.1 Cdk Default Account

<aws_account_id>

###### 1.1.4.4.3.2 Cdk Default Region

eu-central-1

#### 1.1.4.5.0.0 End-to-End Test (Staging)

##### 1.1.4.5.1.0 Name

End-to-End Test (Staging)

##### 1.1.4.5.2.0 Steps

- run-e2e-tests --environment=staging --config=./e2e/config.json

##### 1.1.4.5.3.0 Environment

*No data available*

##### 1.1.4.5.4.0 Quality Gates

- {'name': 'Staging Environment Validation', 'criteria': ['API P95 latency < 200ms', 'zero P1/P2 incidents'], 'blocking': True}

#### 1.1.4.6.0.0 Manual Approval for Production

##### 1.1.4.6.1.0 Name

Manual Approval for Production

##### 1.1.4.6.2.0 Steps

- wait-for-manual-approval --timeout=4h

##### 1.1.4.6.3.0 Environment

###### 1.1.4.6.3.1 Notify Channel

slack#release-approvals

#### 1.1.4.7.0.0 Deploy to Production

##### 1.1.4.7.1.0 Name

Deploy to Production

##### 1.1.4.7.2.0 Steps

- cdk deploy ProductionStack --require-approval never --outputs-file ./cdk-outputs.json

##### 1.1.4.7.3.0 Environment

###### 1.1.4.7.3.1 Cdk Default Account

<aws_account_id>

###### 1.1.4.7.3.2 Cdk Default Region

eu-central-1

## 1.2.0.0.0.0 pipeline-flutter-mobile

### 1.2.1.0.0.0 Id

pipeline-flutter-mobile

### 1.2.2.0.0.0 Name

Flutter Mobile App Release

### 1.2.3.0.0.0 Description

Builds, tests, signs, and deploys the Flutter application to Google Play Store and Apple App Store, fulfilling the requirements in REQ-TRN-001.

### 1.2.4.0.0.0 Stages

#### 1.2.4.1.0.0 Setup & Test

##### 1.2.4.1.1.0 Name

Setup & Test

##### 1.2.4.1.2.0 Steps

- flutter pub get
- flutter analyze
- flutter test --coverage

##### 1.2.4.1.3.0 Environment

###### 1.2.4.1.3.1 Flutter Version

3.x

##### 1.2.4.1.4.0 Quality Gates

- {'name': 'Code Coverage & Linting', 'criteria': ['coverage >= 80%', 'zero linting errors'], 'blocking': True}

#### 1.2.4.2.0.0 Build & Sign Android

##### 1.2.4.2.1.0 Name

Build & Sign Android

##### 1.2.4.2.2.0 Steps

- echo $ANDROID_KEYSTORE_B64 | base64 --decode > key.jks
- flutter build appbundle --release

##### 1.2.4.2.3.0 Environment

| Property | Value |
|----------|-------|
| Keystore Password | $SECRET_KEYSTORE_PASSWORD |
| Key Alias | upload |
| Key Password | $SECRET_KEY_PASSWORD |

#### 1.2.4.3.0.0 Build & Sign iOS

##### 1.2.4.3.1.0 Name

Build & Sign iOS

##### 1.2.4.3.2.0 Steps

- fastlane match appstore
- fastlane gym --scheme Runner --export_method app-store

##### 1.2.4.3.3.0 Environment

###### 1.2.4.3.3.1 Match Git Url

🔗 [https://github.com/example/certs](https://github.com/example/certs)

###### 1.2.4.3.3.2 Match Password

$SECRET_MATCH_PASSWORD

#### 1.2.4.4.0.0 Deploy to Internal Testing

##### 1.2.4.4.1.0 Name

Deploy to Internal Testing

##### 1.2.4.4.2.0 Steps

- fastlane supply --aab build/app/outputs/bundle/release/app-release.aab --track internal
- fastlane pilot upload --ipa ./Runner.ipa

##### 1.2.4.4.3.0 Environment

###### 1.2.4.4.3.1 App Identifier

com.readtrack.app

#### 1.2.4.5.0.0 Manual Approval for Store Release

##### 1.2.4.5.1.0 Name

Manual Approval for Store Release

##### 1.2.4.5.2.0 Steps

- wait-for-manual-approval --reason='UAT on TestFlight/Internal Testing completed successfully.'

##### 1.2.4.5.3.0 Environment

*No data available*

#### 1.2.4.6.0.0 Submit to Production Stores

##### 1.2.4.6.1.0 Name

Submit to Production Stores

##### 1.2.4.6.2.0 Steps

- fastlane supply promote --track internal --to_track production
- fastlane deliver --submit_for_review true --force

##### 1.2.4.6.3.0 Environment

*No data available*

# 2.0.0.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Artifact Repository | {"docker": "Amazon ECR", "mobile": "Google Play Co... |
| Default Branch | main |
| Retention Policy | 90d |
| Notification Channel | slack#cicd-notifications |

