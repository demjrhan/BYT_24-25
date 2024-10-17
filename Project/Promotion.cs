namespace Project;

public class Promotion
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int DiscountPercentage { get; set; }

    public Promotion(string name, string description, int discountPercentage)
    {
        Name = name;
        Description = description;
        DiscountPercentage = discountPercentage;
    }
}
