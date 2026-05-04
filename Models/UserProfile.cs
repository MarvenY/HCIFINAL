namespace Le2me.Models;

/// <summary>
/// Represents an application user stored in Firestore's "Users" collection.
/// </summary>
public class UserProfile
{
    public string Uid { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    /// <summary>Cloud Storage path or HTTPS URL for the profile avatar.</summary>
    public string ProfileIconUrl { get; set; } = string.Empty;

    public int PostCount { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }

    public List<string> Bookmarks { get; set; } = new();

    // Computed
    public bool IsCurrentUser { get; set; }
    public bool IsFollowedByCurrentUser { get; set; }
}
