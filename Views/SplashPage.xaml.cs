using Le2me.Services;

namespace Le2me.Views;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Simulate init delay (Firebase, Hive, etc.)
        await Task.Delay(2000);

        // Navigate to Login page (replace splash in nav stack)
        if (Navigation.NavigationStack.Count > 0)
        {
            await Navigation.PushAsync(new LoginPage());

            // Remove splash from back stack
            var existing = Navigation.NavigationStack[0];
            Navigation.RemovePage(existing);
        }
    }
}
