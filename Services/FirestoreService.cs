using Le2me.Models;

namespace Le2me.Services;

/// <summary>
/// Stub Firestore implementation with sample data for UI development.
/// Replace each method body with real Firestore SDK calls once packages are wired up.
/// </summary>
public class FirestoreService : IFirestoreService
{
    // ── Sample data ────────────────────────────────────────────────────────
    private static readonly List<Post> _samplePosts = new()
    {
        new Post
        {
            Id = "post001",
            AuthorUid = "user001",
            AuthorName = "ChefMaria",
            Title = "Avocado Toast Supreme",
            Content = "Start with sourdough bread, mash ripe avocados, add chili flakes and a poached egg on top. Season with salt, pepper and squeeze of lemon.",
            ImageUrl = "https://images.unsplash.com/photo-1603046891744-1f84f5c2e7e5?w=600",
            LikeCount = 42,
            Calories = 320,
            CookingTime = "10 min",
            Timestamp = DateTime.UtcNow.AddHours(-2)
        },
        new Post
        {
            Id = "post002",
            AuthorUid = "user002",
            AuthorName = "GrillMaster",
            Title = "BBQ Chicken Skewers",
            Content = "Marinate chicken chunks in BBQ sauce, garlic, and honey overnight. Skewer with bell peppers and grill for 15 min turning frequently.",
            ImageUrl = "https://images.unsplash.com/photo-1555939594-58d7cb561ad1?w=600",
            LikeCount = 87,
            Calories = 450,
            CookingTime = "25 min",
            Timestamp = DateTime.UtcNow.AddHours(-5)
        },
        new Post
        {
            Id = "post003",
            AuthorUid = "user003",
            AuthorName = "VeganVibes",
            Title = "Rainbow Buddha Bowl",
            Content = "Layer quinoa, roasted sweet potato, chickpeas, cucumber, shredded purple cabbage, and drizzle with tahini lemon dressing.",
            ImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=600",
            LikeCount = 65,
            Calories = 510,
            CookingTime = "20 min",
            Timestamp = DateTime.UtcNow.AddDays(-1)
        },
        new Post
        {
            Id = "post004",
            AuthorUid = "user004",
            AuthorName = "PastaPrincess",
            Title = "Creamy Tuscan Pasta",
            Content = "Sauté garlic in olive oil. Add sun-dried tomatoes, spinach, heavy cream, and parmesan. Toss with cooked pasta and basil.",
            ImageUrl = "https://images.unsplash.com/photo-1621996346565-e3dbc646d9a9?w=600",
            LikeCount = 133,
            Calories = 680,
            CookingTime = "30 min",
            Timestamp = DateTime.UtcNow.AddDays(-2)
        },
        new Post
        {
            Id = "post005",
            AuthorUid = "user005",
            AuthorName = "SmoothieKing",
            Title = "Green Detox Smoothie",
            Content = "Blend spinach, frozen banana, mango chunks, coconut water, chia seeds, and a squeeze of lime. Serve immediately.",
            ImageUrl = "https://images.unsplash.com/photo-1502741338009-cac2772e18bc?w=600",
            LikeCount = 28,
            Calories = 195,
            CookingTime = "5 min",
            Timestamp = DateTime.UtcNow.AddDays(-3)
        }
    };

    private static readonly List<Comment> _sampleComments = new()
    {
        new Comment { Id = "c1", AuthorUid = "u2", AuthorName = "GrillMaster", Text = "Looks delicious! 🔥", Timestamp = DateTime.UtcNow.AddMinutes(-30) },
        new Comment { Id = "c2", AuthorUid = "u3", AuthorName = "VeganVibes", Text = "I added some feta on top, 10/10!", Timestamp = DateTime.UtcNow.AddMinutes(-15) }
    };

    // ── Posts ──────────────────────────────────────────────────────────────
    public async Task<List<Post>> GetPostsAsync(int limit = 30)
    {
        await Task.Delay(300); // simulate latency
        return _samplePosts.Take(limit).ToList();
    }

    public async Task<Post?> GetPostByIdAsync(string postId)
    {
        await Task.Delay(150);
        return _samplePosts.FirstOrDefault(p => p.Id == postId);
    }

    public async Task<string> CreatePostAsync(Post post)
    {
        await Task.Delay(400);
        post.Id = Guid.NewGuid().ToString();
        _samplePosts.Insert(0, post);
        return post.Id;
    }

    public async Task DeletePostAsync(string postId)
    {
        await Task.Delay(300);
        _samplePosts.RemoveAll(p => p.Id == postId);
    }

    public async Task ToggleLikeAsync(string postId, string userId)
    {
        await Task.Delay(200);
        var post = _samplePosts.FirstOrDefault(p => p.Id == postId);
        if (post is null) return;
        if (post.Likes.Contains(userId))
        {
            post.Likes.Remove(userId);
            post.LikeCount = Math.Max(0, post.LikeCount - 1);
        }
        else
        {
            post.Likes.Add(userId);
            post.LikeCount++;
        }
    }

    public async Task ToggleBookmarkAsync(string userId, string postId)
    {
        // In a real implementation this would update the user's Firestore document
        await Task.Delay(200);
    }

    // ── Comments ──────────────────────────────────────────────────────────
    public async Task<List<Comment>> GetCommentsAsync(string postId)
    {
        await Task.Delay(200);
        return _sampleComments.ToList();
    }

    public async Task AddCommentAsync(string postId, Comment comment)
    {
        await Task.Delay(250);
        comment.Id = Guid.NewGuid().ToString();
        _sampleComments.Add(comment);
    }

    public async Task DeleteCommentAsync(string postId, string commentId)
    {
        await Task.Delay(200);
        _sampleComments.RemoveAll(c => c.Id == commentId);
    }

    // ── Users ─────────────────────────────────────────────────────────────
    public async Task<UserProfile?> GetUserProfileAsync(string userId)
    {
        await Task.Delay(200);
        return new UserProfile
        {
            Uid = userId,
            Username = "SampleUser",
            ProfileIconUrl = "https://i.pravatar.cc/150?u=" + userId,
            PostCount = 5,
            FollowersCount = 42,
            FollowingCount = 18
        };
    }

    public async Task UpdateUsernameAsync(string userId, string newUsername)
    {
        await Task.Delay(300);
    }

    public async Task UpdateProfileIconAsync(string userId, string avatarPath)
    {
        await Task.Delay(200);
    }

    public async Task FollowUserAsync(string currentUserId, string targetUserId)
    {
        await Task.Delay(200);
    }

    public async Task UnfollowUserAsync(string currentUserId, string targetUserId)
    {
        await Task.Delay(200);
    }

    public async Task<bool> IsFollowingAsync(string currentUserId, string targetUserId)
    {
        await Task.Delay(100);
        return false;
    }

    // ── Explore ────────────────────────────────────────────────────────────
    public async Task<List<Post>> SearchRecipesAsync(string query, int limit = 20)
    {
        await Task.Delay(400);
        var q = query.ToLower();
        return _samplePosts
            .Where(p => p.Title.Contains(q, StringComparison.OrdinalIgnoreCase)
                     || p.Content.Contains(q, StringComparison.OrdinalIgnoreCase))
            .Take(limit)
            .ToList();
    }

    public async Task ReportPostAsync(string postId, string reportedByUid)
    {
        await Task.Delay(200);
    }
}
