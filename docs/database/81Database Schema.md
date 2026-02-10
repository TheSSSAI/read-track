# 1 Entities

## 1.1 User

### 1.1.1 Name

User

### 1.1.2 Description

Represents system users, their authentication details, and current status.

### 1.1.3 Attributes

#### 1.1.3.1 Guid

##### 1.1.3.1.1 Name

userId

##### 1.1.3.1.2 Type

🔹 Guid

##### 1.1.3.1.3 Is Required

✅ Yes

##### 1.1.3.1.4 Is Primary Key

✅ Yes

##### 1.1.3.1.5 Is Unique

✅ Yes

##### 1.1.3.1.6 Index Type

UniqueIndex

##### 1.1.3.1.7 Size

0

##### 1.1.3.1.8 Constraints

*No items available*

##### 1.1.3.1.9 Default Value

null

##### 1.1.3.1.10 Is Foreign Key

❌ No

##### 1.1.3.1.11 Precision

0

##### 1.1.3.1.12 Scale

0

#### 1.1.3.2.0 VARCHAR

##### 1.1.3.2.1 Name

email

##### 1.1.3.2.2 Type

🔹 VARCHAR

##### 1.1.3.2.3 Is Required

✅ Yes

##### 1.1.3.2.4 Is Primary Key

❌ No

##### 1.1.3.2.5 Is Unique

✅ Yes

##### 1.1.3.2.6 Index Type

UniqueIndex

##### 1.1.3.2.7 Size

255

##### 1.1.3.2.8 Constraints

- EMAIL_FORMAT

##### 1.1.3.2.9 Default Value

null

##### 1.1.3.2.10 Is Foreign Key

❌ No

##### 1.1.3.2.11 Precision

0

##### 1.1.3.2.12 Scale

0

#### 1.1.3.3.0 VARCHAR

##### 1.1.3.3.1 Name

passwordHash

##### 1.1.3.3.2 Type

🔹 VARCHAR

##### 1.1.3.3.3 Is Required

✅ Yes

##### 1.1.3.3.4 Is Primary Key

❌ No

##### 1.1.3.3.5 Is Unique

❌ No

##### 1.1.3.3.6 Index Type

None

##### 1.1.3.3.7 Size

255

##### 1.1.3.3.8 Constraints

*No items available*

##### 1.1.3.3.9 Default Value

null

##### 1.1.3.3.10 Is Foreign Key

❌ No

##### 1.1.3.3.11 Precision

0

##### 1.1.3.3.12 Scale

0

#### 1.1.3.4.0 VARCHAR

##### 1.1.3.4.1 Name

subscriptionTier

##### 1.1.3.4.2 Type

🔹 VARCHAR

##### 1.1.3.4.3 Is Required

✅ Yes

##### 1.1.3.4.4 Is Primary Key

❌ No

##### 1.1.3.4.5 Is Unique

❌ No

##### 1.1.3.4.6 Index Type

Index

##### 1.1.3.4.7 Size

20

##### 1.1.3.4.8 Constraints

- ENUM('Free', 'Premium')

##### 1.1.3.4.9 Default Value

'Free'

##### 1.1.3.4.10 Is Foreign Key

❌ No

##### 1.1.3.4.11 Precision

0

##### 1.1.3.4.12 Scale

0

#### 1.1.3.5.0 BOOLEAN

##### 1.1.3.5.1 Name

onboardingCompleted

##### 1.1.3.5.2 Type

🔹 BOOLEAN

##### 1.1.3.5.3 Is Required

✅ Yes

##### 1.1.3.5.4 Is Primary Key

❌ No

##### 1.1.3.5.5 Is Unique

❌ No

##### 1.1.3.5.6 Index Type

None

##### 1.1.3.5.7 Size

0

##### 1.1.3.5.8 Constraints

*No items available*

##### 1.1.3.5.9 Default Value

false

##### 1.1.3.5.10 Is Foreign Key

❌ No

##### 1.1.3.5.11 Precision

0

##### 1.1.3.5.12 Scale

0

#### 1.1.3.6.0 BOOLEAN

##### 1.1.3.6.1 Name

isDeleted

##### 1.1.3.6.2 Type

🔹 BOOLEAN

##### 1.1.3.6.3 Is Required

✅ Yes

##### 1.1.3.6.4 Is Primary Key

❌ No

##### 1.1.3.6.5 Is Unique

❌ No

##### 1.1.3.6.6 Index Type

Index

##### 1.1.3.6.7 Size

0

##### 1.1.3.6.8 Constraints

*No items available*

##### 1.1.3.6.9 Default Value

false

##### 1.1.3.6.10 Is Foreign Key

❌ No

##### 1.1.3.6.11 Precision

0

##### 1.1.3.6.12 Scale

0

#### 1.1.3.7.0 DateTime

##### 1.1.3.7.1 Name

createdAt

##### 1.1.3.7.2 Type

🔹 DateTime

##### 1.1.3.7.3 Is Required

✅ Yes

##### 1.1.3.7.4 Is Primary Key

❌ No

##### 1.1.3.7.5 Is Unique

❌ No

##### 1.1.3.7.6 Index Type

Index

##### 1.1.3.7.7 Size

0

##### 1.1.3.7.8 Constraints

*No items available*

##### 1.1.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.7.10 Is Foreign Key

❌ No

##### 1.1.3.7.11 Precision

0

##### 1.1.3.7.12 Scale

0

#### 1.1.3.8.0 DateTime

##### 1.1.3.8.1 Name

updatedAt

##### 1.1.3.8.2 Type

🔹 DateTime

##### 1.1.3.8.3 Is Required

✅ Yes

##### 1.1.3.8.4 Is Primary Key

❌ No

##### 1.1.3.8.5 Is Unique

❌ No

##### 1.1.3.8.6 Index Type

None

##### 1.1.3.8.7 Size

0

##### 1.1.3.8.8 Constraints

*No items available*

##### 1.1.3.8.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.8.10 Is Foreign Key

❌ No

##### 1.1.3.8.11 Precision

0

##### 1.1.3.8.12 Scale

0

### 1.1.4.0.0 Primary Keys

- userId

### 1.1.5.0.0 Unique Constraints

- {'name': 'UC_User_Email', 'columns': ['email']}

### 1.1.6.0.0 Indexes

#### 1.1.6.1.0 BTree

##### 1.1.6.1.1 Name

IX_User_Email

##### 1.1.6.1.2 Columns

- email

##### 1.1.6.1.3 Type

🔹 BTree

#### 1.1.6.2.0 BTree

##### 1.1.6.2.1 Name

IX_User_IsDeleted_SubscriptionTier

##### 1.1.6.2.2 Columns

- isDeleted
- subscriptionTier

##### 1.1.6.2.3 Type

🔹 BTree

## 1.2.0.0.0 Subscription

### 1.2.1.0.0 Name

Subscription

### 1.2.2.0.0 Description

Stores the history and status of user subscriptions.

### 1.2.3.0.0 Attributes

#### 1.2.3.1.0 Guid

##### 1.2.3.1.1 Name

subscriptionId

##### 1.2.3.1.2 Type

🔹 Guid

##### 1.2.3.1.3 Is Required

✅ Yes

##### 1.2.3.1.4 Is Primary Key

✅ Yes

##### 1.2.3.1.5 Is Unique

✅ Yes

##### 1.2.3.1.6 Index Type

UniqueIndex

##### 1.2.3.1.7 Size

0

##### 1.2.3.1.8 Constraints

*No items available*

##### 1.2.3.1.9 Default Value

null

##### 1.2.3.1.10 Is Foreign Key

❌ No

##### 1.2.3.1.11 Precision

0

##### 1.2.3.1.12 Scale

0

#### 1.2.3.2.0 Guid

##### 1.2.3.2.1 Name

UserId

##### 1.2.3.2.2 Type

🔹 Guid

##### 1.2.3.2.3 Is Required

✅ Yes

##### 1.2.3.2.4 Is Primary Key

❌ No

##### 1.2.3.2.5 Is Unique

❌ No

##### 1.2.3.2.6 Index Type

Index

##### 1.2.3.2.7 Size

0

##### 1.2.3.2.8 Constraints

*No items available*

##### 1.2.3.2.9 Default Value

null

##### 1.2.3.2.10 Is Foreign Key

✅ Yes

##### 1.2.3.2.11 Precision

0

##### 1.2.3.2.12 Scale

0

#### 1.2.3.3.0 VARCHAR

##### 1.2.3.3.1 Name

status

##### 1.2.3.3.2 Type

🔹 VARCHAR

##### 1.2.3.3.3 Is Required

✅ Yes

##### 1.2.3.3.4 Is Primary Key

❌ No

##### 1.2.3.3.5 Is Unique

❌ No

##### 1.2.3.3.6 Index Type

Index

##### 1.2.3.3.7 Size

20

##### 1.2.3.3.8 Constraints

- ENUM('Active', 'Canceled', 'Expired')

##### 1.2.3.3.9 Default Value

null

##### 1.2.3.3.10 Is Foreign Key

❌ No

##### 1.2.3.3.11 Precision

0

##### 1.2.3.3.12 Scale

0

#### 1.2.3.4.0 DateTime

##### 1.2.3.4.1 Name

startDate

##### 1.2.3.4.2 Type

🔹 DateTime

##### 1.2.3.4.3 Is Required

✅ Yes

##### 1.2.3.4.4 Is Primary Key

❌ No

##### 1.2.3.4.5 Is Unique

❌ No

##### 1.2.3.4.6 Index Type

None

##### 1.2.3.4.7 Size

0

##### 1.2.3.4.8 Constraints

*No items available*

##### 1.2.3.4.9 Default Value

null

##### 1.2.3.4.10 Is Foreign Key

