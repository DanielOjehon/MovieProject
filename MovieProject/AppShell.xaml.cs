using MovieProject.Pages;
namespace MovieProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));
            Routing.RegisterRoute(nameof(FavouritesPage), typeof(FavouritesPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }
    }
}
