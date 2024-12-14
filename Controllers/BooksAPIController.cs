using BookAndBite2.Interfaces;
using BookAndBite2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAndBite2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksAPIController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksAPIController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Returns a list of all books. Everyone can see.
        /// </summary>
        /// <returns>Returns a list of books.</returns>
        /// <example>/api/BooksAPI/ListBooks</example>
        [HttpGet("ListBooks")]
        public async Task<ActionResult<IEnumerable<BookDto>>> ListBooks()
        {
            var bookDtos = await _bookService.ListBooks();
            return Ok(bookDtos);
        }

        /// <summary>
        /// Retrieves information about a book by its ID. Everyone can see.
        /// </summary>
        /// <param name="id">The unique ID of the book.</param>
        /// <returns>Returns book information if found, otherwise shows error.</returns>
        /// <example>/api/BooksAPI/FindBook/1</example>
        [HttpGet("FindBook/{id}")]        
        public async Task<IActionResult> FindBook(int id)
        {
            var bookDto = await _bookService.FindBook(id);
            if (bookDto == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(bookDto);
        }

        /// <summary>
        /// Adds a new book. Only admin.
        /// </summary>
        /// <param name="bookDto">The transfer object containing the new book's details.</param>
        /// <returns>Creates a new book or returns an error if input is invalid.</returns>
        /// <example>/api/BooksAPI/AddBook</example>
        [HttpPost("AddBook")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddBook(BookDto bookDto)
        {
            var serviceResponse = await _bookService.AddBook(bookDto);
            if (serviceResponse.Status == ServiceResponse.ServiceStatus.Created)
            {
                return CreatedAtAction(nameof(FindBook), new { id = serviceResponse.CreatedId }, bookDto);
            }
            return BadRequest(serviceResponse.Messages);
        }

        /// <summary>
        /// Updates a book's details by its ID. Only admin.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="bookDto">The transfer object containing updated book details.</param>
        /// <returns>No content if successful, or an error if the book is not found or input is invalid.</returns>
        /// <example>/api/BooksAPI/UpdateBook/1</example>
        [HttpPut("UpdateBook/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            bookDto.BookId = id;
            var serviceResponse = await _bookService.UpdateBook(bookDto);
            if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(serviceResponse.Messages);
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a book by its ID. Only admin
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>No content if successful, or an error if the book is not found.</returns>
        /// <example>/api/BooksAPI/DeleteBook/1</example>
        [HttpDelete("DeleteBook/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var serviceResponse = await _bookService.DeleteBook(id);
            if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(serviceResponse.Messages);
            }
            return NoContent();
        }

        /// <summary>
        /// Retrieves a list of carts containing a specific book. Everyone can see
        /// </summary>
        /// <param name="bookId">The ID of the book.</param>
        /// <returns>A list of carts containing the specified book.</returns>
        /// <example>/api/BooksAPI/{bookId}/Carts</example>
        [HttpGet("{bookId}/Carts")]
        
        public async Task<ActionResult<IEnumerable<CartDto>>> GetBookCarts(int bookId)
        {
            var cartDtos = await _bookService.ListBookCarts(bookId);
            if (cartDtos == null)
            {
                return NotFound("No carts found for this book.");
            }
            return Ok(cartDtos);
        }
    }
}

