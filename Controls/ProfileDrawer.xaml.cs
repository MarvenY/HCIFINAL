using Le2me.Views;

namespace Le2me.Controls;

public partial class ProfileDrawer : ContentPage
{
    public ProfileDrawer()
    {
        InitializeComponent();
    }

    private async void OnProfileTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
        Shell.Current.FlyoutIsPresented = false;
    }

    private async void OnBookmarksTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BookmarksPage));
        Shell.Current.FlyoutIsPresented = false;
    }

    private async void OnSettingsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
        Shell.Current.FlyoutIsPresented = false;
    }

    private async void OnAboutTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AboutPage));
        Shell.Current.FlyoutIsPresented = false;
    }

    private async void OnSignOutTapped(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = false;
        var confirm = await Shell.Current.DisplayAlert("Sign Out", "Are you sure?", "Sign Out", "Cancel");
        if (confirm)
        {
            Application.Current!.MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromArgb("#51CE5C"),
                BarTextColor = Colors.White
            };
        }
    }
}
