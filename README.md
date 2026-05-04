# Le2me – .NET MAUI C#/XAML Rebuild

A full remake of the original **Le2me** Flutter/Dart senior project, rebuilt as a **.NET MAUI** application using **C#** and **XAML**.

---

## 📱 App Overview

Le2me is a food/recipe social network where users can:
- Browse a recipe feed with like, comment, bookmark & share
- Chat with an AI kitchen assistant (OpenAI GPT)
- Explore/search a recipe database
- Create & post their own recipes (with AI "Need Help?" generator)
- Follow other users and view their profiles
- Save bookmarks and receive notifications

---

## 🗂️ Project Structure

```
Le2me/
├── Le2me.csproj              # MAUI project file + NuGet packages
├── MauiProgram.cs            # Dependency injection setup
├── App.xaml / App.xaml.cs   # App resources (colors, styles, converters)
├── AppShell.xaml / .cs       # Navigation: Flyout sidebar + Bottom tab bar
│
├── Models/
│   ├── Post.cs
│   ├── UserProfile.cs
│   ├── Comment.cs
│   └── AppNotification.cs
│
├── Services/
│   ├── IAuthService.cs  /  AuthService.cs       (Firebase Auth stub)
│   ├── IFirestoreService.cs / FirestoreService.cs (Firestore stub w/ sample data)
│   └── IOpenAIService.cs  / OpenAIService.cs    (OpenAI Chat Completions)
│
├── ViewModels/               # MVVM (CommunityToolkit.Mvvm)
│   ├── BaseViewModel.cs
│   ├── LoginViewModel.cs
│   ├── RegisterViewModel.cs
│   ├── HomeViewModel.cs
│   ├── ChatAIViewModel.cs
│   ├── ExploreViewModel.cs
│   ├── CreatePostViewModel.cs
│   ├── PostDetailViewModel.cs
│   ├── NotificationsViewModel.cs
│   ├── ProfileViewModel.cs
│   ├── EditProfileViewModel.cs
│   ├── SettingsViewModel.cs
│   └── BookmarksViewModel.cs
│
├── Views/                    # Pages (XAML + code-behind)
│   ├── SplashPage
│   ├── LoginPage
│   ├── RegisterPage
│   ├── HomePage             ← feed + flash-card carousel
│   ├── ChatAIPage           ← AI chat bubbles
│   ├── ExplorePage          ← search recipes
│   ├── CreatePostPage       ← new recipe with AI helper
│   ├── PostDetailPage       ← full post + comments
│   ├── NotificationsPage
│   ├── ProfilePage          ← stats + posts grid
│   ├── EditProfilePage      ← avatar + username
│   ├── SettingsPage
│   ├── BookmarksPage
│   └── AboutPage
│
├── Controls/
│   ├── PostCardView.xaml/cs  ← reusable feed card
│   ├── FlashCard.xaml/cs     ← home page carousel card
│   └── ProfileDrawer.xaml/cs ← sidebar flyout
│
└── Converters/
    └── Converters.cs         ← StringToBool, InvertBool, BoolToString
```

---

## 🚀 Setup & Running

### Prerequisites
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) **with .NET MAUI workload** installed  
  *or* [.NET 9 SDK](https://dotnet.microsoft.com/) + `dotnet workload install maui`
- Android Emulator / iOS Simulator / Windows

### Steps
1. Open `Le2me/Le2me.csproj` in Visual Studio 2022
2. Restore NuGet packages (automatic on open)
3. Select a target: **Android Emulator**, **iOS Simulator**, or **Windows**
4. Press **▶ Run** (F5)

### Connect to Firebase (production)
The services are **stub implementations** with sample data.  
To use real Firebase:
1. Add NuGet packages: `Plugin.Firebase.Auth`, `Plugin.Firebase.Firestore`, `Plugin.Firebase.Storage`
2. Replace stub method bodies in `AuthService.cs` and `FirestoreService.cs`
3. Place your `google-services.json` (Android) / `GoogleService-Info.plist` (iOS) in the project

### Enable OpenAI AI Chat
Open `Services/OpenAIService.cs` and replace:
```csharp
private const string ApiKey = "YOUR_OPENAI_API_KEY_HERE";
```
with your actual key from [platform.openai.com](https://platform.openai.com).

---

## 🎨 Navigation & UI

| Feature | Implementation |
|---|---|
| Bottom tab bar (Home, AI Chat, Explore, Add Post) | `AppShell` `<TabBar>` |
| Slide-out sidebar drawer (Profile, Bookmarks, Settings, Sign Out) | `AppShell` `FlyoutBehavior=Flyout` + `ProfileDrawer` |
| Back buttons | MAUI `NavigationPage` / Shell back navigation (automatic) |
| Scrollable pages | `ScrollView` / `CollectionView` with `RefreshView` pull-to-refresh |
| Dark mode | `AppThemeBinding` on all colors + `SettingsViewModel.IsDarkMode` toggle |
| Swipe-to-dismiss notifications | `SwipeView` in `NotificationsPage` |
| Swipe-to-remove bookmarks | `SwipeView` in `BookmarksPage` |

---

## 🔄 Page Flow

```
Splash ──► Login ──► [Register]
               │
               ▼
         AppShell (Tab Bar + Drawer)
          ├── Home ──► PostDetail ──► Profile
          │       └──► Notifications
          ├── AI Chat
          ├── Explore ──► PostDetail
          └── Add Post
         Drawer:
          ├── Profile ──► EditProfile
          ├── Bookmarks ──► PostDetail
          ├── Settings ──► About
          └── Sign Out ──► Login
```
