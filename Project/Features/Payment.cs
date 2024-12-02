using Project.Enum;
using Project.Models;

namespace Project.Features
{
    public class Payment
    {
        private static int _lastId = 0;
        private static List<Payment> Instances = new List<Payment>();
        private int _orderId;
        private double _amount;
        public int PaymentId { get; private set; } = _lastId++;
        public PaymentMethod PaymentMethod { get; set; }
   

        public Payment(int orderId, PaymentMethod paymentMethod, double amount)
        {
            ValidateOrderExists(orderId);
            ValidatePaymentAmount(amount);

            OrderId = orderId;
            PaymentMethod = paymentMethod;
            Amount = amount;

            Instances.Add(this);
        }

        public static Payment CreatePayment(int orderId, PaymentMethod paymentMethod, double amount)
        {
            ValidateOrderExists(orderId);
            ValidatePaymentAmount(amount);

            return new Payment(orderId, paymentMethod, amount);
        }


        
        public int OrderId
        {
            get => _orderId;
            set
            {
                ValidateOrderExists(value);
                _orderId = value;
            }
        }
        
        public double Amount
        {
            get => _amount;
            set
            {
                ValidatePaymentAmount(value);
                _amount = value;
            }
        }
        
        public static void PrintInstances()
        {
            foreach (var payment in Instances)
            {
                Console.WriteLine(payment.ToString());
            }
        }

        
        // Validation methods added seperately to maintain reusability and readability.
        private static void ValidateOrderExists(int orderId)
        {
            if (!Order.Exists(orderId))
                throw new ArgumentException($"Order with ID {orderId} does not exist.");
        }

        private static void ValidatePaymentAmount(double amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        public override string ToString()
        {
            return $"Payment ID: {PaymentId}, Order ID: {OrderId}, Amount: {Amount:C}, Payment Method: {PaymentMethod}";
        }
    }

}

