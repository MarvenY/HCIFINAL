using System.Net.Http.Json;
using System.Text.Json;

namespace Le2me.Services;

/// <summary>
/// Calls the OpenAI Chat Completions API to generate recipe suggestions.
/// Set your API key in the constant below or read from secure storage.
/// </summary>
public class OpenAIService : IOpenAIService
{
    private const string ApiKey = "YOUR_OPENAI_API_KEY_HERE";
    private const string Model = "gpt-4o-mini";
    private const string ApiUrl = "https://api.openai.com/v1/chat/completions";

    private readonly HttpClient _http;

    public OpenAIService()
    {
        _http = new HttpClient();
        _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
    }

    public async Task<string> GetRecipeSuggestionAsync(string prompt)
    {
        if (ApiKey == "YOUR_OPENAI_API_KEY_HERE")
        {
            // Demo response when key is not configured
            await Task.Delay(800);
            return $"Here's a recipe idea for \"{prompt}\":\n\n" +
                   "Ingredients:\n- 2 cups flour\n- 1 cup milk\n- 2 eggs\n- Salt & pepper\n\n" +
                   "Instructions:\n1. Mix dry ingredients.\n2. Add wet ingredients.\n3. Cook until golden.\n\n" +
                   "⚠️ Add your OpenAI API key in OpenAIService.cs to get real AI suggestions!";
        }

        var payload = new
        {
            model = Model,
            messages = new[]
            {
                new { role = "system", content = "You are Le2me, a friendly cooking and recipe assistant. Provide helpful, concise recipe suggestions." },
                new { role = "user", content = prompt }
            },
            max_tokens = 512
        };

        var response = await _http.PostAsJsonAsync(ApiUrl, payload);
        response.EnsureSuccessStatusCode();

        using var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        return json.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "No response received.";
    }
}
