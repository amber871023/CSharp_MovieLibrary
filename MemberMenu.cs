using System;
using Application;
using static System.Console;

namespace N11422807
{
    public class MemberMenu
    {
        public int Option { get; set; }
        private MovieCollection movieCollection;
        private MemberCollection memberCollection;
        private Member member;

        public MemberMenu(MovieCollection movieCollection, MemberCollection memberCollection,Member member)
        {
            this.movieCollection = movieCollection;
            this.memberCollection = memberCollection;
            this.member = member; 
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
                Write("Enter your choice:");
                Option = GetOption(6);  // Maximum option is 6
                // Handle member menu options...
                switch (Option)
                {
                    case 0:
                        isExecute = false;
                        break;
                    case 1:
                        WriteLine("\nBrowsing all the movies...\n");
                        // Header
                        WriteLine("{0,-25} {1,-15} {2,-15} {3,-10} {4,-10} {5,-10}", "Title", "Genre", "Classification", "Duration", "Avail. Qty", "Borrowed times");
                        WriteLine("-----------------------------------------------------------------------------------------");
                        // Get all movies
                        Movie[] allMovies = movieCollection.GetAllMovies();
                        // Display movie info
                        foreach (Movie movie in allMovies)
                        {
                            if (movie != null)
                            {
                                WriteLine("{0,-25} {1,-15} {2,-15} {3,-10} {4,-10} {5,-10}", movie.Title, movie.Genre, movie.Classification, movie.Duration, movie.Quantity, movie.BorrowingFrequency);
                            }
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the member menu");
                        ReadLine();
                        break;
                    case 2:
                        // Display information about a movie
                        Write("\nEnter the title of the movie: ");
                        string titleToFind = ReadLine();
                        Movie foundMovie = movieCollection.GetMovie(titleToFind);
                        if (foundMovie != null)
                        {
                            // Header
                            WriteLine("{0,-25} {1,-15} {2,-15} {3,-10} {4,-10} {5,-10}", "Title", "Genre", "Classification", "Duration", "Avail. Qty", "Borrowed times");
                            WriteLine("-----------------------------------------------------------------------------------------");
                            // Display movie info
                            WriteLine("{0,-25} {1,-15} {2,-15} {3,-10} {4,-10} {5,-10}", foundMovie.Title, foundMovie.Genre, foundMovie.Classification, foundMovie.Duration, foundMovie.Quantity, foundMovie.BorrowingFrequency);
                        }
                        else
                        {
                            WriteLine("Movie not found.");
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the member menu");
                        ReadLine();
                        break;
                    case 3:
                        WriteLine("\nBrowsing all the movies...\n");
                        // Display movie list with numbers
                        WriteLine("Movie List:");
                        allMovies = movieCollection.GetAllMovies();
                        for (int i = 0; i < allMovies.Length; i++)
                        {
                            if (allMovies[i] != null)
                            {
                                WriteLine($"{i + 1}. {allMovies[i].Title}");
                            }
                        }

                        // Prompt for movie selection
                        Write("\nEnter the number of the movie you want to borrow: ");
                        int movieNumber;
                        if (int.TryParse(ReadLine(), out movieNumber) && movieNumber > 0 && movieNumber <= allMovies.Length)
                        {
                            // Get the selected movie from the array
                            Movie selectedMovie = allMovies[movieNumber - 1];
                            member.BorrowMovie(selectedMovie);
                        }
                        else
                        {
                            WriteLine("Invalid input. Please enter a valid movie number.");
                        }

                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the member menu");
                        ReadLine();
                        break;
                    case 4:
                        WriteLine("\nReturning a movie DVD...");
                        Write("Enter the title of the movie you are returning:");
                        string movieTitle = ReadLine();
                        // Find the member in the collection
                        Member foundMember = memberCollection.FindMember(member.FirstName, member.LastName);
                        if (foundMember != null)
                        {
                            bool movieReturned = false;
                            for (int i = 0; i < foundMember.BorrowingCount; i++)
                            {
                                if (foundMember.BorrowingHistory[i] != null && foundMember.BorrowingHistory[i].MovieTitle == movieTitle)
                                {
                                    // Get the returned movie directly from the movie collection
                                    Movie returnedMovie = movieCollection.GetMovie(foundMember.BorrowingHistory[i].MovieTitle);
                                    if (returnedMovie != null)
                                    {
                                        WriteLine("The movie you want to return is: ");
                                        WriteLine(returnedMovie);
                                        // Update movie availability
                                        returnedMovie.Quantity++;
                                        // Remove the movie from the member's borrowing history
                                        foundMember.BorrowingHistory[i] = null;
                                        foundMember.BorrowingCount--;

                                        WriteLine($"Successfully returned '{movieTitle}'. Thank you!");
                                        movieReturned = true;
                                        break;
                                    }
                                    else
                                    {
                                        WriteLine($"Error: Returned movie is null.");
                                    }
                                }
                            }
                            if (!movieReturned)
                            {
                                WriteLine($"You have not borrowed '{movieTitle}'.");
                            }
                        }
                        else
                        {
                            WriteLine("Member not found.");
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the member menu");
                        ReadLine();
                        break;
                    case 5:
                        WriteLine("\nListing current borrowing movies...");
                        member = memberCollection.FindMember(member.FirstName, member.LastName);
                        if (member != null)
                        {
                            WriteLine("Current borrowed movies:");
                            WriteLine("-----------------------------------------------------------------------------------------------");
                            WriteLine("|   No.   |           Title            |       Genre        |  Classification  |   Duration   |");
                            WriteLine("-----------------------------------------------------------------------------------------------");
                            for (int i = 0; i < member.BorrowingCount; i++)
                            {
                                if (member.BorrowingHistory[i] != null)
                                {
                                    Movie borrowedMovie = movieCollection.GetMovie(member.BorrowingHistory[i].MovieTitle);
                                    if (borrowedMovie != null)
                                    {
                                        WriteLine($"|  {i + 1,-5}  |  {borrowedMovie.Title,-24}  |  {borrowedMovie.Genre,-16}  |  {borrowedMovie.Classification,-14}  |  {borrowedMovie.Duration,-10}  |");
                                    }
                                }
                            }
                            WriteLine("-----------------------------------------------------------------------------------------------");
                        }
                        else
                        {
                            WriteLine("Member not found.");
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the member menu");
                        ReadLine();
                        break;
                    case 6:
                        WriteLine("\nTop 3 movies rented by members:");

                        // Table header
                        WriteLine("------------------------------------------------------------------------");
                        WriteLine("|    No.   |               Title               |  Borrowing Frequency  |");
                        WriteLine("------------------------------------------------------------------------");

                        Movie[] topThreeMovies = movieCollection.GetTopThreeMovies();
                        if (topThreeMovies.Length == 0)
                        {
                            WriteLine("|                           No movies rented yet                        |");
                            WriteLine("------------------------------------------------------------------------");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (i < topThreeMovies.Length && topThreeMovies[i] != null)
                                {
                                    WriteLine($"|    {i + 1,-2}    |       {topThreeMovies[i].Title,-20}        |         {topThreeMovies[i].BorrowingFrequency,-5}         |");
                                    WriteLine("------------------------------------------------------------------------");
                                }
                                else
                                {
                                    WriteLine($"|    {i + 1,-2}    |                -                  |            -           |");
                                    WriteLine("------------------------------------------------------------------------");
                                }
                            }
                        }
                        WriteLine("Press Enter to return to the member menu");
                        ReadLine();
                        break;
                    default:
                        WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            return Option;
        }
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
