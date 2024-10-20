using CoffeeManagementSystem.Entities.Models;
using CoffeeManagementSystem.Repositories.Context;
using CoffeeManagementSystem.Repositories.Interfaces;
using ContextFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.App.Controllers
{
    [Authorize(Roles ="User")]
    public class CartController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IBaseRepository<Product> _productRepository;
        private IBaseRepository<Cart> _cartRepository;
		private readonly MyDbContext _context;


		public CartController(UserManager<AppUser> userManager, IBaseRepository<Product> productRepository, IBaseRepository<Cart> cartRepository, MyDbContext context)
		{
			_userManager = userManager;
			_productRepository = productRepository;
			_cartRepository = cartRepository;
			_context = context;
		}

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);
      
            if (cart != null)
            {

                cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price);
                _context.SaveChanges();
            }
            return View(cart);
        }



        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
		{
			var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var product = await _productRepository.GetById(productId);
			if (product == null) return NotFound();

			var cart = await _context.Carts
				.Include(c => c.CartItems)
				.FirstOrDefaultAsync(c => c.UserId == user.Id);

			if (cart == null)
			{
				cart = new Cart { UserId = user.Id };
				_context.Carts.Add(cart);
			}

			var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

			if (cartItem == null)
			{
				cartItem = new CartItem
				{
					ProductId = product.ProductId,
					Quantity = quantity,
					Price = product.Price
				};
				cart.CartItems.Add(cartItem);
			}
			else
			{
				cartItem.Quantity += quantity;
			}

			cart.TotalPrice = cart.CartItems.Sum(ci => ci.TotalPrice);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Cart");
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCartItem(int itemId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.Id == itemId);

            if (cartItem != null)
            {
                var cartId = cartItem.Id; 

                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();

                var cart = _context.Carts
                    .Include(c => c.CartItems)      
                    .FirstOrDefault(c => c.Id == cartId); 

                if (cart != null)
                {
                    
                    cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price);
                    _context.SaveChanges();  
                }
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Payment() {

            return View();
        }

        public IActionResult ThankYou()
        {
           
            return View();
        }



    }
}
