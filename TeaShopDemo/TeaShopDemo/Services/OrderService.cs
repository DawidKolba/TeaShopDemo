using TeaShopDemo.Models;

namespace TeaShopDemo.Services
{
    public class OrderService
    {
        private readonly TeaShopDemoContext _context;

        public OrderService(TeaShopDemoContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}
