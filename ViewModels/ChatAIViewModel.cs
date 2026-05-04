using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Models;
using Le2me.Services;

namespace Le2me.ViewModels;

public partial class ChatAIViewModel : BaseViewModel
{
    private readonly IOpenAIService _ai;

    [ObservableProperty] private string _inputText = string.Empty;
    [ObservableProperty] private ObservableCollection<ChatMessage> _messages = new();

    public ChatAIViewModel(IOpenAIService ai)
    {
        _ai = ai;
        // Welcome message
        Messages.Add(new ChatMessage
        {
            IsUser = false,
            Text = "👋 Hi! I'm Le2me AI — your personal kitchen assistant. Ask me for recipe ideas, ingredient substitutions, cooking tips, or anything food-related!"
        });
    }

    [RelayCommand]
    private async Task SendAsync()
    {
        var text = InputText.Trim();
        if (string.IsNullOrEmpty(text)) return;

        Messages.Add(new ChatMessage { IsUser = true, Text = text });
        InputText = string.Empty;

        await RunSafeAsync(async () =>
        {
            var reply = await _ai.GetRecipeSuggestionAsync(text);
            Messages.Add(new ChatMessage { IsUser = false, Text = reply });
        });
    }

    [RelayCommand]
    private void ClearChat()
    {
        Messages.Clear();
        Messages.Add(new ChatMessage
        {
            IsUser = false,
            Text = "Chat cleared. How can I help you today?"
        });
    }
}

public class ChatMessage
{
    public bool IsUser { get; set; }
    public string Text { get; set; } = string.Empty;
    public LayoutOptions HorizontalOptions => IsUser ? LayoutOptions.End : LayoutOptions.Start;
    public Color BubbleColor => IsUser
        ? Color.FromArgb("#51CE5C")
        : Color.FromArgb("#E0E0E0");
    public Color TextColor => IsUser ? Colors.White : Color.FromArgb("#212121");
}
