namespace Project;

public class Member
{
    public int MembershipId { get; set; }
    public bool Status { get; set; }
    public double DiscountRate { get; set; }

    public Member(int customerId, bool status, double discountRate) // membershipId indicates the customerId to not lose person contact info.
    {
        MembershipId = customerId;
        Status = status;
        DiscountRate = discountRate;
    }
}