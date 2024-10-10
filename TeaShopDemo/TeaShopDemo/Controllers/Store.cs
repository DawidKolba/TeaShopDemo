using Microsoft.AspNetCore.Mvc;

namespace TeaShopDemo.Controllers
{
    public class Store : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
