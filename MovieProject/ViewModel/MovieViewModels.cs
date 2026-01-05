using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using MovieProject.Model;

namespace MovieProject.ViewModel
{
    
    public partial class MovieViewModels:BaseViewModel
     {
        public ObservableCollection<Movie> Movies { get; } = new();
        public MovieViewModels()
        {
            Title = "MovieApp";
        }
    }
}
