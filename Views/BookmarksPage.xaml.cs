using Le2me.ViewModels;

namespace Le2me.Views;

public partial class BookmarksPage : ContentPage
{
    public BookmarksPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BookmarksViewModel vm)
            await vm.LoadBookmarksCommand.ExecuteAsync(null);
    }
}
