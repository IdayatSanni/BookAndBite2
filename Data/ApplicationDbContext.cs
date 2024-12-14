using BookAndBite2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookAndBite2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        //customer.cs will map to Customers table
        public DbSet<Customer> Customers { get; set; }

        //cart.cs will map to Carts table
        public DbSet<Cart> Carts { get; set; }

        //product.cs will map to Products table
        public DbSet<Product> Products { get; set; }

        //book.cs will map to Books table
        public DbSet<Book> Books { get; set; }

        //category.cs will map to Categories table
        public DbSet<Category> Categories { get; set; }

        //review.cs will map to Reviews table
        public DbSet<Review> Reviews { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
