using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TeaShopDemo.Models;
using TeaShopDemo.Services;

namespace TeaShopDemo.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly CartService _cartService;

        public CheckoutController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            ViewData["CartItems"] = cartItems;

            return View();
        }
    }
}
