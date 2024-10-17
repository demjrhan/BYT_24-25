namespace Project;

public class Shipping
{
    public string ShippingId { get; set; }
    public string ShippingMethod { get; set; }
    public double ShippingCost { get; set; }
    public string ShippingAddress { get; set; }

    public Shipping(string shippingId, string shippingMethod, double shippingCost, string shippingAddress)
    {
        ShippingId = shippingId;
        ShippingMethod = shippingMethod;
        ShippingCost = shippingCost;
        ShippingAddress = shippingAddress;
    }
}
