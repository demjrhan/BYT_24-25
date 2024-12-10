using Project.Entities;
using Project.Enum;
using Project.Models;

namespace Project.Features
{
    public class Payment
    {
        private static int _lastId = 0;
        private static List<Payment> Instances = new List<Payment>();
        private Customer _customer;
        private Order _order;
        private int _orderId;
        private double _amount;
        public int PaymentId { get; private set; } = _lastId++;
        public PaymentMethod PaymentMethod { get; set; }
        
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
        public Payment(Customer customer, Order order, PaymentMethod paymentMethod, double amount)
        {
            ValidateOrderExists(order.OrderId);
            ValidatePaymentAmount(amount);

            _customer = customer;
            OrderId = order.OrderId;
            _order = order;
            PaymentMethod = paymentMethod;
            Amount = amount;

            customer.AddPayment(this);

            order.Pay(this);

            Instances.Add(this);
        }

        public static void PrintInstances()
        {
            foreach (var payment in Instances)
            {
                Console.WriteLine(payment.ToString());
            }
        }

        public static IReadOnlyList<Payment> GetInstances()
        {
            return Instances.AsReadOnly();
        }
        
        public static void ClearInstances()
        {
            Instances.Clear();
        }

        public static bool Exists(Payment givenPayment)
        {
            foreach (var payment in Instances)
            {
                if (givenPayment == payment)
                    return true;
            }
            return false;
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

        public static void RemovePayment(Payment payment)
        {
            payment._order.ResetPayment();
            payment._customer.RemovePayment(payment);
            Instances.Remove(payment);
        }

        public override string ToString()
        {
            return $"Payment ID: {PaymentId}, Order ID: {OrderId}, Amount: {Amount:C}, Payment Method: {PaymentMethod}";
        }
    }

}

