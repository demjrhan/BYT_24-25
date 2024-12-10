using Project.Enum;
using Project.Models;

namespace Project.Features
{
    public class Shipping
    {
        private static int _lastId = 0;
        private static List<Shipping> Instances = new List<Shipping>();
        private Order _order;
        private int _orderId;
        private double _cost;
        private string _address = null!;
        public ShippingMethod Method { get; set; }
        public int ShippingId { get; private set; } = _lastId++;
        public int OrderId
        {
            get => _orderId;
            set
            {
                ValidateOrderId(value);
                _orderId = value;
            }
        }

        public double Cost
        {
            get => _cost;
            set
            {
                ValidateCost(value);
                _cost = value;
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                ValidateAddress(value);
                _address = value;
            }
        }

        public Shipping(Order order, ShippingMethod method, double cost, string address)
        {
            _order = order;
            OrderId = order.OrderId;
            Method = method;
            Cost = cost;
            Address = address;

            Instances.Add(this);
        }
        
        public static IReadOnlyList<Shipping> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        
        public static bool Exists(int id)
        {
            foreach (var shipping in Instances)
            {
                if (shipping.ShippingId == id)
                    return true;
            }
            return false;
        }
       
        private static void ValidateOrderId(int orderId)
        {
            if (!Order.Exists(orderId))
                throw new ArgumentException($"Order with ID {orderId} does not exist.");
        }

        private static void ValidateCost(double cost)
        {
            if (cost < 0)
                throw new ArgumentOutOfRangeException(nameof(cost), "Cost cannot be negative.");
        }

        private static void ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be null or empty.");
        }
        
        public static void PrintInstances()
        {
            foreach (var shipping in Instances)
            {
                Console.WriteLine(shipping.ToString());
            }
        }

        public static void RemoveShipping(Shipping shipping)
        {
            shipping._order.ResetShipping();
            Instances.Remove(shipping);
        }
        public override string ToString()
        {
            return $"Shipping ID: {ShippingId}, Address: {Address}, Cost: {Cost:C}, Method: {Method}";
        }
    }

}

