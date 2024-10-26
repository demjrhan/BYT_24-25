namespace Project
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
        public string Type { get; set; }
        public MaterialType MaterialType { get; set; } 

        public Accessory(
            string title,
            double price, int stockQuantity,
            string type, MaterialType material
            ) : base(
                title,
                price, stockQuantity
                )
        {
            Type = type;
            MaterialType = material;
            Inventory.TotalAccessoriesQuantity += StockQuantity;
            Inventory.UpdateInventory();
        }
        public override string ToString()
        {
            return base.ToString() + " Type: " + Type + " material: " + MaterialType;
        }
    }

}

