﻿namespace Project
{
    public class Accessory : Product
    {
        public static readonly string _verbose = "accessory";
        public static readonly string _verbosePlural = "accessories";
        public string ProductClass { get; set; } = typeof(Accessory).Name;

        public string Type { get; set; }
        public string Material { get; set; }
        public static List<Accessory> Products = [];

        static Accessory()
        {
            Types.Add(typeof(Accessory));
        }

        public Accessory(
            string title, double price, 
            int stockQuantity, string type, 
            string material
            ) : base(
                title, price,
                stockQuantity
                )
        {
            Type = type;
            Material = material;
            Products.Add(this);
            Inventory.TotalAccessoriesQuantity += StockQuantity;
            Inventory.UpdateInventory();
        }
        public override string ToString()
        {
            return base.ToString() + " Type: " + Type + " material: " + Material;
        }
    }

}

