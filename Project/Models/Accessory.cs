using Project.Enum;
using Project.Utilities;

namespace Project.Models
{
    public class Accessory : Product
    {
        private static readonly List<Accessory> Instances = new List<Accessory>();

        private string _accessoryType = null!;
        private MaterialType Material { get; set; }

        private string AccessoryType
        {
            get => _accessoryType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Accessory type cannot be null or empty.");
                _accessoryType = value;
            }
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
            try
            {
                ValidateAccessoryType(type);

                Material = material;
                AccessoryType = type;

                UpdateInventory();
                Instances.Add(this);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize Accessory.", ex);
            }
        }
        
        public static bool Exists(Accessory givenAccessory)
        {
            foreach (var accessory in Instances)
            {
                if (accessory == givenAccessory)
                    return true;
            }
            return false;
        }
        // Extra validations to maintain safety of accessory type. In case of "" input etc.
        private static void ValidateAccessoryType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Accessory type cannot be null or empty.", nameof(type));
        }

        
        // Added validation of stock quantity, new accessory's quantity can not be lower than 0.
        private void UpdateInventory()
        {
            if (StockQuantity < 0)
                throw new InvalidOperationException("Stock quantity cannot be negative.");

            Inventory.TotalAccessoriesQuantity += StockQuantity;
            Inventory.UpdateInventory();
        }
        public new static void PrintInstances()
        {
            foreach (var accessory in Instances)
            {
                Console.WriteLine(accessory.ToString());
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Type: {AccessoryType}, Material: {Material}";
        }

    }

}

