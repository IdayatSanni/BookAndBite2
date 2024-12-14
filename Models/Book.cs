using System.ComponentModel.DataAnnotations;

namespace BookAndBite2.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(50)]
        public string BookName { get; set; } = "";

        [Required]
        [MaxLength(50)]
        public string BookAuthor { get; set; } = "";       

        public string? BookPicture { get; set; }

        public bool HasPic { get; set; } = false;

        [Required]
        public bool IsBookOfTheMonth { get; set; }

        //Book can belong to many carts

        public ICollection<Cart>? Carts { get; set; }

        //Book can have many categories
        public ICollection<Category>? Categories { get; set; }

    }

    public class BookDto
    {
        public int BookId { get; set; }

        public string BookName { get; set; }

        public string BookAuthor { get; set; }

        public string? BookPicture { get; set; }

        public List<CartDto> Carts { get; set; }   


    }
}
