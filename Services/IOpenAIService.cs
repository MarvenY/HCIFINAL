namespace Le2me.Services;

public interface IOpenAIService
{
    Task<string> GetRecipeSuggestionAsync(string prompt);
}
