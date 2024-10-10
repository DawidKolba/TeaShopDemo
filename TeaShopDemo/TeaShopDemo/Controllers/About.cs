using Microsoft.AspNetCore.Mvc;

namespace TeaShopDemo.Controllers
{
    public class About : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
