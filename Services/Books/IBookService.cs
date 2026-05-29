using LibraryManagement.Models.Books;

namespace LibraryManagement.Services.Books;

public interface IBookService
{
	void CreateBook(Book book);

	Book[] GetAllBooks();

	Book GetBookById(int id);

	void BorrowBook(int id);

	void ReturnBook(int id);
}