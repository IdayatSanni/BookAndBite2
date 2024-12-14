using BookAndBite2.Models;
using Microsoft.AspNetCore.Mvc;
using BookAndBite2.Services;
using BookAndBite2.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BookAndBite2.Controllers
{
    public class CartPageController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICustomerService _customerService;


        public CartPageController(ICartService cartService, ICustomerService customerService)
        {
            _cartService = cartService;
            _customerService = customerService;
        }

        // GET: CartPage/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<CartDto> carts = await _cartService.ListCarts();

            if (carts == null || !carts.Any())
            {
                return NotFound();
            }

            return View(carts);
        }

        // GET: CartPage/Details/{id}
        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            // Call the GetCartWithBooks method to retrieve cart with associated books
            CartDto cart = await _cartService.GetCartWithBooks(id);

            // If cart is not found, return a 404 error
            if (cart == null)
            {
                return NotFound();
            }

            // Pass the cart with books to the view
            return View(cart);  // Ensure the view is strongly typed to CartDto
        }

        // GET: CartPage/New

        public async Task<IActionResult> New()
        {
            //Selecting an existing customer from dropdow
            var customers = await _customerService.ListCustomers();
            ViewBag.Customers = new SelectList(customers, "CustomerId", "CustomerLname", "CustomerFname");

            return View();
        }


        // POST: CartPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(CartDto cartDto)
        {

            if (cartDto.DateCreated == DateTime.MinValue)
            {
                cartDto.DateCreated = DateTime.Now;
            }

            ServiceResponse response = await _cartService.AddCart(cartDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "CartPage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: CartPage/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {

            var cartDto = await _cartService.FindCart(id);
            if (cartDto == null)
            {
                return NotFound();
            }

            return View(cartDto);
        }

        // POST: CartPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            ServiceResponse response = await _cartService.DeleteCart(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {

                return RedirectToAction("List");
            }
            else
            {

                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }

        }

        // GET: CartPage/Edit/{id} 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var cartDto = await _cartService.FindCart(id);
            if (cartDto == null)
            {
                return NotFound();
            }


            var customers = await _customerService.ListCustomers();
            ViewBag.Customers = new SelectList(customers, "CustomerId", "CustomerLname");

            return View(cartDto);
        }


        // POST: CartPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, CartDto cartDto)
        {
            ServiceResponse response = await _cartService.UpdateCart(cartDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("List");
            }
            else
            {
                var customers = await _customerService.ListCustomers();
                ViewBag.Customers = new SelectList(customers, "CustomerId", "CustomerLname");
                return View(cartDto);
            }
        }


    }
}
