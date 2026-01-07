using System;

namespace MovieProject.Model
{
    public class Movie
    {
        // Core movie data (from JSON)
        public string Title { get; set; } = string.Empty;
        public string Emoji { get; set; } = string.Empty;
        public int Year { get; set; }
        public string[] Genre { get; set; } = Array.Empty<string>();
        public string Director { get; set; } = string.Empty;
        public double Rating { get; set; }

        // Display helper
        public string GenreString { get; set; } = string.Empty;

        // Favourite state (persisted)
        public bool IsFavorite { get; set; } = false;

        // Unique ID for persistence (simple & safe)
        public string Id => Title;
    }
}
