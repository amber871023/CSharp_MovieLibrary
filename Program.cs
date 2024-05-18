﻿using System;
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
        // Pass the single instance to both menus
        Menu menu = new Menu(movieCollection, memberCollection);
        menu.DisplayMainMenu();
    }
}

