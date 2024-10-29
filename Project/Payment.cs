namespace Project
{
    public enum PaymentMethod
    {
        Cash,
        Card
    }

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
                if (!Order.GetInstances().Exists(o => o.OrderId == value))
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

        public static List<Payment> GetInstances()
        {
            return Instances;
        }

        public override string ToString()
        {
            return "Payment id: " + PaymentId + " Order id: " + OrderId + " Amount " + Amount; 
        }
    }

}

