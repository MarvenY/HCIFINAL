using CommunityToolkit.Maui;
using Le2me.Services;
using Le2me.ViewModels;
using Le2me.Views;

namespace Le2me;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // ── Services (Dependency Injection) ──────────────────────────────────
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<IFirestoreService, FirestoreService>();
        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();

        // ── ViewModels ────────────────────────────────────────────────────────
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<ChatAIViewModel>();
        builder.Services.AddTransient<ExploreViewModel>();
        builder.Services.AddTransient<CreatePostViewModel>();
        builder.Services.AddTransient<PostDetailViewModel>();
        builder.Services.AddTransient<NotificationsViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<EditProfileViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<BookmarksViewModel>();

        // ── Pages (Views) ─────────────────────────────────────────────────────
        builder.Services.AddTransient<SplashPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ChatAIPage>();
        builder.Services.AddTransient<ExplorePage>();
        builder.Services.AddTransient<CreatePostPage>();
        builder.Services.AddTransient<PostDetailPage>();
        builder.Services.AddTransient<NotificationsPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<EditProfilePage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<BookmarksPage>();
        builder.Services.AddTransient<AboutPage>();

        return builder.Build();
    }
}
