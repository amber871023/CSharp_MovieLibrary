using System;
using Application;
using static System.Console;

namespace N11422807
{
    public class StaffMenu
    {
        public int Option { get; set; }
        private MovieCollection movieCollection;
        private MemberCollection memberCollection;
        public StaffMenu(MovieCollection movieCollection)
        {
            this.movieCollection = movieCollection;
            memberCollection = new MemberCollection();
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

        public int DisplayStaffMenu()
        {
            bool isExecute = true;
            while (isExecute)
            {
                Clear();
                WriteLine("----------------------------------------");
                WriteLine("Staff Menu");
                WriteLine("----------------------------------------");
                WriteLine("Select from the following:");
                WriteLine("1. Add Movies(DVDs) to system");
                WriteLine("2. Remove Movies(DVDs) from system");
                WriteLine("3. Register a new member to system");
                WriteLine("4. Remove a registered member from system");
                WriteLine("5. Find a member contact phone number, given the member's name");
                WriteLine("6. Find members who are currently renting a particular movie");
                WriteLine("0. Return to main menu");
                Write("Enter your choice: ");
                Option = GetOption(6);  // Maximum option is 6
                // Handle staff menu options...
                switch (Option)
                {
                    case 0:
                        isExecute = false;
                        break;
                    case 1:
                        WriteLine("Adding Movies(DVDs)");
                        Write("Enter movie title: ");
                        string title = ReadLine();
                        string genre = GetValidGenre();  // Get valid genre
                        string classification = GetValidClassification();  // Get valid classification
                        Write("Enter movie duration: ");
                        double duration;

                        while (!double.TryParse(ReadLine(), out duration) || duration <= 0)
                        {
                            WriteLine("Invalid duration. Please enter a positive number.");
                            Write("Enter movie duration: ");
                        }
                        Movie newMovie = new Movie(title, genre, classification, duration);
                        bool added = movieCollection.AddMovie(newMovie, 1);

                        if (added)
                        {
                            WriteLine("Movie added successfully.");
                        }
                        else
                        {
                            WriteLine("Failed to add movie. The movie may already exist or the collection may be full.");
                        }
                        // wait for user input before continuing
                        ReadLine();
                        break;

                    case 2:
                        WriteLine("Enter the title of the movie to remove: ");
                        string titleToRemove = ReadLine();
                        WriteLine("Enter the number of copies to remove: ");
                        int numCopiesToRemove;
                        if (!int.TryParse(ReadLine(), out numCopiesToRemove))
                        {
                            WriteLine("Invalid input. Please enter a number.");
                            break;
                        }
                        int result = movieCollection.RemoveMovie(titleToRemove, numCopiesToRemove);
                        bool isValid = true;
                        while (isValid) {
                            if (result == 0)
                            {
                                WriteLine("Movie not found.");
                                isValid = false;
                            }
                            else if (result < numCopiesToRemove)
                            {
                                WriteLine($"Not enough copies to remove. There are only {result} copies. Please enter a smaller number.");
                                WriteLine("Enter the number of copies to remove: ");
                                if (!int.TryParse(ReadLine(), out numCopiesToRemove))
                                {
                                    WriteLine("Invalid input. Please enter a number.");
                                    break;
                                }
                            }
                            else
                            {
                                WriteLine($"Removed {numCopiesToRemove} copies. There are now {result} copies left.");
                                isValid = false;
                            }
                        }
                        ReadLine();
                        break;
                    case 3: // Register a new member
                        Write("Enter your first name: ");
                        string firstName = ReadLine();
                        if (string.IsNullOrEmpty(firstName))
                        {
                            WriteLine("Invalid first name. Please try again.");
                            continue;
                        }
                        Write("Enter member last name: ");
                        string lastName = ReadLine();
                        if (string.IsNullOrEmpty(lastName))
                        {
                            WriteLine("Invalid last name. Please try again.");
                            continue;
                        }
                        Write("Enter your phone number: ");
                        string phoneNo = ReadLine();
                        if (string.IsNullOrEmpty(phoneNo))
                        {
                            WriteLine("Invalid phone number. Please try again.");
                            continue;
                        }
                        Write("Enter a four-digit password: ");
                        string password = ReadPassword();
                        if (string.IsNullOrEmpty(password))
                        {
                            WriteLine("Invalid password. Please try again.");
                            continue;
                        }
                        Member newMember = new Member(firstName, lastName, phoneNo, password);
                        added = memberCollection.AddMember(newMember);
                        if (added)
                        {
                            WriteLine("Member registered successfully.");
                        }
                        else
                        {
                            WriteLine("Failed to register member. The member may already exist or the collection may be full.");
                        }
                        ReadLine();
                        break;
                    case 4:
                        // Implement logic for removing a registered member from the system
                        WriteLine("Removing a registered member from the system...");
                        ReadLine();
                        break;
                    case 5:
                        // Implement logic for finding a member's contact phone number
                        WriteLine("Enter the contact number of the member to find: ");
                        string contactNumberToFind = ReadLine();
                        Member foundMember = memberCollection.FindMember(contactNumberToFind);
                        if (foundMember != null)
                        {
                            WriteLine($"Found member:\n {foundMember.FirstName} {foundMember.LastName}, Contact Number: {foundMember.ContactNumber}");
                        }
                        else
                        {
                            WriteLine("Member not found. Press Enter to return to the staff menu");
                        }
                        ReadLine();
                        break;
                    case 6:
                        // Implement logic for finding members who are currently renting a particular movie
                        WriteLine("Finding members who are currently renting a particular movie...");
                        ReadLine();
                        break;
                    default:
                        WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            return Option;
        }

        public string ReadPassword()
        {
            string password = "";
            while (true)
            {
                var key = ReadKey(true);
                // Backspace Should Not Work
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Write("*");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    WriteLine();
                    return password;
                }
            }
        }
        // Define valid genre options
        private string[] validGenres= { "drama", "adventure", "family", "action", "sci-fi", "comedy", "animated", "thriller", "Other" };
        private string GetValidGenre()
        {
            WriteLine($"Enter movie genre{{{string.Join(", ", validGenres)}}}: ");
            string genre = ReadLine().ToLower();
            while (!validGenres.Contains(genre))
            {
                WriteLine("Invalid genre. Please enter a valid genre.");
                Write("Enter movie genre: ");
                genre = ReadLine().ToLower();
            }
            return genre;
        }
        // Define valid classification options
        private string[] validClassifications = { "G", "PG", "M15+", "MA15+" };
        private string GetValidClassification()
        {
            WriteLine("Enter movie classification G (genreGeneral), PG (Parental Guidance), M15+ (Mature), or MA15+(Mature Accompanied): ");

            string classification = ReadLine().ToUpper();
            while (!validClassifications.Contains(classification))
            {
                WriteLine("Invalid classification. Please enter a valid classification.");
                Write("Enter movie classification: ");
                classification = ReadLine().ToUpper();
            }
            return classification;
        }
    }
}
