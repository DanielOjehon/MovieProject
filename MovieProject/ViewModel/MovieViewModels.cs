using MovieProject.Model;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace MovieProject.ViewModel
{
    public class MovieViewModels
    {
        private const string MoviesUrl =
            "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json";

        private const string CacheFileName = "movies_cache.json";
        private const string FavouritesKey = "favourites";

        public ObservableCollection<Movie> Movies { get; } = new();

        public MovieViewModels()
        {
            LoadMoviesAsync();
        }

        private async void LoadMoviesAsync()
        {
            List<Movie>? movies = null;
            string cachePath = Path.Combine(FileSystem.AppDataDirectory, CacheFileName);

            
            if (File.Exists(cachePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(cachePath);
                    movies = JsonSerializer.Deserialize<List<Movie>>(json);
                }
                catch
                {
                    movies = null;
                }
            }

            
            if (movies == null)
            {
                try
                {
                    using var client = new HttpClient();
                    movies = await client.GetFromJsonAsync<List<Movie>>(MoviesUrl);

                    if (movies != null)
                    {
                        var json = JsonSerializer.Serialize(movies);
                        await File.WriteAllTextAsync(cachePath, json);
                    }
                }
                catch
                {
                    movies = new List<Movie>();
                }
            }

            if (movies == null) return;

            
            var favJson = Preferences.Get(FavouritesKey, "");
            var favouriteIds = string.IsNullOrEmpty(favJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(favJson) ?? new List<string>();

            
            foreach (var movie in movies)
            {
                movie.GenreString = string.Join(", ", movie.Genre);
                movie.IsFavorite = favouriteIds.Contains(movie.Id);
            }

            
            Movies.Clear();
            foreach (var movie in movies)
                Movies.Add(movie);
        }
    }
}
