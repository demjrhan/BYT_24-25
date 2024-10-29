using System.Reflection;

namespace Project
{
    public abstract class Product
    {
        private static int _lastId = 0;
        private static List<Product> Instances = [];

        private string? _title;
        private double _price;
        private int _stockQuantity;

        public int ProductId { get; private set; } = _lastId++;
        public string? Title 
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

        public Product(string title, double price, int quantity)
        {
            Title = title;
            Price = price;
            StockQuantity = quantity;

            Instances.Add(this);
        }

        public void AddPromotion(string name, string description, double discountPercentage)
        {
            Promotion promotion = new(name, description, discountPercentage, ProductId);
        }

        public void RemovePromotion(Promotion promotion)
        {
            if (promotion != null)
            {
                Promotions.Remove(promotion);
                Promotion.GetInstances().Remove(promotion);
            }
        }

        public double ApplyPromotion(Promotion promotion)
        {
            double finalPrice = Price;
            if (promotion != null) finalPrice -= finalPrice * (promotion.DiscountPercentage / 100);
            return finalPrice;
        }

        protected internal static List<Product> GetInstances()
        {
            return Instances;
        }

        public override string ToString()
        {
            return "Id: " + ProductId + " type: " + this.GetType() + " title: " + Title;
        }
    }

}

