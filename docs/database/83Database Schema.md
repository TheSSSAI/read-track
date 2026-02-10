# 1 Title

AI Recommendation Vector Database

# 2 Name

ai_recommendation_vectors

# 3 Db Type

- vector
- search

# 4 Db Technology

Amazon OpenSearch Serverless

# 5 Entities

- {'name': 'UserContextEmbedding', 'description': "An OpenSearch index storing vector embeddings of user's reading history, goals, and vocabulary for k-NN similarity search to power AI recommendations.", 'attributes': [{'name': 'embeddingId', 'type': 'Guid', 'isRequired': True, 'isPrimaryKey': True, 'size': 0, 'isUnique': True, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'UserId', 'type': 'Guid', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'embeddingVector', 'type': 'k-NN Vector', 'isRequired': True, 'isPrimaryKey': False, 'size': 1536, 'isUnique': False, 'constraints': ['dimension: 1536 (e.g., for OpenAI text-embedding-ada-002)', 'space_type: cosinesimil'], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'sourceText', 'type': 'TEXT', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'sourceType', 'type': 'VARCHAR', 'isRequired': True, 'isPrimaryKey': False, 'size': 50, 'isUnique': False, 'constraints': ["ENUM('ReadingHistory', 'Goal', 'Vocabulary', 'Feedback')"], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'sourceId', 'type': 'Guid', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'createdAt', 'type': 'DateTime', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': [], 'precision': 0, 'scale': 0, 'isForeignKey': False}], 'primaryKeys': ['embeddingId'], 'uniqueConstraints': [], 'indexes': [{'name': 'IX_Embedding_Vector', 'columns': ['embeddingVector'], 'type': 'HNSW (Hierarchical Navigable Small World)'}, {'name': 'IX_Embedding_UserId_SourceType', 'columns': ['UserId', 'sourceType'], 'type': 'Keyword'}]}

