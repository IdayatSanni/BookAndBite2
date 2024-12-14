using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAndBite2.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ReviewText { get; set; } = "";

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;        

        public bool IsApproved { get; set; } = false;

        //Review can belong to one cart

        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }
        
    }

    public class ReviewDto
    {
        public int ReviewId { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }

        public int CartName { get; set; }

    }



}
