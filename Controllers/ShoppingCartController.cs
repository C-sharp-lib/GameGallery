using GameGallery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using GameGallery.Services;

namespace GameGallery.Controllers
{

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _cartService;
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(IShoppingCartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            var total = _cartService.GetTotalPrice();
            ViewBag.Total = total;
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int gameId, int quantity = 1)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
            {
                return NotFound();
            }

            _cartService.AddToCart(game, quantity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int gameId)
        {
            _cartService.RemoveFromCart(gameId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
