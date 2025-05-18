using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Lab9T4
{
    private Hashtable catalog = new Hashtable();

    public void Run()
    {
        LoadFromFiles("discs"); // ������������ � ����� ��� �������

        while (true)
        {
            Console.WriteLine("\n1 - ������ ����");
            Console.WriteLine("2 - �������� ����");
            Console.WriteLine("3 - ������ ���� �� ����");
            Console.WriteLine("4 - �������� ���� � �����");
            Console.WriteLine("5 - ����������� �� �����");
            Console.WriteLine("6 - ����������� ���������� ����");
            Console.WriteLine("7 - ����� ����� �� ����������");
            Console.WriteLine("0 - �����");
            Console.Write("��� ����: ");

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
                    Console.WriteLine("������� ����.");
                    break;
            }
        }
    }

    public void LoadFromFiles(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("����� � ������� �� ��������.");
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

        Console.WriteLine($"������� {catalog.Count} ����� �� �����.");
    }

    private void AddDisc()
    {
        Console.Write("����� ����� �����: ");
        string discName = Console.ReadLine();

        if (!catalog.ContainsKey(discName))
        {
            catalog[discName] = new MusicDisc(discName);
            File.WriteAllText($"discs/{discName}.txt", ""); // ��������� ����
            Console.WriteLine("���� ������.");
        }
        else
        {
            Console.WriteLine("����� ���� ��� ����.");
        }
    }

    private void RemoveDisc()
    {
        Console.Write("����� ����� �����: ");
        string discName = Console.ReadLine();

        if (catalog.ContainsKey(discName))
        {
            catalog.Remove(discName);
            string filePath = $"discs/{discName}.txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath); // ��������� ����
            }
            Console.WriteLine("���� ��������.");
        }
        else
        {
            Console.WriteLine("���� �� ��������.");
        }
    }

    private void AddSongToDisc()
    {
        Console.Write("����� �����: ");
        string discName = Console.ReadLine();

        if (catalog[discName] is MusicDisc disc)
        {
            Console.Write("����� ���: ");
            string title = Console.ReadLine();
            Console.Write("����������: ");
            string artist = Console.ReadLine();

            var song = new Song(title, artist);
            disc.AddSong(song);

            string line = $"{title}|{artist}\n";
            File.AppendAllText($"discs/{discName}.txt", line); // ������ � ����
        }
        else
        {
            Console.WriteLine("���� �� ��������.");
        }
    }

    private void RemoveSongFromDisc()
    {
        Console.Write("����� �����: ");
        string discName = Console.ReadLine();

        if (catalog[discName] is MusicDisc disc)
        {
            Console.Write("����� ��� ��� ���������: ");
            string title = Console.ReadLine();

            disc.RemoveSong(title);

            // ��������� ����
            var lines = new List<string>();
            foreach (var s in disc.Songs)
            {
                lines.Add($"{s.Title}|{s.Artist}");
            }
            File.WriteAllLines($"discs/{discName}.txt", lines);
        }
        else
        {
            Console.WriteLine("���� �� ��������.");
        }
    }

    private void ViewCatalog()
    {
        foreach (DictionaryEntry entry in catalog)
        {
            Console.WriteLine($"\n=== ����: {entry.Key} ===");
            ((MusicDisc)entry.Value).PrintSongs();
        }
    }

    private void ViewDisc()
    {
        Console.Write("����� ����� �����: ");
        string discName = Console.ReadLine();

        if (catalog[discName] is MusicDisc disc)
        {
            disc.PrintSongs();
        }
        else
        {
            Console.WriteLine("���� �� ��������.");
        }
    }

    private void SearchByArtist()
    {
        Console.Write("����� ��� ���������: ");
        string artist = Console.ReadLine();

        bool found = false;
        foreach (DictionaryEntry entry in catalog)
        {
            MusicDisc disc = (MusicDisc)entry.Value;
            foreach (var song in disc.Songs)
            {
                if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{song.Title} ({song.Artist}) �� �����: {disc.Name}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("����� ��� �� ��������.");
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
        return $"{Title} � {Artist}";
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
        Console.WriteLine("ϳ��� ������.");
    }

    public void RemoveSong(string title)
    {
        Song song = Songs.Find(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (song != null)
        {
            Songs.Remove(song);
            Console.WriteLine("ϳ��� ��������.");
        }
        else
        {
            Console.WriteLine("ϳ��� �� ��������.");
        }
    }

    public void PrintSongs()
    {
        if (Songs.Count == 0)
        {
            Console.WriteLine("ϳ���� ����.");
            return;
        }

        foreach (var song in Songs)
        {
            Console.WriteLine(song);
        }
    }
}
