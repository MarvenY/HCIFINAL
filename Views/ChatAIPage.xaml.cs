using Le2me.ViewModels;

namespace Le2me.Views;

public partial class ChatAIPage : ContentPage
{
    public ChatAIPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Auto-scroll to latest message when new ones arrive
        if (BindingContext is ChatAIViewModel vm)
            vm.Messages.CollectionChanged += (_, _) => ScrollToBottom();
    }

    private void ScrollToBottom()
    {
        if (BindingContext is ChatAIViewModel vm && vm.Messages.Count > 0)
            MessageList.ScrollTo(vm.Messages[^1], animate: true);
    }
}
