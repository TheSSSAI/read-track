# 1 Title

Application Primary Relational Database

# 2 Name

app_main_db

# 3 Db Type

- relational

# 4 Db Technology

PostgreSQL

# 5 Entities

## 5.1 User

### 5.1.1 Name

User

### 5.1.2 Description

Represents system users, their authentication details, and current subscription status. Compliant with hard-delete requirements for PII.

### 5.1.3 Attributes

#### 5.1.3.1 Guid

##### 5.1.3.1.1 Name

userId

##### 5.1.3.1.2 Type

🔹 Guid

##### 5.1.3.1.3 Is Required

✅ Yes

##### 5.1.3.1.4 Is Primary Key

✅ Yes

##### 5.1.3.1.5 Size

0

##### 5.1.3.1.6 Is Unique

✅ Yes

##### 5.1.3.1.7 Constraints

*No items available*

##### 5.1.3.1.8 Precision

0

##### 5.1.3.1.9 Scale

0

##### 5.1.3.1.10 Is Foreign Key

❌ No

#### 5.1.3.2.0 VARCHAR

##### 5.1.3.2.1 Name

auth0UserId

##### 5.1.3.2.2 Type

🔹 VARCHAR

##### 5.1.3.2.3 Is Required

✅ Yes

##### 5.1.3.2.4 Is Primary Key

❌ No

##### 5.1.3.2.5 Size

128

##### 5.1.3.2.6 Is Unique

✅ Yes

##### 5.1.3.2.7 Constraints

*No items available*

##### 5.1.3.2.8 Precision

0

##### 5.1.3.2.9 Scale

0

##### 5.1.3.2.10 Is Foreign Key

❌ No

#### 5.1.3.3.0 VARCHAR

##### 5.1.3.3.1 Name

email

##### 5.1.3.3.2 Type

🔹 VARCHAR

##### 5.1.3.3.3 Is Required

✅ Yes

##### 5.1.3.3.4 Is Primary Key

❌ No

##### 5.1.3.3.5 Size

255

##### 5.1.3.3.6 Is Unique

✅ Yes

##### 5.1.3.3.7 Constraints

- EMAIL_FORMAT

##### 5.1.3.3.8 Precision

0

##### 5.1.3.3.9 Scale

0

##### 5.1.3.3.10 Is Foreign Key

❌ No

#### 5.1.3.4.0 VARCHAR

##### 5.1.3.4.1 Name

displayName

##### 5.1.3.4.2 Type

🔹 VARCHAR

##### 5.1.3.4.3 Is Required

❌ No

##### 5.1.3.4.4 Is Primary Key

❌ No

##### 5.1.3.4.5 Size

100

##### 5.1.3.4.6 Is Unique

❌ No

##### 5.1.3.4.7 Constraints

*No items available*

##### 5.1.3.4.8 Precision

0

##### 5.1.3.4.9 Scale

0

##### 5.1.3.4.10 Is Foreign Key

❌ No

#### 5.1.3.5.0 VARCHAR

##### 5.1.3.5.1 Name

profilePictureUrl

##### 5.1.3.5.2 Type

🔹 VARCHAR

##### 5.1.3.5.3 Is Required

❌ No

##### 5.1.3.5.4 Is Primary Key

❌ No

##### 5.1.3.5.5 Size

512

##### 5.1.3.5.6 Is Unique

❌ No

##### 5.1.3.5.7 Constraints

*No items available*

##### 5.1.3.5.8 Precision

0

##### 5.1.3.5.9 Scale

0

##### 5.1.3.5.10 Is Foreign Key

❌ No

#### 5.1.3.6.0 VARCHAR

##### 5.1.3.6.1 Name

subscriptionTier

##### 5.1.3.6.2 Type

🔹 VARCHAR

##### 5.1.3.6.3 Is Required

✅ Yes

##### 5.1.3.6.4 Is Primary Key

❌ No

##### 5.1.3.6.5 Size

20

##### 5.1.3.6.6 Is Unique

❌ No

##### 5.1.3.6.7 Constraints

- ENUM('Free User', 'Premium User')
- DEFAULT 'Free User'

##### 5.1.3.6.8 Precision

0

##### 5.1.3.6.9 Scale

0

##### 5.1.3.6.10 Is Foreign Key

❌ No

#### 5.1.3.7.0 BOOLEAN

##### 5.1.3.7.1 Name

onboardingCompleted

##### 5.1.3.7.2 Type

🔹 BOOLEAN

##### 5.1.3.7.3 Is Required

✅ Yes

##### 5.1.3.7.4 Is Primary Key

❌ No

##### 5.1.3.7.5 Size

0

##### 5.1.3.7.6 Is Unique

❌ No

##### 5.1.3.7.7 Constraints

- DEFAULT false

##### 5.1.3.7.8 Precision

0

##### 5.1.3.7.9 Scale

0

##### 5.1.3.7.10 Is Foreign Key

❌ No

#### 5.1.3.8.0 DateTime

##### 5.1.3.8.1 Name

createdAt

##### 5.1.3.8.2 Type

🔹 DateTime

##### 5.1.3.8.3 Is Required

✅ Yes

##### 5.1.3.8.4 Is Primary Key

❌ No

##### 5.1.3.8.5 Size

0

##### 5.1.3.8.6 Is Unique

❌ No

##### 5.1.3.8.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.8.8 Precision

0

##### 5.1.3.8.9 Scale

0

##### 5.1.3.8.10 Is Foreign Key

❌ No

#### 5.1.3.9.0 DateTime

##### 5.1.3.9.1 Name

updatedAt

##### 5.1.3.9.2 Type

🔹 DateTime

##### 5.1.3.9.3 Is Required

✅ Yes

##### 5.1.3.9.4 Is Primary Key

❌ No

##### 5.1.3.9.5 Size

0

##### 5.1.3.9.6 Is Unique

❌ No

##### 5.1.3.9.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.9.8 Precision

0

##### 5.1.3.9.9 Scale

0

##### 5.1.3.9.10 Is Foreign Key

❌ No

### 5.1.4.0.0 Primary Keys

- userId

### 5.1.5.0.0 Unique Constraints

#### 5.1.5.1.0 UC_User_Email

##### 5.1.5.1.1 Name

UC_User_Email

##### 5.1.5.1.2 Columns

- email

#### 5.1.5.2.0 UC_User_Auth0UserId

##### 5.1.5.2.1 Name

UC_User_Auth0UserId

##### 5.1.5.2.2 Columns

- auth0UserId

### 5.1.6.0.0 Indexes

#### 5.1.6.1.0 BTree

##### 5.1.6.1.1 Name

IX_User_Email

##### 5.1.6.1.2 Columns

- email

##### 5.1.6.1.3 Type

🔹 BTree

#### 5.1.6.2.0 BTree

##### 5.1.6.2.1 Name

IX_User_SubscriptionTier

##### 5.1.6.2.2 Columns

- subscriptionTier

##### 5.1.6.2.3 Type

🔹 BTree

## 5.2.0.0.0 Subscription

### 5.2.1.0.0 Name

Subscription

### 5.2.2.0.0 Description

Stores the history and status of user subscriptions from payment providers.

### 5.2.3.0.0 Attributes

#### 5.2.3.1.0 Guid

##### 5.2.3.1.1 Name

subscriptionId

##### 5.2.3.1.2 Type

🔹 Guid

##### 5.2.3.1.3 Is Required

✅ Yes

##### 5.2.3.1.4 Is Primary Key

✅ Yes

##### 5.2.3.1.5 Size

0

##### 5.2.3.1.6 Is Unique

✅ Yes

##### 5.2.3.1.7 Constraints

*No items available*

##### 5.2.3.1.8 Precision

0

##### 5.2.3.1.9 Scale

0

##### 5.2.3.1.10 Is Foreign Key

❌ No

#### 5.2.3.2.0 Guid

##### 5.2.3.2.1 Name

UserId

##### 5.2.3.2.2 Type

🔹 Guid

##### 5.2.3.2.3 Is Required

✅ Yes

##### 5.2.3.2.4 Is Primary Key

❌ No

##### 5.2.3.2.5 Size

0

##### 5.2.3.2.6 Is Unique

❌ No

##### 5.2.3.2.7 Constraints

*No items available*

##### 5.2.3.2.8 Precision

0

##### 5.2.3.2.9 Scale

0

##### 5.2.3.2.10 Is Foreign Key

✅ Yes

#### 5.2.3.3.0 VARCHAR

##### 5.2.3.3.1 Name

status

##### 5.2.3.3.2 Type

🔹 VARCHAR

##### 5.2.3.3.3 Is Required

✅ Yes

##### 5.2.3.3.4 Is Primary Key

❌ No

##### 5.2.3.3.5 Size

20

##### 5.2.3.3.6 Is Unique

❌ No

##### 5.2.3.3.7 Constraints

- ENUM('Active', 'Canceled', 'Expired')

##### 5.2.3.3.8 Precision

0

##### 5.2.3.3.9 Scale

0

##### 5.2.3.3.10 Is Foreign Key

❌ No

