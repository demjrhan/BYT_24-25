namespace Project;

public class Product
{
    
    public string ProductId { get; private set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    
    public List<Promotion> Promotions { get; set; }

    public Product(string productId, string title, double price, int stockQuantity, string description = null)
    {
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Price = price;
        StockQuantity = stockQuantity;
        Description = description; // optional description.
        Promotions = new List<Promotion>();
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
    
    public double GetFinalPrice() 
    {
        double finalPrice = Price;
        foreach (var promotion in Promotions)
        {
            finalPrice -= finalPrice * (promotion.DiscountPercentage / 100);
        }
        return finalPrice;
    }
}

