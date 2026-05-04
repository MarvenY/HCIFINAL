using Le2me.Models;
using Le2me.ViewModels;

namespace Le2me.Views;

[QueryProperty(nameof(Post), "Post")]
public partial class PostDetailPage : ContentPage
{
    private PostDetailViewModel? _vm => BindingContext as PostDetailViewModel;

    public Post? Post
    {
        set
        {
            if (_vm is not null)
                _vm.CurrentPost = value;
        }
    }

    public PostDetailPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_vm?.CurrentPost is not null)
            await _vm.LoadCommentsCommand.ExecuteAsync(null);
    }
}
