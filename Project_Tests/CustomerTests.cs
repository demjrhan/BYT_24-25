
using Project;

namespace Project_Tests
{
    public class CustomerTests
    {
        [Test]
        //Should create order with cart contents
        public void CreateOrder()
        {
            var customer = new Customer("Ruslan", "Teimurov", "Rus@mail.com", "12345", "Address", 30, false, true, false);
            var product = new Book("Book", 50, 10, "Author", "Genre", 2022);
            customer.Cart.AddProduct(product);

            var order = customer.CreateOrder();

            Assert.AreEqual(1, order.Products.Count);
            Assert.AreEqual(product, order.Products[0]);
            Assert.AreEqual(customer.CustomerId, order.CustomerId);
        }

        [Test]
        //Should create payment successfully
        public void CreatePayment()
        {
            var customer = new Customer("Ruslan", "Teimurov", "Rus@mail.com", "12345", "Address", 30, false, true, false);
            var orderId = 1;
            var amount = 100;

            var payment = customer.CreatePayment(orderId, "Credit Card", amount);

            Assert.AreEqual(orderId, payment.OrderId);
            Assert.AreEqual(amount, payment.Amount);
            Assert.AreEqual("Credit Card", payment.PaymentMethod);
        }

      /*  [Test]
        //Should return correct customer details
        public void ToString()
        {
            var customer = new Customer("Ruslan", "Teimurov", "Rus@mail.com", "12345", "Address", 30, false, true, false);

            string result = customer.ToString();

            Assert.IsTrue(result.Contains("Ruslan"));
            Assert.IsTrue(result.Contains("Teimurov"));
        }*/
    }
}
