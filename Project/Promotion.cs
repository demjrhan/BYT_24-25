namespace Project
{
    public class Promotion
    {
        private static int _lastId = 0;
        private static List<Promotion> Instances = [];

        private int _productId;
        private string _name = null!;
        private string? _description;
        private double _discountPercentage;

        public int PromotionId { get; private set; } = _lastId++;
        public int ProductId
        {
            get => _productId;
            set
            {
                if (!Product.Exists(value))
                    throw new ArgumentException("Product ID does not exist.");
                _productId = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.");
                _name = value;
            }
        }
        public string? Description
        {
            get => _description;
            set
            {
                if (value != null && value.Equals(""))
                    throw new ArgumentException("Description cannot be empty.");
                _description = value;
            }
        }
        public double DiscountPercentage
        {
            get => _discountPercentage;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Discount Percentage cannot be negative.");
                _discountPercentage = value;
            }
        }

        public Promotion(string name, string description, double discountPercentage, int productId)
        {
            Name = name;
            Description = description;
            DiscountPercentage = discountPercentage;
            ProductId = productId;

            Product.AddPromotion(productId, this);

            Instances.Add(this);
        }

        public static void GetInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static void Remove(Promotion p)
        {
            Instances.Remove(p);
        }

        public override string ToString()
        {
            return "Promotion number: " + PromotionId + " Discount: " + DiscountPercentage;
        }
    }

}

