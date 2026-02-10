using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Recommendations.Application.Interfaces
{
    public record ChatMessage(string Role, string Content);
    
    public record ChatPrompt(
        string SystemMessage, 
        string UserMessage, 
        float Temperature = 0.7f, 
        int MaxTokens = 1000
    );

    /// <summary>
    /// Abstraction for interacting with Large Language Model providers (e.g., OpenAI).
    /// </summary>
    public interface ILLMClient
    {
        /// <summary>
        /// Sends a prompt to the LLM and retrieves the generated response.
        /// </summary>
        /// <param name="prompt">The structured chat prompt.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The raw text content of the LLM's response.</returns>
        Task<string> GetChatCompletionAsync(ChatPrompt prompt, CancellationToken cancellationToken);
    }
}