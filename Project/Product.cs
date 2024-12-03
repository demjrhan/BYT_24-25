namespace Project
{
    public abstract class Product
    {
        private static int _lastId = 0;
        private static List<Product> Instances = [];

        private string _title = null!;
        private double _price;
        private int _stockQuantity;

        public int ProductId { get; private set; } = _lastId++;
        public string Title 
        { 
            get => _title; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be null or empty.");
                _title = value;
            }
        }
        public double Price 
        { 
            get => _price; 
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative.");
                _price = value;
            }
        }
        public int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Stock quantity cannot be negative.");
                _stockQuantity = value;
            }
        }
        public List<Promotion> Promotions { get; set; } = [];
        private List<Review> Reviews { get; set; } = [];

        public Product(string title, double price, int quantity)
        {
            Title = title;
            Price = price;
            StockQuantity = quantity;

            Instances.Add(this);
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
            return new Review(customerId, rating, this, comment);
        }

        public Promotion AddPromotion(string name, string description, double discountPercentage)
        {
            return new Promotion(name, description, discountPercentage, this);
        }

        public void RemovePromotion(Promotion promotion)
        {
            if (promotion != null)
            {
                Promotions.Remove(promotion);
                Promotion.RemovePromotion(promotion);
            }
        }

        public void RemoveReview(Review review)
        {
            Reviews.Remove(review);
        }

        public double ApplyPromotion(Promotion promotion)
        {
            double finalPrice = Price;
            if (promotion != null) finalPrice -= finalPrice * (promotion.DiscountPercentage / 100);
            return finalPrice;
        }

        public static void PrintInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static bool Exists(int id)
        {
            bool flag = false;
            try
            {
                Product? o = Instances.Find(x => (x.ProductId == id));
                if (o != null) flag = true;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return flag;
        }

        public static void RemoveProduct(Product product)
        {
            Instances.Remove(product);
            foreach (var i in product.Reviews)
            {
                Review.RemoveReview(i);
            }
            foreach (var i in product.Promotions)
            {
                Promotion.RemovePromotion(i);
            }
        }

        public override string ToString()
        {
            return "Id: " + ProductId + " type: " + this.GetType() + " title: " + Title;
        }
    }

}