#### 5.2.3.4.0 VARCHAR

##### 5.2.3.4.1 Name

provider

##### 5.2.3.4.2 Type

🔹 VARCHAR

##### 5.2.3.4.3 Is Required

✅ Yes

##### 5.2.3.4.4 Is Primary Key

❌ No

##### 5.2.3.4.5 Size

20

##### 5.2.3.4.6 Is Unique

❌ No

##### 5.2.3.4.7 Constraints

- ENUM('AppleAppStore', 'GooglePlayStore')

##### 5.2.3.4.8 Precision

0

##### 5.2.3.4.9 Scale

0

##### 5.2.3.4.10 Is Foreign Key

❌ No

#### 5.2.3.5.0 VARCHAR

##### 5.2.3.5.1 Name

providerTransactionId

##### 5.2.3.5.2 Type

🔹 VARCHAR

##### 5.2.3.5.3 Is Required

✅ Yes

##### 5.2.3.5.4 Is Primary Key

❌ No

##### 5.2.3.5.5 Size

255

##### 5.2.3.5.6 Is Unique

✅ Yes

##### 5.2.3.5.7 Constraints

*No items available*

##### 5.2.3.5.8 Precision

0

##### 5.2.3.5.9 Scale

0

##### 5.2.3.5.10 Is Foreign Key

❌ No

#### 5.2.3.6.0 DateTime

##### 5.2.3.6.1 Name

startDate

##### 5.2.3.6.2 Type

🔹 DateTime

##### 5.2.3.6.3 Is Required

✅ Yes

##### 5.2.3.6.4 Is Primary Key

❌ No

##### 5.2.3.6.5 Size

0

##### 5.2.3.6.6 Is Unique

❌ No

##### 5.2.3.6.7 Constraints

*No items available*

##### 5.2.3.6.8 Precision

0

##### 5.2.3.6.9 Scale

0

##### 5.2.3.6.10 Is Foreign Key

❌ No

#### 5.2.3.7.0 DateTime

##### 5.2.3.7.1 Name

endDate

##### 5.2.3.7.2 Type

🔹 DateTime

##### 5.2.3.7.3 Is Required

✅ Yes

##### 5.2.3.7.4 Is Primary Key

❌ No

##### 5.2.3.7.5 Size

0

##### 5.2.3.7.6 Is Unique

❌ No

##### 5.2.3.7.7 Constraints

*No items available*

##### 5.2.3.7.8 Precision

0

##### 5.2.3.7.9 Scale

0

##### 5.2.3.7.10 Is Foreign Key

❌ No

#### 5.2.3.8.0 DateTime

##### 5.2.3.8.1 Name

createdAt

##### 5.2.3.8.2 Type

🔹 DateTime

##### 5.2.3.8.3 Is Required

✅ Yes

##### 5.2.3.8.4 Is Primary Key

❌ No

##### 5.2.3.8.5 Size

0

##### 5.2.3.8.6 Is Unique

❌ No

##### 5.2.3.8.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.2.3.8.8 Precision

0

##### 5.2.3.8.9 Scale

0

##### 5.2.3.8.10 Is Foreign Key

❌ No

#### 5.2.3.9.0 DateTime

##### 5.2.3.9.1 Name

updatedAt

##### 5.2.3.9.2 Type

🔹 DateTime

##### 5.2.3.9.3 Is Required

✅ Yes

##### 5.2.3.9.4 Is Primary Key

❌ No

##### 5.2.3.9.5 Size

0

##### 5.2.3.9.6 Is Unique

❌ No

##### 5.2.3.9.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.2.3.9.8 Precision

0

##### 5.2.3.9.9 Scale

0

##### 5.2.3.9.10 Is Foreign Key

❌ No

### 5.2.4.0.0 Primary Keys

- subscriptionId

### 5.2.5.0.0 Unique Constraints

- {'name': 'UC_Subscription_ProviderTransactionId', 'columns': ['providerTransactionId']}

### 5.2.6.0.0 Indexes

#### 5.2.6.1.0 BTree

##### 5.2.6.1.1 Name

IX_Subscription_UserId_Status

##### 5.2.6.1.2 Columns

- UserId
- status

##### 5.2.6.1.3 Type

🔹 BTree

#### 5.2.6.2.0 BTree

##### 5.2.6.2.1 Name

IX_Subscription_EndDate

##### 5.2.6.2.2 Columns

- endDate

##### 5.2.6.2.3 Type

🔹 BTree

#### 5.2.6.3.0 BTree

##### 5.2.6.3.1 Name

IX_Subscription_Status_EndDate

##### 5.2.6.3.2 Columns

- status
- endDate

##### 5.2.6.3.3 Type

🔹 BTree

## 5.3.0.0.0 Book

### 5.3.1.0.0 Name

Book

### 5.3.2.0.0 Description

A master table of all books known to the system, typically populated from an external API like Google Books.

### 5.3.3.0.0 Attributes

#### 5.3.3.1.0 Guid

##### 5.3.3.1.1 Name

bookId

##### 5.3.3.1.2 Type

🔹 Guid

##### 5.3.3.1.3 Is Required

✅ Yes

##### 5.3.3.1.4 Is Primary Key

✅ Yes

##### 5.3.3.1.5 Size

0

##### 5.3.3.1.6 Is Unique

✅ Yes

##### 5.3.3.1.7 Constraints

*No items available*

##### 5.3.3.1.8 Precision

0

##### 5.3.3.1.9 Scale

0

##### 5.3.3.1.10 Is Foreign Key

❌ No

#### 5.3.3.2.0 VARCHAR

##### 5.3.3.2.1 Name

googleBooksId

##### 5.3.3.2.2 Type

🔹 VARCHAR

##### 5.3.3.2.3 Is Required

❌ No

##### 5.3.3.2.4 Is Primary Key

❌ No

##### 5.3.3.2.5 Size

50

##### 5.3.3.2.6 Is Unique

✅ Yes

##### 5.3.3.2.7 Constraints

*No items available*

##### 5.3.3.2.8 Precision

0

##### 5.3.3.2.9 Scale

0

##### 5.3.3.2.10 Is Foreign Key

❌ No

#### 5.3.3.3.0 VARCHAR

##### 5.3.3.3.1 Name

title

##### 5.3.3.3.2 Type

🔹 VARCHAR

##### 5.3.3.3.3 Is Required

✅ Yes

##### 5.3.3.3.4 Is Primary Key

❌ No

##### 5.3.3.3.5 Size

255

##### 5.3.3.3.6 Is Unique

❌ No

##### 5.3.3.3.7 Constraints

*No items available*

##### 5.3.3.3.8 Precision

0

##### 5.3.3.3.9 Scale

0

##### 5.3.3.3.10 Is Foreign Key

❌ No

#### 5.3.3.4.0 VARCHAR

##### 5.3.3.4.1 Name

author

##### 5.3.3.4.2 Type

🔹 VARCHAR

##### 5.3.3.4.3 Is Required

✅ Yes

##### 5.3.3.4.4 Is Primary Key

❌ No

##### 5.3.3.4.5 Size

255

##### 5.3.3.4.6 Is Unique

❌ No

##### 5.3.3.4.7 Constraints

*No items available*

##### 5.3.3.4.8 Precision

0

##### 5.3.3.4.9 Scale

0

##### 5.3.3.4.10 Is Foreign Key

❌ No

#### 5.3.3.5.0 INT

##### 5.3.3.5.1 Name

pageCount

##### 5.3.3.5.2 Type

🔹 INT

##### 5.3.3.5.3 Is Required

❌ No

##### 5.3.3.5.4 Is Primary Key

❌ No

##### 5.3.3.5.5 Size

0

##### 5.3.3.5.6 Is Unique

❌ No

##### 5.3.3.5.7 Constraints

- CHECK (pageCount IS NULL OR pageCount >= 0)

##### 5.3.3.5.8 Precision

0

##### 5.3.3.5.9 Scale

0

##### 5.3.3.5.10 Is Foreign Key

❌ No

#### 5.3.3.6.0 VARCHAR

##### 5.3.3.6.1 Name

coverImageUrl

##### 5.3.3.6.2 Type

🔹 VARCHAR

##### 5.3.3.6.3 Is Required

❌ No

##### 5.3.3.6.4 Is Primary Key

❌ No

##### 5.3.3.6.5 Size

512

##### 5.3.3.6.6 Is Unique

❌ No

##### 5.3.3.6.7 Constraints

*No items available*

##### 5.3.3.6.8 Precision

0

##### 5.3.3.6.9 Scale

0

##### 5.3.3.6.10 Is Foreign Key

❌ No

#### 5.3.3.7.0 DateTime

##### 5.3.3.7.1 Name

createdAt

##### 5.3.3.7.2 Type

🔹 DateTime

##### 5.3.3.7.3 Is Required

✅ Yes

##### 5.3.3.7.4 Is Primary Key

❌ No

##### 5.3.3.7.5 Size

0

##### 5.3.3.7.6 Is Unique

❌ No

##### 5.3.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.3.3.7.8 Precision

0

##### 5.3.3.7.9 Scale

0

