using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using TeaShopDemo.Models;

namespace TeaShopDemo.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConcurrentDictionary<string, List<CartItem>> _userCarts = new();
            
        private readonly IHubContext<CartHub> _hubContext;
        private readonly List<CartItem> _cartItems = new();

        public CartService(IHttpContextAccessor httpContextAccessor, IHubContext<CartHub> hubContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCartItems();
            cart.RemoveAll(i => i.ProductId == productId);
        }

        public void ClearCart()
        {
            var cart = GetCartItems();
            cart.Clear();
        }

        public decimal GetTotalPrice()
        {
            var cart = GetCartItems();
            return cart.Sum(i => i.Price * i.Quantity);
        }

        public List<CartItem> GetCartItems()
        {
            var sessionId = GetSessionId();
            return _userCarts.GetOrAdd(sessionId, new List<CartItem>());
        }

        public void UpdateQuantity(int productId, int change)
        {
            var cartItem = GetCartItems().FirstOrDefault(item => item.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += change;
       
                if (cartItem.Quantity <= 0)
                {
                    RemoveFromCart(productId);
                }
            }
        }

        
        public async Task AddToCart(CartItem item)
        {
            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }
            var sessionId = GetSessionId();
            await _hubContext.Clients.User(sessionId).SendAsync("UpdateCartCount", GetCartItems().Count);
        }


        private string GetSessionId()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context is null)
                return string.Empty;

            if (!context.Session.TryGetValue("SessionId", out _))
            {
                var sessionId = Guid.NewGuid().ToString();
                context.Session.SetString("SessionId", sessionId);
                return sessionId;
            }

            return context.Session.GetString("SessionId");
        }
    }
}
