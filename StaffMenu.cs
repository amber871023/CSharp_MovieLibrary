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

        public StaffMenu(MovieCollection movieCollection, MemberCollection memberCollection)
        {
            this.movieCollection = movieCollection;
            this.memberCollection = memberCollection;
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
                        Clear();
                        WriteLine("Adding Movies(DVDs)");
                        Write("Enter movie title (enter 'q' to quit): ");
                        string title = ReadLine();
                        if (title.ToLower() == "q") break; // Exit if 'q' is entered

                        string genre = GetValidGenre();  // Get valid genre
                        if (genre.ToLower() == "q") break; // Exit if 'q' is entered

                        string classification = GetValidClassification();  // Get valid classification
                        if (classification.ToLower() == "q") break; // Exit if 'q' is entered

                        Write("Enter movie duration (enter 'q' to quit): ");
                        double duration;
                        string durationInput = ReadLine();
                        if (durationInput.ToLower() == "q") break; // Exit if 'q' is entered

                        while (!double.TryParse(durationInput, out duration) || duration <= 0)
                        {
                            WriteLine("Invalid duration. Please enter a positive number (enter 'q' to quit).");
                            Write("Enter movie duration: ");
                            durationInput = ReadLine();
                            if (durationInput.ToLower() == "q") break; // Exit if 'q' is entered
                        }
                        // Check if the user entered 'q' during duration input
                        if (durationInput.ToLower() != "q")
                        {
                            Movie newMovie = new Movie(title, genre, classification, duration);
                            bool isAdded = movieCollection.AddMovie(newMovie, 1);
                            if (isAdded)
                            {
                                WriteLine("Movie added successfully.");
                            }
                            else
                            {
                                WriteLine("Failed to add movie. The movie may already exist or the collection may be full.");
                            }
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the staff menu");
                        ReadLine();
                        break;
                    case 2:
                        WriteLine("\nEnter the title of the movie to remove: ");
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
                            else if (result == 999)
                            {
                                WriteLine("The movie has been deleted.");
                                isValid = false;
                            }
                            else
                            {
                                WriteLine($"Removed {numCopiesToRemove} copies. There are now {result} copies left.");
                                isValid = false;
                            }
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the staff menu");
                        ReadLine();
                        break;
                    case 3:
                        Clear();
                        WriteLine("- Register a new member -");
                        Write("Enter your first name: ");
                        string firstName = ReadLine().Trim();
                        if (string.IsNullOrEmpty(firstName))
                        {
                            WriteLine("Invalid first name. Please try again.");
                            continue;
                        }
                        Write("Enter member last name: ");
                        string lastName = ReadLine().Trim();
                        if (string.IsNullOrEmpty(lastName))
                        {
                            WriteLine("Invalid last name. Please try again.");
                            continue;
                        }
                        Write("Enter your phone number: ");
                        string phoneNo = ReadLine().Trim();
                        if (string.IsNullOrEmpty(phoneNo))
                        {
                            WriteLine("Invalid phone number. Please try again.");
                            continue;
                        }
                        string password;
                        do
                        {
                            Write("Enter a four-digit password: ");
                            password = ReadPassword();
                            if (string.IsNullOrEmpty(password) || !IsFourDigitPassword(password))
                            {
                                WriteLine("Invalid password. Please enter a four-digit number.");
                            }
                        } while (string.IsNullOrEmpty(password) || !IsFourDigitPassword(password));

                        Member newMember = new Member(firstName, lastName, phoneNo, password);
                        bool added = memberCollection.AddMember(newMember);
                        if (added)
                        {
                            WriteLine("Member registered successfully.");
                        }
                        else
                        {
                            WriteLine("Failed to register member. The member may already exist or the collection may be full.");
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the staff menu");
                        ReadLine();
                        break;
                    case 4:
                        WriteLine("\n - Remove a member -");
                        Write("Enter first name:");
                        firstName = ReadLine().Trim();
                        Write("Enter last name:");
                        lastName = ReadLine().Trim();
                        Member memberToRemove = memberCollection.FindMember(firstName, lastName);
                        if (memberToRemove != null)
                        {
                            bool hasBorrowed = false;
                            foreach (BorrowingRecord record in memberToRemove.BorrowingHistory)
                            {
                                if (record != null)
                                {
                                    hasBorrowed = true;
                                    break;
                                }
                            }
                            if (hasBorrowed)
                            {
                                WriteLine("This member is currently holding some DVDs.\n They must return all DVDs before they can be removed from the system. \nPress Enter to return to the staff menu");
                            }
                            else
                            {
                                memberCollection.RemoveMember(memberToRemove);
                                WriteLine($"Member {memberToRemove}has been successfully removed. \nPress Enter to return to the staff menu");
                            }
                        }
                        else
                        {
                            WriteLine("Member not found.");
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the staff menu");
                        ReadLine();
                        break;
                    case 5:
                        WriteLine("\nFind a member's contact phone number...");
                        Write("Enter first name:");
                        firstName = ReadLine();
                        Write("Enter last name:");
                        lastName = ReadLine();
                        Member foundMemberPhone = memberCollection.FindMember(firstName, lastName);
                        if (foundMemberPhone != null)
                        {
                            WriteLine($"Found member:\n {firstName} {lastName}, Contact Number: {foundMemberPhone.ContactNumber}");
                        }
                        else
                        {
                            WriteLine("Member not found.");
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the staff menu");
                        ReadLine();
                        break;
                    case 6:
                        WriteLine("Enter the title of the movie:");
                        string movieTitle = ReadLine();
                        WriteLine($"Finding members who are currently renting '{movieTitle}'...");

                        bool found = false;
                        for (int i = 0; i < memberCollection.Count; i++)
                        {
                            Member member = memberCollection.GetMemberAtIndex(i);
                            foreach (BorrowingRecord record in member.BorrowingHistory)
                            {
                                if (record != null && record.MovieTitle == movieTitle)
                                {
                                    WriteLine($"{i +1}. {member.FirstName} {member.LastName}");
                                    found = true;
                                    break;
                                }
                            }
                        }
                        if (!found)
                        {
                            WriteLine("No members found who are currently renting this movie.");
                        }
                        WriteLine("\n----------------------------------------");
                        WriteLine("Press Enter to return to the staff menu");
                        ReadLine();
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
        private bool IsFourDigitPassword(string password)
        {
            return password.Length == 4 && int.TryParse(password, out _);
        }

        private string[] validGenres = { "drama", "adventure", "family", "action", "sci-fi", "comedy", "animated", "thriller", "Other" };

        private string GetValidGenre()
        {
            WriteLine("Valid movie genres:");
            for (int i = 0; i < validGenres.Length; i++)
            {
                WriteLine($"{i + 1}. {validGenres[i]}");
            }
            WriteLine("Enter the number corresponding to the movie genre from the list above or (enter 'q' to quit): ");

            string input = ReadLine()?.Trim(); // Trim to remove any leading or trailing whitespace

            while (true)
            {
                if (input == "q") break; // Exit loop if 'q' is entered
                if (int.TryParse(input, out int selection) && selection >= 1 && selection <= validGenres.Length)
                {
                    // Check if the input is a valid number within the range
                    return validGenres[selection - 1]; // Return the selected genre
                }

                WriteLine("Invalid input. Please enter a number corresponding to a genre from the list above or 'q' to quit.");
                Write("Enter the number corresponding to the movie genre: ");
                input = ReadLine()?.Trim(); // Read the input again
            }
            return input; 
        }
        private string[] validClassifications = { "G", "PG", "M15+", "MA15+" };
        private string GetValidClassification()
        {
            WriteLine($"Enter movie classification{{{string.Join(", ", validClassifications)}}} or (enter 'q' to quit): ");
            string classification;
            classification = ReadLine()?.ToUpper(); // Added null-conditional operator to handle null input
            while (true)
            {
                if (classification == "Q") return "q"; // Exit loop if 'q' is entered
                if (validClassifications.Contains(classification)) break; // Exit loop if a valid classification is entered
                WriteLine("Invalid classification. Please enter a valid classification or 'q' to quit.");
                Write("Enter movie classification: ");
            }
            return classification;
        }
    }
}
