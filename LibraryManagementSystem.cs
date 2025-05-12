using System;
using System.Collections.Generic;
using System.IO;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public override string ToString()
    {
        return $"{Title} by {Author} ({Year})";
    }
}

class Library
{
    private List<Book> books = new List<Book>();
    private string filePath = "library.txt";

    public void AddBook()
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();
        Console.Write("Enter author: ");
        string author = Console.ReadLine();
        Console.Write("Enter publication year: ");
        int year = int.Parse(Console.ReadLine());

        books.Add(new Book(title, author, year));
        Console.WriteLine("Book added successfully.");
    }

    public void ViewBooks()
    {
        Console.WriteLine("\n--- Library Books ---");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public void SearchBook()
    {
        Console.Write("Enter title to search: ");
        string keyword = Console.ReadLine().ToLower();
        var found = books.FindAll(b => b.Title.ToLower().Contains(keyword));
        Console.WriteLine("\n--- Search Results ---");
        if (found.Count == 0)
            Console.WriteLine("No books found.");
        else
            foreach (var book in found)
                Console.WriteLine(book);
    }

    public void SaveToFile()
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var book in books)
            {
                sw.WriteLine($"{book.Title}|{book.Author}|{book.Year}");
            }
        }
        Console.WriteLine("Data saved to file.");
    }

    public void LoadFromFile()
    {
        if (!File.Exists(filePath)) return;

        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var parts = line.Split('|');
                if (parts.Length == 3)
                {
                    books.Add(new Book(parts[0], parts[1], int.Parse(parts[2])));
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();
        library.LoadFromFile();
        while (true)
        {
            Console.WriteLine("\n1. Add Book\n2. View Books\n3. Search Book\n4. Save and Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    library.AddBook();
                    break;
                case "2":
                    library.ViewBooks();
                    break;
                case "3":
                    library.SearchBook();
                    break;
                case "4":
                    library.SaveToFile();
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}