##### 5.3.3.7.10 Is Foreign Key

❌ No

#### 5.3.3.8.0 DateTime

##### 5.3.3.8.1 Name

updatedAt

##### 5.3.3.8.2 Type

🔹 DateTime

##### 5.3.3.8.3 Is Required

✅ Yes

##### 5.3.3.8.4 Is Primary Key

❌ No

##### 5.3.3.8.5 Size

0

##### 5.3.3.8.6 Is Unique

❌ No

##### 5.3.3.8.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.3.3.8.8 Precision

0

##### 5.3.3.8.9 Scale

0

##### 5.3.3.8.10 Is Foreign Key

❌ No

### 5.3.4.0.0 Primary Keys

- bookId

### 5.3.5.0.0 Unique Constraints

- {'name': 'UC_Book_GoogleBooksId', 'columns': ['googleBooksId']}

### 5.3.6.0.0 Indexes

#### 5.3.6.1.0 BTree

##### 5.3.6.1.1 Name

IX_Book_GoogleBooksId

##### 5.3.6.1.2 Columns

- googleBooksId

##### 5.3.6.1.3 Type

🔹 BTree

#### 5.3.6.2.0 BTree

##### 5.3.6.2.1 Name

IX_Book_Title_Author

##### 5.3.6.2.2 Columns

- title
- author

##### 5.3.6.2.3 Type

🔹 BTree

## 5.4.0.0.0 LibraryItem

### 5.4.1.0.0 Name

LibraryItem

### 5.4.2.0.0 Description

Represents a book or article in a user's personal library. This table combines the concept of UserBook for both item types.

### 5.4.3.0.0 Attributes

#### 5.4.3.1.0 Guid

##### 5.4.3.1.1 Name

libraryItemId

##### 5.4.3.1.2 Type

🔹 Guid

##### 5.4.3.1.3 Is Required

✅ Yes

##### 5.4.3.1.4 Is Primary Key

✅ Yes

##### 5.4.3.1.5 Size

0

##### 5.4.3.1.6 Is Unique

✅ Yes

##### 5.4.3.1.7 Constraints

*No items available*

##### 5.4.3.1.8 Precision

0

##### 5.4.3.1.9 Scale

0

##### 5.4.3.1.10 Is Foreign Key

❌ No

#### 5.4.3.2.0 Guid

##### 5.4.3.2.1 Name

UserId

##### 5.4.3.2.2 Type

🔹 Guid

##### 5.4.3.2.3 Is Required

✅ Yes

##### 5.4.3.2.4 Is Primary Key

❌ No

##### 5.4.3.2.5 Size

0

##### 5.4.3.2.6 Is Unique

❌ No

##### 5.4.3.2.7 Constraints

*No items available*

##### 5.4.3.2.8 Precision

0

##### 5.4.3.2.9 Scale

0

##### 5.4.3.2.10 Is Foreign Key

✅ Yes

#### 5.4.3.3.0 Guid

##### 5.4.3.3.1 Name

BookId

##### 5.4.3.3.2 Type

🔹 Guid

##### 5.4.3.3.3 Is Required

❌ No

##### 5.4.3.3.4 Is Primary Key

❌ No

##### 5.4.3.3.5 Size

0

##### 5.4.3.3.6 Is Unique

❌ No

##### 5.4.3.3.7 Constraints

*No items available*

##### 5.4.3.3.8 Precision

0

##### 5.4.3.3.9 Scale

0

##### 5.4.3.3.10 Is Foreign Key

✅ Yes

#### 5.4.3.4.0 VARCHAR

##### 5.4.3.4.1 Name

itemType

##### 5.4.3.4.2 Type

🔹 VARCHAR

##### 5.4.3.4.3 Is Required

✅ Yes

##### 5.4.3.4.4 Is Primary Key

❌ No

##### 5.4.3.4.5 Size

20

##### 5.4.3.4.6 Is Unique

❌ No

##### 5.4.3.4.7 Constraints

- ENUM('Book', 'Article')

##### 5.4.3.4.8 Precision

0

##### 5.4.3.4.9 Scale

0

##### 5.4.3.4.10 Is Foreign Key

❌ No

#### 5.4.3.5.0 VARCHAR

##### 5.4.3.5.1 Name

title

##### 5.4.3.5.2 Type

🔹 VARCHAR

##### 5.4.3.5.3 Is Required

✅ Yes

##### 5.4.3.5.4 Is Primary Key

❌ No

##### 5.4.3.5.5 Size

255

##### 5.4.3.5.6 Is Unique

❌ No

##### 5.4.3.5.7 Constraints

*No items available*

##### 5.4.3.5.8 Precision

0

##### 5.4.3.5.9 Scale

0

##### 5.4.3.5.10 Is Foreign Key

❌ No

#### 5.4.3.6.0 VARCHAR

##### 5.4.3.6.1 Name

author

##### 5.4.3.6.2 Type

🔹 VARCHAR

##### 5.4.3.6.3 Is Required

❌ No

##### 5.4.3.6.4 Is Primary Key

❌ No

##### 5.4.3.6.5 Size

255

##### 5.4.3.6.6 Is Unique

❌ No

##### 5.4.3.6.7 Constraints

*No items available*

##### 5.4.3.6.8 Precision

0

##### 5.4.3.6.9 Scale

0

##### 5.4.3.6.10 Is Foreign Key

❌ No

#### 5.4.3.7.0 VARCHAR

##### 5.4.3.7.1 Name

coverImageUrl

##### 5.4.3.7.2 Type

🔹 VARCHAR

##### 5.4.3.7.3 Is Required

❌ No

##### 5.4.3.7.4 Is Primary Key

❌ No

##### 5.4.3.7.5 Size

512

##### 5.4.3.7.6 Is Unique

❌ No

##### 5.4.3.7.7 Constraints

*No items available*

##### 5.4.3.7.8 Precision

0

##### 5.4.3.7.9 Scale

0

##### 5.4.3.7.10 Is Foreign Key

❌ No

#### 5.4.3.8.0 VARCHAR

##### 5.4.3.8.1 Name

url

##### 5.4.3.8.2 Type

🔹 VARCHAR

##### 5.4.3.8.3 Is Required

❌ No

##### 5.4.3.8.4 Is Primary Key

❌ No

##### 5.4.3.8.5 Size

512

##### 5.4.3.8.6 Is Unique

❌ No

##### 5.4.3.8.7 Constraints

*No items available*

##### 5.4.3.8.8 Precision

0

##### 5.4.3.8.9 Scale

0

##### 5.4.3.8.10 Is Foreign Key

❌ No

#### 5.4.3.9.0 VARCHAR

##### 5.4.3.9.1 Name

shelf

##### 5.4.3.9.2 Type

🔹 VARCHAR

##### 5.4.3.9.3 Is Required

✅ Yes

##### 5.4.3.9.4 Is Primary Key

❌ No

##### 5.4.3.9.5 Size

30

##### 5.4.3.9.6 Is Unique

❌ No

##### 5.4.3.9.7 Constraints

- ENUM('WantToRead', 'CurrentlyReading', 'Read', 'DidNotFinish')
- DEFAULT 'WantToRead'

##### 5.4.3.9.8 Precision

0

##### 5.4.3.9.9 Scale

0

##### 5.4.3.9.10 Is Foreign Key

❌ No

#### 5.4.3.10.0 DateTime

##### 5.4.3.10.1 Name

addedAt

##### 5.4.3.10.2 Type

🔹 DateTime

##### 5.4.3.10.3 Is Required

✅ Yes

##### 5.4.3.10.4 Is Primary Key

❌ No

##### 5.4.3.10.5 Size

0

##### 5.4.3.10.6 Is Unique

❌ No

##### 5.4.3.10.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.4.3.10.8 Precision

0

##### 5.4.3.10.9 Scale

0

##### 5.4.3.10.10 Is Foreign Key

❌ No

#### 5.4.3.11.0 DateTime

##### 5.4.3.11.1 Name

updatedAt

##### 5.4.3.11.2 Type

🔹 DateTime

##### 5.4.3.11.3 Is Required

✅ Yes

##### 5.4.3.11.4 Is Primary Key

❌ No

##### 5.4.3.11.5 Size

0

##### 5.4.3.11.6 Is Unique

❌ No

##### 5.4.3.11.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.4.3.11.8 Precision

0

##### 5.4.3.11.9 Scale

0

##### 5.4.3.11.10 Is Foreign Key

❌ No

### 5.4.4.0.0 Primary Keys

- libraryItemId

### 5.4.5.0.0 Unique Constraints

*No items available*

### 5.4.6.0.0 Indexes

#### 5.4.6.1.0 BTree

##### 5.4.6.1.1 Name

IX_LibraryItem_UserId_Shelf

##### 5.4.6.1.2 Columns

- UserId
- shelf

##### 5.4.6.1.3 Type

🔹 BTree

#### 5.4.6.2.0 BTree

##### 5.4.6.2.1 Name

IX_LibraryItem_BookId

##### 5.4.6.2.2 Columns

- BookId

##### 5.4.6.2.3 Type

🔹 BTree

