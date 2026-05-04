using Le2me.ViewModels;

namespace Le2me.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HomeViewModel vm && vm.Posts.Count == 0)
            await vm.LoadPostsCommand.ExecuteAsync(null);
    }
}
