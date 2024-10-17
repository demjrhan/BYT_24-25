namespace Project;

public class CustomerRole 
{
    
    private static int _lastCustomerId = 0;
    public int CustomerId { get; private set; }
    public DateTime RegisterDate { get; private set; }

    public CustomerRole()
    {
        CustomerId = ++_lastCustomerId;
        RegisterDate = DateTime.Now;
    }

    public Payment CreatePayment(string paymentMethod, double amount)
    {
        return new Payment(paymentMethod, amount);
    }

    public Review CreateReview(int rating, string comment)
    {
        return new Review(CustomerId, rating, comment);
    }
    
}