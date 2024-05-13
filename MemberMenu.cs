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
                Write("Enter your choice: ");
                Option = GetOption(6);  // Maximum option is 6
                // Handle member menu options...
                switch (Option)
                {
                    case 0:
                        isExecute = false;
                        break;
                    case 1:
                        WriteLine("Browsing all the movies...\n");
                        // Header
                        WriteLine("{0,-20} {1,-15} {2,-15} {3,-10} {4,-10}", "Title", "Genre", "Classification", "Duration", "Available Quantity");
                        WriteLine("----------------------------------------------------------------------------------");
                        // Get all movies
                        Movie[] allMovies = movieCollection.GetAllMovies();
                        // Display movie info
                        foreach (Movie movie in allMovies)
                        {
                            if (movie != null)
                            {
                                WriteLine("{0,-20} {1,-15} {2,-15} {3,-10} {4,-10}", movie.Title, movie.Genre, movie.Classification, movie.Duration, movie.Quantity);
                            }
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the member menu");
                        ReadLine();
                        break;
                    case 2:
                        // Display information about a movie
                        Write("Enter the title of the movie: ");
                        string titleToFind = ReadLine();
                        Movie foundMovie = movieCollection.GetMovie(titleToFind);
                        if (foundMovie != null)
                        {
                            // Header
                            WriteLine("{0,-20} {1,-15} {2,-15} {3,-10} {4,-10}", "Title", "Genre", "Classification", "Duration", "Available Quantity");
                            WriteLine("----------------------------------------------------------------------------------");

                            // Display movie info
                            WriteLine("{0,-20} {1,-15} {2,-15} {3,-10} {4,-10}", foundMovie.Title, foundMovie.Genre, foundMovie.Classification, foundMovie.Duration, foundMovie.Quantity);
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
                        WriteLine("Borrowing a movie DVD...");
                        Write("Enter the title of the movie you want to borrow: ");
                        string title = ReadLine();
                        // Get the movie from the collection
                        Movie movieToBorrow = movieCollection.GetMovie(title);
                        if (movieToBorrow != null)
                        {
                            // Attempt to borrow the movie
                            member.BorrowMovie(movieToBorrow);
                        }
                        else
                        {
                            WriteLine($"Sorry, '{title}' is not found in the library.");
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the member menu");
                        ReadLine();
                        break;
                    case 4:
                        WriteLine("Returning a movie DVD...");
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
                        WriteLine("Listing current borrowing movies...");
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
                    case 6: //Todo
                        // Display the top 3 movies rented by the members
                        WriteLine("Displaying the top 3 movies rented by the members...");
                        Movie[] topThreeMovies = movieCollection.GetTopThreeMovies();

                        WriteLine("Top 3 movies rented by members:");
                        for (int i = 0; i < topThreeMovies.Length; i++)
                        {
                            if (topThreeMovies[i] != null)
                            {
                                WriteLine($"{i + 1}. {topThreeMovies[i].Title}");
                            }
                        }
                        WriteLine("\n----------------------------------------");
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
