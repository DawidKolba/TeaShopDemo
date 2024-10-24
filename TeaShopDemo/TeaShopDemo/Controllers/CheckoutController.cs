using Microsoft.AspNetCore.Mvc;

namespace TeaShopDemo.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet("/checkout")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
