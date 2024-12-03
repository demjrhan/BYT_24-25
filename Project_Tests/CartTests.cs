using NUnit.Framework;
using Project.Models;
using Project.Entities;
using System;
using Project.Features;

namespace Project_Tests
{
    

    [TestFixture]
    public class CartTests
    {
        [SetUp]
        public void Setup()
        {
            Customer.Instances.Clear();
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

            Assert.IsFalse(Customer.Instances.Contains(customer));
            Assert.IsFalse(Cart.Instances.Contains(cart));
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
