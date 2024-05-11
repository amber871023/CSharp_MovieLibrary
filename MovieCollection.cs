using System;
using static System.Console;

namespace N11422807
{
    public class MovieCollection
    {
        private readonly Movie[] movies;
        private const int MaxMovies = 1000;
        private const int EmptyIndex = -1; // Flag for empty slots

        public MovieCollection()
        {
            movies = new Movie[MaxMovies];
            for (int i = 0; i < MaxMovies; i++)
            {
                movies[i] = null; // Initialize all slots to empty
            }
            AddMovie(new Movie("IFN664", "Drama", "G", 2.5));
            AddMovie(new Movie("IFN664", "Drama", "G", 2.5));
            AddMovie(new Movie("IFN664", "Drama", "MG", 2.5));
            AddMovie(new Movie("IFN666", "Crime", "M15+", 3));
            AddMovie(new Movie("IFN711", "Action", "M15+", 5));
            AddMovie(new Movie("IFN711", "Action", "M15+", 5));
            AddMovie(new Movie("QUT", "Adventure", "MA15+", 10));
        }

        public bool AddMovie(Movie movie, int numCopies = 1)
        {
            // Check if the collection is full
            if (!Array.Exists(movies, element => element == null))
            {
                //Failed to add movie: The collection is full.
                return false;
            }

            // Check if the movie already exists in the collection
            int index = GetHash(movie.Title);
            while (movies[index] != null && movies[index].Title != movie.Title)
            {
                index = (index + 1) % MaxMovies; // Linear Probing
            }

            if (movies[index] == null)
            {
                // Movie doesn't exist, add it to the collection
                movies[index] = new Movie(movie.Title, movie.Genre, movie.Classification, movie.Duration, numCopies);
                return true;
            }
            else
            {
                // Movie already exists, update the quantity
                movies[index].Quantity += numCopies;
                WriteLine($"Added {numCopies} copies of '{movie.Title}'.");
                return true;
            }
        }


        public int RemoveMovie(string title, int numCopies)
        {
            int index = GetHash(title);
            int probedIndex = index; // Keep track of original index for wrapping

            while (movies[index] != null)
            {
                if (movies[index].Title == title)
                {
                    if (movies[index].Quantity < numCopies)
                    {
                        // Not enough copies to remove, return current quantity
                        return movies[index].Quantity;
                    }

                    movies[index].Quantity -= numCopies;
                    if (movies[index].Quantity <= 0)
                    {
                        movies[index] = null; // Mark slot as empty
                    }
                    return movies[index]?.Quantity ?? 0;
                }

                index = (index + 1) % MaxMovies;
                if (index == probedIndex) // Reached starting index, no movie found
                {
                    return 0;
                }
            }

            return 0; // Movie not found
        }
        public Movie GetMovie(string title)
        {
            int index = GetHash(title);
            int probedIndex = index; // Keep track of original index for wrapping

            while (movies[index] != null)
            {
                if (movies[index].Title == title)
                {
                    return movies[index];
                }
                index = (index + 1) % MaxMovies;
                if (index == probedIndex) // Reached starting index, no movie found
                {
                    return null;
                }
            }

            return null; // Movie not found
        }

        public Movie[] GetAllMovies()
        {
            int count = 0;
            string[] uniqueTitles = new string[MaxMovies]; // Array to store unique titles

            Movie[] allMovies = new Movie[MaxMovies];
            for (int i = 0; i < MaxMovies; i++)
            {
                if (movies[i] != null)
                {
                    // Check if the title is unique
                    bool isUnique = true;
                    for (int j = 0; j < count; j++)
                    {
                        if (uniqueTitles[j] == movies[i].Title)
                        {
                            isUnique = false;
                            break;
                        }
                    }

                    if (isUnique)
                    {
                        // Add title to uniqueTitles array
                        uniqueTitles[count] = movies[i].Title;
                        count++;

                        // Add movie to allMovies array
                        allMovies[i] = movies[i];
                    }
                }
            }
            Array.Sort(allMovies, (movie1, movie2) =>
            {
                if (movie1 == null && movie2 == null) return 0;
                if (movie1 == null) return 1;
                if (movie2 == null) return -1;
                return movie1.Title.CompareTo(movie2.Title);
            });

            return allMovies;
        }
        private int GetHash(string title) // Simple Hash Function
        {
            int sum = 0;
            foreach (char c in title)
            {
                sum += (int)c;
            }
            return sum % MaxMovies;
        }

        public bool BorrowMovie(string title, Member borrower)
        {
            Movie movie = GetMovie(title);
            if (movie != null && movie.IsAvailable)
            {
                // Borrow the movie
                borrower.BorrowMovie(movie);
                return true;
            }
            else
            {
                WriteLine($"Sorry, '{title}' is not available for borrowing.");
                return false;
            }
        }
    }
}
