using Le2me.ViewModels;

namespace Le2me.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ProfileViewModel vm)
            await vm.LoadProfileCommand.ExecuteAsync(null);
    }
}
