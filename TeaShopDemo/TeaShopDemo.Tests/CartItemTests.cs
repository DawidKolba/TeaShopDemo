using NUnit.Framework;
using TeaShopDemo.Models;

namespace TeaShopDemo.Tests
{
    [TestFixture]
    public class CartItemTests
    {
        [Test]
        public void CartItemProperties_ShouldHoldCorrectValues()
        {
            // Arrange
            var item = new CartItem
            {
                ProductId = 1,
                Name = "Green Tea",
                Price = 10.0m,
                Quantity = 2,
                ImageUrl = "https://example.com/green-tea.jpg"
            };

            // Assert
            Assert.AreEqual(1, item.ProductId);
            Assert.AreEqual("Green Tea", item.Name);
            Assert.AreEqual(10.0m, item.Price);
            Assert.AreEqual(2, item.Quantity);
            Assert.AreEqual("https://example.com/green-tea.jpg", item.ImageUrl);
        }
    }
}
