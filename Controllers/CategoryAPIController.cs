using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAndBite2.Models;
using BookAndBite2.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BookAndBite2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;

        public CategoryAPIController(ICategoryService categoryService, IBookService bookService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
        }

        /// <summary>
        /// Returns a list of all categories with book counts. Everyone can see
        /// </summary>
        /// <returns>List of categories with associated book counts.</returns>
        /// <example>/api/CategoryAPI/ListCategories</example>
        [HttpGet("ListCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> ListCategories()
        {
            var categories = await _categoryService.ListCategoriesWithBookCount();
            return Ok(categories);
        }

        /// <summary>
        /// Retrieves details for a specific category, including associated books. Everyone can see
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>Category details and associated books, or 404 if not found.</returns>
        /// <example>/api/CategoryAPI/FindCategory/1</example>
        [HttpGet("FindCategory/{id}")]
        public async Task<IActionResult> FindCategory(int id)
        {
            var category = await _categoryService.FindCategory(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            var books = await _categoryService.GetBooksByCategory(id);
            return Ok(new { Category = category, Books = books });
        }

        /// <summary>
        /// Creates a new category. Only admin
        /// </summary>
        /// <param name="categoryDto">The DTO containing the new category data.</param>
        /// <returns>201 Created if successful, or 400 Bad Request with errors.</returns>
        /// <example>/api/CategoryAPI/AddCategory</example>
        [HttpPost("AddCategory")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _categoryService.AddCategory(categoryDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return CreatedAtAction(nameof(FindCategory), new { id = response.CreatedId }, categoryDto);
            }

            return BadRequest(response.Messages);
        }

        /// <summary>
        /// Updates an existing category by its ID. Only admin
        /// </summary>
        /// <param name="id">The unique identifier of the category to update.</param>
        /// <param name="categoryDto">The DTO containing updated category data.</param>
        /// <returns>NoContent if successful, or 400/404 with errors.</returns>
        /// <example>/api/CategoryAPI/UpdateCategory/1</example>
        [HttpPut("UpdateCategory/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest("Category ID mismatch.");
            }

            var response = await _categoryService.UpdateCategory(categoryDto);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a category by its ID. Only admin
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete.</param>
        /// <returns>NoContent if successful, or 404 if not found.</returns>
        /// <example>/api/CategoryAPI/DeleteCategory/1</example>
        [HttpDelete("DeleteCategory/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategory(id);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }

            return NoContent();
        }

        /// <summary>
        /// Retrieves books associated with a specific category. everyone can see
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <returns>List of books in the category, or 404 if category not found.</returns>
        /// <example>/api/CategoryAPI/{categoryId}/Books</example>
        [HttpGet("{categoryId}/Books")]
        
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByCategory(int categoryId)
        {
            var books = await _categoryService.GetBooksByCategory(categoryId);
            if (books == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(books);
        }
    }
}

