using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MovieProject.Model;


namespace MovieProject.Services
{
    public class MovieService
    {
        HttpClient httpClient;
        public MovieService()
        {
            httpClient = new HttpClient();
        }

        List<Movie> movieList = new ();

        public async Task<List<Movie>> GetMovies()
        {
            if (movieList?.Count>0)
                return movieList;

            var url = "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json";

            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                movieList = await response.Content.ReadFromJsonAsync<List<Movie>>();
            }
            return movieList;
        }
    }
}