#### 5.4.6.3.0 Filtered Unique BTree (WHERE BookId IS NOT NULL)

##### 5.4.6.3.1 Name

UC_LibraryItem_User_Book

##### 5.4.6.3.2 Columns

- UserId
- BookId

##### 5.4.6.3.3 Type

🔹 Filtered Unique BTree (WHERE BookId IS NOT NULL)

## 5.5.0.0.0 ReadingSession

### 5.5.1.0.0 Name

ReadingSession

### 5.5.2.0.0 Description

Logs individual reading sessions for an item in a user's library. Partitioned for performance.

### 5.5.3.0.0 Attributes

#### 5.5.3.1.0 Guid

##### 5.5.3.1.1 Name

readingSessionId

##### 5.5.3.1.2 Type

🔹 Guid

##### 5.5.3.1.3 Is Required

✅ Yes

##### 5.5.3.1.4 Is Primary Key

✅ Yes

##### 5.5.3.1.5 Size

0

##### 5.5.3.1.6 Is Unique

✅ Yes

##### 5.5.3.1.7 Constraints

*No items available*

##### 5.5.3.1.8 Precision

0

##### 5.5.3.1.9 Scale

0

##### 5.5.3.1.10 Is Foreign Key

❌ No

#### 5.5.3.2.0 Guid

##### 5.5.3.2.1 Name

LibraryItemId

##### 5.5.3.2.2 Type

🔹 Guid

##### 5.5.3.2.3 Is Required

✅ Yes

##### 5.5.3.2.4 Is Primary Key

❌ No

##### 5.5.3.2.5 Size

0

##### 5.5.3.2.6 Is Unique

❌ No

##### 5.5.3.2.7 Constraints

*No items available*

##### 5.5.3.2.8 Precision

0

##### 5.5.3.2.9 Scale

0

##### 5.5.3.2.10 Is Foreign Key

✅ Yes

#### 5.5.3.3.0 Guid

##### 5.5.3.3.1 Name

UserId

##### 5.5.3.3.2 Type

🔹 Guid

##### 5.5.3.3.3 Is Required

✅ Yes

##### 5.5.3.3.4 Is Primary Key

❌ No

##### 5.5.3.3.5 Size

0

##### 5.5.3.3.6 Is Unique

❌ No

##### 5.5.3.3.7 Constraints

*No items available*

##### 5.5.3.3.8 Precision

0

##### 5.5.3.3.9 Scale

0

##### 5.5.3.3.10 Is Foreign Key

✅ Yes

#### 5.5.3.4.0 DateTime

##### 5.5.3.4.1 Name

startTime

##### 5.5.3.4.2 Type

🔹 DateTime

##### 5.5.3.4.3 Is Required

✅ Yes

##### 5.5.3.4.4 Is Primary Key

❌ No

##### 5.5.3.4.5 Size

0

##### 5.5.3.4.6 Is Unique

❌ No

##### 5.5.3.4.7 Constraints

*No items available*

##### 5.5.3.4.8 Precision

0

##### 5.5.3.4.9 Scale

0

##### 5.5.3.4.10 Is Foreign Key

❌ No

#### 5.5.3.5.0 DateTime

##### 5.5.3.5.1 Name

endTime

##### 5.5.3.5.2 Type

🔹 DateTime

##### 5.5.3.5.3 Is Required

✅ Yes

##### 5.5.3.5.4 Is Primary Key

❌ No

##### 5.5.3.5.5 Size

0

##### 5.5.3.5.6 Is Unique

❌ No

##### 5.5.3.5.7 Constraints

*No items available*

##### 5.5.3.5.8 Precision

0

##### 5.5.3.5.9 Scale

0

##### 5.5.3.5.10 Is Foreign Key

❌ No

#### 5.5.3.6.0 INT

##### 5.5.3.6.1 Name

pagesRead

##### 5.5.3.6.2 Type

🔹 INT

##### 5.5.3.6.3 Is Required

❌ No

##### 5.5.3.6.4 Is Primary Key

❌ No

##### 5.5.3.6.5 Size

0

##### 5.5.3.6.6 Is Unique

❌ No

##### 5.5.3.6.7 Constraints

- CHECK (pagesRead IS NULL OR pagesRead >= 0)

##### 5.5.3.6.8 Precision

0

##### 5.5.3.6.9 Scale

0

##### 5.5.3.6.10 Is Foreign Key

❌ No

#### 5.5.3.7.0 TEXT

##### 5.5.3.7.1 Name

notes

##### 5.5.3.7.2 Type

🔹 TEXT

##### 5.5.3.7.3 Is Required

❌ No

##### 5.5.3.7.4 Is Primary Key

❌ No

##### 5.5.3.7.5 Size

0

##### 5.5.3.7.6 Is Unique

❌ No

##### 5.5.3.7.7 Constraints

*No items available*

##### 5.5.3.7.8 Precision

0

##### 5.5.3.7.9 Scale

0

##### 5.5.3.7.10 Is Foreign Key

❌ No

#### 5.5.3.8.0 DateTime

##### 5.5.3.8.1 Name

createdAt

##### 5.5.3.8.2 Type

🔹 DateTime

##### 5.5.3.8.3 Is Required

✅ Yes

##### 5.5.3.8.4 Is Primary Key

❌ No

##### 5.5.3.8.5 Size

0

##### 5.5.3.8.6 Is Unique

❌ No

##### 5.5.3.8.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.5.3.8.8 Precision

0

##### 5.5.3.8.9 Scale

0

##### 5.5.3.8.10 Is Foreign Key

❌ No

#### 5.5.3.9.0 DateTime

##### 5.5.3.9.1 Name

updatedAt

##### 5.5.3.9.2 Type

🔹 DateTime

##### 5.5.3.9.3 Is Required

✅ Yes

##### 5.5.3.9.4 Is Primary Key

❌ No

##### 5.5.3.9.5 Size

0

##### 5.5.3.9.6 Is Unique

❌ No

##### 5.5.3.9.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.5.3.9.8 Precision

0

##### 5.5.3.9.9 Scale

0

##### 5.5.3.9.10 Is Foreign Key

❌ No

### 5.5.4.0.0 Primary Keys

- readingSessionId
- startTime

### 5.5.5.0.0 Unique Constraints

*No items available*

### 5.5.6.0.0 Indexes

#### 5.5.6.1.0 BTree

##### 5.5.6.1.1 Name

IX_ReadingSession_LibraryItemId

##### 5.5.6.1.2 Columns

- LibraryItemId

##### 5.5.6.1.3 Type

🔹 BTree

#### 5.5.6.2.0 BTree

##### 5.5.6.2.1 Name

IX_ReadingSession_UserId_StartTime

##### 5.5.6.2.2 Columns

- UserId
- startTime

##### 5.5.6.2.3 Type

🔹 BTree

## 5.6.0.0.0 Goal

### 5.6.1.0.0 Name

Goal

### 5.6.2.0.0 Description

Stores user-defined reading goals and tracks their progress.

### 5.6.3.0.0 Attributes

#### 5.6.3.1.0 Guid

##### 5.6.3.1.1 Name

goalId

##### 5.6.3.1.2 Type

🔹 Guid

##### 5.6.3.1.3 Is Required

✅ Yes

##### 5.6.3.1.4 Is Primary Key

✅ Yes

##### 5.6.3.1.5 Size

0

##### 5.6.3.1.6 Is Unique

✅ Yes

##### 5.6.3.1.7 Constraints

*No items available*

##### 5.6.3.1.8 Precision

0

##### 5.6.3.1.9 Scale

0

##### 5.6.3.1.10 Is Foreign Key

❌ No

#### 5.6.3.2.0 Guid

##### 5.6.3.2.1 Name

UserId

##### 5.6.3.2.2 Type

🔹 Guid

##### 5.6.3.2.3 Is Required

✅ Yes

##### 5.6.3.2.4 Is Primary Key

❌ No

##### 5.6.3.2.5 Size

0

##### 5.6.3.2.6 Is Unique

❌ No

##### 5.6.3.2.7 Constraints

*No items available*

##### 5.6.3.2.8 Precision

0

##### 5.6.3.2.9 Scale

0

##### 5.6.3.2.10 Is Foreign Key

✅ Yes

#### 5.6.3.3.0 VARCHAR

##### 5.6.3.3.1 Name

goalType

##### 5.6.3.3.2 Type

🔹 VARCHAR

##### 5.6.3.3.3 Is Required

✅ Yes

##### 5.6.3.3.4 Is Primary Key

❌ No

##### 5.6.3.3.5 Size

20

##### 5.6.3.3.6 Is Unique

❌ No

##### 5.6.3.3.7 Constraints

- ENUM('Books', 'Pages', 'Time')

##### 5.6.3.3.8 Precision

0

##### 5.6.3.3.9 Scale

0

##### 5.6.3.3.10 Is Foreign Key

❌ No

#### 5.6.3.4.0 VARCHAR

##### 5.6.3.4.1 Name

period

##### 5.6.3.4.2 Type

🔹 VARCHAR

##### 5.6.3.4.3 Is Required

