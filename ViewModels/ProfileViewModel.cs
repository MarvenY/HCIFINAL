using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Models;
using Le2me.Services;
using Le2me.Views;

namespace Le2me.ViewModels;

[QueryProperty(nameof(UserId), "UserId")]
public partial class ProfileViewModel : BaseViewModel
{
    private readonly IFirestoreService _store;
    private readonly IAuthService _auth;

    [ObservableProperty] private string _userId = string.Empty;
    [ObservableProperty] private UserProfile? _profile;
    [ObservableProperty] private ObservableCollection<Post> _userPosts = new();
    [ObservableProperty] private bool _isCurrentUser;
    [ObservableProperty] private bool _isFollowing;

    public ProfileViewModel(IFirestoreService store, IAuthService auth)
    {
        _store = store;
        _auth = auth;
    }

    partial void OnUserIdChanged(string value)
    {
        _ = LoadProfileAsync();
    }

    [RelayCommand]
    public async Task LoadProfileAsync()
    {
        var uid = string.IsNullOrEmpty(UserId) ? (_auth.CurrentUserId ?? string.Empty) : UserId;
        await RunSafeAsync(async () =>
        {
            Profile = await _store.GetUserProfileAsync(uid);
            if (Profile is not null)
            {
                IsCurrentUser = uid == _auth.CurrentUserId;
                IsFollowing = !IsCurrentUser && await _store.IsFollowingAsync(_auth.CurrentUserId ?? "", uid);
                var posts = await _store.GetPostsAsync();
                UserPosts.Clear();
                foreach (var p in posts.Where(p => p.AuthorUid == uid))
                    UserPosts.Add(p);
            }
        });
    }

    [RelayCommand]
    private async Task ToggleFollowAsync()
    {
        var me = _auth.CurrentUserId;
        if (me is null || Profile is null) return;
        if (IsFollowing)
            await _store.UnfollowUserAsync(me, Profile.Uid);
        else
            await _store.FollowUserAsync(me, Profile.Uid);
        IsFollowing = !IsFollowing;
    }

    [RelayCommand]
    private async Task GoToEditProfileAsync()
    {
        await Shell.Current.GoToAsync(nameof(EditProfilePage));
    }

    [RelayCommand]
    private async Task OpenPostAsync(Post post)
    {
        await Shell.Current.GoToAsync(nameof(PostDetailPage),
            new Dictionary<string, object> { ["Post"] = post });
    }
}
