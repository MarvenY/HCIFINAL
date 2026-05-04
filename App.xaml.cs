using Le2me.Views;

namespace Le2me;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Start at the Splash screen; it will redirect to Login or Home
        MainPage = new NavigationPage(new SplashPage())
        {
            BarBackgroundColor = Color.FromArgb("#51CE5C"),
            BarTextColor = Colors.White
        };
    }
}
