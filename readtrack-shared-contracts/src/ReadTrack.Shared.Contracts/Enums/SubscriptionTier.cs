using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.Enums;

/// <summary>
/// Defines the user's subscription level, which controls access to features and limits within the application.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubscriptionTier
{
    /// <summary>
    /// The default tier for all users.
    /// Subject to limitations (e.g., max 20 library items, 1 active goal) and includes advertisements.
    /// </summary>
    Free = 0,

    /// <summary>
    /// The paid tier.
    /// Grants unlimited library items, unlimited goals, advanced statistics, AI recommendations, and removes advertisements.
    /// </summary>
    Premium = 1
}