namespace Project
{
    public class Promotion
    {
        private static int _lastId = 0;
        private static List<Promotion> Instances = [];

        private int _productId;
        private string? _name;
        private string? _description;
        private double _discountPercentage;

        public int PromotionId { get; private set; } = _lastId++;
        public int ProductId
        {
            get => _productId;
            set
            {
                if (!Product.GetInstances().Exists(p => p.ProductId == value))
                    throw new ArgumentException("Product ID does not exist.");
                _productId = value;
            }
        }
        public string? Name
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

            foreach (var product in Product.GetInstances())
            {
                if (productId == product.ProductId)
                    product.Promotions.Add(this); break;
            }
           
            Instances.Add(this);
        }

        public static List<Promotion> GetInstances()
        {
            return Instances;
        }

        public override string ToString()
        {
            return "Promotion number: " + PromotionId + " Discount: " + DiscountPercentage;
        }
    }

}

