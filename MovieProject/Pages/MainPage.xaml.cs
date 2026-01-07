using MovieProject.Model;
using MovieProject.Pages;
using MovieProject.ViewModel;
namespace MovieProject
{

    public partial class MainPage : ContentPage
    {
        private MovieViewModels _viewModel;
        private List<Movie> _favourites = new();

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MovieViewModels();
            BindingContext = _viewModel;
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
                .Where(m => m.Title.ToLower().Contains(keyword) || m.Genre.Any(g => g.ToLower().Contains(keyword)))
                .ToList();
        }

        private void OnFavouriteClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is Movie movie)
            {
                movie.IsFavorite = !movie.IsFavorite;

                if (movie.IsFavorite && !_favourites.Contains(movie))
                    _favourites.Add(movie);
                else if (!movie.IsFavorite && _favourites.Contains(movie))
                   _favourites.Remove(movie);

                btn.Text = movie.IsFavorite ? "♥" : "♡";
            }
        }

        private async void GoToFavouritesPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavouritesPage(_favourites));
        }

        private async void GoToSettingsPage(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new SettingsPage());
        }

    }
}
