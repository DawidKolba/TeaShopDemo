namespace TeaShopDemo.Models
{
    namespace TeaShopDemo.Models
    {
        public class OrderItem
        {
            public int OrderItemId { get; set; }

            public int OrderId { get; set; } // Foreign key
            public Order Order { get; set; }

            public int ProductId { get; set; }
            public Product Product { get; set; }

            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }

}
