using NUnit.Framework;
using Project.Entities;
using Project.Models;
using Project.Features;
using System;
using System.Reflection;

namespace Project_Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [SetUp]
        public void Setup()
        {
            Customer.ClearInstances();
            Membership.ClearInstances();
        }

        [Test]
        public void CustomerCreation_ShouldInitializeWithCartAndBeAddedToInstances()
        {
            string name = "John";
            string surname = "Doe";
            string email = "john.doe@example.com";
            string phone = "123456789";
            string address = "123 Elm St";
            int age = 30;
            bool isStudying = false;
            bool isWorking = true;
            bool isRetired = false;

            var customer = new Customer(name, surname, email, phone, address, age, isStudying, isWorking, isRetired);

            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.Cart);
            Assert.IsTrue(Customer.GetInstances().Contains(customer));
        }

        [Test]
        public void CartDeletion_WhenCustomerDeleted_ShouldRemoveAssociatedCart()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var cart = customer.Cart;

            Customer.RemoveCustomer(customer);

            Assert.IsFalse(Customer.GetInstances().Contains(customer));
            Assert.IsFalse(Cart.GetInstances().Contains(cart));
        }

        [Test]
        public void MembershipCreation_ShouldLinkToCustomer()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var membership = new Membership(customer, true, 15.0);

            Assert.IsTrue(customer.IsMember());
            Assert.That(membership.CustomerId, Is.EqualTo(customer.CustomerId));
        }

        [Test]
        public void MembershipDeletion_WhenCustomerDeleted_ShouldRemoveAssociatedMembership()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var membership = new Membership(customer, true, 15.0);

            Customer.RemoveCustomer(customer);

            Assert.IsFalse(Customer.GetInstances().Contains(customer));
            Assert.IsFalse(Membership.GetInstances().Contains(membership)); 
        }

        [Test]
        public void CustomerInstances_ShouldBeEncapsulated()
        {
            
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var instances = Customer.GetInstances();
            
            Assert.IsTrue(instances.Contains(customer));
            Assert.Throws<InvalidCastException>(() =>
            {
                var modifiableList = (List<Customer>)instances; 
                modifiableList.Clear(); 
            });

            Assert.IsTrue(Customer.GetInstances().Contains(customer));
            Customer.ClearInstances();
            Assert.IsEmpty(Customer.GetInstances());
        }


        [Test]
        public void ModifyMembership_ShouldUpdateReverseConnection()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", 30, false, true, false);
            var oldMembership = new Membership(customer, true, 10.0);
            var newMembership = new Membership(customer, true, 20.0);

            customer.SetMembership(newMembership);

            var membershipField = typeof(Customer).GetField("_membership", BindingFlags.Instance | BindingFlags.NonPublic);
            var membership = membershipField!.GetValue(customer);

            Assert.That(membership, Is.EqualTo(newMembership));
            Assert.That(membership, Is.Not.EqualTo(oldMembership));
        }

        [Test]
        public void CustomerCreation_InvalidAge_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Customer("John", "Doe", "john.doe@example.com", "123456789", "123 Elm St", -5, false, true, false);
            });
        }

        [Test]
        public void MembershipAssignment_InvalidCustomer_ShouldThrowException()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                new Membership(null!, true, 10.0);
            });
        }
    }
}