❌ No

##### 1.2.3.4.11 Precision

0

##### 1.2.3.4.12 Scale

0

#### 1.2.3.5.0 DateTime

##### 1.2.3.5.1 Name

endDate

##### 1.2.3.5.2 Type

🔹 DateTime

##### 1.2.3.5.3 Is Required

✅ Yes

##### 1.2.3.5.4 Is Primary Key

❌ No

##### 1.2.3.5.5 Is Unique

❌ No

##### 1.2.3.5.6 Index Type

Index

##### 1.2.3.5.7 Size

0

##### 1.2.3.5.8 Constraints

*No items available*

##### 1.2.3.5.9 Default Value

null

##### 1.2.3.5.10 Is Foreign Key

❌ No

##### 1.2.3.5.11 Precision

0

##### 1.2.3.5.12 Scale

0

#### 1.2.3.6.0 DateTime

##### 1.2.3.6.1 Name

createdAt

##### 1.2.3.6.2 Type

🔹 DateTime

##### 1.2.3.6.3 Is Required

✅ Yes

##### 1.2.3.6.4 Is Primary Key

❌ No

##### 1.2.3.6.5 Is Unique

❌ No

##### 1.2.3.6.6 Index Type

None

##### 1.2.3.6.7 Size

0

##### 1.2.3.6.8 Constraints

*No items available*

##### 1.2.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.2.3.6.10 Is Foreign Key

❌ No

##### 1.2.3.6.11 Precision

0

##### 1.2.3.6.12 Scale

0

#### 1.2.3.7.0 DateTime

##### 1.2.3.7.1 Name

updatedAt

##### 1.2.3.7.2 Type

🔹 DateTime

##### 1.2.3.7.3 Is Required

✅ Yes

##### 1.2.3.7.4 Is Primary Key

❌ No

##### 1.2.3.7.5 Is Unique

❌ No

##### 1.2.3.7.6 Index Type

None

##### 1.2.3.7.7 Size

0

##### 1.2.3.7.8 Constraints

*No items available*

##### 1.2.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.2.3.7.10 Is Foreign Key

❌ No

##### 1.2.3.7.11 Precision

0

##### 1.2.3.7.12 Scale

0

### 1.2.4.0.0 Primary Keys

- subscriptionId

### 1.2.5.0.0 Unique Constraints

*No items available*

### 1.2.6.0.0 Indexes

#### 1.2.6.1.0 BTree

##### 1.2.6.1.1 Name

IX_Subscription_UserId_Status

##### 1.2.6.1.2 Columns

- UserId
- status

##### 1.2.6.1.3 Type

🔹 BTree

#### 1.2.6.2.0 BTree

##### 1.2.6.2.1 Name

IX_Subscription_EndDate

##### 1.2.6.2.2 Columns

- endDate

##### 1.2.6.2.3 Type

🔹 BTree

#### 1.2.6.3.0 BTree

##### 1.2.6.3.1 Name

IX_Subscription_Status_EndDate

##### 1.2.6.3.2 Columns

- status
- endDate

##### 1.2.6.3.3 Type

🔹 BTree

## 1.3.0.0.0 Book

### 1.3.1.0.0 Name

Book

### 1.3.2.0.0 Description

A master table of all books known to the system.

### 1.3.3.0.0 Attributes

#### 1.3.3.1.0 Guid

##### 1.3.3.1.1 Name

bookId

##### 1.3.3.1.2 Type

🔹 Guid

##### 1.3.3.1.3 Is Required

✅ Yes

##### 1.3.3.1.4 Is Primary Key

✅ Yes

##### 1.3.3.1.5 Is Unique

✅ Yes

##### 1.3.3.1.6 Index Type

UniqueIndex

##### 1.3.3.1.7 Size

0

##### 1.3.3.1.8 Constraints

*No items available*

##### 1.3.3.1.9 Default Value

null

##### 1.3.3.1.10 Is Foreign Key

❌ No

##### 1.3.3.1.11 Precision

0

##### 1.3.3.1.12 Scale

0

#### 1.3.3.2.0 VARCHAR

##### 1.3.3.2.1 Name

googleBooksId

##### 1.3.3.2.2 Type

🔹 VARCHAR

##### 1.3.3.2.3 Is Required

❌ No

##### 1.3.3.2.4 Is Primary Key

❌ No

##### 1.3.3.2.5 Is Unique

✅ Yes

##### 1.3.3.2.6 Index Type

UniqueIndex

##### 1.3.3.2.7 Size

50

##### 1.3.3.2.8 Constraints

*No items available*

##### 1.3.3.2.9 Default Value

null

##### 1.3.3.2.10 Is Foreign Key

❌ No

##### 1.3.3.2.11 Precision

0

##### 1.3.3.2.12 Scale

0

#### 1.3.3.3.0 VARCHAR

##### 1.3.3.3.1 Name

title

##### 1.3.3.3.2 Type

🔹 VARCHAR

##### 1.3.3.3.3 Is Required

✅ Yes

##### 1.3.3.3.4 Is Primary Key

❌ No

##### 1.3.3.3.5 Is Unique

❌ No

##### 1.3.3.3.6 Index Type

Index

##### 1.3.3.3.7 Size

255

##### 1.3.3.3.8 Constraints

*No items available*

##### 1.3.3.3.9 Default Value

null

##### 1.3.3.3.10 Is Foreign Key

❌ No

##### 1.3.3.3.11 Precision

0

##### 1.3.3.3.12 Scale

0

#### 1.3.3.4.0 VARCHAR

##### 1.3.3.4.1 Name

author

##### 1.3.3.4.2 Type

🔹 VARCHAR

##### 1.3.3.4.3 Is Required

✅ Yes

##### 1.3.3.4.4 Is Primary Key

❌ No

##### 1.3.3.4.5 Is Unique

❌ No

##### 1.3.3.4.6 Index Type

Index

##### 1.3.3.4.7 Size

255

##### 1.3.3.4.8 Constraints

*No items available*

##### 1.3.3.4.9 Default Value

null

##### 1.3.3.4.10 Is Foreign Key

❌ No

##### 1.3.3.4.11 Precision

0

##### 1.3.3.4.12 Scale

0

#### 1.3.3.5.0 INT

##### 1.3.3.5.1 Name

pageCount

##### 1.3.3.5.2 Type

🔹 INT

##### 1.3.3.5.3 Is Required

❌ No

##### 1.3.3.5.4 Is Primary Key

❌ No

##### 1.3.3.5.5 Is Unique

❌ No

##### 1.3.3.5.6 Index Type

None

##### 1.3.3.5.7 Size

0

##### 1.3.3.5.8 Constraints

- NON_NEGATIVE

##### 1.3.3.5.9 Default Value

null

##### 1.3.3.5.10 Is Foreign Key

❌ No

##### 1.3.3.5.11 Precision

0

##### 1.3.3.5.12 Scale

0

#### 1.3.3.6.0 VARCHAR

##### 1.3.3.6.1 Name

coverImageUrl

##### 1.3.3.6.2 Type

🔹 VARCHAR

##### 1.3.3.6.3 Is Required

❌ No

##### 1.3.3.6.4 Is Primary Key

❌ No

##### 1.3.3.6.5 Is Unique

❌ No

##### 1.3.3.6.6 Index Type

None

##### 1.3.3.6.7 Size

512

##### 1.3.3.6.8 Constraints

*No items available*

##### 1.3.3.6.9 Default Value

null

##### 1.3.3.6.10 Is Foreign Key

❌ No

##### 1.3.3.6.11 Precision

0

##### 1.3.3.6.12 Scale

0

#### 1.3.3.7.0 DateTime

##### 1.3.3.7.1 Name

createdAt

##### 1.3.3.7.2 Type

🔹 DateTime

##### 1.3.3.7.3 Is Required

✅ Yes

##### 1.3.3.7.4 Is Primary Key

❌ No

##### 1.3.3.7.5 Is Unique

❌ No

##### 1.3.3.7.6 Index Type

None

##### 1.3.3.7.7 Size

0

##### 1.3.3.7.8 Constraints

*No items available*

##### 1.3.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.3.3.7.10 Is Foreign Key

❌ No

##### 1.3.3.7.11 Precision

0

##### 1.3.3.7.12 Scale

0

#### 1.3.3.8.0 DateTime

##### 1.3.3.8.1 Name

updatedAt

##### 1.3.3.8.2 Type

🔹 DateTime

##### 1.3.3.8.3 Is Required

✅ Yes

##### 1.3.3.8.4 Is Primary Key

❌ No

##### 1.3.3.8.5 Is Unique

❌ No

##### 1.3.3.8.6 Index Type

None

##### 1.3.3.8.7 Size

0

##### 1.3.3.8.8 Constraints

*No items available*

##### 1.3.3.8.9 Default Value

CURRENT_TIMESTAMP

##### 1.3.3.8.10 Is Foreign Key

❌ No

##### 1.3.3.8.11 Precision

0

##### 1.3.3.8.12 Scale

0

### 1.3.4.0.0 Primary Keys

- bookId

### 1.3.5.0.0 Unique Constraints

- {'name': 'UC_Book_GoogleBooksId', 'columns': ['googleBooksId']}

### 1.3.6.0.0 Indexes

#### 1.3.6.1.0 BTree

##### 1.3.6.1.1 Name

IX_Book_GoogleBooksId

##### 1.3.6.1.2 Columns

- googleBooksId

##### 1.3.6.1.3 Type

🔹 BTree

#### 1.3.6.2.0 BTree

##### 1.3.6.2.1 Name

IX_Book_Title_Author

##### 1.3.6.2.2 Columns

- title
- author

##### 1.3.6.2.3 Type

🔹 BTree

## 1.4.0.0.0 UserBook

