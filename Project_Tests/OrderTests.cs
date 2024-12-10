using NUnit.Framework;
using Project.Models;
using Project.Entities;
using Project.Enum;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Project_Tests
{
    [TestFixture]
    public class OrderTests
    {
        [SetUp]
        public void Setup()
        {
            Customer.ClearInstances();
           
        }

        [Test]
        public void OrderCreation_ShouldInitializeCorrectly()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var products = new List<Product> { new TestProduct("Fancy Book", 55.0, 2) };
            var order = new Order(customer, DateTime.Now, OrderStatus.Proccessing, 155.0, products);

            Assert.That(order.CustomerId, Is.EqualTo(customer.CustomerId));
            Assert.That(order.Status, Is.EqualTo(OrderStatus.Proccessing));
            Assert.That(order.Amount, Is.EqualTo(155.0));
            Assert.That(order.Products.Count, Is.EqualTo(1));
            Assert.IsTrue(Order.Exists(order.OrderId));
        }
        
        [Test]
        public void RemoveProductFromCart_ShouldRemoveReverseConnection()
        {
            
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var product = new TestProduct("Book2", 15.0, 15);
            customer.Cart.AddProduct(product);

            customer.Cart.RemoveProduct(product);

            var cartsField = typeof(Product).GetField("_carts", BindingFlags.Instance | BindingFlags.NonPublic);
            var carts = cartsField!.GetValue(product) as List<Cart>;

            Assert.That(customer.Cart.Count, Is.EqualTo(0));
            Assert.IsEmpty(carts!);            
        }

        //maybe change that as we dont use indices here now
        [Test]
        public void OrderCreation_InvalidCustomer_ShouldThrowException()
        {
            var products = new List<Product> { new TestProduct("Fancy Book", 55.0, 2) };
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                new Order(null!, DateTime.Now, OrderStatus.Proccessing, 155.0, products);
            });

            Assert.That(ex.InnerException, Is.TypeOf<NullReferenceException>());
            Assert.That(ex.InnerException.Message, Does.Contain("Object reference not set to an instance of an object."));
        }


        [Test]
        public void OrderCreation_InvalidOrderDate_ShouldThrowException()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var products = new List<Product> { new TestProduct("Fancy Book", 55.0, 2) };
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                new Order(customer, DateTime.Now.AddDays(1), OrderStatus.Proccessing, 155.0, products);
            });

            Assert.That(ex.InnerException, Is.TypeOf<ArgumentException>());
            Assert.That(ex.InnerException.Message, Does.Contain("Order date cannot be set to a future time."));
        }

        [Test]
        public void OrderCreation_InvalidAmount_ShouldThrowException()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var products = new List<Product> { new TestProduct("Fancy Book", 55.0, 2) };
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                new Order(customer, DateTime.Now, OrderStatus.Proccessing, -155.0, products);
            });

            Assert.That(ex.InnerException, Is.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(ex.InnerException.Message, Does.Contain("Amount cannot be negative"));
        }

        
        [Test]
        public void OrderCreation_NullProducts_ShouldThrowException()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                new Order(customer, DateTime.Now, OrderStatus.Proccessing, 155.0, null!);
            });

            Assert.That(ex.InnerException, Is.TypeOf<ArgumentNullException>());
            Assert.That(ex.InnerException.Message, Does.Contain("Products list cannot be null"));
        }


        [Test]
        public void OrderStatusTransition_ShouldChangeStatusCorrectly()
        {
            var customer = new Customer("Jane", "Doe", "jane.doe@example.com", "987654321", "456 Oak St", 25, false, true, false);
            var products = new List<Product> { new TestProduct("Fancy Book", 55.0, 2) };
            var order = new Order(customer, DateTime.Now, OrderStatus.Proccessing, 155.0, products);

            order.Status = OrderStatus.Departed;
            Assert.That(order.Status, Is.EqualTo(OrderStatus.Departed));

            order.Status = OrderStatus.Arrived;
            Assert.That(order.Status, Is.EqualTo(OrderStatus.Arrived));
        }
    }
}
