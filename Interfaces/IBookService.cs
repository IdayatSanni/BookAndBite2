using BookAndBite2.Models;

namespace BookAndBite2.Interfaces
{
    public interface IBookService
    {
        // Base CRUD methods

        Task<IEnumerable<BookDto>> ListBooks(); // List all books
        Task<BookDto?> FindBook(int id);       // Find a book by ID
        Task<ServiceResponse> AddBook(BookDto bookDto);  // Add a new book
        Task<ServiceResponse> UpdateBook(BookDto bookDto); // Update a book
        Task<ServiceResponse> DeleteBook(int id);  // Delete a book

        // Related methods

        Task<IEnumerable<CartDto>> ListBookCarts(int bookId); // List carts associated with a book
        Task<ServiceResponse> AddBookToCart(int bookId, int cartId); // Add a book to a cart
        Task<ServiceResponse> RemoveBookFromCart(int bookId, int cartId); // Remove a book from a cart
    }
}

