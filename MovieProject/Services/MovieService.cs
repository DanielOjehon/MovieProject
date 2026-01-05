using System.Collections.Generic;
using System.Threading.Tasks;
using MovieProject.Model;


namespace MovieProject.Services
{
    public class MovieService
    {
        List<Movie> movieList = new List<Movie>();

        public Task<List<Movie>> GetMovies()
        {
            return Task.FromResult(movieList);
        }
    }
}
