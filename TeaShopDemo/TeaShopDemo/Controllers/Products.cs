using Microsoft.AspNetCore.Mvc;

namespace TeaShopDemo.Controllers
{
    public class Products : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
