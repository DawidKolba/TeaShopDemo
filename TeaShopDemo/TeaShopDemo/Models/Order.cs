using System;
using System.Collections.Generic;
using TeaShopDemo.Models.Enums;
using TeaShopDemo.Models.TeaShopDemo.Models;

namespace TeaShopDemo.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
