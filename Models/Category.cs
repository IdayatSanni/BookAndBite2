using System.ComponentModel.DataAnnotations;

namespace BookAndBite2.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string BookCategory { get; set; } = "";

        //Category can have many books
        public ICollection<Book> Books { get; set; }
        
    }

    public class CategoryDto
    {
        public int CategoryId { get; set; }



        [Required, MaxLength(25)]
        public string BookCategory { get; set; } = "";

        // Navigation property for books
        public virtual ICollection<BookDto> Books { get; set; } = new List<BookDto>();

        public string? Title { get; set; }
        public int? BookCount { get; set; }
    }
}
