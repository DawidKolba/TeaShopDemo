using System;
using System.ComponentModel.DataAnnotations;

namespace TeaShopDemo.Models
{
    public class Newsletter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "HTML content is required.")]
        public string HtmlContent { get; set; }

        public DateTime CreationDate { get; set; }
    }
}