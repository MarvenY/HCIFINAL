using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Le2me.Models;
using Le2me.Services;
using Le2me.Views;

namespace Le2me.ViewModels;

public partial class ExploreViewModel : BaseViewModel
{
    private readonly IFirestoreService _store;

    [ObservableProperty] private string _searchQuery = string.Empty;
    [ObservableProperty] private ObservableCollection<Post> _results = new();
    [ObservableProperty] private bool _hasSearched;

    public ExploreViewModel(IFirestoreService store)
    {
        _store = store;
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery)) return;
        await RunSafeAsync(async () =>
        {
            HasSearched = true;
            var list = await _store.SearchRecipesAsync(SearchQuery);
            Results.Clear();
            foreach (var p in list) Results.Add(p);
        });
    }

    [RelayCommand]
    private async Task OpenPostAsync(Post post)
    {
        await Shell.Current.GoToAsync(nameof(PostDetailPage),
            new Dictionary<string, object> { ["Post"] = post });
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchQuery = string.Empty;
        Results.Clear();
        HasSearched = false;
    }
}
