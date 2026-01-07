using MovieProject.Model;
namespace MovieProject.Pages;

[QueryProperty(nameof(Movie), "movie")]
public partial class MovieDetailPage : ContentPage
{
    public Movie Movie
    {
        set => BindingContext = value;
    }

    public MovieDetailPage()
    {
        InitializeComponent();
    }
}
