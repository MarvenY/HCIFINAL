using Le2me.Models;

namespace Le2me.Services;

/// <summary>
/// Authentication service contract (backed by Firebase Auth).
/// </summary>
public interface IAuthService
{
    /// <summary>Currently signed-in user UID, or null.</summary>
    string? CurrentUserId { get; }

    string? CurrentUserEmail { get; }

    Task<(bool Success, string? Error)> LoginAsync(string email, string password);

    Task<(bool Success, string? Error)> RegisterAsync(string email, string password, string username);

    Task SignOutAsync();

    Task SendPasswordResetEmailAsync(string email);
}
