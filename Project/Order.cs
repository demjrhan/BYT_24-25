namespace Project;

public class Order
{
    public string OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }

    public Order(string orderId, DateTime orderDate, string status)
    {
        OrderID = orderId;
        OrderDate = orderDate;
        Status = status;
    }
}
