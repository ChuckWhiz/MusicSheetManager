using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO.Enumeration;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace MusicSheetManager;

class Initialization
{
    public static void Init()
    {
        string fileName = "settings.json";
        string currentDirectory = Directory.GetCurrentDirectory();
        string settingsPath = Path.Combine(currentDirectory, fileName);
        bool success = false;

        Console.Clear();

        if (File.Exists(settingsPath))
        {
            string jsonString = File.ReadAllText(fileName);
            Settings? settings = JsonSerializer.Deserialize<Settings>(jsonString);


            // This do-while loop is made here for safety reasons in case the file folder changes location or gets deleted.
            do
            {
                try
                {
                    ScanDir(settings.Directory);
                    success = true;
                    return;
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.Clear();
                    Console.WriteLine($"ERROR: Could not find the specified directory \"{settings.Directory}\" Please enter a valid directory in settings.json and try again. (Was the folder moved or deleted?)");
                    Console.Write("Enter new directory here: ");

                    success = false;

                    settings.Directory = Console.ReadLine();
                    Serializer(settings.Directory, settingsPath);
                    
                }
            }
            while (success == false);
        }
        else
        {
            FirstLaunch(settingsPath);
        }
    }

    private static void FirstLaunch(string settingsPath)
    {
        string folderPath;  // This holds the path to the music sheet folder.
        bool success = false;

        Console.Clear();
        Console.WriteLine("Welcome to Music Sheet Manager! It seems this is the first time you launch this program. Please fill out the settings below!");

        // MUSIC FOLDER PATH
        do
        {
            Console.Write("Please enter the path you wish to point the program at: ");
            folderPath = Console.ReadLine();

            if (Directory.Exists(folderPath))
            {
                Console.WriteLine("Folder has been detected successfully!");
                success = true;
                Thread.Sleep(1000);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"ERROR: Could not find the specified directory \"{folderPath}\" Please enter a valid directory and try again.");
                success = false;
            }
        }
        while (success == false);

        success = false;

        Serializer(folderPath, settingsPath);
    }

    private static void ScanDir(string songDir)
    {
        string[] allFilePaths = Directory.GetFiles(songDir);

        foreach (string file in allFilePaths)
        {
            MusicSheetMetadata musicSheetMetadata = new MusicSheetMetadata();
            

            musicSheetMetadata.FileName = Path.GetFileNameWithoutExtension(file);

            musicSheetMetadata.FilePath = file;

            musicSheetMetadata.FileType = Path.GetExtension(file);

            FileInfo fileInfo = new FileInfo(file);

            musicSheetMetadata.FileSize = fileInfo.Length;

            musicSheetMetadata.LastModified = Directory.GetLastWriteTime(file);

            MusicSheets.songs.Add(musicSheetMetadata);
        }
    }

    private static void Serializer(string folderPath, string settingsPath)
    {
        Settings settings = new Settings();
        {
            settings.Directory = folderPath;
            //settings.SheetListDirectory = sheetListPath;
        }


        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string jsonString = JsonSerializer.Serialize(settings, options);

        File.WriteAllText(settingsPath, jsonString);
    }
}