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
        public static readonly string _verbose = "accessory";
        public static readonly string _verbosePlural = "accessories";
        public string ProductClass { get; set; } = typeof(Accessory).Name;

        public string Type { get; set; }
        public MaterialType MaterialType { get; set; }
        public static List<Accessory> Products = [];

        static Accessory()
        {
            Types.Add(typeof(Accessory));
        }

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
            MaterialType = material;
            Products.Add(this);
            Inventory.TotalAccessoriesQuantity += StockQuantity;
            Inventory.UpdateInventory();
        }

        public override string ToString()
        {
            return base.ToString() + " Type: " + Type + " material: " + MaterialType;
        }
    }
}

