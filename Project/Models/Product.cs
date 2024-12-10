using Project.Features;

namespace Project.Models
{
    public abstract class Product
    {
        private static int _lastId = 0;
        private static List<Product> Instances = new List<Product>();
        private List<Promotion> _promotions = new List<Promotion>();
        private List<Review> _reviews = new List<Review>();
        private List<Cart> _carts = new List<Cart>();

        private string _title = null!;
        private double _price;
        private int _stockQuantity;
        public int ProductId { get; private set; }  

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
        public static IReadOnlyList<Product> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Product givenProduct)
        {
            foreach (var product in Instances)
            {
                if (product == givenProduct)
                    return true;
            }
            return false;
        }
        public static void AddReviewToProduct(Product product, Review review)
        {
            product?._reviews.Add(review);
        }

        public static void AddPromotionToProduct(Product product, Promotion promotion)
        {
            product?._promotions.Add(promotion);
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
            _promotions.Remove(promotion);
            Promotion.RemovePromotion(promotion);
        }

        public void RemoveReview(Review review)
        {
            this._reviews.Remove(review);
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

        public void AddCart(Cart cart)
        {
            _carts.Add(cart);
        }

        public void RemoveCart(Cart cart)
        {
            _carts.Remove(cart);
        }

        public void RemoveProduct()
        {
            foreach(var review in _reviews)
            {
                Review.RemoveReview(review);
            }
            foreach (var promotion in _promotions)
            {
                Promotion.RemovePromotion(promotion);
            }
            foreach (var cart in _carts)
            {
                cart.RemoveProduct(this);
            }

            Instances.Remove(this);
        }

        public bool ContainPromotion(Promotion promotion)
        {
            return this._promotions.Contains(promotion);
        }

        public int CountReviews()
        {
            return _reviews.Count;
        }

        public int CountPromotions()
        {
            return _promotions.Count;
        }

        public override string ToString()
        {
            return $"Id: {ProductId}, Type: {GetType().Name}, Title: {Title}";
        }
    }

}

