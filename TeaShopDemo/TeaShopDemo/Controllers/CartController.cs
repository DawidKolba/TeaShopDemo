using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TeaShopDemo.Models;
using TeaShopDemo.Services;

namespace TeaShopDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private ISession _session;

        public CartController(CartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _session = httpContextAccessor.HttpContext.Session;
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

        [HttpPost("SaveCartToSession")]
        public IActionResult SaveCartToSession([FromBody] List<CartItem> cartItems)
        {
            var cartData = JsonConvert.SerializeObject(cartItems);
            _session.SetString("CartData", cartData);

            return Ok();
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
