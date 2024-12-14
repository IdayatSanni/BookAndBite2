using BookAndBite2.Data;
using BookAndBite2.Models;
using Microsoft.EntityFrameworkCore;
using BookAndBite2.Interfaces;

namespace BookAndBite2.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        // Dependency injection of database context
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all books
        public async Task<IEnumerable<BookDto>> ListBooks()
        {
            var books = await _context.Books.Include(b => b.Carts).ToListAsync();
            var bookDtos = books.Select(book => new BookDto
            {
                BookId = book.BookId,
                BookName = book.BookName,
                BookAuthor = book.BookAuthor,
                Carts = book.Carts?.Select(cart => new CartDto
                {
                    CartId = cart.CartId,
                    CartName = cart.CartName,
                    DateCreated = cart.DateCreated
                }).ToList() ?? new List<CartDto>()
            }).ToList();

            return bookDtos;
        }

        // Find a book by ID
        public async Task<BookDto?> FindBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Carts)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null) return null;

            var bookDto = new BookDto
            {
                BookId = book.BookId,
                BookName = book.BookName,
                BookAuthor = book.BookAuthor,
                Carts = book.Carts?.Select(cart => new CartDto
                {
                    CartId = cart.CartId,
                    CartName = cart.CartName,
                    DateCreated = cart.DateCreated
                }).ToList() ?? new List<CartDto>()
            };

            return bookDto;
        }

        // Add a new book
        public async Task<ServiceResponse> AddBook(BookDto bookDto)
        {
            var serviceResponse = new ServiceResponse();

            var book = new Book
            {
                BookName = bookDto.BookName,
                BookAuthor = bookDto.BookAuthor,
                HasPic = !string.IsNullOrEmpty(bookDto.BookName),
                IsBookOfTheMonth = false // Default value; update as needed
            };

            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
                serviceResponse.CreatedId = book.BookId;
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Error adding the book.");
                serviceResponse.Messages.Add(ex.Message);
            }

            return serviceResponse;
        }

        // Update a book
        public async Task<ServiceResponse> UpdateBook(BookDto bookDto)
        {
            var serviceResponse = new ServiceResponse();

            var book = await _context.Books.FindAsync(bookDto.BookId);
            if (book == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Book not found.");
                return serviceResponse;
            }

            book.BookName = bookDto.BookName;
            book.BookAuthor = bookDto.BookAuthor;

            try
            {
                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Error updating the book.");
                serviceResponse.Messages.Add(ex.Message);
            }

            return serviceResponse;
        }

        // Delete a book
        public async Task<ServiceResponse> DeleteBook(int id)
        {
            var serviceResponse = new ServiceResponse();

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Book not found.");
                return serviceResponse;
            }

            try
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Error deleting the book.");
                serviceResponse.Messages.Add(ex.Message);
            }

            return serviceResponse;
        }

        // List all carts associated with a book
        public async Task<IEnumerable<CartDto>> ListBookCarts(int bookId)
        {
            var book = await _context.Books
                .Include(b => b.Carts)
                .FirstOrDefaultAsync(b => b.BookId == bookId);

            if (book == null) return null;

            var cartDtos = book.Carts?.Select(cart => new CartDto
            {
                CartId = cart.CartId,
                CartName = cart.CartName,
                DateCreated = cart.DateCreated
            }).ToList() ?? new List<CartDto>();

            return cartDtos;
        }

        // Add a book to a cart
        public async Task<ServiceResponse> AddBookToCart(int bookId, int cartId)
        {
            var serviceResponse = new ServiceResponse();

            var book = await _context.Books.Include(b => b.Carts).FirstOrDefaultAsync(b => b.BookId == bookId);
            var cart = await _context.Carts.FindAsync(cartId);

            if (book == null || cart == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                if (book == null) serviceResponse.Messages.Add("Book not found.");
                if (cart == null) serviceResponse.Messages.Add("Cart not found.");
                return serviceResponse;
            }

            try
            {
                book.Carts.Add(cart);
                await _context.SaveChangesAsync();
                serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Error adding book to cart.");
                serviceResponse.Messages.Add(ex.Message);
            }

            return serviceResponse;
        }

        // Remove a book from a cart
        public async Task<ServiceResponse> RemoveBookFromCart(int bookId, int cartId)
        {
            var serviceResponse = new ServiceResponse();

            var book = await _context.Books.Include(b => b.Carts).FirstOrDefaultAsync(b => b.BookId == bookId);
            var cart = await _context.Carts.FindAsync(cartId);

            if (book == null || cart == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                if (book == null) serviceResponse.Messages.Add("Book not found.");
                if (cart == null) serviceResponse.Messages.Add("Cart not found.");
                return serviceResponse;
            }

            try
            {
                book.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Error removing book from cart.");
                serviceResponse.Messages.Add(ex.Message);
            }

            return serviceResponse;
        }
    }
}

