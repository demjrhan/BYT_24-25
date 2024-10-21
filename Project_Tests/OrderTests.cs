
using Project;

namespace Project_Tests
{
    public class OrderTests
    {
        [Test]
        //Should initialize correctly
        public void OrderCreation()
        {
            var product1 = new Book("Book 1", 50, 10, "Author", "Genre", 1995);
            var product2 = new Book("Book 2", 75, 5, "Author", "Genre", 2015);
            var products = new List<Product> { product1, product2 };

            var order = new Order(1, DateTime.Now, "processing", 125, products);

            Assert.AreEqual(1, order.CustomerId);
            Assert.AreEqual(125, order.Amount);
            Assert.AreEqual(2, order.Products.Count);
        }

        [Test]
        //Should add shipping to order
        public void CreateShipping()
        {
            var order = new Order(1, DateTime.Now, "processing", 100, new List<Product>());
            string method = "InPost";
            double cost = 15.0;
            string address = "Koszykowa 86";

            var shipping = order.CreateShipping(method, cost, address);

            Assert.AreEqual("InPost", shipping.Method);
            Assert.AreEqual(15.0, shipping.Cost);
            Assert.AreEqual("Koszykowa 86", shipping.Address);
            Assert.AreEqual(115.0, order.Amount); // Check 
            Assert.IsNotNull(order.ShippingId);  // ShippingId 
        }

        [Test]
        //Should return correct order details
        public void ToString()
        {
            var product = new Book("Book", 50, 10, "Author", "Genre", 2022);
            var products = new List<Product> { product };
            var order = new Order(1, DateTime.Now, "processing", 50, products);

            string result = order.ToString();

            Assert.IsTrue(result.Contains("Order Id:"));
            Assert.IsTrue(result.Contains("Customer Id: 1"));
            Assert.IsTrue(result.Contains("Amount: 50"));
        }
    }
}
