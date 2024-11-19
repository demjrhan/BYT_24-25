namespace Project
{
    public class Order
    {
        private static int _lastId = 0;
        private static List<Order> Instances = [];

        private int _customerId;
        private int? _shippingId;
        private DateTime _orderDate;
        private double _amount;
        private List<Product> _products = [];


        public int OrderId { get; set; } = _lastId++;
        public int CustomerId
        {
            get => _customerId;
            set
            {
                if (!Customer.Exists(value))
                    throw new ArgumentException("Customer ID does not exist.");
                _customerId = value;
            }
        }
        public int? ShippingId
        {
            get => _shippingId;
            set
            {
                if (value != null && !Shipping.Exists((int) value))
                    throw new ArgumentException("Shipping ID does not exist.");
                _shippingId = value;
            }
        }
        public DateTime OrderDate
        {
            get => _orderDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Date cannot be set to a future time.", nameof(value));
                _orderDate = value;
            }
        }
        public double Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Amount cannot be negative.");
                _amount = value;
            }
        }
        public OrderStatus Status { get; set; }
        public List<Product> Products
        {
            get => _products;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Products list cannot be null.");
                _products = value;
            }
        }

        public Order(
            int customerId, DateTime orderDate,
            OrderStatus status,
            double amount, List<Product> products
            )
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            Amount = amount;
            Status = status;
            Products = products;

            Instances.Add(this);
        }

        public Shipping CreateShipping(ShippingMethod method, double cost, string address)
        {
            Shipping shipping = new Shipping(OrderId, method, cost, address);
            Amount += cost;
            ShippingId = shipping.ShippingId;
            return shipping;
        }

        public static void PrintInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static bool Exists(int id)
        {
            bool flag = false;
            try 
            {
                Order? o = Instances[id];
                if (o != null) flag = true;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return flag;
        }

        public override string ToString()
        {
            return "Order Id: " + OrderId + " Customer Id: " + CustomerId + " Amount: " + Amount;
        }
    }

}

