
using Project;
using Project.Utilities;

namespace Project_Tests
{
    public class InventoryTests
    {
        [Test]
        public void UpdateInventory_LowStock()
        {
            Inventory.TotalBooksQuantity = 50;
            Inventory.TotalAccessoriesQuantity = 50;

            Inventory.UpdateInventory();

            Assert.IsTrue(Inventory.LowBookStock);
            Assert.IsTrue(Inventory.LowAccessoriesStock);
        }

        [Test]
        public void UpdateInventory()
        {
            Inventory.TotalBooksQuantity = 150;
            Inventory.TotalAccessoriesQuantity = 150;

            Inventory.UpdateInventory();

            Assert.IsFalse(Inventory.LowBookStock);
            Assert.IsFalse(Inventory.LowAccessoriesStock);
        }
    }
}
