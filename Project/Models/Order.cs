using Project.Entities;
using Project.Enum;
using Project.Features;

namespace Project.Models
{
    public class Order
    {
        private static int _lastId = 0;
        private static List<Order> Instances = new List<Order>();
        private List<Product> _products = new List<Product>();

        private int _customerId;
        private int? _shippingId;
        private DateTime _orderDate;
        private double _amount;
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }

        public int CustomerId
        {
            get => _customerId;
            set
            {
                ValidateCustomerId(value);
                _customerId = value;
            }
        }

        public int? ShippingId
        {
            get => _shippingId;
            set
            {
                ValidateShippingId(value);
                _shippingId = value;
            }
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set
            {
                ValidateOrderDate(value);
                _orderDate = value;
            }
        }

        public List<Product> Products
        {
            get => _products;
            set
            {
                ValidateProducts(value);
                _products = value;
            }
        }

        public double Amount
        {
            get => _amount;
            set
            {
                ValidateAmount(value);
                _amount = value;
            }
        }
        public Order(
            int customerId, DateTime orderDate,
            OrderStatus status,
            double amount, List<Product> products
        )
        {
            try
            {
                ValidateCustomerId(customerId);
                ValidateOrderDate(orderDate);
                ValidateAmount(amount);
                ValidateProducts(products);

                CustomerId = customerId;
                OrderDate = orderDate;
                Amount = amount;
                Status = status;
                Products = products;
                OrderId = _lastId++;
                Instances.Add(this);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize Order.", ex);
            }
        }

        public static IReadOnlyList<Order> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Order givenoOrder)
        {
            foreach (var order in Instances)
            {
                if (order == givenoOrder)
                    return true;
            }
            return false;
        }
        public Shipping CreateShipping(ShippingMethod method, double cost, string address)
        {
            Shipping shipping = new Shipping(OrderId, method, cost, address);
            Amount += cost;
            ShippingId = shipping.ShippingId;
            return shipping;
        }
        
        // Validation methods added seperately to maintain reusability and readability.

        private static void ValidateCustomerId(int customerId)
        {
            if (!Customer.Exists(customerId))
                throw new ArgumentException("Customer ID does not exist.");
        }

        private static void ValidateShippingId(int? shippingId)
        {
            if (shippingId != null && !Shipping.Exists((int)shippingId))
                throw new ArgumentException("Shipping ID does not exist.");
        }

        private static void ValidateOrderDate(DateTime orderDate)
        {
            if (orderDate > DateTime.Now)
                throw new ArgumentException("Order date cannot be set to a future time.");
        }

        private static void ValidateAmount(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        }

        private static void ValidateProducts(List<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products), "Products list cannot be null.");
        }

        public static void PrintInstances()
        {
            foreach (var order in Instances)
            {
                Console.WriteLine(order.ToString());
            }
        }

        public static bool Exists(int id)
        {
            foreach (var order in Instances)
            {
                if (order.OrderId == id)
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Order ID: {OrderId}, Customer ID: {CustomerId}, Amount: {Amount}, Status: {Status}";
        }
    }
}