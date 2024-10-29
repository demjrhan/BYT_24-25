
using Project;

namespace Project_Tests
{
    public class CustomerTests
    {
        [Test]
        public void CreateOrder()
        {
            var customer = new Customer("Ruslan", "Teimurov", "Rus@mail.com", "12345", "Address", 30, false, true, false);
            var product = new Book("Book", 50, 10, "Author", "Genre", 2022);
            customer.Cart.AddProduct(product);

            var order = customer.CreateOrder();

            Assert.That(order.Products, Has.Count.EqualTo(1));
            Assert.That(order.Products[0], Is.EqualTo(product));
            Assert.That(order.CustomerId, Is.EqualTo(customer.CustomerId));
        }

        [Test]
        public void CreatePayment()
        {
            var customer = new Customer("Ruslan", "Teimurov", "Rus@mail.com", "12345", "Address", 30, false, true, false);
            var order = new Order(customer.CustomerId, DateTime.Now, OrderStatus.Proccessing, 30, []);
            var orderId = 0;
            var amount = 100;

            var payment = Customer.CreatePayment(orderId, PaymentMethod.Card, amount);

            Assert.That(payment.OrderId, Is.EqualTo(orderId));
            Assert.That(payment.Amount, Is.EqualTo(amount));
            Assert.That(payment.PaymentMethod, Is.EqualTo(PaymentMethod.Card));
        }

        [Test]
        public void Test_ToString()
        {
            var customer = new Customer("Ruslan", "Teimurov", "Rus@mail.com", "12345", "Address", 30, false, true, false);

            string result = customer.ToString();

            Assert.IsTrue(result.Contains("Ruslan"));
            Assert.IsTrue(result.Contains("Teimurov"));
        }
    }
}
