using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MovieProject.Services;
using MovieProject.Model;

namespace MovieProject.ViewModel
{
    
    public partial class MovieViewModels:BaseViewModel
     {
        MovieService movieService;
        public ObservableCollection<Movie> Movies { get; } = new();
        public MovieViewModels(MovieService movieService)
        {
            Title = "MovieApp";
            this.movieService = movieService;
        }
        async Task GetMoviesAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
