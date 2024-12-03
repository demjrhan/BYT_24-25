
using Project.Models;
using Project.Entities;
using Project.Features;

namespace Project_Tests
{
    

    [TestFixture]
    public class CartTests
    {
        [SetUp]
        public void Setup()
        {
            Customer.ClearInstances();
        }

        [Test]
        public void CartCreation_ShouldBeLinkedToCustomer()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var cart = customer.Cart;

            Assert.IsNotNull(cart);
            Assert.That(cart.CustomerId, Is.EqualTo(customer.CustomerId));
            Assert.That(cart.Customer, Is.EqualTo(customer));

        }
        
        [Test]
        public void Cart_ShouldNotBeAssignedToMultipleCustomers()
        {
            var customer1 = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var customer2 = new Customer("Jane", "Smith", "jane.smith@example.com", "987654321", "456 Maple St", 28, false, true, false);

            var cart = customer1.Cart;
    
            Assert.Throws<InvalidOperationException>(() => customer2.Cart = cart);
        }


        [Test]
        public void AddProductToCart_ShouldIncreaseProductCount()
        {
            var customer = new Customer("Jane", "Doe", "jane.doe@example.com", "987654321", "456 Oak St", 25, false, true, false);
            var product = new TestProduct("Book", 10.0, 5);

            customer.Cart.Products.Add(new Tuple<Product, Promotion?>(product, null));

            Assert.That(customer.Cart.Products.Count, Is.EqualTo(1));
            Assert.That(customer.Cart.Products[0].Item1, Is.EqualTo(product));
        }
      

        
        [Test]
        public void RemoveProductFromCart_ShouldDecreaseProductCount()
        {
            var customer = new Customer("Jane", "Doe", "jane.doe@example.com", "987654321", "456 Oak St", 25, false, true, false);
            var product = new TestProduct("Book", 10.0, 5);
            customer.Cart.Products.Add(new Tuple<Product, Promotion?>(product, null));

            customer.Cart.Products.RemoveAt(0);

            Assert.That(customer.Cart.Products.Count, Is.EqualTo(0));
        }

        [Test]
        public void CartDeletion_WhenCustomerDeleted_ShouldRemoveCart()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var cart = customer.Cart;

            Customer.RemoveCustomer(customer);

            Assert.IsFalse(Customer.GetInstances().Contains(customer));
            Assert.IsFalse(Cart.Instances.Contains(cart));
        }

        [Test]
        public void CartDeletion_ShouldRemoveAssociatedProducts()
        {
            var customer = new Customer("Max", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var product = new TestProduct("Book", 10.0, 5);
            var product2 = new TestProduct("Book2", 15.0, 15);
            customer.Cart.AddProduct(product);
            customer.Cart.AddProduct(product2);
            
            Cart.RemoveCart(customer.Cart);
            Assert.IsEmpty(customer.Cart.Products, "Cart products should be empty after deletion.");
        }

        [Test]
        public void AddNullProductToCart_ShouldThrowException()
        {
            var customer = new Customer("Jane", "Doe", "jane.doe@example.com", "987654321", "456 Oak St", 25, false, true, false);

            Assert.Throws<ArgumentNullException>(() =>
            {
                customer.Cart.AddProduct(null);
            });
        }
    }
}
