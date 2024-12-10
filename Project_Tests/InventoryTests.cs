using NUnit.Framework;
using Project.Utilities;
using System;

namespace Project_Tests
{
    [TestFixture]
    public class InventoryTests
    {
        [SetUp]
        public void Setup()
        {
            Inventory.ResetInventory();
        }

        [Test]
        public void UpdateInventory_ShouldSetLowStockFlags_WhenBelowThreshold()
        {
            Inventory.TotalBooksQuantity = 50;
            Inventory.TotalAccessoriesQuantity = 50;

            Inventory.UpdateInventory();

            Assert.IsTrue(Inventory.LowBookStock);
            Assert.IsTrue(Inventory.LowAccessoriesStock);
        }

        [Test]
        public void UpdateInventory_ShouldNotSetLowStockFlags_WhenAboveThreshold()
        {
            Inventory.TotalBooksQuantity = 150;
            Inventory.TotalAccessoriesQuantity = 150;

            Inventory.UpdateInventory();

            Assert.IsFalse(Inventory.LowBookStock);
            Assert.IsFalse(Inventory.LowAccessoriesStock);
        }

        [Test]
        public void InventoryInfo_ShouldIncreaseStock_WhenStockIsLow()
        {
            Inventory.TotalBooksQuantity = 50;
            Inventory.TotalAccessoriesQuantity = 50;

            Inventory.InventoryInfo();

            Assert.That(Inventory.TotalBooksQuantity, Is.EqualTo(150));
            Assert.That(Inventory.TotalAccessoriesQuantity, Is.EqualTo(150));
        }

        [Test]
        public void ResetInventory_ShouldSetAllFieldsToDefault()
        {
            Inventory.TotalBooksQuantity = 200;
            Inventory.TotalAccessoriesQuantity = 300;
            Inventory.LowBookStock = true;
            Inventory.LowAccessoriesStock = true;
            Inventory.SupplierInfo = "Supplier A";

            Inventory.ResetInventory();

            Assert.That(Inventory.TotalBooksQuantity, Is.EqualTo(0));
            Assert.That(Inventory.TotalAccessoriesQuantity, Is.EqualTo(0));
            Assert.IsFalse(Inventory.LowBookStock);
            Assert.IsFalse(Inventory.LowAccessoriesStock);
            Assert.IsNull(Inventory.SupplierInfo);
        }
    }
}
