using System;

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

            // Add initial movies
            AddMovie(new Movie("Mission: Impossible", "Action", "PG", 2.5, 3));
            AddMovie(new Movie("Mission: Impossible 2", "Action", "PG", 2.5, 3));
            AddMovie(new Movie("Harry Potter", "Other", "PG", 2.5, 5));
            AddMovie(new Movie("Harry Potter1", "Other", "PG", 2.5, 1));
            AddMovie(new Movie("It", "thriller", "MA15+", 2, 2));
            AddMovie(new Movie("Inception", "Sci-Fi", "PG", 2.5, 1));
            AddMovie(new Movie("The Dark Knight", "Action", "PG", 2.5, 2));
            AddMovie(new Movie("Pulp Fiction", "Other", "G", 3, 1));
            AddMovie(new Movie("The Silence of the Lambs", "Thriller", "MA15+", 3, 1));
            AddMovie(new Movie("The Godfather", "Other", "G", 5, 1));
            AddMovie(new Movie("Forrest Gump", "Drama", "PG", 10, 1));
            AddMovie(new Movie("The Matrix", "Sci-Fi", "R", 2.5, 3));
            AddMovie(new Movie("The Incredibles", "Animated", "PG", 2.5, 1));
            AddMovie(new Movie("Titanic", "Other", "PG", 2.5, 5));
            AddMovie(new Movie("Avatar", "Action", "PG", 2.5, 1));
            AddMovie(new Movie("Jurassic Park", "Adventure", "PG", 2.5, 3));
            AddMovie(new Movie("The Avengers", "Action", "G", 2.5, 1));
            AddMovie(new Movie("IFN664", "Other", "G", 2.5, 1));
            AddMovie(new Movie("IFN664", "Other", "G", 2.5, 2));
            AddMovie(new Movie("IFN711", "Other", "M15+", 5, 2));
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

        public bool AddMovie(Movie movie)
        {
            int index = GetHash(movie.Title);
            HashNode current = hashTable[index];
            HashNode previous = null;

            // Check if the movie already exists in the collision chain
            while (current != null)
            {
                if (current.Title == movie.Title)
                {
                    // Movie already exists, update quantity
                    current.Movie.Quantity += movie.Quantity;
                    UpdateMoviesArray(current.Movie, 0); // Update movies array with additional quantity
                    return true; // Updated quantity
                }
                previous = current;
                current = current.Next;
            }

            // Check if the capacity has been exceeded
            if (GetTotalMoviesCount() >= MaxMovies)
            {
                return false;
            }

            // If the movie does not exist in the collision chain, add it
            HashNode newNode = new HashNode(movie.Title, movie);
            if (previous == null)
            {
                // If it's the first node in the collision chain
                hashTable[index] = newNode;
            }
            else
            {
                // Add the new node to the end of the collision chain
                previous.Next = newNode;
            }

            // Update the movies array
            UpdateMoviesArray(movie, movie.Quantity);
            return true; // Added successfully
        }

        private int GetTotalMoviesCount()
        {
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
            return count;
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
                    UpdateMoviesArray(current.Movie, 0); // Update movies array with removed quantity
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
                        return 999; // Indicate movie has been deleted
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

        public Movie SearchMovie(string title)
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
            // Filter out null movies and count non-null movies
            int count = 0;
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i] != null)
                {
                    count++;
                }
            }

            // Create a new array to store non-null movies
            Movie[] allMovies = new Movie[count];
            int index = 0;

            // Copy non-null movies to the new array
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i] != null)
                {
                    allMovies[index++] = movies[i];
                }
            }

            // Sort the array by movie title using bubble sort
            for (int i = 0; i < allMovies.Length - 1; i++)
            {
                for (int j = 0; j < allMovies.Length - i - 1; j++)
                {
                    if (string.Compare(allMovies[j].Title, allMovies[j + 1].Title) > 0)
                    {
                        Movie temp = allMovies[j];
                        allMovies[j] = allMovies[j + 1];
                        allMovies[j + 1] = temp;
                    }
                }
            }
            return allMovies;
        }
        public Movie[] GetTopThreeMovies()
        {
            // Flatten the hash table from the movie collection and get all movies
            Movie[] allMovies = FlattenHashTable();
            // Filter out movies with borrowing frequency <= 0
            allMovies = allMovies.Where(movie => movie.BorrowingFrequency > 0).ToArray();
            // Sort the movies by borrowing frequency in descending order
            QuickSortByBorrowingFrequency(allMovies, 0, allMovies.Length - 1);
            // Get the top three movies with non-zero borrowing frequency
            Movie[] topThreeMovies = allMovies.Take(3).ToArray();

            return topThreeMovies;
        }
        // Flatten the hash table to get all movies
        private Movie[] FlattenHashTable()
        {
            // Count the total number of movies
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

            // Create an array to store all movies
            Movie[] allMovies = new Movie[count];
            int index = 0;

            // Fill the array with movies
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

        private void QuickSortByBorrowingFrequency(Movie[] movies, int left, int right)
        {
            if (left < right)
            {
                // Partition the array
                int pivotIndex = Partition(movies, left, right);

                // Recursively sort the sub-arrays
                QuickSortByBorrowingFrequency(movies, left, pivotIndex - 1);
                QuickSortByBorrowingFrequency(movies, pivotIndex + 1, right);
            }
        }

        private int Partition(Movie[] movies, int left, int right)
        {
            Movie pivot = movies[right];
            int i = left;

            for (int j = left; j < right; j++)
            {
                if (movies[j].BorrowingFrequency > pivot.BorrowingFrequency)
                {
                    // Swap movies[i] and movies[j]
                    Movie temp = movies[i];
                    movies[i] = movies[j];
                    movies[j] = temp;
                    i++;
                }
            }

            // Swap movies[i] and movies[right] (pivot)
            Movie tempPivot = movies[i];
            movies[i] = movies[right];
            movies[right] = tempPivot;

            return i;
        }
    }
}
