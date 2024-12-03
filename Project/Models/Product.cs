using Project.Features;

namespace Project.Models
{
    public abstract class Product
    {
        private static int _lastId = 0;
        private static List<Product> Instances = new List<Product>();
        public List<Promotion> Promotions { get; set; } = new List<Promotion>();
        public List<Review> Reviews { get; set; } = new List<Review>();

        private string _title = null!;
        private double _price;
        private int _stockQuantity;
        public int ProductId { get; private set; }  

        public Product(string title, double price, int quantity)
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

        public string Title 
        { 
            get => _title; 
            set
            {
                ValidateTitle(value);
                _title = value;
            }
        }

        public double Price 
        { 
            get => _price; 
            set
            {
                ValidatePrice(value);
                _price = value;
            }
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                ValidateStockQuantity(value);
                _stockQuantity = value;
            }
        }

        public static void AddReviewToProduct(Product product, Review review)
        {
            product?.Reviews.Add(review);
        }

        public static void AddPromotionToProduct(Product product, Promotion promotion)
        {
            product?.Promotions.Add(promotion);
        }
        
        public Review AddReview(int customerId, int rating, string? comment)
        {
            return new Review(customerId, this, rating, comment);
        }
        public Promotion AddPromotion(string name, string description, double discountPercentage)
        {
            return new Promotion(name, description, discountPercentage, this);
        }

        public void RemovePromotion(Promotion promotion)
        {
            if (promotion == null)
                throw new ArgumentNullException(nameof(promotion), "Promotion cannot be null.");
            Promotions.Remove(promotion);
            Promotion.RemovePromotion(promotion);
        }

        public void RemoveReview(Review review)
        {
            this.Reviews.Remove(review);
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

        public void RemoveProduct()
        {
            foreach(var review in Reviews)
            {
                Review.RemoveReview(review);
            }
            foreach (var promotion in Promotions)
            {
                Promotion.RemovePromotion(promotion);
            }

            Instances.Remove(this);
        }

        public override string ToString()
        {
            return $"Id: {ProductId}, Type: {GetType().Name}, Title: {Title}";
        }
    }

}

