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
        public BorrowingRecord[] BorrowingHistory { get; set; }
        public int BorrowingCount { get; set; }

        public Member(string firstName, string lastName, string contactNumber, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            Password = password;
            BorrowingHistory = new BorrowingRecord[5]; // Assuming a maximum of 5 borrowings
                                                       // Initialize each element of BorrowingHistory to null
            for (int i = 0; i < BorrowingHistory.Length; i++)
            {
                BorrowingHistory[i] = null;
            }

            BorrowingCount = 0;
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Contact Number: {ContactNumber}, BorrowingHistory: {BorrowingHistory}\n";
        }
        public void BorrowMovie(Movie movie)
        {
            if (BorrowingCount >= 5)
            {
                WriteLine("You have already borrowed the maximum number of movies.");
                return;
            }

            // Check if the member has already borrowed a copy of the same movie
            foreach (BorrowingRecord record in BorrowingHistory)
            {
                if (record != null && record.MovieTitle == movie.Title)
                {
                    WriteLine($"You have already borrowed '{movie.Title}'.");
                    return;
                }
            }

            if (movie.Quantity > 0)
            {
                // Update movie availability
                movie.Quantity--;
                // Record borrowing details
                BorrowingRecord record = new BorrowingRecord(movie.Title, this);
                // Add the record to the member's borrowing history
                BorrowingHistory[BorrowingCount++] = record;
                WriteLine($"Successfully borrowed '{movie.Title}'.");
            }
            else
            {
                WriteLine($"Sorry, '{movie.Title}' is not available for borrowing.");
            }
        }
    }

}

