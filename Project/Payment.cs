namespace Project;

public class Payment
{
    private static int _lastPaymentId = 0;
    public int PaymentID { get; set; }
    public string PaymentMethod { get; set; }
    public double Amount { get; set; }

    public Payment(string paymentMethod, double amount)
    {
        PaymentID = _lastPaymentId++;
        PaymentMethod = paymentMethod;
        Amount = amount;
    }
}
