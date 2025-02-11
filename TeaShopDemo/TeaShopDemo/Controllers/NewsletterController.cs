using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeaShopDemo.Models;
using TeaShopDemo.Services;

namespace TeaShopDemo.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly NewsletterService _newsletterService;

        public NewsletterController(NewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(NewsletterSubscription subscription)
        {
            if (ModelState.IsValid)
            {
                await _newsletterService.AddSubscriptionAsync(subscription);
                TempData["SubscriptionSuccess"] = "Thank you for subscribing!";
            }
            else
            {
                TempData["SubscriptionError"] = "Please enter a valid email address.";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}