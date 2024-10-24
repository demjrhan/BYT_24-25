using System.Reflection;

namespace Project
{
    public abstract class Product(string title, double price, int quantity)
    {
        private static int _lastProductId = 0;
        public static List<Type> Types { get; set; } = [];

        public int ProductId { get; private set; } = _lastProductId++;
        public string Title { get; set; } = title ?? throw new ArgumentNullException(nameof(title));
        public double Price { get; set; } = price;
        public int StockQuantity { get; set; } = quantity;
        public List<Promotion> Promotions = [];

        public void AddPromotion(Promotion promotion)
        {
            if (promotion != null)
            {
                Promotions.Add(promotion);
            }
        }

        public void RemovePromotion(Promotion promotion)
        {
            if (promotion != null)
            {
                Promotions.Remove(promotion);
            }
        }

        public double ApplyPromotion(Promotion promotion)
        {
            double finalPrice = Price;
            if (promotion != null) finalPrice -= finalPrice * (promotion.DiscountPercentage / 100);
            return finalPrice;
        }

        public static List<T>? GetProductsOfType<T>(Type productType)
        {
            var products = productType.GetField("Products", BindingFlags.Static | BindingFlags.Public);
            Console.WriteLine("here " + products);

            if (products != null)
            {
                return products.GetValue(null) as List<T>;
            }

            return null;
        }

        public override string ToString()
        {
            return "Id: " + ProductId + " type: " + this.GetType() + " title: " + Title;
        }
    }

}

