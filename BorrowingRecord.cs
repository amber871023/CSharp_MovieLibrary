using System;
namespace N11422807
{
    public class BorrowingRecord
    {
        public string MovieTitle { get; set; }
        public Member Borrower { get; set; }
        public DateTime BorrowDate { get; set; }
        public Movie Movie { get; set; }

        public BorrowingRecord(string movieTitle, Member borrower)
        {
            MovieTitle = movieTitle;
            Borrower = borrower;
            BorrowDate = DateTime.Now;
        }
    }
}

