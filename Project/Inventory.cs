namespace Project
{
    public static class Inventory
    {
        public static int TotalBooksQuantity { get; set; } = 0;
        public static int TotalAccessoriesQuantity { get; set; } = 0;
        public static bool LowBookStock { get; set; } = false;
        public static bool LowAccessoriesStock { get; set; } = false;
        public static string? SupplierInfo { get; set; }

        public static void UpdateInventory()
        {
            LowBookStock = TotalBooksQuantity < 100 ? true : false;
            LowAccessoriesStock = TotalAccessoriesQuantity < 100 ? true : false;
        }

        public static void InventoryInfo()
        {
            Console.WriteLine("Books - " + TotalBooksQuantity);
            Console.WriteLine("Accessories - " + TotalAccessoriesQuantity);
        }
    }

}

