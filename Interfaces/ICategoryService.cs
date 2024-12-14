using System.Collections.Generic;
using System.Threading.Tasks;
using BookAndBite2.Models;
using BookAndBite2.Services;
using BookAndBite2.Interfaces;


namespace BookAndBite2.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> ListCategoriesWithBookCount();
        Task<IEnumerable<BookDto>> GetBooksByCategory(int categoryId);
        Task<CategoryDto?> FindCategory(int Categoryid);
        Task<ServiceResponse> AddCategory(int CategoryId);
        Task<ServiceResponse> UpdateCategory(int CategoryId);
        Task<ServiceResponse> DeleteCategory(int CategoryId);
    }
}