✅ Yes

##### 5.6.3.4.4 Is Primary Key

❌ No

##### 5.6.3.4.5 Size

20

##### 5.6.3.4.6 Is Unique

❌ No

##### 5.6.3.4.7 Constraints

- ENUM('Day', 'Week', 'Month', 'Year')

##### 5.6.3.4.8 Precision

0

##### 5.6.3.4.9 Scale

0

##### 5.6.3.4.10 Is Foreign Key

❌ No

#### 5.6.3.5.0 INT

##### 5.6.3.5.1 Name

targetValue

##### 5.6.3.5.2 Type

🔹 INT

##### 5.6.3.5.3 Is Required

✅ Yes

##### 5.6.3.5.4 Is Primary Key

❌ No

##### 5.6.3.5.5 Size

0

##### 5.6.3.5.6 Is Unique

❌ No

##### 5.6.3.5.7 Constraints

- CHECK (targetValue > 0)

##### 5.6.3.5.8 Precision

0

##### 5.6.3.5.9 Scale

0

##### 5.6.3.5.10 Is Foreign Key

❌ No

#### 5.6.3.6.0 INT

##### 5.6.3.6.1 Name

currentValue

##### 5.6.3.6.2 Type

🔹 INT

##### 5.6.3.6.3 Is Required

✅ Yes

##### 5.6.3.6.4 Is Primary Key

❌ No

##### 5.6.3.6.5 Size

0

##### 5.6.3.6.6 Is Unique

❌ No

##### 5.6.3.6.7 Constraints

- CHECK (currentValue >= 0)
- DEFAULT 0

##### 5.6.3.6.8 Precision

0

##### 5.6.3.6.9 Scale

0

##### 5.6.3.6.10 Is Foreign Key

❌ No

#### 5.6.3.7.0 DateTime

##### 5.6.3.7.1 Name

startDate

##### 5.6.3.7.2 Type

🔹 DateTime

##### 5.6.3.7.3 Is Required

✅ Yes

##### 5.6.3.7.4 Is Primary Key

❌ No

##### 5.6.3.7.5 Size

0

##### 5.6.3.7.6 Is Unique

❌ No

##### 5.6.3.7.7 Constraints

*No items available*

##### 5.6.3.7.8 Precision

0

##### 5.6.3.7.9 Scale

0

##### 5.6.3.7.10 Is Foreign Key

❌ No

#### 5.6.3.8.0 DateTime

##### 5.6.3.8.1 Name

endDate

##### 5.6.3.8.2 Type

🔹 DateTime

##### 5.6.3.8.3 Is Required

✅ Yes

##### 5.6.3.8.4 Is Primary Key

❌ No

##### 5.6.3.8.5 Size

0

##### 5.6.3.8.6 Is Unique

❌ No

##### 5.6.3.8.7 Constraints

*No items available*

##### 5.6.3.8.8 Precision

0

##### 5.6.3.8.9 Scale

0

##### 5.6.3.8.10 Is Foreign Key

❌ No

#### 5.6.3.9.0 BOOLEAN

##### 5.6.3.9.1 Name

isActive

##### 5.6.3.9.2 Type

🔹 BOOLEAN

##### 5.6.3.9.3 Is Required

✅ Yes

##### 5.6.3.9.4 Is Primary Key

❌ No

##### 5.6.3.9.5 Size

0

##### 5.6.3.9.6 Is Unique

❌ No

##### 5.6.3.9.7 Constraints

- DEFAULT true

##### 5.6.3.9.8 Precision

0

##### 5.6.3.9.9 Scale

0

##### 5.6.3.9.10 Is Foreign Key

❌ No

#### 5.6.3.10.0 DateTime

##### 5.6.3.10.1 Name

createdAt

##### 5.6.3.10.2 Type

🔹 DateTime

##### 5.6.3.10.3 Is Required

✅ Yes

##### 5.6.3.10.4 Is Primary Key

❌ No

##### 5.6.3.10.5 Size

0

##### 5.6.3.10.6 Is Unique

❌ No

##### 5.6.3.10.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.6.3.10.8 Precision

0

##### 5.6.3.10.9 Scale

0

##### 5.6.3.10.10 Is Foreign Key

❌ No

#### 5.6.3.11.0 DateTime

##### 5.6.3.11.1 Name

updatedAt

##### 5.6.3.11.2 Type

🔹 DateTime

##### 5.6.3.11.3 Is Required

✅ Yes

##### 5.6.3.11.4 Is Primary Key

❌ No

##### 5.6.3.11.5 Size

0

##### 5.6.3.11.6 Is Unique

❌ No

##### 5.6.3.11.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.6.3.11.8 Precision

0

##### 5.6.3.11.9 Scale

0

##### 5.6.3.11.10 Is Foreign Key

❌ No

### 5.6.4.0.0 Primary Keys

- goalId

### 5.6.5.0.0 Unique Constraints

- {'name': 'UC_Goal_User_Active_Type_Period_Start', 'columns': ['UserId', 'goalType', 'period', 'startDate', 'isActive']}

### 5.6.6.0.0 Indexes

- {'name': 'IX_Goal_UserId_IsActive', 'columns': ['UserId', 'isActive'], 'type': 'BTree'}

## 5.7.0.0.0 DailyTask

### 5.7.1.0.0 Name

DailyTask

### 5.7.2.0.0 Description

Defines a custom, recurring daily task for a user.

### 5.7.3.0.0 Attributes

#### 5.7.3.1.0 Guid

##### 5.7.3.1.1 Name

dailyTaskId

##### 5.7.3.1.2 Type

🔹 Guid

##### 5.7.3.1.3 Is Required

✅ Yes

##### 5.7.3.1.4 Is Primary Key

✅ Yes

##### 5.7.3.1.5 Size

0

##### 5.7.3.1.6 Is Unique

✅ Yes

##### 5.7.3.1.7 Constraints

*No items available*

##### 5.7.3.1.8 Precision

0

##### 5.7.3.1.9 Scale

0

##### 5.7.3.1.10 Is Foreign Key

❌ No

#### 5.7.3.2.0 Guid

##### 5.7.3.2.1 Name

UserId

##### 5.7.3.2.2 Type

🔹 Guid

##### 5.7.3.2.3 Is Required

✅ Yes

##### 5.7.3.2.4 Is Primary Key

❌ No

##### 5.7.3.2.5 Size

0

##### 5.7.3.2.6 Is Unique

❌ No

##### 5.7.3.2.7 Constraints

*No items available*

##### 5.7.3.2.8 Precision

0

##### 5.7.3.2.9 Scale

0

##### 5.7.3.2.10 Is Foreign Key

✅ Yes

#### 5.7.3.3.0 VARCHAR

##### 5.7.3.3.1 Name

description

##### 5.7.3.3.2 Type

🔹 VARCHAR

##### 5.7.3.3.3 Is Required

✅ Yes

##### 5.7.3.3.4 Is Primary Key

❌ No

##### 5.7.3.3.5 Size

255

##### 5.7.3.3.6 Is Unique

❌ No

##### 5.7.3.3.7 Constraints

*No items available*

##### 5.7.3.3.8 Precision

0

##### 5.7.3.3.9 Scale

0

##### 5.7.3.3.10 Is Foreign Key

❌ No

#### 5.7.3.4.0 VARCHAR

##### 5.7.3.4.1 Name

recurrenceRule

##### 5.7.3.4.2 Type

🔹 VARCHAR

##### 5.7.3.4.3 Is Required

❌ No

##### 5.7.3.4.4 Is Primary Key

❌ No

##### 5.7.3.4.5 Size

255

##### 5.7.3.4.6 Is Unique

❌ No

##### 5.7.3.4.7 Constraints

*No items available*

##### 5.7.3.4.8 Precision

0

##### 5.7.3.4.9 Scale

0

##### 5.7.3.4.10 Is Foreign Key

❌ No

#### 5.7.3.5.0 BOOLEAN

##### 5.7.3.5.1 Name

isActive

##### 5.7.3.5.2 Type

🔹 BOOLEAN

##### 5.7.3.5.3 Is Required

✅ Yes

##### 5.7.3.5.4 Is Primary Key

❌ No

##### 5.7.3.5.5 Size

0

##### 5.7.3.5.6 Is Unique

❌ No

##### 5.7.3.5.7 Constraints

- DEFAULT true

##### 5.7.3.5.8 Precision

0

##### 5.7.3.5.9 Scale

0

##### 5.7.3.5.10 Is Foreign Key

❌ No

#### 5.7.3.6.0 DateTime

##### 5.7.3.6.1 Name

createdAt

##### 5.7.3.6.2 Type

🔹 DateTime

##### 5.7.3.6.3 Is Required

✅ Yes

##### 5.7.3.6.4 Is Primary Key

❌ No

##### 5.7.3.6.5 Size

0

##### 5.7.3.6.6 Is Unique

❌ No

##### 5.7.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.7.3.6.8 Precision

0

##### 5.7.3.6.9 Scale

0

##### 5.7.3.6.10 Is Foreign Key

❌ No

#### 5.7.3.7.0 DateTime

