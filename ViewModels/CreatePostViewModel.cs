using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Models;
using Le2me.Services;

namespace Le2me.ViewModels;

public partial class CreatePostViewModel : BaseViewModel
{
    private readonly IFirestoreService _store;
    private readonly IAuthService _auth;
    private readonly IOpenAIService _ai;

    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private string _content = string.Empty;
    [ObservableProperty] private string _imageUrl = string.Empty;
    [ObservableProperty] private string _calories = string.Empty;
    [ObservableProperty] private string _cookingTime = string.Empty;
    [ObservableProperty] private bool _isGenerating;
    [ObservableProperty] private string _successMessage = string.Empty;

    public CreatePostViewModel(IFirestoreService store, IAuthService auth, IOpenAIService ai)
    {
        _store = store;
        _auth = auth;
        _ai = ai;
    }

    [RelayCommand]
    private async Task GenerateRecipeDetailsAsync()
    {
        if (string.IsNullOrWhiteSpace(Title)) return;
        IsGenerating = true;
        try
        {
            var suggestion = await _ai.GetRecipeSuggestionAsync(
                $"Give me a short recipe description for: {Title}. " +
                "Include estimated calories and cooking time. Keep it under 150 words.");
            Content = suggestion;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsGenerating = false;
        }
    }

    [RelayCommand]
    private async Task SubmitPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Content))
        {
            ErrorMessage = "Title and content are required.";
            return;
        }

        await RunSafeAsync(async () =>
        {
            var me = _auth.CurrentUserId ?? "anonymous";
            var post = new Post
            {
                AuthorUid = me,
                AuthorName = "Me", // Replace with real username fetch
                Title = Title,
                Content = Content,
                ImageUrl = ImageUrl,
                Calories = int.TryParse(Calories, out var cal) ? cal : 0,
                CookingTime = CookingTime,
                Timestamp = DateTime.UtcNow
            };
            await _store.CreatePostAsync(post);
            SuccessMessage = "Post created successfully!";
            Title = Content = ImageUrl = Calories = CookingTime = string.Empty;
        });
    }
}
