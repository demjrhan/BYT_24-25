
using Project;
using Project.Enum;
using Project.Models;

namespace Project_Tests
{
    public class OrderTests
    {
        [Test]
        public void OrderCreation()
        {
            var product1 = new Book("Book 1", 50, 10, "Author", "Genre", 1995);
            var product2 = new Book("Book 2", 75, 5, "Author", "Genre", 2015);
            var products = new List<Product> { product1, product2 };

            var order = new Order(1, DateTime.Now, OrderStatus.Proccessing, 125, products);

            Assert.That(order.CustomerId, Is.EqualTo(1));
            Assert.That(order.Amount, Is.EqualTo(125));
            Assert.That(order.Products.Count, Is.EqualTo(2));
        }

        [Test]
        public void CreateShipping()
        {
            var order = new Order(1, DateTime.Now, OrderStatus.Proccessing, 100, new List<Product>());
            ShippingMethod method = ShippingMethod.Express;
            double cost = 15.0;
            string address = "Koszykowa 86";

            var shipping = order.CreateShipping(method, cost, address);

            Assert.That(shipping.Method, Is.EqualTo(ShippingMethod.Express));
            Assert.That(shipping.Cost, Is.EqualTo(15.0));
            Assert.That(shipping.Address, Is.EqualTo("Koszykowa 86"));
            Assert.That(order.Amount, Is.EqualTo(115.0)); 
            Assert.IsNotNull(order.ShippingId);  
        }

        [Test]
        public void Test_ToString()
        {
            var product = new Book("Book", 50, 10, "Author", "Genre", 2022);
            var products = new List<Product> { product };
            var order = new Order(1, DateTime.Now, OrderStatus.Proccessing, 50, products);

            string result = order.ToString();

            Assert.IsTrue(result.Contains("Order Id:"));
            Assert.IsTrue(result.Contains("Customer Id: 1"));
            Assert.IsTrue(result.Contains("Amount: 50"));
        }
    }
}
