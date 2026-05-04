using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Services;
using Le2me.Views;

namespace Le2me.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly IAuthService _auth;

    [ObservableProperty] private bool _isDarkMode;
    [ObservableProperty] private string _appVersion = "0.8.2";

    public SettingsViewModel(IAuthService auth)
    {
        _auth = auth;
        _isDarkMode = Application.Current?.RequestedTheme == AppTheme.Dark;
    }

    partial void OnIsDarkModeChanged(bool value)
    {
        if (Application.Current is not null)
            Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
    }

    [RelayCommand]
    private async Task GoToAboutAsync()
    {
        await Shell.Current.GoToAsync(nameof(AboutPage));
    }

    [RelayCommand]
    private async Task SignOutAsync()
    {
        var confirm = await Shell.Current.DisplayAlert("Sign Out", "Are you sure?", "Sign Out", "Cancel");
        if (!confirm) return;
        await RunSafeAsync(async () =>
        {
            await _auth.SignOutAsync();
            Application.Current!.MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromArgb("#51CE5C"),
                BarTextColor = Colors.White
            };
        });
    }
}