### 1.4.1.0.0 Name

UserBook

### 1.4.2.0.0 Description

A join table representing a book in a user's personal library.

### 1.4.3.0.0 Attributes

#### 1.4.3.1.0 Guid

##### 1.4.3.1.1 Name

userBookId

##### 1.4.3.1.2 Type

🔹 Guid

##### 1.4.3.1.3 Is Required

✅ Yes

##### 1.4.3.1.4 Is Primary Key

✅ Yes

##### 1.4.3.1.5 Is Unique

✅ Yes

##### 1.4.3.1.6 Index Type

UniqueIndex

##### 1.4.3.1.7 Size

0

##### 1.4.3.1.8 Constraints

*No items available*

##### 1.4.3.1.9 Default Value

null

##### 1.4.3.1.10 Is Foreign Key

❌ No

##### 1.4.3.1.11 Precision

0

##### 1.4.3.1.12 Scale

0

#### 1.4.3.2.0 Guid

##### 1.4.3.2.1 Name

UserId

##### 1.4.3.2.2 Type

🔹 Guid

##### 1.4.3.2.3 Is Required

✅ Yes

##### 1.4.3.2.4 Is Primary Key

❌ No

##### 1.4.3.2.5 Is Unique

❌ No

##### 1.4.3.2.6 Index Type

Index

##### 1.4.3.2.7 Size

0

##### 1.4.3.2.8 Constraints

*No items available*

##### 1.4.3.2.9 Default Value

null

##### 1.4.3.2.10 Is Foreign Key

✅ Yes

##### 1.4.3.2.11 Precision

0

##### 1.4.3.2.12 Scale

0

#### 1.4.3.3.0 Guid

##### 1.4.3.3.1 Name

BookId

##### 1.4.3.3.2 Type

🔹 Guid

##### 1.4.3.3.3 Is Required

✅ Yes

##### 1.4.3.3.4 Is Primary Key

❌ No

##### 1.4.3.3.5 Is Unique

❌ No

##### 1.4.3.3.6 Index Type

Index

##### 1.4.3.3.7 Size

0

##### 1.4.3.3.8 Constraints

*No items available*

##### 1.4.3.3.9 Default Value

null

##### 1.4.3.3.10 Is Foreign Key

✅ Yes

##### 1.4.3.3.11 Precision

0

##### 1.4.3.3.12 Scale

0

#### 1.4.3.4.0 VARCHAR

##### 1.4.3.4.1 Name

bookTitle

##### 1.4.3.4.2 Type

🔹 VARCHAR

##### 1.4.3.4.3 Is Required

✅ Yes

##### 1.4.3.4.4 Is Primary Key

❌ No

##### 1.4.3.4.5 Is Unique

❌ No

##### 1.4.3.4.6 Index Type

None

##### 1.4.3.4.7 Size

255

##### 1.4.3.4.8 Constraints

*No items available*

##### 1.4.3.4.9 Default Value

null

##### 1.4.3.4.10 Is Foreign Key

❌ No

##### 1.4.3.4.11 Precision

0

##### 1.4.3.4.12 Scale

0

#### 1.4.3.5.0 VARCHAR

##### 1.4.3.5.1 Name

bookAuthor

##### 1.4.3.5.2 Type

🔹 VARCHAR

##### 1.4.3.5.3 Is Required

✅ Yes

##### 1.4.3.5.4 Is Primary Key

❌ No

##### 1.4.3.5.5 Is Unique

❌ No

##### 1.4.3.5.6 Index Type

None

##### 1.4.3.5.7 Size

255

##### 1.4.3.5.8 Constraints

*No items available*

##### 1.4.3.5.9 Default Value

null

##### 1.4.3.5.10 Is Foreign Key

❌ No

##### 1.4.3.5.11 Precision

0

##### 1.4.3.5.12 Scale

0

#### 1.4.3.6.0 VARCHAR

##### 1.4.3.6.1 Name

bookCoverImageUrl

##### 1.4.3.6.2 Type

🔹 VARCHAR

##### 1.4.3.6.3 Is Required

❌ No

##### 1.4.3.6.4 Is Primary Key

❌ No

##### 1.4.3.6.5 Is Unique

❌ No

##### 1.4.3.6.6 Index Type

None

##### 1.4.3.6.7 Size

512

##### 1.4.3.6.8 Constraints

*No items available*

##### 1.4.3.6.9 Default Value

null

##### 1.4.3.6.10 Is Foreign Key

❌ No

##### 1.4.3.6.11 Precision

0

##### 1.4.3.6.12 Scale

0

#### 1.4.3.7.0 VARCHAR

##### 1.4.3.7.1 Name

shelf

##### 1.4.3.7.2 Type

🔹 VARCHAR

##### 1.4.3.7.3 Is Required

✅ Yes

##### 1.4.3.7.4 Is Primary Key

❌ No

##### 1.4.3.7.5 Is Unique

❌ No

##### 1.4.3.7.6 Index Type

Index

##### 1.4.3.7.7 Size

30

##### 1.4.3.7.8 Constraints

- ENUM('WantToRead', 'CurrentlyReading', 'Read')

##### 1.4.3.7.9 Default Value

'WantToRead'

##### 1.4.3.7.10 Is Foreign Key

❌ No

##### 1.4.3.7.11 Precision

0

##### 1.4.3.7.12 Scale

0

#### 1.4.3.8.0 BOOLEAN

##### 1.4.3.8.1 Name

isReadOnly

##### 1.4.3.8.2 Type

🔹 BOOLEAN

##### 1.4.3.8.3 Is Required

✅ Yes

##### 1.4.3.8.4 Is Primary Key

❌ No

##### 1.4.3.8.5 Is Unique

❌ No

##### 1.4.3.8.6 Index Type

None

##### 1.4.3.8.7 Size

0

##### 1.4.3.8.8 Constraints

*No items available*

##### 1.4.3.8.9 Default Value

false

##### 1.4.3.8.10 Is Foreign Key

❌ No

##### 1.4.3.8.11 Precision

0

##### 1.4.3.8.12 Scale

0

#### 1.4.3.9.0 DateTime

##### 1.4.3.9.1 Name

addedAt

##### 1.4.3.9.2 Type

🔹 DateTime

##### 1.4.3.9.3 Is Required

✅ Yes

##### 1.4.3.9.4 Is Primary Key

❌ No

##### 1.4.3.9.5 Is Unique

❌ No

##### 1.4.3.9.6 Index Type

None

##### 1.4.3.9.7 Size

0

##### 1.4.3.9.8 Constraints

*No items available*

##### 1.4.3.9.9 Default Value

CURRENT_TIMESTAMP

##### 1.4.3.9.10 Is Foreign Key

❌ No

##### 1.4.3.9.11 Precision

0

##### 1.4.3.9.12 Scale

0

#### 1.4.3.10.0 DateTime

##### 1.4.3.10.1 Name

updatedAt

##### 1.4.3.10.2 Type

🔹 DateTime

##### 1.4.3.10.3 Is Required

✅ Yes

##### 1.4.3.10.4 Is Primary Key

❌ No

##### 1.4.3.10.5 Is Unique

❌ No

##### 1.4.3.10.6 Index Type

None

##### 1.4.3.10.7 Size

0

##### 1.4.3.10.8 Constraints

*No items available*

##### 1.4.3.10.9 Default Value

CURRENT_TIMESTAMP

##### 1.4.3.10.10 Is Foreign Key

❌ No

##### 1.4.3.10.11 Precision

0

##### 1.4.3.10.12 Scale

0

### 1.4.4.0.0 Primary Keys

- userBookId

### 1.4.5.0.0 Unique Constraints

- {'name': 'UC_UserBook_User_Book', 'columns': ['UserId', 'BookId']}

### 1.4.6.0.0 Indexes

#### 1.4.6.1.0 BTree

##### 1.4.6.1.1 Name

IX_UserBook_UserId_Shelf

##### 1.4.6.1.2 Columns

- UserId
- shelf

##### 1.4.6.1.3 Type

🔹 BTree

#### 1.4.6.2.0 BTree

##### 1.4.6.2.1 Name

IX_UserBook_BookId

##### 1.4.6.2.2 Columns

- BookId

##### 1.4.6.2.3 Type

🔹 BTree

#### 1.4.6.3.0 BTree

##### 1.4.6.3.1 Name

IX_UserBook_UserId_IsReadOnly

##### 1.4.6.3.2 Columns

- UserId
- isReadOnly

##### 1.4.6.3.3 Type

🔹 BTree

## 1.5.0.0.0 ReadingSession

### 1.5.1.0.0 Name

ReadingSession

### 1.5.2.0.0 Description

Logs individual reading sessions for a book in a user's library.

### 1.5.3.0.0 Attributes

#### 1.5.3.1.0 Guid

##### 1.5.3.1.1 Name

readingSessionId

##### 1.5.3.1.2 Type

🔹 Guid

##### 1.5.3.1.3 Is Required

✅ Yes

##### 1.5.3.1.4 Is Primary Key

✅ Yes

##### 1.5.3.1.5 Is Unique

✅ Yes

##### 1.5.3.1.6 Index Type

UniqueIndex

##### 1.5.3.1.7 Size

0

##### 1.5.3.1.8 Constraints

*No items available*

##### 1.5.3.1.9 Default Value

null

##### 1.5.3.1.10 Is Foreign Key

❌ No

##### 1.5.3.1.11 Precision

0

##### 1.5.3.1.12 Scale

0

#### 1.5.3.2.0 Guid

##### 1.5.3.2.1 Name

UserBookId

##### 1.5.3.2.2 Type

🔹 Guid

##### 1.5.3.2.3 Is Required

✅ Yes

##### 1.5.3.2.4 Is Primary Key

