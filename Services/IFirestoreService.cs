using Le2me.Models;

namespace Le2me.Services;

/// <summary>
/// Firestore data-access contract.
/// </summary>
public interface IFirestoreService
{
    // ── Posts ──────────────────────────────────────────────────────────────
    Task<List<Post>> GetPostsAsync(int limit = 30);
    Task<Post?> GetPostByIdAsync(string postId);
    Task<string> CreatePostAsync(Post post);
    Task DeletePostAsync(string postId);
    Task ToggleLikeAsync(string postId, string userId);
    Task ToggleBookmarkAsync(string userId, string postId);

    // ── Comments ──────────────────────────────────────────────────────────
    Task<List<Comment>> GetCommentsAsync(string postId);
    Task AddCommentAsync(string postId, Comment comment);
    Task DeleteCommentAsync(string postId, string commentId);

    // ── Users ─────────────────────────────────────────────────────────────
    Task<UserProfile?> GetUserProfileAsync(string userId);
    Task UpdateUsernameAsync(string userId, string newUsername);
    Task UpdateProfileIconAsync(string userId, string avatarPath);

    Task FollowUserAsync(string currentUserId, string targetUserId);
    Task UnfollowUserAsync(string currentUserId, string targetUserId);
    Task<bool> IsFollowingAsync(string currentUserId, string targetUserId);

    // ── Explore (scraped recipes) ─────────────────────────────────────────
    Task<List<Post>> SearchRecipesAsync(string query, int limit = 20);

    // ── Report ────────────────────────────────────────────────────────────
    Task ReportPostAsync(string postId, string reportedByUid);
}
