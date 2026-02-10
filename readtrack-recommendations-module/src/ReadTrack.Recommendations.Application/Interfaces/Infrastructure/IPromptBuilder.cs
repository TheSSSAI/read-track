using System.Collections.Generic;
using ReadTrack.Recommendations.Application.Interfaces;

namespace ReadTrack.Recommendations.Application.Interfaces.Infrastructure
{
    /// <summary>
    /// Builder interface for constructing optimized prompts for the LLM.
    /// </summary>
    public interface IPromptBuilder
    {
        /// <summary>
        /// Constructs the system prompt that defines the AI persona and constraints.
        /// </summary>
        /// <returns>The system prompt string.</returns>
        string BuildSystemPrompt();

        /// <summary>
        /// Constructs the user prompt combining reading history, preferences, and vector context.
        /// </summary>
        /// <param name="readingHistory">List of books the user has read.</param>
        /// <param name="vectorContext">Relevant context segments retrieved from vector search.</param>
        /// <param name="userPreferences">Optional explicit user preferences string.</param>
        /// <returns>The user prompt string.</returns>
        string BuildUserPrompt(IEnumerable<ReadingHistoryItem> readingHistory, IEnumerable<string> vectorContext, string? userPreferences = null);
    }
}