❌ No

##### 1.5.3.2.5 Is Unique

❌ No

##### 1.5.3.2.6 Index Type

Index

##### 1.5.3.2.7 Size

0

##### 1.5.3.2.8 Constraints

*No items available*

##### 1.5.3.2.9 Default Value

null

##### 1.5.3.2.10 Is Foreign Key

✅ Yes

##### 1.5.3.2.11 Precision

0

##### 1.5.3.2.12 Scale

0

#### 1.5.3.3.0 Guid

##### 1.5.3.3.1 Name

UserId

##### 1.5.3.3.2 Type

🔹 Guid

##### 1.5.3.3.3 Is Required

✅ Yes

##### 1.5.3.3.4 Is Primary Key

❌ No

##### 1.5.3.3.5 Is Unique

❌ No

##### 1.5.3.3.6 Index Type

Index

##### 1.5.3.3.7 Size

0

##### 1.5.3.3.8 Constraints

*No items available*

##### 1.5.3.3.9 Default Value

null

##### 1.5.3.3.10 Is Foreign Key

✅ Yes

##### 1.5.3.3.11 Precision

0

##### 1.5.3.3.12 Scale

0

#### 1.5.3.4.0 DateTime

##### 1.5.3.4.1 Name

startTime

##### 1.5.3.4.2 Type

🔹 DateTime

##### 1.5.3.4.3 Is Required

✅ Yes

##### 1.5.3.4.4 Is Primary Key

❌ No

##### 1.5.3.4.5 Is Unique

❌ No

##### 1.5.3.4.6 Index Type

Index

##### 1.5.3.4.7 Size

0

##### 1.5.3.4.8 Constraints

*No items available*

##### 1.5.3.4.9 Default Value

null

##### 1.5.3.4.10 Is Foreign Key

❌ No

##### 1.5.3.4.11 Precision

0

##### 1.5.3.4.12 Scale

0

#### 1.5.3.5.0 DateTime

##### 1.5.3.5.1 Name

endTime

##### 1.5.3.5.2 Type

🔹 DateTime

##### 1.5.3.5.3 Is Required

✅ Yes

##### 1.5.3.5.4 Is Primary Key

❌ No

##### 1.5.3.5.5 Is Unique

❌ No

##### 1.5.3.5.6 Index Type

None

##### 1.5.3.5.7 Size

0

##### 1.5.3.5.8 Constraints

*No items available*

##### 1.5.3.5.9 Default Value

null

##### 1.5.3.5.10 Is Foreign Key

❌ No

##### 1.5.3.5.11 Precision

0

##### 1.5.3.5.12 Scale

0

#### 1.5.3.6.0 INT

##### 1.5.3.6.1 Name

pagesRead

##### 1.5.3.6.2 Type

🔹 INT

##### 1.5.3.6.3 Is Required

❌ No

##### 1.5.3.6.4 Is Primary Key

❌ No

##### 1.5.3.6.5 Is Unique

❌ No

##### 1.5.3.6.6 Index Type

None

##### 1.5.3.6.7 Size

0

##### 1.5.3.6.8 Constraints

- NON_NEGATIVE

##### 1.5.3.6.9 Default Value

null

##### 1.5.3.6.10 Is Foreign Key

❌ No

##### 1.5.3.6.11 Precision

0

##### 1.5.3.6.12 Scale

0

#### 1.5.3.7.0 TEXT

##### 1.5.3.7.1 Name

notes

##### 1.5.3.7.2 Type

🔹 TEXT

##### 1.5.3.7.3 Is Required

❌ No

##### 1.5.3.7.4 Is Primary Key

❌ No

##### 1.5.3.7.5 Is Unique

❌ No

##### 1.5.3.7.6 Index Type

None

##### 1.5.3.7.7 Size

0

##### 1.5.3.7.8 Constraints

*No items available*

##### 1.5.3.7.9 Default Value

null

##### 1.5.3.7.10 Is Foreign Key

❌ No

##### 1.5.3.7.11 Precision

0

##### 1.5.3.7.12 Scale

0

#### 1.5.3.8.0 DateTime

##### 1.5.3.8.1 Name

createdAt

##### 1.5.3.8.2 Type

🔹 DateTime

##### 1.5.3.8.3 Is Required

✅ Yes

##### 1.5.3.8.4 Is Primary Key

❌ No

##### 1.5.3.8.5 Is Unique

❌ No

##### 1.5.3.8.6 Index Type

None

##### 1.5.3.8.7 Size

0

##### 1.5.3.8.8 Constraints

*No items available*

##### 1.5.3.8.9 Default Value

CURRENT_TIMESTAMP

##### 1.5.3.8.10 Is Foreign Key

❌ No

##### 1.5.3.8.11 Precision

0

##### 1.5.3.8.12 Scale

0

#### 1.5.3.9.0 DateTime

##### 1.5.3.9.1 Name

updatedAt

##### 1.5.3.9.2 Type

🔹 DateTime

##### 1.5.3.9.3 Is Required

✅ Yes

##### 1.5.3.9.4 Is Primary Key

❌ No

##### 1.5.3.9.5 Is Unique

❌ No

##### 1.5.3.9.6 Index Type

None

##### 1.5.3.9.7 Size

0

##### 1.5.3.9.8 Constraints

*No items available*

##### 1.5.3.9.9 Default Value

CURRENT_TIMESTAMP

##### 1.5.3.9.10 Is Foreign Key

❌ No

##### 1.5.3.9.11 Precision

0

##### 1.5.3.9.12 Scale

0

### 1.5.4.0.0 Primary Keys

- readingSessionId

### 1.5.5.0.0 Unique Constraints

*No items available*

### 1.5.6.0.0 Indexes

#### 1.5.6.1.0 BTree

##### 1.5.6.1.1 Name

IX_ReadingSession_UserBookId

##### 1.5.6.1.2 Columns

- UserBookId

##### 1.5.6.1.3 Type

🔹 BTree

#### 1.5.6.2.0 BTree

##### 1.5.6.2.1 Name

IX_ReadingSession_StartTime

##### 1.5.6.2.2 Columns

- startTime

##### 1.5.6.2.3 Type

🔹 BTree

#### 1.5.6.3.0 BTree

##### 1.5.6.3.1 Name

IX_ReadingSession_UserId_StartTime

##### 1.5.6.3.2 Columns

- UserId
- startTime

##### 1.5.6.3.3 Type

🔹 BTree

### 1.5.7.0.0 Partitioning

#### 1.5.7.1.0 Strategy

Range

#### 1.5.7.2.0 Columns

- startTime

#### 1.5.7.3.0 Details

Monthly partitions

## 1.6.0.0.0 Goal

### 1.6.1.0.0 Name

Goal

### 1.6.2.0.0 Description

Stores user-defined reading goals and tracks their progress.

### 1.6.3.0.0 Attributes

#### 1.6.3.1.0 Guid

##### 1.6.3.1.1 Name

goalId

##### 1.6.3.1.2 Type

🔹 Guid

##### 1.6.3.1.3 Is Required

✅ Yes

##### 1.6.3.1.4 Is Primary Key

✅ Yes

##### 1.6.3.1.5 Is Unique

✅ Yes

##### 1.6.3.1.6 Index Type

UniqueIndex

##### 1.6.3.1.7 Size

0

##### 1.6.3.1.8 Constraints

*No items available*

##### 1.6.3.1.9 Default Value

null

##### 1.6.3.1.10 Is Foreign Key

❌ No

##### 1.6.3.1.11 Precision

0

##### 1.6.3.1.12 Scale

0

#### 1.6.3.2.0 Guid

##### 1.6.3.2.1 Name

UserId

##### 1.6.3.2.2 Type

🔹 Guid

##### 1.6.3.2.3 Is Required

✅ Yes

##### 1.6.3.2.4 Is Primary Key

❌ No

##### 1.6.3.2.5 Is Unique

❌ No

##### 1.6.3.2.6 Index Type

Index

##### 1.6.3.2.7 Size

0

##### 1.6.3.2.8 Constraints

*No items available*

##### 1.6.3.2.9 Default Value

null

##### 1.6.3.2.10 Is Foreign Key

✅ Yes

##### 1.6.3.2.11 Precision

0

##### 1.6.3.2.12 Scale

0

#### 1.6.3.3.0 VARCHAR

##### 1.6.3.3.1 Name

goalType

##### 1.6.3.3.2 Type

🔹 VARCHAR

##### 1.6.3.3.3 Is Required

✅ Yes

##### 1.6.3.3.4 Is Primary Key

❌ No

##### 1.6.3.3.5 Is Unique

❌ No

##### 1.6.3.3.6 Index Type

None

##### 1.6.3.3.7 Size

20

##### 1.6.3.3.8 Constraints

- ENUM('Books', 'Pages', 'Time')

##### 1.6.3.3.9 Default Value

null

##### 1.6.3.3.10 Is Foreign Key

❌ No

##### 1.6.3.3.11 Precision

0

##### 1.6.3.3.12 Scale

0

#### 1.6.3.4.0 VARCHAR

##### 1.6.3.4.1 Name

period

##### 1.6.3.4.2 Type

🔹 VARCHAR

##### 1.6.3.4.3 Is Required

✅ Yes

##### 1.6.3.4.4 Is Primary Key

❌ No

##### 1.6.3.4.5 Is Unique

❌ No

##### 1.6.3.4.6 Index Type

None

##### 1.6.3.4.7 Size

20

##### 1.6.3.4.8 Constraints

- ENUM('Day', 'Week', 'Month', 'Year')

##### 1.6.3.4.9 Default Value

null

##### 1.6.3.4.10 Is Foreign Key

❌ No

##### 1.6.3.4.11 Precision

0

