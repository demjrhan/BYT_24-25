namespace Project
{
    public class Shipping
    {
        private static int _lastShippingId = 0;

        public int ShippingId { get; set; }
        public int OrderId { get; set; }
        public string Method { get; set; }
        public double Cost { get; set; }
        public string Address { get; set; }

        public Shipping(int orderId, string method, double cost, string address)
        {
            OrderId = orderId;
            Method = method;
            Cost = cost;
            Address = address;
            ShippingId = _lastShippingId++;
        }
    }

}

