using BookAndBite2.Models;
using BookAndBite2.Services;
using BookAndBite2.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAndBite2.Controllers
{
    public class BookPageController : Controller
    {
        private readonly IBookService _bookService;

        public BookPageController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: BookPage/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<BookDto> books = await _bookService.ListBooks();

            if (books == null || !books.Any())
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: BookPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            BookDto book = await _bookService.FindBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: BookPage/New
        public IActionResult New()
        {
            return View();
        }

        // POST: BookPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return View("New", bookDto);
            }

            ServiceResponse response = await _bookService.AddBook(bookDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "BookPage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: BookPage/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var bookDto = await _bookService.FindBook(id);
            if (bookDto == null)
            {
                return NotFound();
            }

            return View(bookDto);
        }

        // POST: BookPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ServiceResponse response = await _bookService.DeleteBook(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: BookPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var bookDto = await _bookService.FindBook(id);
            if (bookDto == null)
            {
                return NotFound();
            }

            return View(bookDto);
        }

        // POST: BookPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return View(bookDto);
            }

            ServiceResponse response = await _bookService.UpdateBook(bookDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: BookPage/ListBookCarts/{bookId}
        [HttpGet]
        public async Task<IActionResult> ListBookCarts(int bookId)
        {
            var carts = await _bookService.ListBookCarts(bookId);

            if (carts == null || !carts.Any())
            {
                return NotFound();
            }

            return View(carts);
        }
    }
}