##### 5.7.3.7.1 Name

updatedAt

##### 5.7.3.7.2 Type

🔹 DateTime

##### 5.7.3.7.3 Is Required

✅ Yes

##### 5.7.3.7.4 Is Primary Key

❌ No

##### 5.7.3.7.5 Size

0

##### 5.7.3.7.6 Is Unique

❌ No

##### 5.7.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.7.3.7.8 Precision

0

##### 5.7.3.7.9 Scale

0

##### 5.7.3.7.10 Is Foreign Key

❌ No

### 5.7.4.0.0 Primary Keys

- dailyTaskId

### 5.7.5.0.0 Unique Constraints

*No items available*

### 5.7.6.0.0 Indexes

- {'name': 'IX_DailyTask_UserId_IsActive', 'columns': ['UserId', 'isActive'], 'type': 'BTree'}

## 5.8.0.0.0 TaskCompletion

### 5.8.1.0.0 Name

TaskCompletion

### 5.8.2.0.0 Description

Tracks the completion history of a recurring daily task.

### 5.8.3.0.0 Attributes

#### 5.8.3.1.0 Guid

##### 5.8.3.1.1 Name

taskCompletionId

##### 5.8.3.1.2 Type

🔹 Guid

##### 5.8.3.1.3 Is Required

✅ Yes

##### 5.8.3.1.4 Is Primary Key

✅ Yes

##### 5.8.3.1.5 Size

0

##### 5.8.3.1.6 Is Unique

✅ Yes

##### 5.8.3.1.7 Constraints

*No items available*

##### 5.8.3.1.8 Precision

0

##### 5.8.3.1.9 Scale

0

##### 5.8.3.1.10 Is Foreign Key

❌ No

#### 5.8.3.2.0 Guid

##### 5.8.3.2.1 Name

DailyTaskId

##### 5.8.3.2.2 Type

🔹 Guid

##### 5.8.3.2.3 Is Required

✅ Yes

##### 5.8.3.2.4 Is Primary Key

❌ No

##### 5.8.3.2.5 Size

0

##### 5.8.3.2.6 Is Unique

❌ No

##### 5.8.3.2.7 Constraints

*No items available*

##### 5.8.3.2.8 Precision

0

##### 5.8.3.2.9 Scale

0

##### 5.8.3.2.10 Is Foreign Key

✅ Yes

#### 5.8.3.3.0 Guid

##### 5.8.3.3.1 Name

UserId

##### 5.8.3.3.2 Type

🔹 Guid

##### 5.8.3.3.3 Is Required

✅ Yes

##### 5.8.3.3.4 Is Primary Key

❌ No

##### 5.8.3.3.5 Size

0

##### 5.8.3.3.6 Is Unique

❌ No

##### 5.8.3.3.7 Constraints

*No items available*

##### 5.8.3.3.8 Precision

0

##### 5.8.3.3.9 Scale

0

##### 5.8.3.3.10 Is Foreign Key

✅ Yes

#### 5.8.3.4.0 DateTime

##### 5.8.3.4.1 Name

completedAt

##### 5.8.3.4.2 Type

🔹 DateTime

##### 5.8.3.4.3 Is Required

✅ Yes

##### 5.8.3.4.4 Is Primary Key

❌ No

##### 5.8.3.4.5 Size

0

##### 5.8.3.4.6 Is Unique

❌ No

##### 5.8.3.4.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.8.3.4.8 Precision

0

##### 5.8.3.4.9 Scale

0

##### 5.8.3.4.10 Is Foreign Key

❌ No

### 5.8.4.0.0 Primary Keys

- taskCompletionId

### 5.8.5.0.0 Unique Constraints

- {'name': 'UC_TaskCompletion_Task_Date', 'columns': ['DailyTaskId', 'completedAt']}

### 5.8.6.0.0 Indexes

- {'name': 'IX_TaskCompletion_UserId_CompletedAt', 'columns': ['UserId', 'completedAt'], 'type': 'BTree'}

## 5.9.0.0.0 Recommendation

### 5.9.1.0.0 Name

Recommendation

### 5.9.2.0.0 Description

Stores AI-generated book recommendations and user feedback on them.

### 5.9.3.0.0 Attributes

#### 5.9.3.1.0 Guid

##### 5.9.3.1.1 Name

recommendationId

##### 5.9.3.1.2 Type

🔹 Guid

##### 5.9.3.1.3 Is Required

✅ Yes

##### 5.9.3.1.4 Is Primary Key

✅ Yes

##### 5.9.3.1.5 Size

0

##### 5.9.3.1.6 Is Unique

✅ Yes

##### 5.9.3.1.7 Constraints

*No items available*

##### 5.9.3.1.8 Precision

0

##### 5.9.3.1.9 Scale

0

##### 5.9.3.1.10 Is Foreign Key

❌ No

#### 5.9.3.2.0 Guid

##### 5.9.3.2.1 Name

UserId

##### 5.9.3.2.2 Type

🔹 Guid

##### 5.9.3.2.3 Is Required

✅ Yes

##### 5.9.3.2.4 Is Primary Key

❌ No

##### 5.9.3.2.5 Size

0

##### 5.9.3.2.6 Is Unique

❌ No

##### 5.9.3.2.7 Constraints

*No items available*

##### 5.9.3.2.8 Precision

0

##### 5.9.3.2.9 Scale

0

##### 5.9.3.2.10 Is Foreign Key

✅ Yes

#### 5.9.3.3.0 VARCHAR

##### 5.9.3.3.1 Name

recommendedBookTitle

##### 5.9.3.3.2 Type

🔹 VARCHAR

##### 5.9.3.3.3 Is Required

✅ Yes

##### 5.9.3.3.4 Is Primary Key

❌ No

##### 5.9.3.3.5 Size

255

##### 5.9.3.3.6 Is Unique

❌ No

##### 5.9.3.3.7 Constraints

*No items available*

##### 5.9.3.3.8 Precision

0

##### 5.9.3.3.9 Scale

0

##### 5.9.3.3.10 Is Foreign Key

❌ No

#### 5.9.3.4.0 VARCHAR

##### 5.9.3.4.1 Name

recommendedBookAuthor

##### 5.9.3.4.2 Type

🔹 VARCHAR

##### 5.9.3.4.3 Is Required

✅ Yes

##### 5.9.3.4.4 Is Primary Key

❌ No

##### 5.9.3.4.5 Size

255

##### 5.9.3.4.6 Is Unique

❌ No

##### 5.9.3.4.7 Constraints

*No items available*

##### 5.9.3.4.8 Precision

0

##### 5.9.3.4.9 Scale

0

##### 5.9.3.4.10 Is Foreign Key

❌ No

#### 5.9.3.5.0 TEXT

##### 5.9.3.5.1 Name

reasoning

##### 5.9.3.5.2 Type

🔹 TEXT

##### 5.9.3.5.3 Is Required

✅ Yes

##### 5.9.3.5.4 Is Primary Key

❌ No

##### 5.9.3.5.5 Size

0

##### 5.9.3.5.6 Is Unique

❌ No

##### 5.9.3.5.7 Constraints

*No items available*

##### 5.9.3.5.8 Precision

0

##### 5.9.3.5.9 Scale

0

##### 5.9.3.5.10 Is Foreign Key

❌ No

#### 5.9.3.6.0 VARCHAR

##### 5.9.3.6.1 Name

feedback

##### 5.9.3.6.2 Type

🔹 VARCHAR

##### 5.9.3.6.3 Is Required

❌ No

##### 5.9.3.6.4 Is Primary Key

❌ No

##### 5.9.3.6.5 Size

20

##### 5.9.3.6.6 Is Unique

❌ No

##### 5.9.3.6.7 Constraints

- ENUM('Like', 'Dislike', 'Irrelevant')

##### 5.9.3.6.8 Precision

0

##### 5.9.3.6.9 Scale

0

##### 5.9.3.6.10 Is Foreign Key

❌ No

#### 5.9.3.7.0 DateTime

##### 5.9.3.7.1 Name

generatedAt

##### 5.9.3.7.2 Type

🔹 DateTime

##### 5.9.3.7.3 Is Required

✅ Yes

##### 5.9.3.7.4 Is Primary Key

❌ No

##### 5.9.3.7.5 Size

0

##### 5.9.3.7.6 Is Unique

❌ No

##### 5.9.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.9.3.7.8 Precision

0

##### 5.9.3.7.9 Scale

0

##### 5.9.3.7.10 Is Foreign Key

❌ No

### 5.9.4.0.0 Primary Keys

- recommendationId

### 5.9.5.0.0 Unique Constraints

*No items available*

### 5.9.6.0.0 Indexes

#### 5.9.6.1.0 BTree

##### 5.9.6.1.1 Name

IX_Recommendation_UserId_GeneratedAt

##### 5.9.6.1.2 Columns

- UserId
- generatedAt

##### 5.9.6.1.3 Type

🔹 BTree

#### 5.9.6.2.0 BTree

##### 5.9.6.2.1 Name

IX_Recommendation_UserId_Feedback

