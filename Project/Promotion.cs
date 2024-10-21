namespace Project
{
    public class Promotion
    {
        private static int _lastPromotionId = 0;

        public int PromotionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DiscountPercentage { get; set; }

        public Promotion(string name, string description, int discountPercentage)
        {
            Name = name;
            Description = description;
            DiscountPercentage = discountPercentage;
            PromotionId = _lastPromotionId++;
        }

        public override string ToString()
        {
            return "Promotion number: " + PromotionId + " Discount: " + DiscountPercentage;
        }
    }

}

