using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Lab9T4
{
    private Hashtable catalog = new Hashtable();

    public void Run()
    {
        LoadFromFiles("discs"); // завантаження з файлів при запуску

        while (true)
        {
            Console.WriteLine("\n1 - Додати диск");
            Console.WriteLine("2 - Видалити диск");
            Console.WriteLine("3 - Додати пісню на диск");
            Console.WriteLine("4 - Видалити пісню з диска");
            Console.WriteLine("5 - Переглянути всі диски");
            Console.WriteLine("6 - Переглянути конкретний диск");
            Console.WriteLine("7 - Пошук пісень за виконавцем");
            Console.WriteLine("0 - Вийти");
            Console.Write("Твій вибір: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddDisc();
                    break;
                case "2":
                    RemoveDisc();
                    break;
                case "3":
                    AddSongToDisc();
                    break;
                case "4":
                    RemoveSongFromDisc();
                    break;
                case "5":
                    ViewCatalog();
                    break;
                case "6":
                    ViewDisc();
                    break;
                case "7":
                    SearchByArtist();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }

    public void LoadFromFiles(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Папку з дисками не знайдено.");
            return;
        }

        string[] files = Directory.GetFiles(directoryPath, "*.txt");

        foreach (var file in files)
        {
            string discName = Path.GetFileNameWithoutExtension(file);
            MusicDisc disc = new MusicDisc(discName);

            string[] lines = File.ReadAllLines(file);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    string title = parts[0].Trim();
                    string artist = parts[1].Trim();
                    disc.AddSong(new Song(title, artist));
                }
            }

            catalog[discName] = disc;
        }

        Console.WriteLine($"Зчитано {catalog.Count} дисків із файлів.");
    }

    private void AddDisc()
    {
        Console.Write("Введи назву диска: ");
        string discName = Console.ReadLine();

        if (!catalog.ContainsKey(discName))
        {
            catalog[discName] = new MusicDisc(discName);
            File.WriteAllText($"discs/{discName}.txt", ""); // створюємо файл
            Console.WriteLine("Диск додано.");
        }
        else
        {
            Console.WriteLine("Такий диск вже існує.");
        }
    }

    private void RemoveDisc()
    {
        Console.Write("Введи назву диска: ");
        string discName = Console.ReadLine();

        if (catalog.ContainsKey(discName))
        {
            catalog.Remove(discName);
            string filePath = $"discs/{discName}.txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath); // видаляємо файл
            }
            Console.WriteLine("Диск видалено.");
        }
        else
        {
            Console.WriteLine("Диск не знайдено.");
        }
    }

    private void AddSongToDisc()
    {
        Console.Write("Назва диска: ");
        string discName = Console.ReadLine();

        if (catalog[discName] is MusicDisc disc)
        {
            Console.Write("Назва пісні: ");
            string title = Console.ReadLine();
            Console.Write("Виконавець: ");
            string artist = Console.ReadLine();

            var song = new Song(title, artist);
            disc.AddSong(song);

            string line = $"{title}|{artist}\n";
            File.AppendAllText($"discs/{discName}.txt", line); // додаємо у файл
        }
        else
        {
            Console.WriteLine("Диск не знайдено.");
        }
    }

    private void RemoveSongFromDisc()
    {
        Console.Write("Назва диска: ");
        string discName = Console.ReadLine();

        if (catalog[discName] is MusicDisc disc)
        {
            Console.Write("Назва пісні для видалення: ");
            string title = Console.ReadLine();

            disc.RemoveSong(title);

            // оновлюємо файл
            var lines = new List<string>();
            foreach (var s in disc.Songs)
            {
                lines.Add($"{s.Title}|{s.Artist}");
            }
            File.WriteAllLines($"discs/{discName}.txt", lines);
        }
        else
        {
            Console.WriteLine("Диск не знайдено.");
        }
    }

    private void ViewCatalog()
    {
        foreach (DictionaryEntry entry in catalog)
        {
            Console.WriteLine($"\n=== Диск: {entry.Key} ===");
            ((MusicDisc)entry.Value).PrintSongs();
        }
    }

    private void ViewDisc()
    {
        Console.Write("Введи назву диска: ");
        string discName = Console.ReadLine();

        if (catalog[discName] is MusicDisc disc)
        {
            disc.PrintSongs();
        }
        else
        {
            Console.WriteLine("Диск не знайдено.");
        }
    }

    private void SearchByArtist()
    {
        Console.Write("Введи ім’я виконавця: ");
        string artist = Console.ReadLine();

        bool found = false;
        foreach (DictionaryEntry entry in catalog)
        {
            MusicDisc disc = (MusicDisc)entry.Value;
            foreach (var song in disc.Songs)
            {
                if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{song.Title} ({song.Artist}) на диску: {disc.Name}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("Жодної пісні не знайдено.");
        }
    }
}

public class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }

    public Song(string title, string artist)
    {
        Title = title;
        Artist = artist;
    }

    public override string ToString()
    {
        return $"{Title} — {Artist}";
    }
}

public class MusicDisc
{
    public string Name { get; set; }
    public List<Song> Songs { get; private set; }

    public MusicDisc(string name)
    {
        Name = name;
        Songs = new List<Song>();
    }

    public void AddSong(Song song)
    {
        Songs.Add(song);
        Console.WriteLine("Пісню додано.");
    }

    public void RemoveSong(string title)
    {
        Song song = Songs.Find(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (song != null)
        {
            Songs.Remove(song);
            Console.WriteLine("Пісню видалено.");
        }
        else
        {
            Console.WriteLine("Пісня не знайдена.");
        }
    }

    public void PrintSongs()
    {
        if (Songs.Count == 0)
        {
            Console.WriteLine("Пісень немає.");
            return;
        }

        foreach (var song in Songs)
        {
            Console.WriteLine(song);
        }
    }
}
