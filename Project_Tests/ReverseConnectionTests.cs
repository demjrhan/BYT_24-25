using Project.Enum;

namespace Project_Tests;

using System;
using System.Collections.Generic;
using Project.Entities;
using Project.Models;
using NUnit.Framework;

[TestFixture]
public class ReverseConnectionTests
{
    [Test]
    public void TestCustomerOrderReverseConnection()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "1234567", "123 Street", 30, false, true,
            false);
        var order = new Order(customer, DateTime.Now, OrderStatus.Proccessing, 100.0, new List<Product>());


        Assert.Contains(order, customer.GetOrders());
        Assert.AreEqual(customer.CustomerId, order.CustomerId);
    }

    [Test]
    public void TestCustomerCartReverseConnection()
    {
        var customer = new Customer("Jane", "Smith", "jane.smith@example.com", "9876543", "456 Avenue", 25, true, false,
            false);

        var cart = customer.Cart;

        Assert.IsNotNull(cart);
        Assert.AreEqual(customer.CustomerId, cart.CustomerId);
    }

    [Test]
    public void TestOrderProductReverseConnection()
    {
        var product = new TestProduct("The Great Gatsby", 20.0, 50);
        var customer = new Customer("Alice", "Brown", "alice.brown@example.com", "1112222", "789 Road", 40, false, true,
            false);
        var order = new Order(customer, DateTime.Now, OrderStatus.Proccessing, 19.99, new List<Product> { product });

        Assert.Contains(product, order.Products);
    }

    [Test]
    public void TestRemoveCustomerOrderConnection()
    {
        var customer = new Customer("Mark", "Green", "mark.green@example.com", "4445556", "321 Boulevard", 35, false,
            true, false);
        var order = new Order(customer, DateTime.Now, OrderStatus.Arrived, 200.0, new List<Product>());

        customer.RemoveOrder(order);

        Assert.IsFalse(customer.GetOrders().Contains(order));
        Assert.IsTrue(Order.Exists(order.OrderId));
    }
}