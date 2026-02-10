using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Recommendations.Application.Interfaces
{
    /// <summary>
    /// Service responsible for converting text into vector embeddings suitable for similarity search.
    /// </summary>
    public interface IEmbeddingGenerator
    {
        /// <summary>
        /// Generates a vector embedding for the provided text input.
        /// </summary>
        /// <param name="text">The text to embed.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An array of floats representing the embedding vector.</returns>
        Task<float[]> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken);
    }
}