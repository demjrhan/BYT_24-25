﻿namespace Project
{
    public enum MaterialType
    {
        Metal,
        Wood,
        Plastic,
        Gold,
        Leather
    }

    public class Accessory : Product
    {
        private static List<Accessory> Instances = [];

        //public static readonly string _verbose = "Accessory";
        //public static readonly string _verbosePlural = "Accessories";

        //We need to change this name it can be confused with MaterialType or smth
        private string? _type;

        public string? Type
        {
            get => _type;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Type cannot be null or empty.");
                _type = value;
            }
        }
        public MaterialType Material { get; set; }

        public Accessory(
            string title, double price, 
            int stockQuantity, string type, 
            MaterialType material
            ) : base(
                title, price,
                stockQuantity
                )
        {
            Type = type;
            Material = material;
            
            Inventory.TotalAccessoriesQuantity += StockQuantity;
            Inventory.UpdateInventory();

            Instances.Add(this);
        }

        protected internal static new List<Accessory> GetInstances()
        {
            return Instances;
        }

        public override string ToString()
        {
            return base.ToString() + " Type: " + Type + " material: " + Material;
        }
    }

}

