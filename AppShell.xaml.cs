using Le2me.Views;

namespace Le2me;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes for pages that are pushed on the navigation stack
        Routing.RegisterRoute(nameof(PostDetailPage), typeof(PostDetailPage));
        Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(BookmarksPage), typeof(BookmarksPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
    }
}
