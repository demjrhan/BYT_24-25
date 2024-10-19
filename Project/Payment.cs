namespace Project
{
    public class Payment
    {
        private static int _lastPaymentId = 0;

        public int OrderId { get; set; }
        public int PaymentID { get; set; }
        public string PaymentMethod { get; set; }
        public double Amount { get; set; }

        public Payment(int orderId, string paymentMethod, double amount)
        {
            OrderId = orderId;
            PaymentMethod = paymentMethod;
            Amount = amount;
            PaymentID = _lastPaymentId++;
        }
    }

}

