using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Models;
using Le2me.Services;
using Le2me.Views;

namespace Le2me.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    private readonly IFirestoreService _store;
    private readonly IAuthService _auth;

    [ObservableProperty] private ObservableCollection<Post> _posts = new();
    [ObservableProperty] private bool _isRefreshing;

    public HomeViewModel(IFirestoreService store, IAuthService auth)
    {
        _store = store;
        _auth = auth;
    }

    [RelayCommand]
    public async Task LoadPostsAsync()
    {
        await RunSafeAsync(async () =>
        {
            var list = await _store.GetPostsAsync();
            var me = _auth.CurrentUserId ?? string.Empty;
            Posts.Clear();
            foreach (var p in list)
            {
                p.IsLikedByCurrentUser = p.Likes.Contains(me);
                Posts.Add(p);
            }
        });
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;
        await LoadPostsAsync();
        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task OpenPostAsync(Post post)
    {
        var vm = new PostDetailViewModel(_store, _auth) { CurrentPost = post };
        await Shell.Current.GoToAsync(nameof(PostDetailPage),
            new Dictionary<string, object> { ["Post"] = post });
    }

    [RelayCommand]
    private async Task ToggleLikeAsync(Post post)
    {
        var me = _auth.CurrentUserId;
        if (me is null) return;
        await _store.ToggleLikeAsync(post.Id, me);
        post.IsLikedByCurrentUser = !post.IsLikedByCurrentUser;
        post.LikeCount += post.IsLikedByCurrentUser ? 1 : -1;
    }

    [RelayCommand]
    private async Task GoToNotificationsAsync()
    {
        await Shell.Current.GoToAsync(nameof(NotificationsPage));
    }
}
