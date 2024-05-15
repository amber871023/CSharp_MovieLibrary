using System;
using Application;
using static System.Console;
namespace N11422807;
class Program
{
    static void Main(string[] args)
    {
        // Create a single instance of MovieCollection
        MovieCollection movieCollection = new MovieCollection();
        MemberCollection memberCollection = new MemberCollection();
        memberCollection.AddMember(new Member("amber", "cheng", "12345", "1234"));
        memberCollection.AddMember(new Member("et", "cheng", "12345", "1234"));
        memberCollection.AddMember(new Member("yt", "cheng", "12345", "1234"));
        memberCollection.AddMember(new Member("tracy", "cheng", "12345", "1234"));
        memberCollection.AddMember(new Member("injay", "cheng", "12345", "1234"));
        memberCollection.AddMember(new Member("arin", "cheng", "12345", "1234"));
        memberCollection.AddMember(new Member("cody", "cheng", "12345", "1234"));
        memberCollection.AddMember(new Member("Tom", "cheng", "12345", "1234"));
        memberCollection.AddMember(new Member("Dainel", "cheng", "12345", "1234"));

        // Pass the single instance to both menus
        Menu menu = new Menu(movieCollection, memberCollection);
        menu.DisplayMainMenu();
    }
}

