using System.Collections.Generic;
using BookAndBite2.Models;


namespace BookAndBite2.ViewModels
{
    public class CustomerDetailsViewModel
    {    //information about customer
        public CustomerDto Customer { get; set; }
        //i want to show all the carts related to customer
        public IEnumerable<CartDto> Carts { get; set; }
    }
}
