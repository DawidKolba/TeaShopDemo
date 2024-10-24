namespace TeaShopDemo.Models
{
    public class OrderInfo
    {
        public CustomerInfo Customer { get; set; } = new CustomerInfo();
        public DateTime orderDate { get; set; }
        public List <Product> products { get; set; }
    }
}