##### 1.6.3.4.12 Scale

0

#### 1.6.3.5.0 INT

##### 1.6.3.5.1 Name

targetValue

##### 1.6.3.5.2 Type

🔹 INT

##### 1.6.3.5.3 Is Required

✅ Yes

##### 1.6.3.5.4 Is Primary Key

❌ No

##### 1.6.3.5.5 Is Unique

❌ No

##### 1.6.3.5.6 Index Type

None

##### 1.6.3.5.7 Size

0

##### 1.6.3.5.8 Constraints

- POSITIVE_VALUE

##### 1.6.3.5.9 Default Value

null

##### 1.6.3.5.10 Is Foreign Key

❌ No

##### 1.6.3.5.11 Precision

0

##### 1.6.3.5.12 Scale

0

#### 1.6.3.6.0 INT

##### 1.6.3.6.1 Name

currentValue

##### 1.6.3.6.2 Type

🔹 INT

##### 1.6.3.6.3 Is Required

✅ Yes

##### 1.6.3.6.4 Is Primary Key

❌ No

##### 1.6.3.6.5 Is Unique

❌ No

##### 1.6.3.6.6 Index Type

None

##### 1.6.3.6.7 Size

0

##### 1.6.3.6.8 Constraints

- NON_NEGATIVE

##### 1.6.3.6.9 Default Value

0

##### 1.6.3.6.10 Is Foreign Key

❌ No

##### 1.6.3.6.11 Precision

0

##### 1.6.3.6.12 Scale

0

#### 1.6.3.7.0 DateTime

##### 1.6.3.7.1 Name

startDate

##### 1.6.3.7.2 Type

🔹 DateTime

##### 1.6.3.7.3 Is Required

✅ Yes

##### 1.6.3.7.4 Is Primary Key

❌ No

##### 1.6.3.7.5 Is Unique

❌ No

##### 1.6.3.7.6 Index Type

None

##### 1.6.3.7.7 Size

0

##### 1.6.3.7.8 Constraints

*No items available*

##### 1.6.3.7.9 Default Value

null

##### 1.6.3.7.10 Is Foreign Key

❌ No

##### 1.6.3.7.11 Precision

0

##### 1.6.3.7.12 Scale

0

#### 1.6.3.8.0 DateTime

##### 1.6.3.8.1 Name

endDate

##### 1.6.3.8.2 Type

🔹 DateTime

##### 1.6.3.8.3 Is Required

✅ Yes

##### 1.6.3.8.4 Is Primary Key

❌ No

##### 1.6.3.8.5 Is Unique

❌ No

##### 1.6.3.8.6 Index Type

None

##### 1.6.3.8.7 Size

0

##### 1.6.3.8.8 Constraints

*No items available*

##### 1.6.3.8.9 Default Value

null

##### 1.6.3.8.10 Is Foreign Key

❌ No

##### 1.6.3.8.11 Precision

0

##### 1.6.3.8.12 Scale

0

#### 1.6.3.9.0 BOOLEAN

##### 1.6.3.9.1 Name

isActive

##### 1.6.3.9.2 Type

🔹 BOOLEAN

##### 1.6.3.9.3 Is Required

✅ Yes

##### 1.6.3.9.4 Is Primary Key

❌ No

##### 1.6.3.9.5 Is Unique

❌ No

##### 1.6.3.9.6 Index Type

Index

##### 1.6.3.9.7 Size

0

##### 1.6.3.9.8 Constraints

*No items available*

##### 1.6.3.9.9 Default Value

true

##### 1.6.3.9.10 Is Foreign Key

❌ No

##### 1.6.3.9.11 Precision

0

##### 1.6.3.9.12 Scale

0

#### 1.6.3.10.0 DateTime

##### 1.6.3.10.1 Name

createdAt

##### 1.6.3.10.2 Type

🔹 DateTime

##### 1.6.3.10.3 Is Required

✅ Yes

##### 1.6.3.10.4 Is Primary Key

❌ No

##### 1.6.3.10.5 Is Unique

❌ No

##### 1.6.3.10.6 Index Type

None

##### 1.6.3.10.7 Size

0

##### 1.6.3.10.8 Constraints

*No items available*

##### 1.6.3.10.9 Default Value

CURRENT_TIMESTAMP

##### 1.6.3.10.10 Is Foreign Key

❌ No

##### 1.6.3.10.11 Precision

0

##### 1.6.3.10.12 Scale

0

#### 1.6.3.11.0 DateTime

##### 1.6.3.11.1 Name

updatedAt

##### 1.6.3.11.2 Type

🔹 DateTime

##### 1.6.3.11.3 Is Required

✅ Yes

##### 1.6.3.11.4 Is Primary Key

❌ No

##### 1.6.3.11.5 Is Unique

❌ No

##### 1.6.3.11.6 Index Type

None

##### 1.6.3.11.7 Size

0

##### 1.6.3.11.8 Constraints

*No items available*

##### 1.6.3.11.9 Default Value

CURRENT_TIMESTAMP

##### 1.6.3.11.10 Is Foreign Key

❌ No

##### 1.6.3.11.11 Precision

0

##### 1.6.3.11.12 Scale

0

### 1.6.4.0.0 Primary Keys

- goalId

### 1.6.5.0.0 Unique Constraints

- {'name': 'UC_Goal_User_Active_Type_Period_Start', 'columns': ['UserId', 'goalType', 'period', 'startDate', 'isActive']}

### 1.6.6.0.0 Indexes

- {'name': 'IX_Goal_UserId_IsActive', 'columns': ['UserId', 'isActive'], 'type': 'BTree'}

## 1.7.0.0.0 Recommendation

### 1.7.1.0.0 Name

Recommendation

### 1.7.2.0.0 Description

Stores AI-generated book recommendations and user feedback on them.

### 1.7.3.0.0 Attributes

#### 1.7.3.1.0 Guid

##### 1.7.3.1.1 Name

recommendationId

##### 1.7.3.1.2 Type

🔹 Guid

##### 1.7.3.1.3 Is Required

✅ Yes

##### 1.7.3.1.4 Is Primary Key

✅ Yes

##### 1.7.3.1.5 Is Unique

✅ Yes

##### 1.7.3.1.6 Index Type

UniqueIndex

##### 1.7.3.1.7 Size

0

##### 1.7.3.1.8 Constraints

*No items available*

##### 1.7.3.1.9 Default Value

null

##### 1.7.3.1.10 Is Foreign Key

❌ No

##### 1.7.3.1.11 Precision

0

##### 1.7.3.1.12 Scale

0

#### 1.7.3.2.0 Guid

##### 1.7.3.2.1 Name

UserId

##### 1.7.3.2.2 Type

🔹 Guid

##### 1.7.3.2.3 Is Required

✅ Yes

##### 1.7.3.2.4 Is Primary Key

❌ No

##### 1.7.3.2.5 Is Unique

❌ No

##### 1.7.3.2.6 Index Type

Index

##### 1.7.3.2.7 Size

0

##### 1.7.3.2.8 Constraints

*No items available*

##### 1.7.3.2.9 Default Value

null

##### 1.7.3.2.10 Is Foreign Key

✅ Yes

##### 1.7.3.2.11 Precision

0

##### 1.7.3.2.12 Scale

0

#### 1.7.3.3.0 VARCHAR

##### 1.7.3.3.1 Name

recommendedBookTitle

##### 1.7.3.3.2 Type

🔹 VARCHAR

##### 1.7.3.3.3 Is Required

✅ Yes

##### 1.7.3.3.4 Is Primary Key

❌ No

##### 1.7.3.3.5 Is Unique

❌ No

##### 1.7.3.3.6 Index Type

None

##### 1.7.3.3.7 Size

255

##### 1.7.3.3.8 Constraints

*No items available*

##### 1.7.3.3.9 Default Value

null

##### 1.7.3.3.10 Is Foreign Key

❌ No

##### 1.7.3.3.11 Precision

0

##### 1.7.3.3.12 Scale

0

#### 1.7.3.4.0 VARCHAR

##### 1.7.3.4.1 Name

recommendedBookAuthor

##### 1.7.3.4.2 Type

🔹 VARCHAR

##### 1.7.3.4.3 Is Required

✅ Yes

##### 1.7.3.4.4 Is Primary Key

❌ No

##### 1.7.3.4.5 Is Unique

❌ No

##### 1.7.3.4.6 Index Type

None

##### 1.7.3.4.7 Size

255

##### 1.7.3.4.8 Constraints

*No items available*

##### 1.7.3.4.9 Default Value

null

##### 1.7.3.4.10 Is Foreign Key

❌ No

##### 1.7.3.4.11 Precision

0

##### 1.7.3.4.12 Scale

0

#### 1.7.3.5.0 TEXT

##### 1.7.3.5.1 Name

reasoning

##### 1.7.3.5.2 Type

🔹 TEXT

##### 1.7.3.5.3 Is Required

✅ Yes

##### 1.7.3.5.4 Is Primary Key

❌ No

##### 1.7.3.5.5 Is Unique

❌ No

##### 1.7.3.5.6 Index Type

None

##### 1.7.3.5.7 Size

0

##### 1.7.3.5.8 Constraints

*No items available*

##### 1.7.3.5.9 Default Value

null

##### 1.7.3.5.10 Is Foreign Key

❌ No

##### 1.7.3.5.11 Precision

0

##### 1.7.3.5.12 Scale

0

#### 1.7.3.6.0 VARCHAR

##### 1.7.3.6.1 Name

feedback

##### 1.7.3.6.2 Type

🔹 VARCHAR

##### 1.7.3.6.3 Is Required

❌ No

##### 1.7.3.6.4 Is Primary Key

❌ No

##### 1.7.3.6.5 Is Unique

❌ No

