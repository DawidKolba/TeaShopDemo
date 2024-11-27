using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using TeaShopDemo.Models;
using TeaShopDemo.Services;

namespace TeaShopDemo.Tests
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IHubContext<CartHub>> _hubContextMock;
        private CartService _cartService;

        [SetUp]
        public void Setup()
        {
            // Mock HttpContextAccessor to simulate user session
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new MockHttpSession(); // Mocked session

            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            // Mock HubContext to simulate SignalR hub
            _hubContextMock = new Mock<IHubContext<CartHub>>();

            // Create instance of CartService with mocked dependencies
            _cartService = new CartService(_httpContextAccessorMock.Object, _hubContextMock.Object);
        }

        [Test]
        public async Task AddingMultipleProducts_ShouldStoreAllInCart()
        {
            // Arrange - create multiple products to add to the cart
            var item1 = new CartItem { ProductId = 1, Name = "Green Tea", Price = 10.0m, Quantity = 1 };
            var item2 = new CartItem { ProductId = 2, Name = "Black Tea", Price = 15.0m, Quantity = 2 };
            var item3 = new CartItem { ProductId = 3, Name = "Herbal Tea", Price = 12.0m, Quantity = 3 };

            // Act - add products to the cart via CartService
            await _cartService.AddToCart(item1);
            await _cartService.AddToCart(item2);
            await _cartService.AddToCart(item3);

            // Assert - verify that all items are stored in the cart
            var cartItems = _cartService.GetCartItems();
            Assert.AreEqual(3, cartItems.Count, "The cart should contain 3 items.");
            Assert.Contains(item1, cartItems, "The cart should contain the product Green Tea.");
            Assert.Contains(item2, cartItems, "The cart should contain the product Black Tea.");
            Assert.Contains(item3, cartItems, "The cart should contain the product Herbal Tea.");
        }
    }
}
