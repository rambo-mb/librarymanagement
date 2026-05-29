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
		throw new NotImplementedException();
	}

	private void HandleReturnBook()
	{
		throw new NotImplementedException();
	}

	private void HandleBorrowBook()
	{
		throw new NotImplementedException();
	}

	private void HandleCreateBook()
	{
		throw new NotImplementedException();
	}

	private void HandleShowAllBooks()
	{
		throw new NotImplementedException();
	}
}