using BookAndBite2.Models;
using BookAndBite2.Services;

namespace BookAndBite2.Interfaces;

public interface ICartService
{
    // base CRUD
    Task<IEnumerable<CartDto>> ListCarts();
    Task<CartDto?> FindCart(int id);
    Task<ServiceResponse> UpdateCart(CartDto cartDto);
    Task<ServiceResponse> AddCart(CartDto cartDto);
    Task<ServiceResponse> DeleteCart(int id);

    // related methods
    Task<IEnumerable<ProductDto>> ListCartsProducts(int id);
    Task<CartDto> GetCartWithBooks(int cartId);  // Updated return type to CartDto

    Task<ServiceResponse> AddProductToCart(int cartId, int productId);
    Task<ServiceResponse> RemoveProductFromCart(int cartId, int productId);
}
