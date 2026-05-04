namespace Le2me.Models;

/// <summary>
/// Represents a comment on a Post.
/// </summary>
public class Comment
{
    public string Id { get; set; } = string.Empty;
    public string AuthorUid { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorAvatarUrl { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime? Timestamp { get; set; }

    public string TimestampDisplay =>
        Timestamp.HasValue ? Timestamp.Value.ToString("MMM d  h:mm tt") : string.Empty;
}
