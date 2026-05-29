using LibraryManagement;
using LibraryManagement.Services.Books;

IBookService service = new BookService();
App app = new App(service);
app.Run();