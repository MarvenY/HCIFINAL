using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Services;
using Le2me.Views;

namespace Le2me.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthService _auth;

    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty;

    public LoginViewModel(IAuthService auth)
    {
        _auth = auth;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        await RunSafeAsync(async () =>
        {
            var (success, error) = await _auth.LoginAsync(Email, Password);
            if (!success)
            {
                ErrorMessage = error ?? "Login failed. Please check your credentials.";
                return;
            }
            // Navigate to the main shell
            Application.Current!.MainPage = new AppShell();
        });
    }

    [RelayCommand]
    private async Task GoToRegisterAsync()
    {
        await Application.Current!.MainPage!.Navigation.PushAsync(new RegisterPage());
    }
}
