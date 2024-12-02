using Project.Enum;
using Project.Models;

namespace Project.Features
{
    public class Shipping
    {
        private static int _lastId = 0;
        private static List<Shipping> Instances = [];

        private int _orderId;
        private double _cost;
        private string _address = null!;

        public int ShippingId { get; private set; } = _lastId++;
        public int OrderId
        {
            get => _orderId;
            set
            {
                if (!Order.Exists(value))
                    throw new ArgumentException("Order ID does not exist.");
                _orderId = value;
            }
        }
        public ShippingMethod Method { get; set; }
        public double Cost
        {
            get => _cost;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Cost cannot be negative.");
                _cost = value;
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Address cannot be null or empty.");
                _address = value;
            }
        }

        public Shipping(int orderId, ShippingMethod method, double cost, string address)
        {
            OrderId = orderId;
            Method = method;
            Cost = cost;
            Address = address;

            Instances.Add(this);
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
                Shipping? o = Instances[id];
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
            return "Shipping number: " + ShippingId + " address: " + Address + " Cost: " + Cost;
        }
    }

}