##### 5.9.6.2.2 Columns

- UserId
- feedback

##### 5.9.6.2.3 Type

🔹 BTree

## 5.10.0.0.0 VocabularyItem

### 5.10.1.0.0 Name

VocabularyItem

### 5.10.2.0.0 Description

Stores words and definitions saved by a Premium user.

### 5.10.3.0.0 Attributes

#### 5.10.3.1.0 Guid

##### 5.10.3.1.1 Name

vocabularyItemId

##### 5.10.3.1.2 Type

🔹 Guid

##### 5.10.3.1.3 Is Required

✅ Yes

##### 5.10.3.1.4 Is Primary Key

✅ Yes

##### 5.10.3.1.5 Size

0

##### 5.10.3.1.6 Is Unique

✅ Yes

##### 5.10.3.1.7 Constraints

*No items available*

##### 5.10.3.1.8 Precision

0

##### 5.10.3.1.9 Scale

0

##### 5.10.3.1.10 Is Foreign Key

❌ No

#### 5.10.3.2.0 Guid

##### 5.10.3.2.1 Name

UserId

##### 5.10.3.2.2 Type

🔹 Guid

##### 5.10.3.2.3 Is Required

✅ Yes

##### 5.10.3.2.4 Is Primary Key

❌ No

##### 5.10.3.2.5 Size

0

##### 5.10.3.2.6 Is Unique

❌ No

##### 5.10.3.2.7 Constraints

*No items available*

##### 5.10.3.2.8 Precision

0

##### 5.10.3.2.9 Scale

0

##### 5.10.3.2.10 Is Foreign Key

✅ Yes

#### 5.10.3.3.0 VARCHAR

##### 5.10.3.3.1 Name

word

##### 5.10.3.3.2 Type

🔹 VARCHAR

##### 5.10.3.3.3 Is Required

✅ Yes

##### 5.10.3.3.4 Is Primary Key

❌ No

##### 5.10.3.3.5 Size

100

##### 5.10.3.3.6 Is Unique

❌ No

##### 5.10.3.3.7 Constraints

*No items available*

##### 5.10.3.3.8 Precision

0

##### 5.10.3.3.9 Scale

0

##### 5.10.3.3.10 Is Foreign Key

❌ No

#### 5.10.3.4.0 TEXT

##### 5.10.3.4.1 Name

definition

##### 5.10.3.4.2 Type

🔹 TEXT

##### 5.10.3.4.3 Is Required

❌ No

##### 5.10.3.4.4 Is Primary Key

❌ No

##### 5.10.3.4.5 Size

0

##### 5.10.3.4.6 Is Unique

❌ No

##### 5.10.3.4.7 Constraints

*No items available*

##### 5.10.3.4.8 Precision

0

##### 5.10.3.4.9 Scale

0

##### 5.10.3.4.10 Is Foreign Key

❌ No

#### 5.10.3.5.0 TEXT

##### 5.10.3.5.1 Name

exampleSentence

##### 5.10.3.5.2 Type

🔹 TEXT

##### 5.10.3.5.3 Is Required

❌ No

##### 5.10.3.5.4 Is Primary Key

❌ No

##### 5.10.3.5.5 Size

0

##### 5.10.3.5.6 Is Unique

❌ No

##### 5.10.3.5.7 Constraints

*No items available*

##### 5.10.3.5.8 Precision

0

##### 5.10.3.5.9 Scale

0

##### 5.10.3.5.10 Is Foreign Key

❌ No

#### 5.10.3.6.0 DateTime

##### 5.10.3.6.1 Name

createdAt

##### 5.10.3.6.2 Type

🔹 DateTime

##### 5.10.3.6.3 Is Required

✅ Yes

##### 5.10.3.6.4 Is Primary Key

❌ No

##### 5.10.3.6.5 Size

0

##### 5.10.3.6.6 Is Unique

❌ No

##### 5.10.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.10.3.6.8 Precision

0

##### 5.10.3.6.9 Scale

0

##### 5.10.3.6.10 Is Foreign Key

❌ No

#### 5.10.3.7.0 DateTime

##### 5.10.3.7.1 Name

updatedAt

##### 5.10.3.7.2 Type

🔹 DateTime

##### 5.10.3.7.3 Is Required

✅ Yes

##### 5.10.3.7.4 Is Primary Key

❌ No

##### 5.10.3.7.5 Size

0

##### 5.10.3.7.6 Is Unique

❌ No

##### 5.10.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.10.3.7.8 Precision

0

##### 5.10.3.7.9 Scale

0

##### 5.10.3.7.10 Is Foreign Key

❌ No

### 5.10.4.0.0 Primary Keys

- vocabularyItemId

### 5.10.5.0.0 Unique Constraints

- {'name': 'UC_VocabularyItem_User_Word', 'columns': ['UserId', 'word']}

### 5.10.6.0.0 Indexes

- {'name': 'IX_VocabularyItem_UserId_Word', 'columns': ['UserId', 'word'], 'type': 'BTree'}

## 5.11.0.0.0 DataExportJob

### 5.11.1.0.0 Name

DataExportJob

### 5.11.2.0.0 Description

Tracks the status of user-initiated data export requests (GDPR).

### 5.11.3.0.0 Attributes

#### 5.11.3.1.0 Guid

##### 5.11.3.1.1 Name

dataExportJobId

##### 5.11.3.1.2 Type

🔹 Guid

##### 5.11.3.1.3 Is Required

✅ Yes

##### 5.11.3.1.4 Is Primary Key

✅ Yes

##### 5.11.3.1.5 Size

0

##### 5.11.3.1.6 Is Unique

✅ Yes

##### 5.11.3.1.7 Constraints

*No items available*

##### 5.11.3.1.8 Precision

0

##### 5.11.3.1.9 Scale

0

##### 5.11.3.1.10 Is Foreign Key

❌ No

#### 5.11.3.2.0 Guid

##### 5.11.3.2.1 Name

UserId

##### 5.11.3.2.2 Type

🔹 Guid

##### 5.11.3.2.3 Is Required

✅ Yes

##### 5.11.3.2.4 Is Primary Key

❌ No

##### 5.11.3.2.5 Size

0

##### 5.11.3.2.6 Is Unique

❌ No

##### 5.11.3.2.7 Constraints

*No items available*

##### 5.11.3.2.8 Precision

0

##### 5.11.3.2.9 Scale

0

##### 5.11.3.2.10 Is Foreign Key

✅ Yes

#### 5.11.3.3.0 VARCHAR

##### 5.11.3.3.1 Name

status

##### 5.11.3.3.2 Type

🔹 VARCHAR

##### 5.11.3.3.3 Is Required

✅ Yes

##### 5.11.3.3.4 Is Primary Key

❌ No

##### 5.11.3.3.5 Size

20

##### 5.11.3.3.6 Is Unique

❌ No

##### 5.11.3.3.7 Constraints

- ENUM('Queued', 'Processing', 'Completed', 'Failed')
- DEFAULT 'Queued'

##### 5.11.3.3.8 Precision

0

##### 5.11.3.3.9 Scale

0

##### 5.11.3.3.10 Is Foreign Key

❌ No

#### 5.11.3.4.0 VARCHAR

##### 5.11.3.4.1 Name

fileUrl

##### 5.11.3.4.2 Type

🔹 VARCHAR

##### 5.11.3.4.3 Is Required

❌ No

##### 5.11.3.4.4 Is Primary Key

❌ No

##### 5.11.3.4.5 Size

512

##### 5.11.3.4.6 Is Unique

❌ No

##### 5.11.3.4.7 Constraints

*No items available*

##### 5.11.3.4.8 Precision

0

##### 5.11.3.4.9 Scale

0

##### 5.11.3.4.10 Is Foreign Key

❌ No

#### 5.11.3.5.0 DateTime

##### 5.11.3.5.1 Name

expiresAt

##### 5.11.3.5.2 Type

🔹 DateTime

##### 5.11.3.5.3 Is Required

❌ No

##### 5.11.3.5.4 Is Primary Key

❌ No

##### 5.11.3.5.5 Size

0

##### 5.11.3.5.6 Is Unique

❌ No

##### 5.11.3.5.7 Constraints

*No items available*

##### 5.11.3.5.8 Precision

0

##### 5.11.3.5.9 Scale

0

##### 5.11.3.5.10 Is Foreign Key

❌ No

#### 5.11.3.6.0 DateTime

##### 5.11.3.6.1 Name

createdAt

##### 5.11.3.6.2 Type

🔹 DateTime

##### 5.11.3.6.3 Is Required

✅ Yes

##### 5.11.3.6.4 Is Primary Key

❌ No

##### 5.11.3.6.5 Size

0

##### 5.11.3.6.6 Is Unique

❌ No

##### 5.11.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.11.3.6.8 Precision

0

##### 5.11.3.6.9 Scale

0

##### 5.11.3.6.10 Is Foreign Key

❌ No

#### 5.11.3.7.0 DateTime

##### 5.11.3.7.1 Name

updatedAt

##### 5.11.3.7.2 Type

🔹 DateTime

##### 5.11.3.7.3 Is Required

