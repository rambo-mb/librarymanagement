using LibraryManagement.Models.Books;
using LibraryManagement.Services.Books;
using LibraryManagement.Helpers;

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
					ConsoleHelpers.PrintError("Invalid option, try again later");
					ConsoleHelpers.PrintContinue();
					break;
			}
		}		
	}

	private void HandleSearchByAuthor()
	{
		string author = ConsoleHelpers.ValidateString("author");

		Book[] books = _service.SearchByAuthor(author);

		if(books.Length == 0)
		{
			ConsoleHelpers.PrintWarning("No books found by this author");
			ConsoleHelpers.PrintContinue();
			return;
		}

		Console.WriteLine("Search results: ");
		foreach(Book book in books)
		{
			ConsoleHelpers.PrintBook(book);
		}

		ConsoleHelpers.PrintContinue();
	}

	private void HandleReturnBook()
	{
		Book[] books = _service.GetAllBooks();
		int id = ConsoleHelpers.ValidateInt("book ID");

		foreach(Book book in books)
		{
			if(book.Id == id)
			{
				if(book.IsAvailable == false)
				{
					_service.ReturnBook(id);
					ConsoleHelpers.PrintSuccess("Book returned successfully");
					ConsoleHelpers.PrintContinue();
					return;
				}
				else
				{
					ConsoleHelpers.PrintWarning("Book is already returned");
					ConsoleHelpers.PrintContinue();
					return;
				}
			}
		}

		ConsoleHelpers.PrintWarning("Book with this ID not found");

		ConsoleHelpers.PrintContinue();
	}

	private void HandleBorrowBook()
	{
		Book[] books = _service.GetAllBooks();
		int id = ConsoleHelpers.ValidateInt("book ID");

		foreach(Book book in books)
		{
			if(book.Id == id)
			{
				if(book.IsAvailable == true)
				{
					_service.BorrowBook(id);
					ConsoleHelpers.PrintSuccess("Book borrowed successfully");
					ConsoleHelpers.PrintContinue();
					return;
				}
				else
				{
					ConsoleHelpers.PrintWarning("Book is already borrowed");
					ConsoleHelpers.PrintContinue();
					return;
				}
			}
		}

		ConsoleHelpers.PrintWarning("Book with this ID not found");

		ConsoleHelpers.PrintContinue();
	}

	private void HandleCreateBook()
	{

		Console.WriteLine("Create new book");
		string title = ConsoleHelpers.ValidateString("book title");
		string author = ConsoleHelpers.ValidateString("book author");
		int year = ConsoleHelpers.ValidateInt("book year");

		Book book = new Book()
		{
			Title = title,
			Author = author,
			Year = year
		};

		_service.CreateBook(book);
		ConsoleHelpers.PrintSuccess("Book created successfully");
		ConsoleHelpers.PrintContinue();
	}

	private void HandleShowAllBooks()
	{
		Book[] books = _service.GetAllBooks();

		if(books.Length == 0) 
		{
			ConsoleHelpers.PrintWarning("No books found");
			ConsoleHelpers.PrintContinue();
			return;
		}

		foreach(Book book in books)
		{
			ConsoleHelpers.PrintBook(book);
		}

		ConsoleHelpers.PrintContinue();
	}
}