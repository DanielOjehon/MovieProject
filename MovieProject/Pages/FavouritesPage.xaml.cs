using MovieProject.Model;
using Microsoft.Maui.Storage;
using System.Text.Json;

namespace MovieProject.Pages;

public partial class FavouritesPage : ContentPage
{
    public FavouritesPage()
    {
        InitializeComponent();
        LoadFavourites();
    }

    private void LoadFavourites()
    {
        var favJson = Preferences.Get("favourites", "");
        var favIds = string.IsNullOrEmpty(favJson)
            ? new List<string>()
            : JsonSerializer.Deserialize<List<string>>(favJson) ?? new List<string>();

        var cachePath = Path.Combine(FileSystem.AppDataDirectory, "movies_cache.json");

        if (!File.Exists(cachePath))
            return;

        var json = File.ReadAllText(cachePath);
        var movies = JsonSerializer.Deserialize<List<Movie>>(json) ?? new List<Movie>();

        var favourites = movies
            .Where(m => favIds.Contains(m.Id))
            .ToList();

        FavouritesCollectionView.ItemsSource = favourites;
    }

    private async void OnMovieSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Movie movie)
        {
            await Shell.Current.GoToAsync(nameof(MovieDetailPage),
                new Dictionary<string, object> { { "movie", movie } });

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
