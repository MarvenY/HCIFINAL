using System.Windows.Input;
using Le2me.Models;

namespace Le2me.Controls;

public partial class PostCardView : ContentView
{
    // ── Bindable Properties ──────────────────────────────────────────────
    public static readonly BindableProperty PostProperty =
        BindableProperty.Create(nameof(Post), typeof(Post), typeof(PostCardView));

    public static readonly BindableProperty ToggleLikeCommandProperty =
        BindableProperty.Create(nameof(ToggleLikeCommand), typeof(ICommand), typeof(PostCardView));

    public static readonly BindableProperty OpenPostCommandProperty =
        BindableProperty.Create(nameof(OpenPostCommand), typeof(ICommand), typeof(PostCardView));

    public Post? Post
    {
        get => (Post?)GetValue(PostProperty);
        set => SetValue(PostProperty, value);
    }

    public ICommand? ToggleLikeCommand
    {
        get => (ICommand?)GetValue(ToggleLikeCommandProperty);
        set => SetValue(ToggleLikeCommandProperty, value);
    }

    public ICommand? OpenPostCommand
    {
        get => (ICommand?)GetValue(OpenPostCommandProperty);
        set => SetValue(OpenPostCommandProperty, value);
    }

    public PostCardView()
    {
        InitializeComponent();
    }
}
