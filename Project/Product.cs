namespace Project
{
    public abstract class Product
    {
        private static int _lastProductId = 0;

        public int ProductId { get; private set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public List<Promotion> Promotions = new List<Promotion>();

        public Product(string title, double price, int quantity, string description = null)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Price = price;
            StockQuantity = quantity;
            Description = description; // optional description.
            ProductId = _lastProductId++;
        }

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

        public override string ToString()
        {
            return "Id: " + ProductId + " type: " + this.GetType() + " title: " + Title;
        }
    }

}

