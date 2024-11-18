namespace Project
{
    public class Payment
    {
        private static int _lastId = 0;
        private static List<Payment> Instances = [];

        private int _orderId;
        private double _amount;

        public int PaymentId { get; private set; } = _lastId++;
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
        public PaymentMethod PaymentMethod { get; set; }
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

        public Payment(int orderId, PaymentMethod paymentMethod, double amount)
        {
            OrderId = orderId;
            PaymentMethod = paymentMethod;
            Amount = amount;

            Instances.Add(this);
        }

        public static void GetInstances()
        {
            foreach (var i in Instances)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public override string ToString()
        {
            return "Payment id: " + PaymentId + " Order id: " + OrderId + " Amount " + Amount; 
        }
    }

}

