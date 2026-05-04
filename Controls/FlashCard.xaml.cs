namespace Le2me.Controls;

public partial class FlashCard : ContentView
{
    public static readonly BindableProperty CardTypeProperty =
        BindableProperty.Create(nameof(CardType), typeof(string), typeof(FlashCard),
            defaultValue: "welcome", propertyChanged: OnCardTypeChanged);

    public string CardType
    {
        get => (string)GetValue(CardTypeProperty);
        set => SetValue(CardTypeProperty, value);
    }

    public FlashCard()
    {
        InitializeComponent();
    }

    private static void OnCardTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is FlashCard card)
            card.ApplyCardType(newValue as string ?? "welcome");
    }

    private void ApplyCardType(string type)
    {
        switch (type.ToLower())
        {
            case "explore":
                BackgroundBox.Color = Color.FromArgb("#1565C0");
                IconLabel.Text = "🔍";
                TitleLabel.Text = "Go Explore";
                SubtitleLabel.Text = "Dive into thousands of recipes";
                break;

            case "chat":
                BackgroundBox.Color = Color.FromArgb("#6A1B9A");
                IconLabel.Text = "🤖";
                TitleLabel.Text = "Chat with Le2me AI";
                SubtitleLabel.Text = "Your all-in-one kitchen assistant";
                break;

            default: // "welcome"
                BackgroundBox.Color = Color.FromArgb("#2E7D32");
                IconLabel.Text = "🍽️";
                TitleLabel.Text = "Welcome to Le2me";
                SubtitleLabel.Text = "Your all-in-one food application";
                break;
        }
    }
}
