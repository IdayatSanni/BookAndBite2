using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookAndBite2.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public string CartName { get; set; }

        public DateTime DateCreated { get; set; }


        //One cart can have only one Customer(user)

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        //Cart can have many reviews
        public ICollection<Review>? Reviews { get; set; }

        //Carts can contain many products

        public ICollection<Product>? Products { get; set; }

        //Cart can belong to many books

        public ICollection<Book>? Books { get; set; }


    }

    public class CartDto
    {
        public int CartId { get; set; }

        public string CartName { get; set; }

        public DateTime DateCreated { get; set; }

        public List<ProductDto> Products { get; set; }

        public List<BookDto> Books { get; set; } = new List<BookDto>();

        public string CartCustomer { get; set; }

        public int CustomerId { get; set; }

        public List<string> BookName { get; set; }

        public List<string> BookAuthor { get; set; }

        public List<string> Review { get; set; }


    }
}
