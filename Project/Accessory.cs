namespace Project
{
    public class Accessory : Product
    {
        public string Type { get; set; }
        public string Material { get; set; }

        public Accessory(
            string title,
            double price, int stockQuantity,
            string type, string material
            ) : base(
                title,
                price, stockQuantity
                )
        {
            Type = type;
            Material = material;
            Inventory.TotalAccessoriesQuantity += StockQuantity;
            Inventory.UpdateInventory();
        }
        public override string ToString()
        {
            return base.ToString() + " Type: " + Type + " material: " + Material;
        }
    }

}

