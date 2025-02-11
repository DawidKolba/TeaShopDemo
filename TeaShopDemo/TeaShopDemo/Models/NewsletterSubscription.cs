using System.ComponentModel.DataAnnotations;

namespace TeaShopDemo.Models
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public DateTime SubscribedAt { get; set; } = DateTime.Now;
    }
}
