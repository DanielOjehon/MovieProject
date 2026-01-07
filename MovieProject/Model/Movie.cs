using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Model
{
    public class Movie
    {

        public string Title { get; set; } = string.Empty;
        public string Emoji { get; set; } = string.Empty;
        public int Year { get; set; }
        public string[] Genre { get; set; } = Array.Empty<string>();
        public string Director { get; set; } = string.Empty;
        public double Rating { get; set; }

        
        public string GenreString { get; set; } = string.Empty;

       
        public bool IsFavorite { get; set; } = false;
    }
}
