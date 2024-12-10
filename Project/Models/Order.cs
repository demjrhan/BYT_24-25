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
        private Customer _customer;
        private int _customerId;
        private Shipping? _shipping;
        private int? _shippingId;
        private Payment? _payment;
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
            Customer customer, DateTime orderDate,
            OrderStatus status,
            double amount, List<Product> products
        )
        {
            try
            {
                ValidateCustomerId(customer.CustomerId);
                ValidateOrderDate(orderDate);
                ValidateAmount(amount);
                ValidateProducts(products);

                CustomerId = customer.CustomerId;
                _customer = customer;
                OrderDate = orderDate;
                Amount = amount;
                Status = status;
                Products = products;
                OrderId = _lastId++;

                customer.AddOrder(this);
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

        public void AddShipping(Shipping shipping)
        {
            _shipping = shipping;
            ShippingId = shipping.ShippingId;
            Amount += shipping.Cost;
        }

        public void ResetShipping()
        {
            if (_shipping != null)
            {
                Amount -= _shipping.Cost;
                ShippingId = -1;
                _shipping = null;
            }
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

        public void Pay(Payment payment)
        {
            _payment = payment;
        }

        public void ResetPayment()
        {
            _payment = null;
        }

        public static void RemoveOrder(Order order)
        {
            order._customer.RemoveOrder(order);
            if (order._payment != null)
            {
                Payment.RemovePayment(order._payment);
            }
            Shipping.RemoveShipping(order._shipping!);
            Instances.Remove(order);
        }

        public override string ToString()
        {
            return $"Order ID: {OrderId}, Customer ID: {CustomerId}, Amount: {Amount}, Status: {Status}";
        }
    }
}