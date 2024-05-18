using System;
using Application;
using N11422807;
using static System.Console;

public class Menu
{
    public int Option { get; set; }
    private MovieCollection movieCollection;
    private MemberCollection memberCollection;
    private Member GetMember;

    public Menu(MovieCollection movieCollection, MemberCollection memberCollection)
    {
        this.movieCollection = movieCollection;
        this.memberCollection = memberCollection;
    }


    public int GetOption(int maxOption)
    {
        int input;
        while (!int.TryParse(ReadLine(), out input) || input < 0 || input > maxOption)
        {
            WriteLine($"Invalid input. Please enter a number between 0 and {maxOption}.");
            Write("Enter your choice: ");
        }
        return input;
    }

    public bool Login(int option)
    {
        if (option == 1) // Staff login
        {
            string storedUsername = "staff";
            string storedPassword = "today123";
            Write("Enter username:");
            string username = ReadLine().Trim();
            Write("Enter password:");
            string password = ReadPassword();
            if (username == storedUsername && password == storedPassword)
            {
                return true; 
            }
            else
            {
                WriteLine("Error: User does not exist or Incorrect password.");
                ReadLine();
                return false; 
            }
        }
        else if (option == 2) // Member login
        {
            Write("Enter first name:");
            string firstName = ReadLine().Trim();
            Write("Enter last name:");
            string? lastName = ReadLine();
            Write("Enter password:");
            string password = ReadPassword();
            // Check if the member exists and the password is correct
            if (memberCollection.MemberExists(firstName,lastName) && password == memberCollection.GetMemberPassword(firstName,lastName))
            {
                Member loggedInMember = memberCollection.FindMember(firstName, lastName);
                GetMember = loggedInMember;
                return true; // Member exists and password is correct, login successful
            }
            else
            {
                WriteLine("Error: User does not exist or Incorrect password. If you haven't registered yet, please ask the staff to help register.");
                ReadLine();
                return false; // Member does not exist or password is incorrect, login failed
            }
        }

        return false;
    }

    public string ReadPassword()
    {
        string password = "";
        while (true)
        {
            ConsoleKeyInfo key = ReadKey(true);
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

    public void DisplayMainMenu()
    {
        bool isExecute = true;
        while (isExecute)
        {
            Clear();
            WriteLine("----------------------------------------");
            WriteLine("Main Menu");
            WriteLine("----------------------------------------");
            WriteLine("Select from the following:");
            WriteLine("1. Staff Login");
            WriteLine("2. Member Login");
            WriteLine("0. Exit");
            Write("Enter your choice: ");
            Option = GetOption(2);  // Maximum option is 2

            switch (Option)
            {
                case 0:
                    isExecute = false;
                    break;
                case 1:
                    // Staff login
                    if (Login(1))
                    {
                        // Display staff menu
                        StaffMenu staffMenu = new StaffMenu(movieCollection, memberCollection);
                        staffMenu.DisplayStaffMenu();
                    }
                    else
                    {
                        WriteLine("Invalid login. Please try again.");
                    }
                    break;
                case 2:
                    // Member login
                    if (Login(2))
                    {
                        MemberMenu memberMenu = new MemberMenu(movieCollection, memberCollection, GetMember);
                        // Display member menu
                        memberMenu.DisplayMemberMenu();
                    }
                    else
                    {
                        WriteLine("Invalid login. Please try again.");
                    }
                    break;
                default:
                    WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
