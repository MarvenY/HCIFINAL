using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Services;

namespace Le2me.ViewModels;

public partial class EditProfileViewModel : BaseViewModel
{
    private readonly IFirestoreService _store;
    private readonly IAuthService _auth;

    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _selectedAvatar = string.Empty;
    [ObservableProperty] private string _successMessage = string.Empty;

    public List<string> Avatars { get; } = new()
    {
        "https://i.pravatar.cc/150?img=1",
        "https://i.pravatar.cc/150?img=2",
        "https://i.pravatar.cc/150?img=3",
        "https://i.pravatar.cc/150?img=4",
        "https://i.pravatar.cc/150?img=5",
        "https://i.pravatar.cc/150?img=6",
        "https://i.pravatar.cc/150?img=7",
        "https://i.pravatar.cc/150?img=8",
        "https://i.pravatar.cc/150?img=9",
    };

    public EditProfileViewModel(IFirestoreService store, IAuthService auth)
    {
        _store = store;
        _auth = auth;
    }

    [RelayCommand]
    private async Task SaveUsernameAsync()
    {
        if (string.IsNullOrWhiteSpace(Username)) return;
        await RunSafeAsync(async () =>
        {
            await _store.UpdateUsernameAsync(_auth.CurrentUserId!, Username);
            SuccessMessage = "Username updated!";
        });
    }

    [RelayCommand]
    private async Task SelectAvatarAsync(string avatarUrl)
    {
        SelectedAvatar = avatarUrl;
        await RunSafeAsync(async () =>
        {
            await _store.UpdateProfileIconAsync(_auth.CurrentUserId!, avatarUrl);
            SuccessMessage = "Avatar updated!";
        });
    }

    [RelayCommand]
    private async Task ResetPasswordAsync()
    {
        var email = _auth.CurrentUserEmail;
        if (email is null) return;
        await RunSafeAsync(async () =>
        {
            await _auth.SendPasswordResetEmailAsync(email);
            SuccessMessage = "Password reset email sent!";
        });
    }
}
