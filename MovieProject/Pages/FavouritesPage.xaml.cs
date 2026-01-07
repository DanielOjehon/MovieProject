using MovieProject.Model;
using MovieProject.Pages;

namespace MovieProject.Pages;

public partial class FavouritesPage : ContentPage
{
    public FavouritesPage(List<Movie> favourites)
    {
        InitializeComponent();
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
