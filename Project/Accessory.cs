namespace Project;

public class Accessory : Product 
{
    public string Type { get; set; } 
    public string Material { get; set; } 

    public Accessory(string productId, string title, double price, int stockQuantity, string type, string material)
        : base(productId, title, price, stockQuantity)
    {
        Type = type; 
        Material = material; 
    }
}
