using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ReadTrack.Recommendations.Domain.Entities;

namespace ReadTrack.Recommendations.Application.Interfaces
{
    /// <summary>
    /// Abstraction for interacting with the Vector Database (e.g., Amazon OpenSearch).
    /// </summary>
    public interface IVectorStoreClient
    {
        /// <summary>
        /// Stores a user context embedding in the vector index.
        /// </summary>
        /// <param name="embedding">The embedding entity to index.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task IndexEmbeddingAsync(UserContextEmbedding embedding, CancellationToken cancellationToken);

        /// <summary>
        /// Performs a k-Nearest Neighbors search to find context relevant to the query vector.
        /// </summary>
        /// <param name="queryVector">The vector representation of the query.</param>
        /// <param name="limit">The maximum number of results to return.</param>
        /// <param name="minScore">The minimum similarity score threshold.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of relevant text segments found in the vector store.</returns>
        Task<List<string>> SearchSimilarAsync(float[] queryVector, int limit, float minScore, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes embeddings associated with a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task DeleteUserEmbeddingsAsync(System.Guid userId, CancellationToken cancellationToken);
    }
}