##### 1.7.3.6.6 Index Type

Index

##### 1.7.3.6.7 Size

20

##### 1.7.3.6.8 Constraints

- ENUM('Like', 'Dislike')

##### 1.7.3.6.9 Default Value

null

##### 1.7.3.6.10 Is Foreign Key

❌ No

##### 1.7.3.6.11 Precision

0

##### 1.7.3.6.12 Scale

0

#### 1.7.3.7.0 DateTime

##### 1.7.3.7.1 Name

generatedAt

##### 1.7.3.7.2 Type

🔹 DateTime

##### 1.7.3.7.3 Is Required

✅ Yes

##### 1.7.3.7.4 Is Primary Key

❌ No

##### 1.7.3.7.5 Is Unique

❌ No

##### 1.7.3.7.6 Index Type

Index

##### 1.7.3.7.7 Size

0

##### 1.7.3.7.8 Constraints

*No items available*

##### 1.7.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.7.3.7.10 Is Foreign Key

❌ No

##### 1.7.3.7.11 Precision

0

##### 1.7.3.7.12 Scale

0

### 1.7.4.0.0 Primary Keys

- recommendationId

### 1.7.5.0.0 Unique Constraints

*No items available*

### 1.7.6.0.0 Indexes

#### 1.7.6.1.0 BTree

##### 1.7.6.1.1 Name

IX_Recommendation_UserId_GeneratedAt

##### 1.7.6.1.2 Columns

- UserId
- generatedAt

##### 1.7.6.1.3 Type

🔹 BTree

#### 1.7.6.2.0 BTree

##### 1.7.6.2.1 Name

IX_Recommendation_UserId_Feedback

##### 1.7.6.2.2 Columns

- UserId
- feedback

##### 1.7.6.2.3 Type

🔹 BTree

## 1.8.0.0.0 VocabularyItem

### 1.8.1.0.0 Name

VocabularyItem

### 1.8.2.0.0 Description

Stores words and definitions saved by the user.

### 1.8.3.0.0 Attributes

#### 1.8.3.1.0 Guid

##### 1.8.3.1.1 Name

vocabularyItemId

##### 1.8.3.1.2 Type

🔹 Guid

##### 1.8.3.1.3 Is Required

✅ Yes

##### 1.8.3.1.4 Is Primary Key

✅ Yes

##### 1.8.3.1.5 Is Unique

✅ Yes

##### 1.8.3.1.6 Index Type

UniqueIndex

##### 1.8.3.1.7 Size

0

##### 1.8.3.1.8 Constraints

*No items available*

##### 1.8.3.1.9 Default Value

null

##### 1.8.3.1.10 Is Foreign Key

❌ No

##### 1.8.3.1.11 Precision

0

##### 1.8.3.1.12 Scale

0

#### 1.8.3.2.0 Guid

##### 1.8.3.2.1 Name

UserId

##### 1.8.3.2.2 Type

🔹 Guid

##### 1.8.3.2.3 Is Required

✅ Yes

##### 1.8.3.2.4 Is Primary Key

❌ No

##### 1.8.3.2.5 Is Unique

❌ No

##### 1.8.3.2.6 Index Type

Index

##### 1.8.3.2.7 Size

0

##### 1.8.3.2.8 Constraints

*No items available*

##### 1.8.3.2.9 Default Value

null

##### 1.8.3.2.10 Is Foreign Key

✅ Yes

##### 1.8.3.2.11 Precision

0

##### 1.8.3.2.12 Scale

0

#### 1.8.3.3.0 VARCHAR

##### 1.8.3.3.1 Name

word

##### 1.8.3.3.2 Type

🔹 VARCHAR

##### 1.8.3.3.3 Is Required

✅ Yes

##### 1.8.3.3.4 Is Primary Key

❌ No

##### 1.8.3.3.5 Is Unique

❌ No

##### 1.8.3.3.6 Index Type

Index

##### 1.8.3.3.7 Size

100

##### 1.8.3.3.8 Constraints

*No items available*

##### 1.8.3.3.9 Default Value

null

##### 1.8.3.3.10 Is Foreign Key

❌ No

##### 1.8.3.3.11 Precision

0

##### 1.8.3.3.12 Scale

0

#### 1.8.3.4.0 TEXT

##### 1.8.3.4.1 Name

definition

##### 1.8.3.4.2 Type

🔹 TEXT

##### 1.8.3.4.3 Is Required

❌ No

##### 1.8.3.4.4 Is Primary Key

❌ No

##### 1.8.3.4.5 Is Unique

❌ No

##### 1.8.3.4.6 Index Type

None

##### 1.8.3.4.7 Size

0

##### 1.8.3.4.8 Constraints

*No items available*

##### 1.8.3.4.9 Default Value

null

##### 1.8.3.4.10 Is Foreign Key

❌ No

##### 1.8.3.4.11 Precision

0

##### 1.8.3.4.12 Scale

0

#### 1.8.3.5.0 DateTime

##### 1.8.3.5.1 Name

createdAt

##### 1.8.3.5.2 Type

🔹 DateTime

##### 1.8.3.5.3 Is Required

✅ Yes

##### 1.8.3.5.4 Is Primary Key

❌ No

##### 1.8.3.5.5 Is Unique

❌ No

##### 1.8.3.5.6 Index Type

None

##### 1.8.3.5.7 Size

0

##### 1.8.3.5.8 Constraints

*No items available*

##### 1.8.3.5.9 Default Value

CURRENT_TIMESTAMP

##### 1.8.3.5.10 Is Foreign Key

❌ No

##### 1.8.3.5.11 Precision

0

##### 1.8.3.5.12 Scale

0

#### 1.8.3.6.0 DateTime

##### 1.8.3.6.1 Name

updatedAt

##### 1.8.3.6.2 Type

🔹 DateTime

##### 1.8.3.6.3 Is Required

✅ Yes

##### 1.8.3.6.4 Is Primary Key

❌ No

##### 1.8.3.6.5 Is Unique

❌ No

##### 1.8.3.6.6 Index Type

None

##### 1.8.3.6.7 Size

0

##### 1.8.3.6.8 Constraints

*No items available*

##### 1.8.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.8.3.6.10 Is Foreign Key

❌ No

##### 1.8.3.6.11 Precision

0

##### 1.8.3.6.12 Scale

0

### 1.8.4.0.0 Primary Keys

- vocabularyItemId

### 1.8.5.0.0 Unique Constraints

- {'name': 'UC_VocabularyItem_User_Word', 'columns': ['UserId', 'word']}

### 1.8.6.0.0 Indexes

- {'name': 'IX_VocabularyItem_UserId_Word', 'columns': ['UserId', 'word'], 'type': 'BTree'}

## 1.9.0.0.0 DataExportJob

### 1.9.1.0.0 Name

DataExportJob

### 1.9.2.0.0 Description

Tracks the status of user-initiated data export requests.

### 1.9.3.0.0 Attributes

#### 1.9.3.1.0 Guid

##### 1.9.3.1.1 Name

dataExportJobId

##### 1.9.3.1.2 Type

🔹 Guid

##### 1.9.3.1.3 Is Required

✅ Yes

##### 1.9.3.1.4 Is Primary Key

✅ Yes

##### 1.9.3.1.5 Is Unique

✅ Yes

##### 1.9.3.1.6 Index Type

UniqueIndex

##### 1.9.3.1.7 Size

0

##### 1.9.3.1.8 Constraints

*No items available*

##### 1.9.3.1.9 Default Value

null

##### 1.9.3.1.10 Is Foreign Key

❌ No

##### 1.9.3.1.11 Precision

0

##### 1.9.3.1.12 Scale

0

#### 1.9.3.2.0 Guid

##### 1.9.3.2.1 Name

UserId

##### 1.9.3.2.2 Type

🔹 Guid

##### 1.9.3.2.3 Is Required

✅ Yes

##### 1.9.3.2.4 Is Primary Key

❌ No

##### 1.9.3.2.5 Is Unique

❌ No

##### 1.9.3.2.6 Index Type

Index

##### 1.9.3.2.7 Size

0

##### 1.9.3.2.8 Constraints

*No items available*

##### 1.9.3.2.9 Default Value

null

##### 1.9.3.2.10 Is Foreign Key

✅ Yes

##### 1.9.3.2.11 Precision

0

##### 1.9.3.2.12 Scale

0

#### 1.9.3.3.0 VARCHAR

##### 1.9.3.3.1 Name

status

##### 1.9.3.3.2 Type

🔹 VARCHAR

##### 1.9.3.3.3 Is Required

✅ Yes

##### 1.9.3.3.4 Is Primary Key

❌ No

##### 1.9.3.3.5 Is Unique

❌ No

##### 1.9.3.3.6 Index Type

Index

##### 1.9.3.3.7 Size

20

##### 1.9.3.3.8 Constraints

- ENUM('Queued', 'Processing', 'Completed', 'Failed')

##### 1.9.3.3.9 Default Value

'Queued'

##### 1.9.3.3.10 Is Foreign Key

❌ No

##### 1.9.3.3.11 Precision

0

##### 1.9.3.3.12 Scale

0

#### 1.9.3.4.0 VARCHAR

##### 1.9.3.4.1 Name

fileUrl

##### 1.9.3.4.2 Type

🔹 VARCHAR

##### 1.9.3.4.3 Is Required

❌ No

##### 1.9.3.4.4 Is Primary Key

❌ No

##### 1.9.3.4.5 Is Unique

❌ No

##### 1.9.3.4.6 Index Type

None

##### 1.9.3.4.7 Size

512

##### 1.9.3.4.8 Constraints

*No items available*

##### 1.9.3.4.9 Default Value

null

##### 1.9.3.4.10 Is Foreign Key

❌ No

