using MovieProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.ViewModel
{
    public class MovieViewModels
    {
        private const string MoviesUrl = "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json";
        private const string CacheFileName = "movies_cache.json";

        public ObservableCollection<Movie> Movies { get; } = new();

        public MovieViewModels()
        {
            LoadMoviesAsync();
        }

        private async void LoadMoviesAsync()
        {
            List<Movie> movies = null;

            string cachePath = Path.Combine(FileSystem.AppDataDirectory, CacheFileName);

            // 1️⃣ Try reading from cache
            if (File.Exists(cachePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(cachePath);
                    movies = System.Text.Json.JsonSerializer.Deserialize<List<Movie>>(json);
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
                        
                        var json = System.Text.Json.JsonSerializer.Serialize(movies);
                        await File.WriteAllTextAsync(cachePath, json);
                    }
                }
                catch
                {
                   
                    movies = new List<Movie>();
                }
            }

            if (movies == null) return;

            
            foreach (var m in movies)
                m.GenreString = string.Join(", ", m.Genre);

            Movies.Clear();
            foreach (var movie in movies)
                Movies.Add(movie);
        }
    }
}
