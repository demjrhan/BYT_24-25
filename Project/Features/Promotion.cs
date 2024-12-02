using Project.Models;

namespace Project.Features
{
    public class Promotion
    {
        private static int _lastId = 0;
        private static List<Promotion> Instances = new List<Promotion>();

        private int _productId;
        private string _name = null!;
        private string? _description;
        private double _discountPercentage;
        public int PromotionId { get; private set; } = _lastId++;

        public Promotion(string name, string description, double discountPercentage, int productId)
        {
            
            ValidateProductExists(productId);
            ValidateDiscountPercentage(discountPercentage);
            Name = name;
            Description = description;
            DiscountPercentage = discountPercentage;
            ProductId = productId;

            Product.AddPromotion(productId, this);

            Instances.Add(this);
        }
        // Validation methods added seperately to maintain reusability and readability.
        private static void ValidateProductExists(int productId)
        {
            if (!Product.Exists(productId))
                throw new ArgumentException($"Product with ID {productId} does not exist.");
        }

        private static void ValidateDiscountPercentage(double discountPercentage)
        {
            if (discountPercentage < 0)
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount Percentage cannot be negative.");
        }

        public int ProductId
        {
            get => _productId;
            set
            {
                ValidateProductExists(value);
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
                ValidateDiscountPercentage(value);
                _discountPercentage = value;
            }
        }
        public static void PrintInstances()
        {
            foreach (var promotion in Instances)
            {
                Console.WriteLine(promotion.ToString());
            }
        }

        public static void Remove(Promotion promotion)
        {
            Instances.Remove(promotion);
        }

        public override string ToString()
        {
            return "Promotion number: " + PromotionId + " Discount: " + DiscountPercentage;
        }
    }

}

