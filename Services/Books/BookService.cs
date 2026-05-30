using LibraryManagement.Models.Books;
using System.Text.Json;

namespace LibraryManagement.Services.Books;

public class BookService : IBookService
{
    private int _nextId = 1;

    private readonly string _filePath = "books.json";

    private List<Book> books { get; set; }

    public BookService()
    {
        books = LoadBooks();
    }

    public void BorrowBook(int id)
    {
        Book book = GetBookById(id);

        if (book is null)
        {
            return;
        }

        if (book.IsAvailable == true)
        {
            book.IsAvailable = false;
            SaveBooks(books);
        }
    }

    public void CreateBook(Book book)
    {
        book.Id = _nextId++;
        book.IsAvailable = true;
        books.Add(book);
        SaveBooks(books);
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

    public Book GetBookById(int id)
    {
        foreach (Book book in books)
            if (book.Id == id) return book;

        return null;
    }

    public void ReturnBook(int id)
    {
        Book book = GetBookById(id);

        if (book is null)
        {
            return;
        }

        if (book.IsAvailable == false)
        {
            book.IsAvailable = true;
            SaveBooks(books);
        }
    }

    public List<Book> SearchByAuthor(string author)
    {
        List<Book> books = GetAllBooks();
        List<Book> result = new List<Book>();

        foreach (Book book in books)
        {
            if (book.Author.ToLower().Contains(author.ToLower())) result.Add(book);
        }

        return result;
    }

    private List<Book> LoadBooks()
    {
        if (!File.Exists(_filePath))
            return new List<Book>();

        var booksJson = File.ReadAllText(_filePath);

        List<Book> books = JsonSerializer.Deserialize<List<Book>>(booksJson);

        return books ?? new List<Book>();
    }

    private void SaveBooks(List<Book> books)
    {
        var options = new JsonSerializerOptions();

        options.WriteIndented = true;

        string jsonString = JsonSerializer.Serialize<List<Book>>(books, options);

        File.WriteAllText(_filePath, jsonString);
    }

    public void UpdateBook(Book newBook)
    {
        Book book = GetBookById(newBook.Id);

        if (book is null) return;

        book.Author = newBook.Author;
        book.Title = newBook.Title;
        book.Year = newBook.Year;

        SaveBooks(books);
    }

    public void DeleteBook(int id)
    {
        Book book = GetBookById(id);

        if (book is null) return;

        books.Remove(book);

        SaveBooks(books);
    }
}