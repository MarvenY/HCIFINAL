using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Models;
using Le2me.Services;
using Le2me.Views;

namespace Le2me.ViewModels;

public partial class BookmarksViewModel : BaseViewModel
{
    private readonly IFirestoreService _store;
    private readonly IAuthService _auth;

    [ObservableProperty] private ObservableCollection<Post> _bookmarks = new();

    public BookmarksViewModel(IFirestoreService store, IAuthService auth)
    {
        _store = store;
        _auth = auth;
    }

    [RelayCommand]
    public async Task LoadBookmarksAsync()
    {
        await RunSafeAsync(async () =>
        {
            // In real implementation, fetch user's bookmark IDs then each post
            var all = await _store.GetPostsAsync();
            Bookmarks.Clear();
            // Stub: show all posts as if bookmarked
            foreach (var p in all.Take(3))
            {
                p.IsBookmarked = true;
                Bookmarks.Add(p);
            }
        });
    }

    [RelayCommand]
    private async Task OpenPostAsync(Post post)
    {
        await Shell.Current.GoToAsync(nameof(PostDetailPage),
            new Dictionary<string, object> { ["Post"] = post });
    }

    [RelayCommand]
    private async Task RemoveBookmarkAsync(Post post)
    {
        var me = _auth.CurrentUserId;
        if (me is null) return;
        await _store.ToggleBookmarkAsync(me, post.Id);
        Bookmarks.Remove(post);
    }
}
