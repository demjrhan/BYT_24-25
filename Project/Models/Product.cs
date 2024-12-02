using Project.Features;

namespace Project.Models
{
    public abstract class Product
    {
        private static int _lastId = 0;
        private static List<Product> Instances = new List<Product>();
        public List<Promotion> Promotions { get; set; } = new List<Promotion>();
        private string _title = null!;
        private double _price;
        private int _stockQuantity;
        public int ProductId { get; private set; }
        
       

        public Product(string title, double price, int quantity)
        {
            try
            {
                ValidateTitle(title);
                ValidatePrice(price);
                ValidateStockQuantity(quantity);

                Title = title;
                Price = price;
                StockQuantity = quantity;

                ProductId = _lastId++;
                Instances.Add(this);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize Product.", ex);
            }
        }

        private string Title 
        { 
            get => _title; 
            set
            {
                ValidateTitle(value);
                _title = value;
            }
        }

        protected internal double Price 
        { 
            get => _price; 
            set
            {
                ValidatePrice(value);
                _price = value;
            }
        }

        protected int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                ValidateStockQuantity(value);
                _stockQuantity = value;
            }
        }
        public void AddPromotion(string name, string description, double discountPercentage)
        {
            Promotion promotion = new(name, description, discountPercentage, ProductId);
            Promotions.Add(promotion);
        }

        public static void AddPromotion(int id, Promotion p)
        {
            Product? product = Instances.Find(x => (x.ProductId == id));
            if (product != null)
            {
                product.Promotions.Add(p);
            }
        }

        public void RemovePromotion(Promotion promotion)
        {
            if (promotion == null)
                throw new ArgumentNullException(nameof(promotion), "Promotion cannot be null.");
            Promotions.Remove(promotion);
            Promotion.Remove(promotion);
        }

        public double ApplyPromotion(Promotion promotion)
        {
            if (promotion == null)
                throw new ArgumentNullException(nameof(promotion), "Promotion cannot be null.");
            return Price - (Price * (promotion.DiscountPercentage / 100));
        }

        public static void PrintInstances()
        {
            foreach (var product in Instances)
            {
                Console.WriteLine(product.ToString());
            }
        }
        
        // Validation methods added seperately to maintain reusability and readability.
        private static void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
        }

        private static void ValidatePrice(double price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        }

        private static void ValidateStockQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Stock quantity cannot be negative.");
        }
        public static bool Exists(int id)
        {
            foreach (var product in Instances)
            {
                if (product.ProductId == id)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Id: {ProductId}, Type: {GetType().Name}, Title: {Title}";
        }
    }

}

