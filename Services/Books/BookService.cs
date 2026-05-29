using LibraryManagement.Models.Books;

namespace LibraryManagement.Services.Books;

public class BookService : IBookService
{
	private int _nextId = 1;

	private List<Book> books = new List<Book>();

	public void BorrowBook(int id)
	{
		Book book = GetBookById(id);

		if(book is null) 
		{
			Console.WriteLine("Book by this ID not found");
			return;
		}

		if(book.IsAvailable == true) book.IsAvailable = false;
		else Console.WriteLine("Book is already borrowed");
	}

	public void CreateBook(Book book)
	{
		book.Id = _nextId++;
		books.Add(book);
	}

	public Book[] GetAllBooks()
	{
		return books.ToArray();
	}

	public Book GetBookById(int id)
	{
		foreach(Book book in books)
			if(book.Id == id) return book;
		
		return null;
	}

	public void ReturnBook(int id)
	{
		Book book = GetBookById(id);

		if(book is null) 
		{
			Console.WriteLine("Book by this ID not found");
			return;
		}
		
		if(book.IsAvailable == false) book.IsAvailable = true;
		else Console.WriteLine("Book is already returned");
	}
}