✅ Yes

##### 5.11.3.7.4 Is Primary Key

❌ No

##### 5.11.3.7.5 Size

0

##### 5.11.3.7.6 Is Unique

❌ No

##### 5.11.3.7.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.11.3.7.8 Precision

0

##### 5.11.3.7.9 Scale

0

##### 5.11.3.7.10 Is Foreign Key

❌ No

### 5.11.4.0.0 Primary Keys

- dataExportJobId

### 5.11.5.0.0 Unique Constraints

*No items available*

### 5.11.6.0.0 Indexes

#### 5.11.6.1.0 BTree

##### 5.11.6.1.1 Name

IX_DataExportJob_UserId

##### 5.11.6.1.2 Columns

- UserId

##### 5.11.6.1.3 Type

🔹 BTree

#### 5.11.6.2.0 BTree

##### 5.11.6.2.1 Name

IX_DataExportJob_Status

##### 5.11.6.2.2 Columns

- status

##### 5.11.6.2.3 Type

🔹 BTree

#### 5.11.6.3.0 BTree

##### 5.11.6.3.1 Name

IX_DataExportJob_ExpiresAt

##### 5.11.6.3.2 Columns

- expiresAt

##### 5.11.6.3.3 Type

🔹 BTree

## 5.12.0.0.0 UserSetting

### 5.12.1.0.0 Name

UserSetting

### 5.12.2.0.0 Description

A key-value store for user-specific application settings like theme.

### 5.12.3.0.0 Attributes

#### 5.12.3.1.0 Guid

##### 5.12.3.1.1 Name

userSettingId

##### 5.12.3.1.2 Type

🔹 Guid

##### 5.12.3.1.3 Is Required

✅ Yes

##### 5.12.3.1.4 Is Primary Key

✅ Yes

##### 5.12.3.1.5 Size

0

##### 5.12.3.1.6 Is Unique

✅ Yes

##### 5.12.3.1.7 Constraints

*No items available*

##### 5.12.3.1.8 Precision

0

##### 5.12.3.1.9 Scale

0

##### 5.12.3.1.10 Is Foreign Key

❌ No

#### 5.12.3.2.0 Guid

##### 5.12.3.2.1 Name

UserId

##### 5.12.3.2.2 Type

🔹 Guid

##### 5.12.3.2.3 Is Required

✅ Yes

##### 5.12.3.2.4 Is Primary Key

❌ No

##### 5.12.3.2.5 Size

0

##### 5.12.3.2.6 Is Unique

❌ No

##### 5.12.3.2.7 Constraints

*No items available*

##### 5.12.3.2.8 Precision

0

##### 5.12.3.2.9 Scale

0

##### 5.12.3.2.10 Is Foreign Key

✅ Yes

#### 5.12.3.3.0 VARCHAR

##### 5.12.3.3.1 Name

settingKey

##### 5.12.3.3.2 Type

🔹 VARCHAR

##### 5.12.3.3.3 Is Required

✅ Yes

##### 5.12.3.3.4 Is Primary Key

❌ No

##### 5.12.3.3.5 Size

50

##### 5.12.3.3.6 Is Unique

❌ No

##### 5.12.3.3.7 Constraints

*No items available*

##### 5.12.3.3.8 Precision

0

##### 5.12.3.3.9 Scale

0

##### 5.12.3.3.10 Is Foreign Key

❌ No

#### 5.12.3.4.0 VARCHAR

##### 5.12.3.4.1 Name

settingValue

##### 5.12.3.4.2 Type

🔹 VARCHAR

##### 5.12.3.4.3 Is Required

✅ Yes

##### 5.12.3.4.4 Is Primary Key

❌ No

##### 5.12.3.4.5 Size

255

##### 5.12.3.4.6 Is Unique

❌ No

##### 5.12.3.4.7 Constraints

*No items available*

##### 5.12.3.4.8 Precision

0

##### 5.12.3.4.9 Scale

0

##### 5.12.3.4.10 Is Foreign Key

❌ No

#### 5.12.3.5.0 DateTime

##### 5.12.3.5.1 Name

createdAt

##### 5.12.3.5.2 Type

🔹 DateTime

##### 5.12.3.5.3 Is Required

✅ Yes

##### 5.12.3.5.4 Is Primary Key

❌ No

##### 5.12.3.5.5 Size

0

##### 5.12.3.5.6 Is Unique

❌ No

##### 5.12.3.5.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.12.3.5.8 Precision

0

##### 5.12.3.5.9 Scale

0

##### 5.12.3.5.10 Is Foreign Key

❌ No

#### 5.12.3.6.0 DateTime

##### 5.12.3.6.1 Name

updatedAt

##### 5.12.3.6.2 Type

🔹 DateTime

##### 5.12.3.6.3 Is Required

✅ Yes

##### 5.12.3.6.4 Is Primary Key

❌ No

##### 5.12.3.6.5 Size

0

##### 5.12.3.6.6 Is Unique

❌ No

##### 5.12.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.12.3.6.8 Precision

0

##### 5.12.3.6.9 Scale

0

##### 5.12.3.6.10 Is Foreign Key

❌ No

### 5.12.4.0.0 Primary Keys

- userSettingId

### 5.12.5.0.0 Unique Constraints

- {'name': 'UC_UserSetting_User_Key', 'columns': ['UserId', 'settingKey']}

### 5.12.6.0.0 Indexes

- {'name': 'IX_UserSetting_UserId_Key', 'columns': ['UserId', 'settingKey'], 'type': 'BTree'}

## 5.13.0.0.0 UserReadingStats

### 5.13.1.0.0 Name

UserReadingStats

### 5.13.2.0.0 Description

A materialized view or summary table for daily user reading statistics, pre-aggregated to improve dashboard performance.

### 5.13.3.0.0 Attributes

#### 5.13.3.1.0 Guid

##### 5.13.3.1.1 Name

UserId

##### 5.13.3.1.2 Type

🔹 Guid

##### 5.13.3.1.3 Is Required

✅ Yes

##### 5.13.3.1.4 Is Primary Key

✅ Yes

##### 5.13.3.1.5 Size

0

##### 5.13.3.1.6 Is Unique

❌ No

##### 5.13.3.1.7 Constraints

*No items available*

##### 5.13.3.1.8 Precision

0

##### 5.13.3.1.9 Scale

0

##### 5.13.3.1.10 Is Foreign Key

✅ Yes

#### 5.13.3.2.0 Date

##### 5.13.3.2.1 Name

statDate

##### 5.13.3.2.2 Type

🔹 Date

##### 5.13.3.2.3 Is Required

✅ Yes

##### 5.13.3.2.4 Is Primary Key

✅ Yes

##### 5.13.3.2.5 Size

0

##### 5.13.3.2.6 Is Unique

❌ No

##### 5.13.3.2.7 Constraints

*No items available*

##### 5.13.3.2.8 Precision

0

##### 5.13.3.2.9 Scale

0

##### 5.13.3.2.10 Is Foreign Key

❌ No

#### 5.13.3.3.0 INT

##### 5.13.3.3.1 Name

totalMinutesRead

##### 5.13.3.3.2 Type

🔹 INT

##### 5.13.3.3.3 Is Required

✅ Yes

##### 5.13.3.3.4 Is Primary Key

❌ No

##### 5.13.3.3.5 Size

0

##### 5.13.3.3.6 Is Unique

❌ No

##### 5.13.3.3.7 Constraints

- CHECK (totalMinutesRead >= 0)
- DEFAULT 0

##### 5.13.3.3.8 Precision

0

##### 5.13.3.3.9 Scale

0

##### 5.13.3.3.10 Is Foreign Key

❌ No

#### 5.13.3.4.0 INT

##### 5.13.3.4.1 Name

totalPagesRead

##### 5.13.3.4.2 Type

🔹 INT

##### 5.13.3.4.3 Is Required

✅ Yes

##### 5.13.3.4.4 Is Primary Key

❌ No

##### 5.13.3.4.5 Size

0

##### 5.13.3.4.6 Is Unique

❌ No

##### 5.13.3.4.7 Constraints

- CHECK (totalPagesRead >= 0)
- DEFAULT 0

##### 5.13.3.4.8 Precision

0

##### 5.13.3.4.9 Scale

0

##### 5.13.3.4.10 Is Foreign Key

❌ No

#### 5.13.3.5.0 DateTime

##### 5.13.3.5.1 Name

lastUpdatedAt

##### 5.13.3.5.2 Type

🔹 DateTime

##### 5.13.3.5.3 Is Required

✅ Yes

##### 5.13.3.5.4 Is Primary Key

❌ No

##### 5.13.3.5.5 Size

0

##### 5.13.3.5.6 Is Unique

❌ No

##### 5.13.3.5.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.13.3.5.8 Precision

0

##### 5.13.3.5.9 Scale

0

##### 5.13.3.5.10 Is Foreign Key

❌ No

### 5.13.4.0.0 Primary Keys

- UserId
- statDate

### 5.13.5.0.0 Unique Constraints

*No items available*

### 5.13.6.0.0 Indexes

*No items available*

