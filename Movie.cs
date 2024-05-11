using System;
using static System.Console;

namespace N11422807
{
    public class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }  // drama, adventure, family, action, sci-fi, comedy, animated, thriller, or Other
        public string Classification { get; set; }  // General (G), Parental Guidance (PG), Mature (M15+), or Mature Accompanied (MA15+)
        public double Duration { get; set; }  // Duration in minutes
        public int Quantity { get; set; }  // Number of DVDs of the same movie title

        public Movie(string title, string genre, string classification, double duration, int quantity = 1)
        {
            Title = title;
            Genre = genre;
            Classification = classification;
            Duration = duration;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Genre: {Genre}, Classification: {Classification}, Duration: {Duration} hrs \n";
        }
    }
}

