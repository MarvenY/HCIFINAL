namespace Le2me.Models;

/// <summary>
/// Represents a user-generated post or recipe card in the feed.
/// </summary>
public class Post
{
    public string Id { get; set; } = string.Empty;
    public string AuthorUid { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorAvatarUrl { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    /// <summary>Content can be plain text or a list of steps (for recipes).</summary>
    public string Content { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int LikeCount { get; set; }
    public List<string> Likes { get; set; } = new();
    public int CommentCount { get; set; }

    // Recipe-specific extras
    public int Calories { get; set; }
    public string CookingTime { get; set; } = string.Empty;

    public DateTime? Timestamp { get; set; }

    // Computed helpers
    public bool IsLikedByCurrentUser { get; set; }
    public bool IsBookmarked { get; set; }

    public string TimestampDisplay =>
        Timestamp.HasValue ? Timestamp.Value.ToString("MMM d, yyyy  h:mm tt") : "Unknown";
}
