using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Services;

namespace Le2me.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly IAuthService _auth;

    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;

    public RegisterViewModel(IAuthService auth)
    {
        _auth = auth;
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Passwords do not match.";
            return;
        }

        await RunSafeAsync(async () =>
        {
            var (success, error) = await _auth.RegisterAsync(Email, Password, Username);
            if (!success)
            {
                ErrorMessage = error ?? "Registration failed. Please try again.";
                return;
            }
            // After registration go to main app
            Application.Current!.MainPage = new AppShell();
        });
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Application.Current!.MainPage!.Navigation.PopAsync();
    }
}
