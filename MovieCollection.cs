using System;
using System.Linq;

namespace N11422807
{
    public class MovieCollection
    {
        private class HashNode
        {
            public string Title { get; }
            public Movie Movie { get; }
            public HashNode Next { get; set; }

            public HashNode(string title, Movie movie)
            {
                Title = title;
                Movie = movie;
                Next = null;
            }
        }

        private const int MaxMovies = 1000;
        private HashNode[] hashTable;
        private readonly Movie[] movies;

        public MovieCollection()
        {
            hashTable = new HashNode[MaxMovies];
            movies = new Movie[MaxMovies];
            for (int i = 0; i < MaxMovies; i++)
            {
                movies[i] = null; // Initialize all slots to empty
            }

            // Add initial movies
            AddMovie(new Movie("IFN664", "Drama", "G", 2.5));
            AddMovie(new Movie("IFN664", "Drama", "G", 2.5));
            AddMovie(new Movie("IFN664", "Drama", "G", 2.5));
            AddMovie(new Movie("IFN666", "Crime", "M15+", 3));
            AddMovie(new Movie("IFN711", "Action", "M15+", 5));
            AddMovie(new Movie("IFN711", "Action", "M15+", 5));
            AddMovie(new Movie("QUT", "Adventure", "MA15+", 10));
            AddMovie(new Movie("Mission: Impossible", "Action", "PG", 2.5));
            AddMovie(new Movie("Mission: Impossible 2", "Action", "PG", 2.5));
            AddMovie(new Movie("Harry Potter", "fantasy", "PG", 2.5));
            AddMovie(new Movie("Harry Potter", "fantasy", "PG", 2.5));
        }
        private int GetHash(string title)
        {
            int hash = 0;
            foreach (char c in title)
            {
                hash = (hash * 31) + c; // Using a prime number for better distribution
            }
            return Math.Abs(hash) % MaxMovies; // Ensure a positive index within the array bounds
        }
        public bool AddMovie(Movie movie, int numCopies = 1)
        {
            int index = GetHash(movie.Title);
            HashNode current = hashTable[index];

            while (current != null)
            {
                if (current.Title == movie.Title)
                {
                    // Movie already exists, update quantity
                    current.Movie.Quantity += numCopies;
                    UpdateMoviesArray(movie, 0);
                    return true; // Updated quantity
                }
                current = current.Next;
            }

            // Movie not found in collision chain, add new node
            HashNode newNode = new HashNode(movie.Title, movie);
            newNode.Next = hashTable[index];
            hashTable[index] = newNode;
            UpdateMoviesArray(movie, numCopies);
            return true; // Added successfully
        }

        public int RemoveMovie(string title, int numCopies)
        {
            int index = GetHash(title);
            HashNode current = hashTable[index];
            HashNode previous = null;

            while (current != null)
            {
                if (current.Movie.Title == title)
                {
                    if (current.Movie.Quantity < numCopies)
                    {
                        // Not enough copies to remove, return current quantity
                        return current.Movie.Quantity;
                    }

                    current.Movie.Quantity -= numCopies;
                    if (current.Movie.Quantity <= 0)
                    {
                        // Remove the node if all copies are removed
                        if (previous == null)
                        {
                            // If it's the first node in the chain
                            hashTable[index] = current.Next;
                        }
                        else
                        {
                            previous.Next = current.Next;
                        }
                        // If all copies are removed, delete the movie
                        UpdateMoviesArray(current.Movie, -current.Movie.Quantity);
                        return 999; // Indicate movie has been deleted
                    }
                    else
                    {
                        UpdateMoviesArray(current.Movie, 0);
                    }
                    return current.Movie.Quantity; // Return updated quantity
                }
                previous = current;
                current = current.Next;
            }
            return 0; // Movie not found
        }

        private void UpdateMoviesArray(Movie movie, int numCopies)
        {
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i] != null && movies[i].Title == movie.Title)
                {
                    movies[i].Quantity += numCopies;
                    if (movies[i].Quantity <= 0)
                    {
                        // If all copies are removed, set the movie to null
                        movies[i] = null;
                    }
                    return;
                }
            }
            // If the movie is not found in the movies array and copies are being added
            if (numCopies > 0)
            {
                for (int i = 0; i < movies.Length; i++)
                {
                    if (movies[i] == null)
                    {
                        movies[i] = movie;
                        return;
                    }
                }
            }
        }

        public Movie GetMovie(string title)
        {
            int index = GetHash(title);
            HashNode current = hashTable[index];
            while (current != null)
            {
                if (current.Title == title)
                {
                    return current.Movie;
                }
                current = current.Next;
            }
            return null;
        }

        public Movie[] GetAllMovies()
        {
            return movies.Where(movie => movie != null).ToArray();
        }

        public Movie[] GetTopThreeMovies()
        {
            Movie[] allMovies = FlattenHashTable();
            BubbleSortByBorrowingFrequency(allMovies);

            // Filter out movies with borrowing frequency <= 0
            allMovies = allMovies.Where(movie => movie.BorrowingFrequency > 0).ToArray();
            // Get the top three movies
            int length = Math.Min(3, allMovies.Length);
            Movie[] topThreeMovies = new Movie[length];
            Array.Copy(allMovies, topThreeMovies, length);
            return topThreeMovies;
        }

        private Movie[] FlattenHashTable()
        {
            // Flatten the hash table
            int count = 0;
            foreach (HashNode node in hashTable)
            {
                HashNode current = node;
                while (current != null)
                {
                    count++;
                    current = current.Next;
                }
            }

            Movie[] allMovies = new Movie[count];
            int index = 0;
            foreach (HashNode node in hashTable)
            {
                HashNode current = node;
                while (current != null)
                {
                    allMovies[index++] = current.Movie;
                    current = current.Next;
                }
            }
            return allMovies;
        }

        private void BubbleSortByBorrowingFrequency(Movie[] movies)
        {
            int n = movies.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (movies[j].BorrowingFrequency < movies[j + 1].BorrowingFrequency)
                    {
                        // Swap movies[j] and movies[j+1]
                        Movie temp = movies[j];
                        movies[j] = movies[j + 1];
                        movies[j + 1] = temp;
                    }
                }
            }
        }
    }
}
