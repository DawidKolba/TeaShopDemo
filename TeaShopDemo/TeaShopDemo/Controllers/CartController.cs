using Microsoft.AspNetCore.Mvc;
using TeaShopDemo.Services;

namespace TeaShopDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("Count")]
        public IActionResult GetCartItemCount()
        {
            var count = _cartService.GetCartItems().Count;
            return Ok(count);
        }     

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }


    }  
}
