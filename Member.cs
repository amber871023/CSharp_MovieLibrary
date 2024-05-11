using System;
using static System.Console;

namespace N11422807
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }


        public Member(string firstName, string lastName, string contactNumber, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            Password = password;

        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Contact Number: {ContactNumber}\n";
        }
        public void BorrowMovie(Movie movie)
        {
            if (movie.IsAvailable)
            {
                // Update movie availability
                movie.IsAvailable = false;

                // Record borrowing details
                BorrowingRecord record = new BorrowingRecord(movie.Title, this);
                // Add the record to the member's borrowing history
                // You can maintain a list of borrowing records in the Member class
                // For simplicity, let's assume there's a list named BorrowingHistory
                BorrowingHistory.Add(record);

                WriteLine($"Successfully borrowed '{movie.Title}'.");
            }
            else
            {
                WriteLine($"Sorry, '{movie.Title}' is not available for borrowing.");
            }
        }
    }

}

