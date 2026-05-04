using Le2me.ViewModels;

namespace Le2me.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        // ViewModel is set in XAML but we can also inject via DI:
        // BindingContext = viewModel;
    }
}
