using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Models;
using Le2me.Services;

namespace Le2me.ViewModels;

[QueryProperty(nameof(CurrentPost), "Post")]
public partial class PostDetailViewModel : BaseViewModel
{
    private readonly IFirestoreService _store;
    private readonly IAuthService _auth;

    [ObservableProperty] private Post? _currentPost;
    [ObservableProperty] private ObservableCollection<Comment> _comments = new();
    [ObservableProperty] private string _commentText = string.Empty;
    [ObservableProperty] private bool _isBookmarked;

    public PostDetailViewModel(IFirestoreService store, IAuthService auth)
    {
        _store = store;
        _auth = auth;
    }

    partial void OnCurrentPostChanged(Post? value)
    {
        if (value is not null)
            _ = LoadCommentsAsync();
    }

    [RelayCommand]
    private async Task LoadCommentsAsync()
    {
        if (CurrentPost is null) return;
        await RunSafeAsync(async () =>
        {
            var list = await _store.GetCommentsAsync(CurrentPost.Id);
            Comments.Clear();
            foreach (var c in list) Comments.Add(c);
        });
    }

    [RelayCommand]
    private async Task ToggleLikeAsync()
    {
        var me = _auth.CurrentUserId;
        if (me is null || CurrentPost is null) return;
        await _store.ToggleLikeAsync(CurrentPost.Id, me);
        CurrentPost.IsLikedByCurrentUser = !CurrentPost.IsLikedByCurrentUser;
        CurrentPost.LikeCount += CurrentPost.IsLikedByCurrentUser ? 1 : -1;
        OnPropertyChanged(nameof(CurrentPost));
    }

    [RelayCommand]
    private async Task ToggleBookmarkAsync()
    {
        var me = _auth.CurrentUserId;
        if (me is null || CurrentPost is null) return;
        await _store.ToggleBookmarkAsync(me, CurrentPost.Id);
        IsBookmarked = !IsBookmarked;
    }

    [RelayCommand]
    private async Task SubmitCommentAsync()
    {
        var text = CommentText.Trim();
        if (string.IsNullOrEmpty(text) || CurrentPost is null) return;
        var me = _auth.CurrentUserId;
        if (me is null) return;

        await RunSafeAsync(async () =>
        {
            var comment = new Comment
            {
                AuthorUid = me,
                AuthorName = "Me",
                Text = text,
                Timestamp = DateTime.UtcNow
            };
            await _store.AddCommentAsync(CurrentPost.Id, comment);
            Comments.Add(comment);
            CommentText = string.Empty;
        });
    }

    [RelayCommand]
    private async Task DeleteCommentAsync(Comment comment)
    {
        if (CurrentPost is null) return;
        await _store.DeleteCommentAsync(CurrentPost.Id, comment.Id);
        Comments.Remove(comment);
    }

    [RelayCommand]
    private async Task DeletePostAsync()
    {
        if (CurrentPost is null) return;
        var confirm = await Shell.Current.DisplayAlert(
            "Delete Post", "Are you sure you want to delete this post?", "Delete", "Cancel");
        if (!confirm) return;
        await _store.DeletePostAsync(CurrentPost.Id);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task SharePostAsync()
    {
        if (CurrentPost is null) return;
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = CurrentPost.Title,
            Text = $"Check out this recipe on Le2me: {CurrentPost.Title}"
        });
    }
}
