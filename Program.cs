using System;
using static System.Console;
namespace N11422807;
class Program
{
    static void Main(string[] args)
    {
        // Create a single instance of MovieCollection
        MovieCollection movieCollection = new MovieCollection();

        // Add some movies for testing
        movieCollection.AddMovie(new Movie("IFN664", "Drama", "G", 2.5));
        movieCollection.AddMovie(new Movie("IFN664", "Drama", "G", 2.5));
        movieCollection.AddMovie(new Movie("IFN664", "Drama", "MG", 2.5));
        movieCollection.AddMovie(new Movie("IFN666", "Crime", "M15+", 3));
        movieCollection.AddMovie(new Movie("IFN711", "Action", "M15+", 5));
        movieCollection.AddMovie(new Movie("IFN711", "Action", "M15+", 5));
        movieCollection.AddMovie(new Movie("QUT", "Adventure", "MA15+", 10));

        // Pass the single instance to both menus
        Menu menu = new Menu(movieCollection);
        menu.DisplayMainMenu();

        StaffMenu staffMenu = new StaffMenu(movieCollection);
        MemberMenu memberMenu = new MemberMenu(movieCollection);

    }
}