##### 1.9.3.4.11 Precision

0

##### 1.9.3.4.12 Scale

0

#### 1.9.3.5.0 DateTime

##### 1.9.3.5.1 Name

expiresAt

##### 1.9.3.5.2 Type

🔹 DateTime

##### 1.9.3.5.3 Is Required

❌ No

##### 1.9.3.5.4 Is Primary Key

❌ No

##### 1.9.3.5.5 Is Unique

❌ No

##### 1.9.3.5.6 Index Type

Index

##### 1.9.3.5.7 Size

0

##### 1.9.3.5.8 Constraints

*No items available*

##### 1.9.3.5.9 Default Value

null

##### 1.9.3.5.10 Is Foreign Key

❌ No

##### 1.9.3.5.11 Precision

0

##### 1.9.3.5.12 Scale

0

#### 1.9.3.6.0 DateTime

##### 1.9.3.6.1 Name

createdAt

##### 1.9.3.6.2 Type

🔹 DateTime

##### 1.9.3.6.3 Is Required

✅ Yes

##### 1.9.3.6.4 Is Primary Key

❌ No

##### 1.9.3.6.5 Is Unique

❌ No

##### 1.9.3.6.6 Index Type

None

##### 1.9.3.6.7 Size

0

##### 1.9.3.6.8 Constraints

*No items available*

##### 1.9.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.9.3.6.10 Is Foreign Key

❌ No

##### 1.9.3.6.11 Precision

0

##### 1.9.3.6.12 Scale

0

#### 1.9.3.7.0 DateTime

##### 1.9.3.7.1 Name

updatedAt

##### 1.9.3.7.2 Type

🔹 DateTime

##### 1.9.3.7.3 Is Required

✅ Yes

##### 1.9.3.7.4 Is Primary Key

❌ No

##### 1.9.3.7.5 Is Unique

❌ No

##### 1.9.3.7.6 Index Type

None

##### 1.9.3.7.7 Size

0

##### 1.9.3.7.8 Constraints

*No items available*

##### 1.9.3.7.9 Default Value

CURRENT_TIMESTAMP

##### 1.9.3.7.10 Is Foreign Key

❌ No

##### 1.9.3.7.11 Precision

0

##### 1.9.3.7.12 Scale

0

### 1.9.4.0.0 Primary Keys

- dataExportJobId

### 1.9.5.0.0 Unique Constraints

*No items available*

### 1.9.6.0.0 Indexes

#### 1.9.6.1.0 BTree

##### 1.9.6.1.1 Name

IX_DataExportJob_UserId

##### 1.9.6.1.2 Columns

- UserId

##### 1.9.6.1.3 Type

🔹 BTree

#### 1.9.6.2.0 BTree

##### 1.9.6.2.1 Name

IX_DataExportJob_Status

##### 1.9.6.2.2 Columns

- status

##### 1.9.6.2.3 Type

🔹 BTree

#### 1.9.6.3.0 BTree

##### 1.9.6.3.1 Name

IX_DataExportJob_ExpiresAt

##### 1.9.6.3.2 Columns

- expiresAt

##### 1.9.6.3.3 Type

🔹 BTree

## 1.10.0.0.0 UserSetting

### 1.10.1.0.0 Name

UserSetting

### 1.10.2.0.0 Description

A key-value store for user-specific application settings like theme.

### 1.10.3.0.0 Attributes

#### 1.10.3.1.0 Guid

##### 1.10.3.1.1 Name

userSettingId

##### 1.10.3.1.2 Type

🔹 Guid

##### 1.10.3.1.3 Is Required

✅ Yes

##### 1.10.3.1.4 Is Primary Key

✅ Yes

##### 1.10.3.1.5 Is Unique

✅ Yes

##### 1.10.3.1.6 Index Type

UniqueIndex

##### 1.10.3.1.7 Size

0

##### 1.10.3.1.8 Constraints

*No items available*

##### 1.10.3.1.9 Default Value

null

##### 1.10.3.1.10 Is Foreign Key

❌ No

##### 1.10.3.1.11 Precision

0

##### 1.10.3.1.12 Scale

0

#### 1.10.3.2.0 Guid

##### 1.10.3.2.1 Name

UserId

##### 1.10.3.2.2 Type

🔹 Guid

##### 1.10.3.2.3 Is Required

✅ Yes

##### 1.10.3.2.4 Is Primary Key

❌ No

##### 1.10.3.2.5 Is Unique

❌ No

##### 1.10.3.2.6 Index Type

Index

##### 1.10.3.2.7 Size

0

##### 1.10.3.2.8 Constraints

*No items available*

##### 1.10.3.2.9 Default Value

null

##### 1.10.3.2.10 Is Foreign Key

✅ Yes

##### 1.10.3.2.11 Precision

0

##### 1.10.3.2.12 Scale

0

#### 1.10.3.3.0 VARCHAR

##### 1.10.3.3.1 Name

settingKey

##### 1.10.3.3.2 Type

🔹 VARCHAR

##### 1.10.3.3.3 Is Required

✅ Yes

##### 1.10.3.3.4 Is Primary Key

❌ No

##### 1.10.3.3.5 Is Unique

❌ No

##### 1.10.3.3.6 Index Type

Index

##### 1.10.3.3.7 Size

50

##### 1.10.3.3.8 Constraints

*No items available*

##### 1.10.3.3.9 Default Value

null

##### 1.10.3.3.10 Is Foreign Key

❌ No

##### 1.10.3.3.11 Precision

0

##### 1.10.3.3.12 Scale

0

#### 1.10.3.4.0 VARCHAR

##### 1.10.3.4.1 Name

settingValue

##### 1.10.3.4.2 Type

🔹 VARCHAR

##### 1.10.3.4.3 Is Required

✅ Yes

##### 1.10.3.4.4 Is Primary Key

❌ No

##### 1.10.3.4.5 Is Unique

❌ No

##### 1.10.3.4.6 Index Type

None

##### 1.10.3.4.7 Size

255

##### 1.10.3.4.8 Constraints

*No items available*

##### 1.10.3.4.9 Default Value

null

##### 1.10.3.4.10 Is Foreign Key

❌ No

##### 1.10.3.4.11 Precision

0

##### 1.10.3.4.12 Scale

0

#### 1.10.3.5.0 DateTime

##### 1.10.3.5.1 Name

createdAt

##### 1.10.3.5.2 Type

🔹 DateTime

##### 1.10.3.5.3 Is Required

✅ Yes

##### 1.10.3.5.4 Is Primary Key

❌ No

##### 1.10.3.5.5 Is Unique

❌ No

##### 1.10.3.5.6 Index Type

None

##### 1.10.3.5.7 Size

0

##### 1.10.3.5.8 Constraints

*No items available*

##### 1.10.3.5.9 Default Value

CURRENT_TIMESTAMP

##### 1.10.3.5.10 Is Foreign Key

❌ No

##### 1.10.3.5.11 Precision

0

##### 1.10.3.5.12 Scale

0

#### 1.10.3.6.0 DateTime

##### 1.10.3.6.1 Name

updatedAt

##### 1.10.3.6.2 Type

🔹 DateTime

##### 1.10.3.6.3 Is Required

✅ Yes

##### 1.10.3.6.4 Is Primary Key

❌ No

##### 1.10.3.6.5 Is Unique

❌ No

##### 1.10.3.6.6 Index Type

None

##### 1.10.3.6.7 Size

0

##### 1.10.3.6.8 Constraints

*No items available*

##### 1.10.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.10.3.6.10 Is Foreign Key

❌ No

##### 1.10.3.6.11 Precision

0

##### 1.10.3.6.12 Scale

0

### 1.10.4.0.0 Primary Keys

- userSettingId

### 1.10.5.0.0 Unique Constraints

- {'name': 'UC_UserSetting_User_Key', 'columns': ['UserId', 'settingKey']}

### 1.10.6.0.0 Indexes

- {'name': 'IX_UserSetting_UserId_Key', 'columns': ['UserId', 'settingKey'], 'type': 'BTree'}

## 1.11.0.0.0 UserReadingStats

### 1.11.1.0.0 Name

UserReadingStats

### 1.11.2.0.0 Description

A materialized view or summary table for daily user reading statistics, pre-aggregated to improve dashboard performance.

### 1.11.3.0.0 Attributes

#### 1.11.3.1.0 Guid

##### 1.11.3.1.1 Name

UserId

##### 1.11.3.1.2 Type

🔹 Guid

##### 1.11.3.1.3 Is Required

✅ Yes

##### 1.11.3.1.4 Is Primary Key

✅ Yes

##### 1.11.3.1.5 Is Unique

❌ No

##### 1.11.3.1.6 Index Type

None

##### 1.11.3.1.7 Size

0

##### 1.11.3.1.8 Constraints

*No items available*

##### 1.11.3.1.9 Default Value

null

##### 1.11.3.1.10 Is Foreign Key

✅ Yes

##### 1.11.3.1.11 Precision

0

##### 1.11.3.1.12 Scale

0

#### 1.11.3.2.0 Date

##### 1.11.3.2.1 Name

statDate

##### 1.11.3.2.2 Type

🔹 Date

##### 1.11.3.2.3 Is Required

✅ Yes

##### 1.11.3.2.4 Is Primary Key

✅ Yes

##### 1.11.3.2.5 Is Unique

❌ No

##### 1.11.3.2.6 Index Type

None

##### 1.11.3.2.7 Size

0

##### 1.11.3.2.8 Constraints

*No items available*

##### 1.11.3.2.9 Default Value

null

##### 1.11.3.2.10 Is Foreign Key

❌ No

##### 1.11.3.2.11 Precision

0

##### 1.11.3.2.12 Scale

0

#### 1.11.3.3.0 INT

