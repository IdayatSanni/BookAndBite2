using BookAndBite2.Models;


namespace BookAndBite2.ViewModels
{
    public class CartDetailsViewModel
    {
        public CartDto Cart { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        public IEnumerable<BookDto> Books { get; set; }
    }
}
