using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace MusicSheetManager;

class Options
{
    public static void DisplayAllMusicSheets()
    {
        string option = "0";

        Console.Clear();
        Console.WriteLine("How would you like to display the music sheets");
        Console.WriteLine("1. Display them in alphabetic order");
        Console.WriteLine("2. Display from newest to oldest");
        Console.WriteLine("3. Display from oldest to newest");
        Console.WriteLine("4. Display from largest to smallest in file size");
        Console.WriteLine("5. Display from smallest to largest in file size\n");

        do
        {
            // For some reason, I keep getting a very odd bug where if I run this once, it will print the stuff and 
            // if I run it again, it will print the new stuff but show the previous stuff behind it. This will be 
            // fixed soon once I find out what in the heck is causing this to happen (maybe it's just a prob with my
            // setup and the code itself is fine but idk).

            Console.Write("Please select one of the above options: ");
            option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Displaying in alphabetic order...\n");

                    foreach (MusicSheetMetadata song in MusicSheets.songs.OrderBy(s => s.FileName)) // Prints out the names of the contents of MusicSheets.songs in alphabetical order.
                        Console.WriteLine(song.FileName);

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Displaying from newest to oldest...\n");

                    foreach (MusicSheetMetadata song in MusicSheets.songs.OrderByDescending(s => s.LastModified)) // Prints out the names of the contents of MusicSheets.songs in the order of the most recently modified/created to the oldest most recently modified/created.
                        Console.WriteLine(song.FileName);

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Displaying from oldest to newest...\n");

                    foreach (MusicSheetMetadata song in MusicSheets.songs.OrderBy(s => s.LastModified)) // Prints out the names of the contents of MusicSheets.songs in the order of the oldest modified/created to the most recently modified/created.
                        Console.WriteLine(song.FileName);

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Displaying from largest to smallest in file size...\n");

                    foreach (MusicSheetMetadata song in MusicSheets.songs.OrderBy(s => s.FileSize)) // Prints out the names of the contents of MusicSheets.songs in the order of the largest to the smallest file.
                        Console.WriteLine(song.FileName);

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Displaying from smallest to largest in file size...\n");

                    foreach (MusicSheetMetadata song in MusicSheets.songs.OrderByDescending(s => s.FileSize)) // Prints out the names of the contents of MusicSheets.songs in the order of the largest to the smallest file.
                        Console.WriteLine(song.FileName);

                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter a valid option.");
                    break;
            }
            Console.Clear();
        }
        while (option != "1" && option != "2" && option != "3" && option != "4" && option != "5");
    }

    public static void DisplayFileInfo()
    {
        string songFile = "";

        Console.Clear();
        Console.WriteLine("Please type in the name of the file you wish to see the info of (please make sure to type it in exactly): ");
        songFile = Console.ReadLine();

        foreach (MusicSheetMetadata song in MusicSheets.songs.OrderBy(x => x.FileName))
        {
            if (song.FileName == songFile)
            {
                Console.Clear();
                Console.WriteLine($"Showing metadata for song \"{songFile}\":");
                Console.WriteLine($"File Name: {song.FileName}");
                Console.WriteLine($"File Path: {song.FilePath}");
                Console.WriteLine($"File Type: {song.FileType}");
                Console.WriteLine($"File Size: {DisplayFileSize(song)}");
                Console.WriteLine($"File Created: {song.LastModified}");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }

    private static string DisplayFileSize(MusicSheetMetadata song)     // I made this function so I can keep the full actual process of displaying the file metadata cleaner-looking so it's easier to read.
    {
        // Data units in bytes
        double kilobyteThreshold = 1024.0;
        long megabyteThreshold = 1048576;
        long gigabyteThreshold = 1073741824;

        // This if-else statement displays the file size of the current file. Depending on
        // the file size, it will display in bytes, megabytes, or gigabytes (technically, it's KiB, 
        // MiB, and GiB but these units are read as KB, MB, and GB in most cases anyway).
        
        if (song.FileSize < kilobyteThreshold)                                              // Bytes, just... bytes.
            return $"{song.FileSize:F2} B";

        else if (song.FileSize >= kilobyteThreshold && song.FileSize < megabyteThreshold)   // Bytes to kilobytes
            return $"{song.FileSize / kilobyteThreshold:F2} KB";

        else if (song.FileSize >= megabyteThreshold && song.FileSize < gigabyteThreshold)   // Bytes to megabytes
            return $"{song.FileSize / (kilobyteThreshold * kilobyteThreshold):F2} MB";

        else if (song.FileSize >= gigabyteThreshold)                                        // Bytes to gigabytes (highly unnecessary in this program's case but whatever)
            return $"{song.FileSize / (kilobyteThreshold * kilobyteThreshold * kilobyteThreshold):F2} GB";

        else
            return "Either your file is corrupted or some crap, or you got one MASSIVE file. If it's the latter, then you've brought your data hoard to the wrong place bro.";
    }
}
