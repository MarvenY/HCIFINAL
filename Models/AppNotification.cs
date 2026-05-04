namespace Le2me.Models;

/// <summary>
/// Represents a notification pushed to the current user.
/// </summary>
public class AppNotification
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

    public string Display => $"{Title}: {Body}";
}
