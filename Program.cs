using System;

namespace MusicSheetManager;

class Program
{
    static void Main(string[] args)
    {
        string option = "";

        Initialization.Init();

        do
        {
            Console.Clear();
            Console.WriteLine("\tMusic Sheet Manager");
            Console.WriteLine("Press 1 to display all songs");
            Console.WriteLine("Press 2 to look at the file info of any music sheet");
            Console.WriteLine("Press 3 to exit");

            do
            {
                Console.Write("Enter an option: ");
                option = Console.ReadLine();

                if (option == "1")
                {
                    Options.DisplayAllMusicSheets();
                }
                else if (option == "2")
                {
                    Options.DisplayFileInfo();
                }
                else if (option == "3")
                {
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter a valid option.");
                }
            }
            while (option != "1" && option != "2" && option != "3");
        }
        while (option != "3");
    }
}
