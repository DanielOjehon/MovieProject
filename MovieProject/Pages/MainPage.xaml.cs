using MovieProject.Model;
using MovieProject.ViewModel;
using Microsoft.Maui.Storage;
using MovieProject.Pages;

namespace MovieProject;

public partial class MainPage : ContentPage
{
    private MovieViewModels _viewModel;
    private List<Movie> _favourites = new();

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MovieViewModels();
        BindingContext = _viewModel;

        _favourites = _viewModel.Movies.Where(m => m.IsFavorite).ToList();
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

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = e.NewTextValue?.ToLower() ?? "";

        MoviesCollectionView.ItemsSource = _viewModel.Movies
            .Where(m =>
                m.Title.ToLower().Contains(keyword) ||
                m.Genre.Any(g => g.ToLower().Contains(keyword)) ||
                (!string.IsNullOrEmpty(m.Director) &&
                 m.Director.ToLower().Contains(keyword)))
            .ToList();
    }

    private async void OnFavouriteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Movie movie)
        {
            movie.IsFavorite = !movie.IsFavorite;

            if (movie.IsFavorite)
                _favourites.Add(movie);
            else
                _favourites.Remove(movie);

            SaveFavourites();
            btn.Text = movie.IsFavorite ? "♥" : "♡";

            if (movie.IsFavorite && btn.Parent is HorizontalStackLayout hStack)
            {
                var emoji = hStack.Children.OfType<Label>().FirstOrDefault();
                if (emoji != null)
                {
                    await emoji.RotateTo(360, 500);
                    emoji.Rotation = 0;
                }
            }
        }
    }

    private void SaveFavourites()
    {
        var ids = _favourites.Select(m => m.Id).ToList();
        var json = System.Text.Json.JsonSerializer.Serialize(ids);
        Preferences.Set("favourites", json);
    }

    private async void GoToFavouritesPage(object sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(FavouritesPage));

    private async void GoToSettingsPage(object sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(SettingsPage));
}
