using MovieProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;

namespace MovieProject.ViewModel
{
    public class MovieViewModels
    {
        public ObservableCollection<Movie> Movies { get; } = new();

       public MovieViewModels() 
        {
            LoadMovies();
        }

        private async void LoadMovies()
        {
            using var client = new HttpClient();

            var movies = await client.GetFromJsonAsync<List<Movie>>(
                "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json"
            );

            if (movies == null) return;

            // Prepare genre display
            foreach (var m in movies)
            {
                m.GenreString = string.Join(", ", m.Genre);
            }

            Movies.Clear();
            foreach (var movie in movies)
                Movies.Add(movie);
        }
    }
    
}