##### 1.11.3.3.1 Name

totalMinutesRead

##### 1.11.3.3.2 Type

🔹 INT

##### 1.11.3.3.3 Is Required

✅ Yes

##### 1.11.3.3.4 Is Primary Key

❌ No

##### 1.11.3.3.5 Is Unique

❌ No

##### 1.11.3.3.6 Index Type

None

##### 1.11.3.3.7 Size

0

##### 1.11.3.3.8 Constraints

- NON_NEGATIVE

##### 1.11.3.3.9 Default Value

0

##### 1.11.3.3.10 Is Foreign Key

❌ No

##### 1.11.3.3.11 Precision

0

##### 1.11.3.3.12 Scale

0

#### 1.11.3.4.0 INT

##### 1.11.3.4.1 Name

totalPagesRead

##### 1.11.3.4.2 Type

🔹 INT

##### 1.11.3.4.3 Is Required

✅ Yes

##### 1.11.3.4.4 Is Primary Key

❌ No

##### 1.11.3.4.5 Is Unique

❌ No

##### 1.11.3.4.6 Index Type

None

##### 1.11.3.4.7 Size

0

##### 1.11.3.4.8 Constraints

- NON_NEGATIVE

##### 1.11.3.4.9 Default Value

0

##### 1.11.3.4.10 Is Foreign Key

❌ No

##### 1.11.3.4.11 Precision

0

##### 1.11.3.4.12 Scale

0

#### 1.11.3.5.0 DateTime

##### 1.11.3.5.1 Name

lastUpdatedAt

##### 1.11.3.5.2 Type

🔹 DateTime

##### 1.11.3.5.3 Is Required

✅ Yes

##### 1.11.3.5.4 Is Primary Key

❌ No

##### 1.11.3.5.5 Is Unique

❌ No

##### 1.11.3.5.6 Index Type

None

##### 1.11.3.5.7 Size

0

##### 1.11.3.5.8 Constraints

*No items available*

##### 1.11.3.5.9 Default Value

CURRENT_TIMESTAMP

##### 1.11.3.5.10 Is Foreign Key

❌ No

##### 1.11.3.5.11 Precision

0

##### 1.11.3.5.12 Scale

0

### 1.11.4.0.0 Primary Keys

- UserId
- statDate

### 1.11.5.0.0 Unique Constraints

*No items available*

### 1.11.6.0.0 Indexes

*No items available*

# 2.0.0.0.0 Relations

## 2.1.0.0.0 REL_USER_SUBSCRIPTION_001

### 2.1.1.0.0 Name

UserSubscriptions

### 2.1.2.0.0 Id

REL_USER_SUBSCRIPTION_001

### 2.1.3.0.0 Source Entity

User

### 2.1.4.0.0 Target Entity

Subscription

### 2.1.5.0.0 Type

🔹 OneToMany

### 2.1.6.0.0 Source Multiplicity

1

### 2.1.7.0.0 Target Multiplicity

0..*

### 2.1.8.0.0 Cascade Delete

✅ Yes

### 2.1.9.0.0 Is Identifying

❌ No

### 2.1.10.0.0 On Delete

Cascade

### 2.1.11.0.0 On Update

Cascade

## 2.2.0.0.0 REL_USER_USERBOOK_001

### 2.2.1.0.0 Name

UserLibraryBooks

### 2.2.2.0.0 Id

REL_USER_USERBOOK_001

### 2.2.3.0.0 Source Entity

User

### 2.2.4.0.0 Target Entity

UserBook

### 2.2.5.0.0 Type

🔹 OneToMany

### 2.2.6.0.0 Source Multiplicity

1

### 2.2.7.0.0 Target Multiplicity

0..*

### 2.2.8.0.0 Cascade Delete

✅ Yes

### 2.2.9.0.0 Is Identifying

❌ No

### 2.2.10.0.0 On Delete

Cascade

### 2.2.11.0.0 On Update

Cascade

## 2.3.0.0.0 REL_BOOK_USERBOOK_001

### 2.3.1.0.0 Name

BookLibraryEntries

### 2.3.2.0.0 Id

REL_BOOK_USERBOOK_001

### 2.3.3.0.0 Source Entity

Book

### 2.3.4.0.0 Target Entity

UserBook

### 2.3.5.0.0 Type

🔹 OneToMany

### 2.3.6.0.0 Source Multiplicity

1

### 2.3.7.0.0 Target Multiplicity

0..*

### 2.3.8.0.0 Cascade Delete

❌ No

### 2.3.9.0.0 Is Identifying

❌ No

### 2.3.10.0.0 On Delete

Restrict

### 2.3.11.0.0 On Update

Cascade

## 2.4.0.0.0 REL_USERBOOK_READINGSESSION_001

### 2.4.1.0.0 Name

UserBookReadingSessions

### 2.4.2.0.0 Id

REL_USERBOOK_READINGSESSION_001

### 2.4.3.0.0 Source Entity

UserBook

### 2.4.4.0.0 Target Entity

ReadingSession

### 2.4.5.0.0 Type

🔹 Composition

### 2.4.6.0.0 Source Multiplicity

1

### 2.4.7.0.0 Target Multiplicity

0..*

### 2.4.8.0.0 Cascade Delete

✅ Yes

### 2.4.9.0.0 Is Identifying

❌ No

### 2.4.10.0.0 On Delete

Cascade

### 2.4.11.0.0 On Update

Cascade

## 2.5.0.0.0 REL_USER_GOAL_001

### 2.5.1.0.0 Name

UserGoals

### 2.5.2.0.0 Id

REL_USER_GOAL_001

### 2.5.3.0.0 Source Entity

User

### 2.5.4.0.0 Target Entity

Goal

### 2.5.5.0.0 Type

🔹 OneToMany

### 2.5.6.0.0 Source Multiplicity

1

### 2.5.7.0.0 Target Multiplicity

0..*

### 2.5.8.0.0 Cascade Delete

✅ Yes

### 2.5.9.0.0 Is Identifying

❌ No

### 2.5.10.0.0 On Delete

Cascade

### 2.5.11.0.0 On Update

Cascade

## 2.6.0.0.0 REL_USER_RECOMMENDATION_001

### 2.6.1.0.0 Name

UserRecommendations

### 2.6.2.0.0 Id

REL_USER_RECOMMENDATION_001

### 2.6.3.0.0 Source Entity

User

### 2.6.4.0.0 Target Entity

Recommendation

### 2.6.5.0.0 Type

🔹 OneToMany

### 2.6.6.0.0 Source Multiplicity

1

### 2.6.7.0.0 Target Multiplicity

0..*

### 2.6.8.0.0 Cascade Delete

✅ Yes

### 2.6.9.0.0 Is Identifying

❌ No

### 2.6.10.0.0 On Delete

Cascade

### 2.6.11.0.0 On Update

Cascade

## 2.7.0.0.0 REL_USER_VOCABULARYITEM_001

### 2.7.1.0.0 Name

UserVocabularyItems

### 2.7.2.0.0 Id

REL_USER_VOCABULARYITEM_001

### 2.7.3.0.0 Source Entity

User

### 2.7.4.0.0 Target Entity

VocabularyItem

### 2.7.5.0.0 Type

🔹 Composition

### 2.7.6.0.0 Source Multiplicity

1

### 2.7.7.0.0 Target Multiplicity

0..*

### 2.7.8.0.0 Cascade Delete

✅ Yes

### 2.7.9.0.0 Is Identifying

❌ No

### 2.7.10.0.0 On Delete

Cascade

### 2.7.11.0.0 On Update

Cascade

## 2.8.0.0.0 REL_USER_DATAEXPORTJOB_001

### 2.8.1.0.0 Name

UserDataExportJobs

### 2.8.2.0.0 Id

REL_USER_DATAEXPORTJOB_001

### 2.8.3.0.0 Source Entity

User

### 2.8.4.0.0 Target Entity

DataExportJob

### 2.8.5.0.0 Type

🔹 OneToMany

### 2.8.6.0.0 Source Multiplicity

1

### 2.8.7.0.0 Target Multiplicity

0..*

### 2.8.8.0.0 Cascade Delete

✅ Yes

### 2.8.9.0.0 Is Identifying

❌ No

### 2.8.10.0.0 On Delete

Cascade

### 2.8.11.0.0 On Update

Cascade

## 2.9.0.0.0 REL_USER_USERSETTING_001

### 2.9.1.0.0 Name

UserSettings

### 2.9.2.0.0 Id

REL_USER_USERSETTING_001

### 2.9.3.0.0 Source Entity

User

### 2.9.4.0.0 Target Entity

UserSetting

### 2.9.5.0.0 Type

🔹 Composition

### 2.9.6.0.0 Source Multiplicity

1

### 2.9.7.0.0 Target Multiplicity

0..*

### 2.9.8.0.0 Cascade Delete

✅ Yes

### 2.9.9.0.0 Is Identifying

❌ No

### 2.9.10.0.0 On Delete

Cascade

### 2.9.11.0.0 On Update

Cascade

## 2.10.0.0.0 REL_USER_READINGSESSION_001

### 2.10.1.0.0 Name

UserReadingSessions

### 2.10.2.0.0 Id

REL_USER_READINGSESSION_001

### 2.10.3.0.0 Source Entity

User

### 2.10.4.0.0 Target Entity

ReadingSession

### 2.10.5.0.0 Type

🔹 OneToMany

### 2.10.6.0.0 Source Multiplicity

1

### 2.10.7.0.0 Target Multiplicity

0..*

### 2.10.8.0.0 Cascade Delete

✅ Yes

### 2.10.9.0.0 Is Identifying

❌ No

### 2.10.10.0.0 On Delete

Cascade

### 2.10.11.0.0 On Update

Cascade

