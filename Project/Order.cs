namespace Project
{
    public class Order
    {
        public static int LastOrderId = 0;

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public int? ShippingId { get; set; }
        public List<Product> Products;

        public Order(
            int customerId, DateTime orderDate,
            string status,
            double amount, List<Product> products
            )
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            Amount = amount;
            Status = status;
            Products = products;
            OrderId = LastOrderId++;
        }

        public Shipping CreateShipping(string method, double cost, string address)
        {
            Shipping shipping = new Shipping(OrderId, method, cost, address);
            Amount += cost;
            ShippingId = shipping.ShippingId;
            return shipping;
        }
    }

}

