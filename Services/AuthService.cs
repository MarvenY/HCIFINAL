namespace Le2me.Services;

/// <summary>
/// Stub implementation of IAuthService.
/// Replace the bodies with real Firebase Auth SDK calls once NuGet packages are added.
/// </summary>
public class AuthService : IAuthService
{
    // ── Simulated in-memory state ──────────────────────────────────────────
    private string? _currentUserId;
    private string? _currentUserEmail;

    public string? CurrentUserId => _currentUserId;
    public string? CurrentUserEmail => _currentUserEmail;

    public async Task<(bool Success, string? Error)> LoginAsync(string email, string password)
    {
        // TODO: Replace with Firebase Auth sign-in
        await Task.Delay(600); // simulate network call

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return (false, "Email and password are required.");

        // Stub: accept any credentials for demo
        _currentUserId = Guid.NewGuid().ToString();
        _currentUserEmail = email;
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> RegisterAsync(string email, string password, string username)
    {
        // TODO: Replace with Firebase Auth create user + Firestore user doc
        await Task.Delay(600);

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username))
            return (false, "All fields are required.");

        _currentUserId = Guid.NewGuid().ToString();
        _currentUserEmail = email;
        return (true, null);
    }

    public async Task SignOutAsync()
    {
        // TODO: Firebase Auth sign-out
        await Task.Delay(100);
        _currentUserId = null;
        _currentUserEmail = null;
    }

    public async Task SendPasswordResetEmailAsync(string email)
    {
        // TODO: Firebase Auth sendPasswordResetEmail
        await Task.Delay(400);
    }
}
