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
            LowBookStock = TotalBooksQuantity < 100;
            LowAccessoriesStock = TotalAccessoriesQuantity < 100;
        }

        public static void InventoryInfo()
        {
            Console.WriteLine("Books - " + TotalBooksQuantity);
            Console.WriteLine(LowBookStock? "Too less books, ordering new ones." : "");
            TotalBooksQuantity += 100;
            Console.WriteLine("Accessories - " + TotalAccessoriesQuantity);
            Console.WriteLine(LowBookStock? "Too less accessories, ordering new ones." : "");
            TotalAccessoriesQuantity += 100;
        }

        public static void ResetInventory()
        {
            TotalAccessoriesQuantity = 0;
            TotalBooksQuantity = 0;
            LowAccessoriesStock = false;
            LowBookStock = false;
            SupplierInfo = null;
        }
    }

}

