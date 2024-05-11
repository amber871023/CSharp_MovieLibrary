using System;
using Application;
using static System.Console;

namespace N11422807
{
    public class MemberMenu
    {
        public int Option { get; set; }
        private MovieCollection movieCollection;
        public MemberMenu(MovieCollection movieCollection)
        {
            this.movieCollection = movieCollection;
        }

        public int DisplayMemberMenu()
        {
            bool isExecute = true;
            while (isExecute)
            {
                Clear();
                WriteLine("----------------------------------------");
                WriteLine("Member Menu");
                WriteLine("----------------------------------------");
                WriteLine("Select from the following:");
                WriteLine("1. Browse all the movies");
                WriteLine("2. Display all the information about a movie, given the title of the movie");
                WriteLine("3. Borrow a movie DVD");
                WriteLine("4. Return a movie DVD");
                WriteLine("5. List current borrowing movies");
                WriteLine("6. Display the top 3 movies rented by the members");
                WriteLine("0. Return to main menu");
                Write("Enter your choice: ");
                Option = GetOption(6);  // Maximum option is 6
                // Handle member menu options...
                switch (Option)
                {
                    case 0:
                        isExecute = false;
                        break;
                    case 1:
                        WriteLine("Browsing all the movies...");
                        Movie[] allMovies = movieCollection.GetAllMovies();
                        foreach (Movie movie in allMovies)
                        {
                            if (movie != null)
                            {
                                WriteLine($"Title: {movie.Title}, Genre: {movie.Genre}, Duration: {movie.Duration}, Quantity: {movie.Quantity}");
                            }
                        }
                        ReadLine();
                        break;
                    case 2:
                        WriteLine("Enter the title of the movie: ");
                        string titleToFind = ReadLine();
                        Movie foundMovie = movieCollection.GetMovie(titleToFind);
                        if (foundMovie != null)
                        {
                            WriteLine($"Found movie:\n Title: {foundMovie.Title}, Genre: {foundMovie.Genre}, Classification: {foundMovie.Classification}, Duration: {foundMovie.Duration}");
                        }
                        else
                        {
                            WriteLine("Movie not found. Press Enter to return to the staff menu");
                        }
                        ReadLine();
                        break;
                    case 3:
                        // Implement logic for borrowing a movie DVD
                        WriteLine("Borrowing a movie DVD...");
                        ReadLine();
                        break;
                    case 4:
                        // Implement logic for returning a movie DVD
                        WriteLine("Returning a movie DVD...");
                        ReadLine();
                        break;
                    case 5:
                        // Implement logic for listing current borrowing movies
                        WriteLine("Listing current borrowing movies...");
                        ReadLine();
                        break;
                    case 6:
                        // Implement logic for displaying the top 3 movies rented by the members
                        WriteLine("Displaying the top 3 movies rented by the members...");
                        ReadLine();
                        break;
                    default:
                        WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            return Option;        }

        private int GetOption(int maxOption)
        {
            int input;
            while (!int.TryParse(ReadLine(), out input) || input < 0 || input > maxOption)
            {
                WriteLine($"Invalid input. Please enter a number between 0 and {maxOption}.");
                Write("Enter your choice: ");
            }
            return input;
        }
    }
}
