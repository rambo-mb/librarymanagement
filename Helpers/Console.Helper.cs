using LibraryManagement.Models.Books;

namespace LibraryManagement.Helpers;

public static class ConsoleHelpers
{
	public static int ValidateInt(string name)
	{
		int result;
		bool isValid = true;

		do
		{
			Console.Write($"Enter {name}: ");
			string userInput = Console.ReadLine().Trim();

			if(userInput == "0" || userInput.ToLower() == "q") return 0;

			isValid = int.TryParse(userInput, out result) && result > 0;

			if(!isValid) PrintError($"Field {name} is not valid");

		} while(!isValid);

		return result;
	}

	public static string ValidateString(string name)
	{
		string result = string.Empty;
		bool isValid = true;

		do
		{
			Console.Write($"Enter {name}: ");
			string userInput = Console.ReadLine().Trim();

			if(userInput == "0" || userInput.ToLower() == "q") return null;

			if(string.IsNullOrWhiteSpace(userInput))
			{
				isValid = false;
				PrintError($"Field {name} is not valid");
			}
			else
			{
				result = userInput;
				isValid = true;
			}
			
		} while(!isValid);
		
		return result;
	}

	public static void PrintBook(Book book)
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

	public static void PrintSuccess(string message)
	{
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"{message}");
		Console.ResetColor();
	}
	public static void PrintError(string message)
	{
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"{message}");
		Console.ResetColor();
	}

	public static void PrintContinue()
	{
		Console.Write("Press any key to continue...");
		Console.ReadKey();
	}

	public static void PrintWarning(string message)
	{
		Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(message);
    Console.ResetColor();
	}
}