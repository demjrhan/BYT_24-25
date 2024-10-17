namespace Project;

public class Cart
{
    public double TotalSum { get; private set; }
    private List<Product> Products { get; set; } = new List<Product>();

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        Products.Remove(product);
    }

    public double CalculateTotalSum(Person person)
    {
        double discount = person.GetDiscountPercentage(); 
        TotalSum = 0;
        foreach (var product in Products)
        {
            TotalSum += product.GetFinalPrice();;
        }

        if (discount > 0)
        {
            TotalSum -= TotalSum * (discount / 100);
        }

        return TotalSum; 
    }
    
}
