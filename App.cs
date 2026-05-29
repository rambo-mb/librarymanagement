using LibraryManagement.Models.Books;
using LibraryManagement.Services.Books;

namespace LibraryManagement;

public class App
{
	private readonly IBookService _service;

	public App(IBookService service)
	{
		_service = service;
	}

	public void Run()
	{
		while(true)
		{
			Console.Clear();
			Console.WriteLine("Library Management\n");
			Console.WriteLine("1. Show all books");
			Console.WriteLine("2. Create book");
			Console.WriteLine("3. Borrow book");
			Console.WriteLine("4. Return book");
			Console.WriteLine("5. Search by author");
			Console.WriteLine("0. Exit");
			Console.Write("\nChoose an option: ");

			string userOption = Console.ReadLine();

			switch(userOption)
			{
				case "1":
					HandleShowAllBooks();
					break;
				case "2":
					HandleCreateBook();
					break;
				case "3":
					HandleBorrowBook();
					break;
				case "4":
					HandleReturnBook();
					break;
				case "5":
					HandleSearchByAuthor();
					break;
				case "0":
					return;
				default:
					Console.WriteLine("Invalid option, try again later");
					break;
			}
		}		
	}

	private void HandleSearchByAuthor()
	{
		string author = ValidateString("author");

		Book[] books = _service.SearchByAuthor(author);

		if(books.Length == 0)
		{
			Console.WriteLine("No books found by this author");
			return;
		}

		Console.WriteLine("Search results: ");
		foreach(Book book in books)
		{
			string status = book.IsAvailable ? "Available" : "Borrowed";
			Console.WriteLine(
				$"""
				Book {book.Id} info
				--------------------
				Title: {book.Title}
				Author: {book.Author}
				Year: {book.Year}
				Status: {status}
				
				"""
			);
		}
	}

	private void HandleReturnBook()
	{
		Book[] books = _service.GetAllBooks();
		int id = ValidateInt("book ID");

		foreach(Book book in books)
		{
			if(book.Id == id)
			{
				if(book.IsAvailable == false)
				{
					_service.ReturnBook(id);
					Console.WriteLine("Book returned successfully");
					return;
				}
				else
				{
					Console.WriteLine("Book is already returned");
					return;
				}
			}
		}

		Console.WriteLine("Book with this ID not found");
	}

	private void HandleBorrowBook()
	{
		Book[] books = _service.GetAllBooks();
		int id = ValidateInt("book ID");

		foreach(Book book in books)
		{
			if(book.Id == id)
			{
				if(book.IsAvailable == true)
				{
					_service.BorrowBook(id);
					Console.WriteLine("Book borrowed successfully");
					return;
				}
				else
				{
					Console.WriteLine("Book is already borrowed");
					return;
				}
			}
		}

		Console.WriteLine("Book with this ID not found");
	}

	private void HandleCreateBook()
	{

		Console.WriteLine("Create new book");
		string title = ValidateString("book title");
		string author = ValidateString("book author");
		int year = ValidateInt("book year");

		Book book = new Book()
		{
			Title = title,
			Author = author,
			Year = year
		};

		_service.CreateBook(book);
	}

	private int ValidateInt(string name)
	{
		int result;
		bool isValid = true;

		do
		{
			Console.Write($"Enter {name}:");
			string userInput = Console.ReadLine().Trim();

			isValid = int.TryParse(userInput, out result) && result > 0;

			if(!isValid)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Field {name} is not valid");
				Console.ResetColor();
			}

		} while(!isValid);

		return result;
	}

	private void HandleShowAllBooks()
	{
		Book[] books = _service.GetAllBooks();

		if(books.Length == 0) 
		{
			Console.WriteLine("No books found");
			return;
		}

		foreach(Book book in books)
		{
			string status = book.IsAvailable ? "Available" : "Borrowed";
			Console.WriteLine(
				$"""
				Book {book.Id} info
				--------------------
				Title: {book.Title}
				Author: {book.Author}
				Year: {book.Year}
				Status: {status}
				
				"""
			);
		}
	}

	private string ValidateString(string name)
	{
		string result = string.Empty;
		bool isValid = true;

		do
		{
			Console.Write($"Enter {name}:");
			string userInput = Console.ReadLine().Trim();

			if(string.IsNullOrWhiteSpace(userInput))
			{
				isValid = false;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Field {name} is not valid");
				Console.ResetColor();
			}
			else
			{
				result = userInput;
				isValid = true;
			}
			
		} while(!isValid);
		
		return result;
	}
}